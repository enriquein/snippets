using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace SSISFrameworkMap.Service.Utility
{
    public class CommandExecutor
    {
        private string _lastResult;
        private string _lastError;

        public string LastResult
        {
            get { return _lastResult; }
            set { _lastResult = value; }
        }
        
        public string LastError
        {
            get { return _lastError; }
            set { _lastError = value; }
        }

        public string RunCommand(string filename, string arguments = null, bool showWindow = false)
        {
            var process = new Process();

            process.StartInfo.FileName = filename;
            if (!string.IsNullOrEmpty(arguments))
            {
                process.StartInfo.Arguments = arguments;
            }

            process.StartInfo.CreateNoWindow = showWindow;
            process.StartInfo.WindowStyle = showWindow ? ProcessWindowStyle.Normal : ProcessWindowStyle.Hidden;
            process.StartInfo.UseShellExecute = false;

            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.RedirectStandardOutput = true;
            var stdOutput = new StringBuilder();
            process.OutputDataReceived += (sender, args) => stdOutput.Append(args.Data);

            _lastError = null;
            try
            {
                process.Start();
                process.BeginOutputReadLine();
                _lastError = process.StandardError.ReadToEnd();
                process.WaitForExit();
            }
            catch (Exception e)
            {
                throw new Exception("OS error while executing " + Format(filename, arguments) + ": " + e.Message, e);
            }

            if (process.ExitCode == 0)
            {
                _lastResult = stdOutput.ToString();
                return _lastResult;
            }
            else
            {
                var message = new StringBuilder();

                if (!string.IsNullOrEmpty(_lastError))
                {
                    message.AppendLine(_lastError);
                }

                if (stdOutput.Length != 0)
                {
                    message.AppendLine("Std output:");
                    message.AppendLine(stdOutput.ToString());
                }

                throw new Exception(Format(filename, arguments) + " finished with exit code = " + process.ExitCode + ": " + message);
            }
        }

        private string Format(string filename, string arguments)
        {
            return string.Format("'{0}{1}'", filename, ((string.IsNullOrEmpty(arguments)) ? string.Empty : " " + arguments));
        }
    }
}
