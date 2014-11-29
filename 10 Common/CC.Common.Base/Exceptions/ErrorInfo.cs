using System;
using System.Runtime.Serialization;
using Emit.LocalizationProvider.Definition;

namespace CC.Common.Base.Exceptions
{
    [Serializable]
    [DataContract]
    public abstract class ErrorMessage : IErrorInfo
    {
        #region Implementation of IErrorInfo

        [DataMember]
        public abstract string Group { get; protected set; }

        [DataMember]
        public abstract string Code { get; protected set; }

        [DataMember]
        public abstract string FaultMessage { get; protected set; }

        [DataMember]
        public virtual string UserVisibleMessage { get; protected set; }

        public override string ToString()
        {
            return FaultMessage;
        }

        #endregion

        /// <summary>
        /// Load the exception
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ex"></param>
        public abstract void SetException<T>(T ex) where T : Exception;

        /// <summary>
        /// Loaclize error information
        /// </summary>
        /// <param name="localizationProvider"></param>
        public abstract void LocalizeUserVisibleErrorMessage(ILocalizationProvider localizationProvider);
    }
}
