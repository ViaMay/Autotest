namespace Autotests.Utilities.Core.Configuration.Settings.Exception
{
    public class SettingsKeyNotFoundException : System.Exception
    {
        public SettingsKeyNotFoundException(string fileName, string key)
            : base(
                string.Format("В файле настроек '{0}'\nНе найден параметр приложения с именем \"{1}\"", fileName, key))
        {
        }
    }
}