// Type: SKBKontur.Catalogue.WebTestCore.Pages.Timing.IPageTimeStatistics
// Assembly: Catalogue.WebTestCore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FF1C74E4-7846-47AC-8368-20A72FAF99FF
// Assembly location: D:\icat\Assemblies\Temp\Catalogue.WebTestCore.dll

using System;

namespace Autotests.Utilities.WebTestCore.Pages.Timing
{
    public interface IPageTimeStatistics
    {
        T InvolveAction<T>(Type pageType, Func<T> func);

        void InvolveAction(Type pageType, Action func);
    }
}