using System;
using Autotests.Utilities.WebTestCore.TestSystem;

namespace Autotests.Utilities.WebTestCore.SystemControls
{
    public static class JsLogger
    {
        private static string log;

        public static void Complete()
        {
            log += Read();
        }

        public static void Reset()
        {
            log = "";
        }

        public static void Show()
        {
            Console.Out.WriteLine("JavaScript log START\n");
            Console.Out.WriteLine(log);
            Console.Out.WriteLine("JavaScript log END");
            Reset();
        }

        private static string Read()
        {
//            return WebDriverCache.WebDriver.ExecuteScript("return Logger.read()") as string;
            return WebDriverCache.WebDriver.ExecuteScript("return $(document.body)") as string;
        }
    }
}