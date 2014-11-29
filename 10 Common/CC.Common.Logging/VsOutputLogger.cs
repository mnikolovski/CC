using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
using CC.Common.Base.Logging;
using CC.Common.Helpers.Serialization;

namespace CC.Common.Logging
{
    [Export(typeof(IChildLogger))]
    internal class VsOutputLogger : IChildLogger
    {
        /// <summary>
        /// Each logger can have a child attached to it.
        /// Can be used for both writing in file and console or set a loggers chain for logging
        /// </summary>
        public ILogger ChildLogger { get; set; }

        /// <summary>
        /// Log exception with a option to give an additional message
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="message"></param>
        public void Log(Exception ex, string message = null)
        {
            Trace.WriteLine(DateTime.Now);
            Trace.WriteLine(ex);
            Trace.WriteLine(message);

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
                                  ? string.Format(@"{0}{1}{2}", message, Environment.NewLine, _instanceDescription)
                                  : _instanceDescription;

            Trace.WriteLine(DateTime.Now);
            Trace.WriteLine(ex);
            Trace.WriteLine(_logMessage);

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
            Trace.WriteLine(DateTime.Now);
            Trace.WriteLine(message);

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
                      ? string.Format(@"{0}{1}{2}", message, Environment.NewLine, _instanceDescription)
                      : _instanceDescription;
            Log(_logMessage);
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
            Trace.WriteLine(DateTime.Now);
            Trace.WriteLine(ex);

            if (ChildLogger != null)
            {
                ChildLogger.Log(ex);
            }
        }
    }
}
