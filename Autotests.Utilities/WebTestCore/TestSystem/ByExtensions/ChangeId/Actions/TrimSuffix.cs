using System;

namespace Autotests.Utilities.WebTestCore.TestSystem.ByExtensions.ChangeId.Actions
{
    public class TrimSuffix : IChangeIdAction
    {
        private readonly string suffix;

        public TrimSuffix(string suffix)
        {
            this.suffix = suffix;
        }

        public string GetDescription()
        {
            return string.Format("Trim suffix '{0}'", suffix);
        }

        public string ChangeId(string oldId)
        {
            if (!oldId.EndsWith(suffix))
                throw new Exception(string.Format("String '{0}' doesn't contais suffix '{1}'", oldId, suffix));
            return oldId.Substring(0, oldId.Length - suffix.Length);
        }
    }
}