using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Autotests.Utilities.Core.Configuration.Settings.Exception;

namespace Autotests.Utilities.Core.Configuration.Settings.Implementation
{
    internal class PlainTextApplicationSettings : IApplicationSettings
    {
        private static readonly TryParseDelegate<string> tryParseString =
            delegate(string s, out string result)
            {
                result = s;
                return true;
            };

        private readonly string settingsName;
        private IDictionary<string, string> internalCache;

        public PlainTextApplicationSettings(string settingsName)
        {
            this.settingsName = settingsName;
        }

        public int GetInt(string name)
        {
            return GetValue<int>(name, int.TryParse);
        }

        public long GetLong(string name)
        {
            return GetValue<long>(name, long.TryParse);
        }

        public bool TryGetLong(string name, out long result)
        {
            return TryGetValue(name, long.TryParse, out result);
        }

        public bool TryGetInt(string name, out int result)
        {
            return TryGetValue(name, int.TryParse, out result);
        }

        public string GetString(string name)
        {
            return GetValue(name, tryParseString);
        }

        public string[] GetStringArray(string name)
        {
            string result;
            if (!TryGetString(name, out result))
                return new string[0];
            return result.Split(new[] {',', ' '}, StringSplitOptions.RemoveEmptyEntries);
        }

        public TimeSpan GetTimeSpan(string name)
        {
            return GetValue<TimeSpan>(name, TimeSpanParser.TryParseTimeSpan);
        }

        public bool TryGetTimeSpan(string name, out TimeSpan timeSpan)
        {
            return TryGetValue(name, TimeSpanParser.TryParseTimeSpan, out timeSpan);
        }

        public string GetFullPath(string name)
        {
            string path = GetString(name);
            return Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path));
        }

        public T GetEnum<T>(string name) where T : struct
        {
            string valueString = GetString(name);
            T result;
            if (!Enum.TryParse(valueString, true, out result))
                throw new System.Exception(string.Format("'{0}' is not the value of enum type '{1}'", valueString,
                    typeof (T).Name));
            return result;
        }

        public string[] GetAllKeys()
        {
            return internalCache.Keys.ToArray();
        }

        public bool TryGetString(string name, out string value)
        {
            return TryGetValue(name, tryParseString, out value);
        }

        public bool GetBool(string name)
        {
            return GetValue<bool>(name, bool.TryParse);
        }

        public bool TryGetBool(string name, out bool value)
        {
            return TryGetValue(name, bool.TryParse, out value);
        }

        internal void LoadFromStream(Stream stream, Encoding encoding)
        {
            internalCache = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            using (var streamReader = new StreamReader(stream, encoding))
                new KeyValueReader(streamReader).ReadAll(AddToCache);
        }

        private void AddToCache(KeyValuePair<string, string> pair)
        {
            if (internalCache.ContainsKey(pair.Key))
                throw new InvalidSettingsFileException(settingsName, pair);
            internalCache.Add(pair);
        }

        private T GetValue<T>(string name, TryParseDelegate<T> parseDelegate)
        {
            T result;
            if (!TryGetValue(name, parseDelegate, out result))
                throw new SettingsKeyNotFoundException(settingsName, name);
            return result;
        }

        private bool TryGetValue<T>(string name, TryParseDelegate<T> parseDelegate, out T result)
        {
            string s;
            result = default(T);
            if (!internalCache.TryGetValue(name, out s))
                return false;
            if (!parseDelegate(s, out result))
                throw new FormatException(string.Format("Key '{0}': '{1}' is not a value of type {2}", name, s,
                    typeof (T).Name));
            return true;
        }

        private delegate bool TryParseDelegate<T>(string s, out T result);
    }
}