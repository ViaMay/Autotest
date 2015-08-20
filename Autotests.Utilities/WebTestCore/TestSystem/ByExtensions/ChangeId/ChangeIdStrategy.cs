using System.Collections.Generic;
using System.Linq;
using Autotests.Utilities.WebTestCore.TestSystem.ByExtensions.ChangeId.Actions;

namespace Autotests.Utilities.WebTestCore.TestSystem.ByExtensions.ChangeId
{
    public class ChangeIdStrategy
    {
        private readonly List<IChangeIdAction> actions = new List<IChangeIdAction>();

        private ChangeIdStrategy()
        {
        }

        public string ChangeId(string id)
        {
            string result = id;
            foreach (IChangeIdAction changeIdAction in actions)
                result = changeIdAction.ChangeId(result);
            return result;
        }

        public override string ToString()
        {
            return string.Join(";", actions.Select(x => x.ToString()));
        }

        public static ChangeIdStrategy GetId()
        {
            return new ChangeIdStrategy();
        }

        public ChangeIdStrategy AppendSuffix(string suffix)
        {
            return AddAction(new AppendSuffix(suffix));
        }

        public ChangeIdStrategy TrimSuffix(string suffix)
        {
            return AddAction(new TrimSuffix(suffix));
        }

        private ChangeIdStrategy AddAction(IChangeIdAction action)
        {
            actions.Add(action);
            return this;
        }
    }
}