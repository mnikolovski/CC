using System;
using System.Runtime.Serialization;

namespace CC.Common.Base.Results
{
    /// <summary>
    /// Represent a result with a return value
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    [DataContract]
    public class MethodResult<T> : VoidResult
    {
        /// <summary>
        /// Executed method result
        /// </summary>
        [DataMember]
        public T Result { get; set; }

        /// <summary>
        /// CTOR
        /// </summary>
        public MethodResult(){}

        /// <summary>
        /// CTOR
        /// </summary>
        /// <param name="result"></param>
        public MethodResult(T result)
        {
            Result = result;
        }
    }
}
