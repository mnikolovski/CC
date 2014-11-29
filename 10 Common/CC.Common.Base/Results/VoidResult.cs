using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using CC.Common.Base.Exceptions;

namespace CC.Common.Base.Results
{
    /// <summary>
    /// Represent a result without a return value
    /// </summary>
    [Serializable]
    [DataContract]
    public class VoidResult
    {
        public VoidResult()
        {
            ErrorMessages = new List<ErrorMessage>();
        }

        /// <summary>
        /// Indicate that the result is faulted
        /// </summary>
        [DataMember]
        public bool IsFaulted { get; set; }

        /// <summary>
        /// Indicate that the result was faulted because of a system error
        /// </summary>
        [DataMember]
        public bool IsSystemFault { get; set; }

        /// <summary>
        /// Error messages associated with the result
        /// </summary>
        [DataMember]
        public List<ErrorMessage> ErrorMessages { get; set; }

        /// <summary>
        /// Create new faulted result
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T FaultedResult<T>() where T:VoidResult, new()
        {
            var faultedResult = new T();
            faultedResult.IsFaulted = true;
            return faultedResult;
        }
    }
}
