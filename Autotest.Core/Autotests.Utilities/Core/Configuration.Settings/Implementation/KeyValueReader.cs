using System;
using System.Collections.Generic;
using System.IO;

namespace Autotests.Utilities.Core.Configuration.Settings.Implementation
{
    internal class KeyValueReader
    {
        public delegate void ProcessKeyValueDelegate(KeyValuePair<string, string> keyValuePair);

        private readonly TextReader reader;

        public KeyValueReader(TextReader reader)
        {
            this.reader = reader;
        }

        public bool TryRead(out KeyValuePair<string, string> keyValuePair)
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                if (ProcessLine(line.Trim(), out keyValuePair))
                    return true;
            }

            keyValuePair = default(KeyValuePair<string, string>);
            return false;
        }

        public void ReadAll(ProcessKeyValueDelegate processKeyValueDelegate)
        {
            KeyValuePair<string, string> keyValuePair;
            while (TryRead(out keyValuePair))
                processKeyValueDelegate(keyValuePair);
        }

        private static bool ProcessLine(string line, out KeyValuePair<string, string> keyValuePair)
        {
            if (string.IsNullOrEmpty(line) || line[0] == '#')
            {
                keyValuePair = default(KeyValuePair<string, string>);
                return false;
            }
            string[] items = line.Split(new[] {'='}, 2);
            if (items.Length != 2)
                throw new FormatException(string.Format("Invalid format: '{0}'", line));
            string key = items[0].Trim();
            if (string.IsNullOrEmpty(key))
                throw new FormatException();
            keyValuePair = new KeyValuePair<string, string>(key, items[1].Trim());
            return true;
        }
    }
}