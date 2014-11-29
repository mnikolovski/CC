namespace CC.Common.Base.Exceptions
{
    /// <summary>
    /// Represent an error info in the system
    /// </summary>
    public interface IErrorInfo
    {
        /// <summary>
        /// Error group 
        /// </summary>
        string Group { get; }

        /// <summary>
        /// Error code
        /// </summary>
        string Code { get; }

        /// <summary>
        /// Error system message
        /// </summary>
        string FaultMessage { get; }

        /// <summary>
        /// Interface error
        /// </summary>
        string UserVisibleMessage { get; }
    }
}
