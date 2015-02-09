// Type: SKBKontur.Catalogue.Core.Configuration.Settings.IApplicationSettings
// Assembly: Catalogue.Core.Configuration.Settings, Version=1.0.0.0, Culture=neutral
// MVID: E7476800-4CEB-4E9F-ACF0-0AD847D2FE0A
// Assembly location: D:\icat\Assemblies\Temp\Catalogue.Core.Configuration.Settings.dll

using System;

namespace Autotests.Utilities.Core.Configuration.Settings
{
    public interface IApplicationSettings
    {
        int GetInt(string name);

        long GetLong(string name);

        bool TryGetLong(string name, out long result);

        string GetString(string name);

        string[] GetStringArray(string name);

        TimeSpan GetTimeSpan(string name);

        bool TryGetTimeSpan(string name, out TimeSpan timeSpan);

        bool TryGetString(string name, out string value);

        bool GetBool(string name);

        bool TryGetBool(string name, out bool value);

        bool TryGetInt(string name, out int result);

        string GetFullPath(string name);

        T GetEnum<T>(string name) where T : struct;

        string[] GetAllKeys();
    }
}