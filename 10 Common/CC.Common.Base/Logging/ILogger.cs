using System;

namespace CC.Common.Base.Logging
{
    public interface ILogger
    {
        /// <summary>
        /// Each logger can have a child attached to it.
        /// Can be used for both writing in file and console or set a loggers chain for logging
        /// </summary>
        ILogger ChildLogger { get; set; }

        /// <summary>
        /// Log exception with a option to give an additional message
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="message"></param>
        void Log(Exception ex, string message = null);

        /// <summary>
        /// Log an exception together with fault data if any with option of serialization and extra message
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ex"></param>
        /// <param name="instance"></param>
        /// <param name="shouldSerializeInstance"></param>
        /// <param name="message"></param>
        void Log<T>(Exception ex, T instance, bool shouldSerializeInstance = false, string message = null) where T : class;

        /// <summary>
        /// Log an message
        /// </summary>
        /// <param name="message"></param>
        void Log(string message);

        /// <summary>
        /// Log an instance with an option of serialization
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <param name="shouldSerializeInstance"></param>
        /// <param name="message"></param>
        void Log<T>(T instance, bool shouldSerializeInstance = false, string message = null) where T : class;

        /// <summary>
        /// Log an exception
        /// </summary>
        /// <param name="ex"></param>
        void Log(Exception ex);
    }
}
