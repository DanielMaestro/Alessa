using Serilog;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace TesterBase
{
    internal class LoggingInitialize
    {
        private static readonly LoggingInitialize loggingInitialize;
        private static readonly object padlock = new object();
        private static ILogger logger;
        private bool _initialized = false;
        static LoggingInitialize()
        {
            loggingInitialize = new LoggingInitialize();
        }

        private LoggingInitialize()
        {
        }

        internal void Initialize(string logPath)
        {
            if (!Instance._initialized)
            {
                lock (padlock)
                {
                    if (logger == null)
                    {
                        logger = new LoggerConfiguration()
                           .WriteTo.Debug()
                           .WriteTo.File(logPath)
                           .CreateLogger();

                        Serilog.Log.Logger = logger;

                        Instance._initialized = true;
                    }
                }
            }
        }

        internal static LoggingInitialize Instance
        {
            get { return loggingInitialize; }
        }

        /// <summary>
        /// Writes in the log.
        /// </summary>
        /// <param name="result"></param>
        /// <param name="stopwatch"></param>
        internal void Log(string result, Stopwatch stopwatch, Type caller)
        {
            StackTrace stackTrace = new StackTrace();
            var method = (from m in caller.GetMethods()
                          join d in stackTrace.GetFrames() on m.Name equals d.GetMethod()?.Name ?? null
                          select m.Name).FirstOrDefault();

            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("\t{0}\t{1}\t{2}\t{3}", string.Join('\t', caller.ToString().Split('.')), method, stopwatch.Elapsed, result);

            this.Log(builder.ToString());
        }

        /// <summary>
        /// Writes in the log.
        /// </summary>
        /// <param name="message"></param>
        internal void Log(string message)
        {
            Serilog.Log.Information(message);
        }
    }

    public class TestLogBase
    {

        static TestLogBase()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestLogBase"/> class with the log file's path and the frame level to get the stack call.
        /// </summary>
        /// <param name="logPath">Log file path.</param>
        public TestLogBase(string logPath)
        {
            LoggingInitialize.Instance.Initialize(logPath);
        }

        /// <summary>
        /// Executes an scpecifed function with a time elapsed.
        /// </summary>
        /// <typeparam name="Tout">The result type.</typeparam>
        /// <param name="func">Function to execute. It mus return a <typeparamref name="Tout"/> object.</param>
        /// <param name="logConvert">Log convertion function. It show return the <typeparamref name="Tout"/> object to a readable string. If not specified then it uses the <typeparamref name="Tout"/>.ToString() method.</param>
        /// <param name="method">The method where this was invoked.</param>
        /// <returns></returns>
        protected Tout Execute<Tout>(Func<Tout> func, Func<Tout, string> logConvert, Type type)
        {
            var watch = new Stopwatch();
            // Starts the timer.
            watch.Start();
            var result = func.Invoke();
            watch.Stop();

            // If null then gets the ToString method.
            if (logConvert == null)
                logConvert = r => r?.ToString();

            var objStr = logConvert.Invoke(result);


            // Logs into the file.
            this.Log(objStr, watch, type);

            return result;
        }

        /// <summary>
        /// Writes in the log.
        /// </summary>
        /// <param name="result"></param>
        /// <param name="stopwatch"></param>
        /// <param name="type">The method where this was invoked.</param>
        protected void Log(string result, Stopwatch stopwatch, Type type)
        {
            LoggingInitialize.Instance.Log(result, stopwatch, type);
        }

        /// <summary>
        /// Writes in the log.
        /// </summary>
        /// <param name="message"></param>
        protected void Log(string message)
        {
            LoggingInitialize.Instance.Log(message);
        }

    }
}
