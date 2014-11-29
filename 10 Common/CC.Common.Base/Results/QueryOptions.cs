using System;
using System.Runtime.Serialization;

namespace CC.Common.Base.Results
{
    /// <summary>
    /// Search query options
    /// </summary>
    [Serializable]
    [DataContract]
    public class QueryOptions
    {
        /// <summary>
        /// Page to be retrieved
        /// </summary>
        [DataMember]
        public int Page { get; set; }

        /// <summary>
        /// Pagesize - result count
        /// </summary>
        [DataMember]
        public int PageSize { get; set; }
    }
}
