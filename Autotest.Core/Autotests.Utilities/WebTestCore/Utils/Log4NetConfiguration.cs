using System;
using System.IO;
using System.Text;
using Autotests.Utilities.Core.Configuration.Settings;
using Autotests.Utilities.Core.Configuration.Settings.Implementation;

namespace Autotests.Utilities.WebTestCore.Utils
{
    public class ApplicationSettings
    {
        public static IApplicationSettings LoadDefault(string fileName)
        {
            return Load(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "settings", fileName));
        }

        public static IApplicationSettings Load(string fileName)
        {
            using (var settingFileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
                return LoadFromStream(settingFileStream, fileName);
        }

        public static IApplicationSettings LoadFromStream(Stream stream, Encoding encoding, string settingsName = null)
        {
            var result =
                new PlainTextApplicationSettings(string.IsNullOrWhiteSpace(settingsName) ? "(null)" : settingsName);
            result.LoadFromStream(stream, encoding);
            return result;
        }

        public static IApplicationSettings LoadFromStream(Stream stream, string settingsName = null)
        {
            return LoadFromStream(stream, Encoding.GetEncoding("windows-1251"), settingsName);
        }
    }
}