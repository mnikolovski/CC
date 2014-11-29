using System;
using System.Runtime.Serialization;

namespace CC.Client.Web.Models.Logs
{
    [Serializable]
    [DataContract]
    public class LogModel
    {
        /// <summary>
        /// Represent the error message
        /// </summary>
        [DataMember]
        public string Message { get; set; }

        /// <summary>
        /// Faulted file
        /// </summary>
        [DataMember]
        public string Filename { get; set; }

        /// <summary>
        /// Fault line number
        /// </summary>
        [DataMember]
        public int? LineNumber { get; set; }

        /// <summary>
        /// Fault column number
        /// </summary>
        [DataMember]
        public int? ColumnNumber { get; set; }

        /// <summary>
        /// Execution stacktrace
        /// </summary>
        [DataMember]
        public string StackTrace { get; set; }

        /// <summary>
        /// Type of error script/image
        /// </summary>
        [DataMember]
        public string ErrorType { get; set; }

        /// <summary>
        /// Represent extracted request data
        /// </summary>
        [DataMember]
        public RequestData RequestData { get; set; }
    }
}