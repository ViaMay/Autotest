namespace Autotests.Utilities.WebTestCore.TestSystem.ByExtensions.ChangeId.Actions
{
    public class AppendSuffix : IChangeIdAction
    {
        private readonly string suffix;

        public AppendSuffix(string suffix)
        {
            this.suffix = suffix;
        }

        public string GetDescription()
        {
            return string.Format("Append suffix '{0}'", suffix);
        }

        public string ChangeId(string oldId)
        {
            return oldId + suffix;
        }
    }
}