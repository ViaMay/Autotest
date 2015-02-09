// Type: SKBKontur.Catalogue.WebTestCore.TestSystem.ByExtensions.ChangeId.Actions.IChangeIdAction
// Assembly: Catalogue.WebTestCore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FF1C74E4-7846-47AC-8368-20A72FAF99FF
// Assembly location: D:\icat\EDI.FunctionalTests\Tests\bin\Debug\Catalogue.WebTestCore.dll

namespace Autotests.Utilities.WebTestCore.TestSystem.ByExtensions.ChangeId.Actions
{
    public interface IChangeIdAction
    {
        string GetDescription();

        string ChangeId(string oldId);
    }
}