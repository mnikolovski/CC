using System;
using System.ComponentModel.Composition;
using CC.Common.Base.Logging;
using CC.Common.Helpers.Serialization;
using NLog;

namespace CC.Common.Logging
{
    [Export(typeof(ILogger))]
    internal sealed class NLogLogger : ILogger 
    {
        private static readonly Logger MLogger = LogManager.GetCurrentClassLogger();
        private static readonly LogLevel MDefaultLogLevel = LogLevel.Info;
        /// <summary>
        /// Each logger can have a child attached to it.
        /// Can be used for both writing in file and console or set a loggers chain for logging
        /// </summary>
        [Import(typeof(IChildLogger))]
        public ILogger ChildLogger { get; set; }

        /// <summary>
        /// Log exception with a option to give an additional message
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="message"></param>
        public void Log(Exception ex, string message = null)
        {
            if (MLogger != null)
            {
                MLogger.LogException(LogLevel.Fatal, message, ex);
                if (ex.InnerException != null)
                {
                    MLogger.LogException(MDefaultLogLevel, @"InnerException", ex.InnerException);
                }
            }

            if (ChildLogger != null)
            {
                ChildLogger.Log(ex, message);
            }
        }

        /// <summary>
        /// Log an exception together with fault data if any with option of serialization and extra message
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ex"></param>
        /// <param name="instance"></param>
        /// <param name="shouldSerializeInstance"></param>
        /// <param name="message"></param>
        public void Log<T>(Exception ex, T instance, bool shouldSerializeInstance = false, string message = null) where T : class
        {
            var _instanceDescription = shouldSerializeInstance ? instance.SerializeToJSon(false, false) : (instance != null ? instance.ToString() : typeof(T) + " " + "IsNull");
            var _logMessage = !string.IsNullOrEmpty(message)
                                  ? string.Format(@"{0}{1}{2}", message, " ", _instanceDescription)
                                  : _instanceDescription;
            if (MLogger != null)
            {
                MLogger.LogException(MDefaultLogLevel, _logMessage, ex);
                if (ex.InnerException != null)
                {
                    MLogger.LogException(MDefaultLogLevel, @"InnerException", ex.InnerException);
                }
            }

            if (ChildLogger != null)
            {
                ChildLogger.Log(ex, instance, shouldSerializeInstance, message);
            }
        }

        /// <summary>
        /// Log an message
        /// </summary>
        /// <param name="message"></param>
        public void Log(string message)
        {
            if (MLogger != null)
            {
                MLogger.Info(message);
            }

            if (ChildLogger != null)
            {
                ChildLogger.Log(message);
            }
        }

        /// <summary>
        /// Log an instance with an option of serialization
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <param name="shouldSerializeInstance"></param>
        /// <param name="message"></param>
        public void Log<T>(T instance, bool shouldSerializeInstance = false, string message = null) where T : class
        {
            var _instanceDescription = shouldSerializeInstance ? instance.SerializeToJSon(false, false) : (instance != null ? instance.ToString() : typeof(T) + " " + "IsNull");
            var _logMessage = !string.IsNullOrEmpty(message)
                      ? string.Format(@"{0}{1}{2}", message, " ", _instanceDescription)
                      : _instanceDescription;
            if (MLogger != null)
            {
                MLogger.Info(_logMessage);
            }

            if (ChildLogger != null)
            {
                ChildLogger.Log(instance, shouldSerializeInstance: shouldSerializeInstance, message: message);
            }
        }

        /// <summary>
        /// Logs an exception
        /// </summary>
        /// <param name="ex"></param>
        public void Log(Exception ex)
        {
            if (MLogger != null)
            {
                MLogger.Info(ex);
                if (ex.InnerException != null)
                {
                    MLogger.Fatal(ex.InnerException);
                }
            }

            if (ChildLogger != null)
            {
                ChildLogger.Log(ex);
            }
        }
    }
}
