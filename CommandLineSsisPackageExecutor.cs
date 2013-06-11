using System.Collections.Generic;
using System.Text;

namespace SSISFrameworkMap.Service.Utility
{
    public class SsisPackageExecutor
    {
        private readonly string _pathToDtExec;

        public SsisPackageExecutor(string pathToDtexec)
        {
            _pathToDtExec = pathToDtexec;
        }

        public string ExecutePackage(string packagePath, Dictionary<string, string> packageVariables, string password = null)
        {
            var fullArgumentList = GetFullArgumentString(packagePath, packageVariables, password);

            var command = new CommandExecutor();
            return command.RunCommand(_pathToDtExec, fullArgumentList);
        }

        private string GetFullArgumentString(string packagePath, Dictionary<string, string> packageVariables, string password)
        { 
            string passwordParam = string.IsNullOrEmpty(password) ? string.Empty : "/DECRYPT " + password;
            string variables = string.Empty;

            if (packageVariables != null)
            {
                if (packageVariables.Count > 0)
                {
                    variables = GetVariables(packageVariables);
                }
            }

            return string.Format("/FILE {0} {1} {2}", packagePath, passwordParam, variables);        
        }

        private string GetVariables(Dictionary<string, string> variables)
        { 
            var format = " /SET \"Package.Variables[User::{0}].Properties[Value]\";\"{1}\" ";
            var appender = new StringBuilder();

            foreach (var kvp in variables)
            {
                appender.AppendFormat(format, kvp.Key, kvp.Value);
            }

            return appender.ToString();
        }
    }
}