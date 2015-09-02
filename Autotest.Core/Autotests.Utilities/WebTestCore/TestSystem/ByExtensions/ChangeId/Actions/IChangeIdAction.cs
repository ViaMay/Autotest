namespace Autotests.Utilities.WebTestCore.TestSystem.ByExtensions.ChangeId.Actions
{
    public interface IChangeIdAction
    {
        string GetDescription();

        string ChangeId(string oldId);
    }
}