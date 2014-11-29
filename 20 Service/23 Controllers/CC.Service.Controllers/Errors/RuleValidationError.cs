using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using CC.Common.Base.Exceptions;
using CC.Common.Helpers.Instance;
using CC.Common.Helpers.Serialization;
using Emit.LocalizationProvider.Definition;
using Emit.RuleEngine.Entities;

namespace CC.Service.Controllers.Errors
{
    /// <summary>
    /// Rule validation exception represent exception that occurs in *ValidationController
    /// Usage only in *ValidationController due to stack filtering that is done to get a small and refined stacktrace
    /// where the validation error occured
    /// </summary>
    public sealed class RuleValidationError : ErrorMessage
    {
        /// <summary>
        /// In which controler method this error occured
        /// </summary>
        private string CallingMethod { get; set; }

        /// <summary>
        /// What data did not passed the validtion checks
        /// </summary>
        private string ValidatedDataJson { get; set; }

        /// <summary>
        /// In which error group the exception belongs to
        /// </summary>
        public override string Group
        {
            get { return @"RVE"; }
            protected set {  }
        }

        /// <summary>
        /// What is the error code of the exception
        /// </summary>
        public override string Code
        {
            get { return @"10100"; }
            protected set { }
        }

        private string _faultMessage = "";
        public override string FaultMessage
        {
            get
            {
                return _faultMessage;
            }
            protected set
            {
                _faultMessage = value;
            }
        }

        /// <summary>
        /// Failed rules types
        /// </summary>
        public ReadOnlyCollection<Type> FalseReturnValidationRules { get; private set; }

        /// <summary>
        /// Give the validation error message (used as system error message)
        /// </summary>
        /// <param name="validationResult">Result of the validation</param>
        /// <param name="input">Data that failed on validation specifications</param>
        public RuleValidationError(IValidationResult validationResult, object input = null)
        {
            // get the calling method
            CallingMethod = @"N/A";

            if (input != null)
            {
                // get the json from the validated input
                ValidatedDataJson = input.SerializeToJSon(false);
            }
            else
            {
                ValidatedDataJson = @"N/A";
            }

            // log only in case when we have a valid result
            if (validationResult.IsNull()) return;
            var _failedTypes = validationResult.GetTypesFailedRulesTypes();
            if (_failedTypes.IsNullOrHasZeroElements()) return;
            // preserve the failed validations
            FalseReturnValidationRules = new ReadOnlyCollection<Type>(_failedTypes);
            // build the error info
            var _rulesBuilder = new StringBuilder();
            _rulesBuilder.AppendFormat(@"{0}: ", GetType().Name);
            for (var _i = 0; _i < FalseReturnValidationRules.Count; _i++)
            {
                var _validationRule = FalseReturnValidationRules[_i];
                if (_i + 1 == FalseReturnValidationRules.Count || FalseReturnValidationRules.Count == 1)
                {
                    _rulesBuilder.Append(_validationRule.Name);
                }
                else
                {
                    _rulesBuilder.AppendFormat(@"{0}, ", _validationRule.Name);
                }
            }

            FaultMessage = string.Concat(@"Failed rules: ", _rulesBuilder.ToString(), Environment.NewLine, GetCombinedExceptionMessage());
        }

        /// <summary>
        /// Give the validation error message (used as system error message)
        /// </summary>
        /// <param name="failedValidationType"></param>
        /// <param name="input">Data that failed on validation specifications</param>
        public RuleValidationError(Type failedValidationType, object input = null)
        {
            FalseReturnValidationRules = new ReadOnlyCollection<Type>(new List<Type> { failedValidationType });
            // get the calling method
            CallingMethod = @"N/A";

            if (input != null)
            {
                // get the json from the validated input
                ValidatedDataJson = input.SerializeToJSon(false);
            }
            else
            {
                ValidatedDataJson = @"N/A";
            }

            // build the error info
            var _rulesBuilder = new StringBuilder();
            _rulesBuilder.AppendFormat(@"{0}: ", GetType().Name);
            _rulesBuilder.Append(failedValidationType.Name);

            FaultMessage = string.Concat(@"Failed rules: ", _rulesBuilder.ToString(), Environment.NewLine, GetCombinedExceptionMessage());
        }

        /// <summary>
        /// Create the custom error message format 
        /// </summary>
        /// <returns></returns>
        private string GetCombinedExceptionMessage()
        {
            string _message;
            if (FaultMessage.Contains(Environment.NewLine))
            {
                _message = string.Format("{0}   at {1}{2}   data {3}",
                                          FaultMessage,
                                          StackTrace,
                                          Environment.NewLine,
                                          ValidatedDataJson);
            }
            else
            {
                _message = string.Format("{0}{2}   at {1}{2}   data {3}",
                                          FaultMessage,
                                          StackTrace,
                                          Environment.NewLine,
                                          ValidatedDataJson);
            }

            return _message;
        }

        /// <summary>
        /// Gets a string representation of the immediate frames on the call stack.
        /// </summary>
        /// <returns>
        /// A string that describes the immediate frames of the call stack.
        /// </returns>
        /// <filterpriority>2</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*"/></PermissionSet>
        public string StackTrace
        {
            get
            {
                return CallingMethod;
            }
        }

        /// <summary>
        /// Load the exception
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ex"></param>
        public override void SetException<T>(T ex) {}

        /// <summary>
        /// Set the user visible error message localized
        /// </summary>
        /// <param name="localizationProvider"></param>
        public override void LocalizeUserVisibleErrorMessage(ILocalizationProvider localizationProvider)
        {
            // set only if the localization provider is set and initialized
            if (localizationProvider == null) return;

            var _errorBuilder = new StringBuilder();
            foreach (var _rule in FalseReturnValidationRules)
            {
                var _typeNonGenericName = _rule.Name.Split('`');
                var _errorMessage = localizationProvider.GetTranslation(_typeNonGenericName[0]) ?? "N/A";
                _errorBuilder.AppendLine(_errorMessage);
            }
            UserVisibleMessage = _errorBuilder.ToString();
        }
    }
}
