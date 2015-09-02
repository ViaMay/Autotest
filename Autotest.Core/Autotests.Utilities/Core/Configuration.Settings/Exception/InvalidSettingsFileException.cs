using System.Collections.Generic;

namespace Autotests.Utilities.Core.Configuration.Settings.Exception
{
    public class InvalidSettingsFileException : System.Exception
    {
        public InvalidSettingsFileException(string fileName, KeyValuePair<string, string> pair)
            : base(
                string.Format("Application settings file '{0}' has duplicate setting with name {1}", fileName, pair.Key)
                )
        {
        }
    }
}