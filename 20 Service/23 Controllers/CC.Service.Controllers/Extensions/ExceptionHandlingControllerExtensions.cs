using System;
using System.Data;
using System.Data.SqlClient;
using CC.Common.Base.Exceptions;
using CC.Common.Base.Results;
using CC.Service.Controllers.Errors;
using CC.Service.DomainEntities.Exceptions;
using CC.Service.DomainProcesses.Extensibility.Markers;
using Emit.RuleEngine.Entities;

namespace CC.Service.Controllers.Extensions
{
    internal static class ExceptionHandlingControllerExtensions
    {
        /// <summary>
        /// Wrap reqest/response calls
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="ehci"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        internal static TResponse WrapRequestResponseCall<TResponse>(this IExceptionHandlingController ehci, Func<TResponse> action) where TResponse : VoidResult, new()
        {
            var _response = new TResponse();

            try
            {
                _response = action();
                // localize the error messages and log them
                if (_response.IsFaulted)
                {
                    // logger is centralized we dont need it here
                    ehci.Logger.Log(_response.ErrorMessages, shouldSerializeInstance: true);
                    _response.ErrorMessages.ForEach(x => x.LocalizeUserVisibleErrorMessage(ehci.LocalizationProvider));
                }
            }
            catch (ConstraintException _cex)
            {
                _response.IsSystemFault = true;
                CreateFaultedResponse<TResponse, GeneralError, ConstraintException>(ehci, _response, _cex);
            }
            catch (DataException dex)
            {
                _response.IsSystemFault = true;
                CreateFaultedResponse<TResponse, GeneralError, DataException>(ehci, _response, dex);
            }
            catch (SqlExceptionEx sqlExceptionEx)
            {
                _response.IsSystemFault = true;
                CreateFaultedResponse<TResponse, SqlExError, SqlExceptionEx>(ehci, _response, sqlExceptionEx);                
            }
            catch (SqlException sqlException)
            {
                _response.IsSystemFault = true;
                CreateFaultedResponse<TResponse, GeneralError, SqlException>(ehci, _response, sqlException);
            }
            catch (Exception _ex)
            {
                _response.IsSystemFault = true;
                CreateFaultedResponse<TResponse, GeneralError, Exception>(ehci, _response, _ex);
            }

            return _response;
        }

        /// <summary>
        /// Create faulted response from validation result
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="validationResult"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        internal static TResponse CreateFaultedResponse<TResponse>(this IValidationResult validationResult, object input = null) where TResponse : VoidResult, new()
        {
            var _response = new TResponse();
            _response.IsFaulted = true;

            var _failedTypes = validationResult.GetTypesFailedRulesTypes();
            foreach (var _failedType in _failedTypes)
            {
                _response.ErrorMessages.Add(new RuleValidationError(_failedType, input));
            }

            return _response;
        }

        /// <summary>
        /// Make the given response faulted and extract error messages from exception
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <typeparam name="TErrorType"></typeparam>
        /// /// <typeparam name="TException"></typeparam>
        /// <param name="ehci"></param>
        /// <param name="response"></param>
        /// <param name="ex"></param>
        private static void CreateFaultedResponse<TResponse, TErrorType, TException>(this IExceptionHandlingController ehci, TResponse response, TException ex)
            where TResponse : VoidResult
            where TException : Exception
            where TErrorType : ErrorMessage, new()
        {
            // fault the response
            response.IsFaulted = true;
            // and create general error message
            var _errorMessage = new TErrorType();
            _errorMessage.SetException(ex);
            _errorMessage.LocalizeUserVisibleErrorMessage(ehci.LocalizationProvider);
            // add it to the response
            response.ErrorMessages.Add(_errorMessage);
        }
    }
}
