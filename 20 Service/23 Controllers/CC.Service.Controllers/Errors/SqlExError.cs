using System;
using System.Runtime.Serialization;
using CC.Common.Base.Exceptions;
using Emit.LocalizationProvider.Definition;

namespace CC.Service.Controllers.Errors
{
    /// <summary>
    /// Rule validation exception represent exception that occurs in *ValidationController
    /// Usage only in *ValidationController due to stack filtering that is done to get a small and refined stacktrace
    /// where the validation error occured
    /// </summary>
    [DataContract, Serializable]
    public sealed class SqlExError : ErrorMessage
    {
        private string _faultMessage = "";

        /// <summary>
        /// In which error group the exception belongs to
        /// </summary>
        [DataMember]
        public override string Group
        {
            get { return @"GE"; }
            protected set {  }
        }

        /// <summary>
        /// What is the error code of the exception
        /// </summary>
        [DataMember]
        public override string Code
        {
            get { return @"10100"; }
            protected set { }
        }

        [DataMember]
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
        /// Load the exception
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ex"></param>
        public override void SetException<T>(T ex)
        {
            _faultMessage = ex.Message;
            _faultMessage = string.Format("{0}{2}   at {1}{2}",
                                          FaultMessage,
                                          ex.StackTrace,
                                          Environment.NewLine);
            UserVisibleMessage = ex.Message;
        }

        /// <summary>
        /// Loaclize error information
        /// </summary>
        /// <param name="localizationProvider"></param>
        public override void LocalizeUserVisibleErrorMessage(ILocalizationProvider localizationProvider) {}
    }
}
