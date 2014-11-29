using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CC.Common.Base.Results
{
    /// <summary>
    /// Represent a result with a return value List<typeparam name=">"></typeparam>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    [DataContract]
    public class ListResult<T> : VoidResult
    {
        /// <summary>
        /// Executed method result
        /// </summary>
        [DataMember]
        public new List<T> Result { get; set; }

        /// <summary>
        /// CTOR
        /// </summary>
        public ListResult()
        {
            Result = new List<T>();
        }

        /// <summary>
        /// CTOR
        /// </summary>
        /// <param name="result"></param>
        public ListResult(List<T> result = null)
        {
            Result = result ?? new List<T>();
        }
    }
}
