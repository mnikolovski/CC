using System;

namespace CC.Common.Base.Exceptions
{
    /// <summary>
    /// Represents base exception
    /// </summary>
    [Serializable]
    public abstract class EmitBaseException : Exception, IErrorInfo
    {
        private string _mErrorMessage;

        /// <summary>
        /// User visible error message
        /// </summary>
        private string _mUserVisibleErrorMessage;

        /// <summary>
        /// Return combined exception message (ErrorGroup + ErrorCode + Error Message) 
        /// </summary>
        public override string Message
        {
            get
            {
                return string.Concat(GetErrorGroupAndCode(), @" ", GetCombinedExceptionMessage());
            }
        }

        /// <summary>
        /// In which error group the exception belongs to
        /// </summary>
        public abstract string Group { get; }

        /// <summary>
        /// What is the error code of the exception
        /// </summary>
        public abstract string Code { get; }

        /// <summary>
        /// Custom error message
        /// </summary>
        public string FaultMessage
        {
            get { return _mErrorMessage ?? string.Empty; }
            protected set { _mErrorMessage = value; }
        }

        /// <summary>
        /// Interface error
        /// </summary>
        public string UserVisibleMessage
        {
            get
            {
                return _mUserVisibleErrorMessage ?? string.Empty;
            }
            protected set
            {
                _mUserVisibleErrorMessage = value;
            }
        }

        /// <summary>
        /// Create the custom error message format 
        /// </summary>
        /// <returns></returns>
        protected virtual string GetCombinedExceptionMessage()
        {
            if (string.IsNullOrEmpty(base.Message))
            {
                return FaultMessage;
            }

            string _message;
            if (FaultMessage.Contains(Environment.NewLine))
            {
                _message = string.Format("{0}Base:{1}{2}Stack:{3}",
                                          FaultMessage,
                                          base.Message,
                                          Environment.NewLine,
                                          StackTrace);
            }
            else
            {
                _message = string.Format("{0}{2}Base:{1}{2}Stack:{3}",
                                          FaultMessage,
                                          base.Message,
                                          Environment.NewLine,
                                          StackTrace);
            }

            return _message;
        }

        /// <summary>
        /// Return concat. error group + error code
        /// </summary>
        /// <returns></returns>
        public string GetErrorGroupAndCode()
        {
            return string.Concat(Group, Code);
        }

        /// <summary>
        /// Returns the message
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Message;
        }
    }
}
