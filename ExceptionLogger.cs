using System;
using System.Text;
using log4net;

namespace Snippets
{
    /// <summary>
    /// Defines extension methods for ILog interfaces. 
    /// </summary>
    public static class ExceptionLogger
    {
        /// <summary>
        /// Custom extension for logging exceptions 
        /// </summary>
        /// <param name="logger">This logger.</param>
        /// <param name="callingMethod">Method where the call took place.</param>
        /// <param name="exception">Exception object that you want to log.</param>
        public static void LogCustomException(this ILog logger, string callingMethod, Exception exception)
        {
            StringBuilder errorMsg = new StringBuilder();
            errorMsg.AppendLine("An exception was thrown while calling {0}.");
            errorMsg.AppendLine("The exception thrown was of type: {1} and had the following message: {2}.");
            errorMsg.AppendLine("The inner exception was of type: {3} and had the following message: {4}");
            errorMsg.AppendLine("Here's the stacktrace: {5}");

            logger.FatalFormat(errorMsg.ToString(), callingMethod, exception.GetType(), exception.Message, exception.InnerException.GetType(), exception.InnerException.Message, exception.StackTrace);
        }
    }
}