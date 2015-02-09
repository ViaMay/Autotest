using System;
using System.IO;
using System.Linq;
using System.Threading;
using log4net;

namespace Autotests.Utilities.WebTestCore.TestSystem
{
    internal static class WebDriverDownloadDirectory
    {
        public static readonly string DirectoryPath = Path.GetFullPath(@"..\..\..\.downloads");
        private static readonly ILog logger = LogManager.GetLogger(typeof (WebDriverDownloadDirectory));

        public static void Clean()
        {
            foreach (string file in GetDownloadedFileNames())
                File.Delete(file);
        }

        public static string[] GetDownloadedFileNames()
        {
            if (!Directory.Exists(DirectoryPath))
                return new string[0];
            return
                Directory.GetFiles(DirectoryPath)
                    .Where(name => !name.EndsWith(".crdownload") && !name.EndsWith(".tmp"))
                    .ToArray();
        }

        public static void WaitDownloadFiles(int expectedCountFiles, int maximalWaitTime = 15000,
            int sleepInterval = 100)
        {
            logger.InfoFormat("Wait with time {0} sec. is started", maximalWaitTime/1000);
            long startTicks = DateTime.UtcNow.Ticks;
            while (true)
            {
                long currentTicks = DateTime.UtcNow.Ticks;
                if (currentTicks - startTicks > maximalWaitTime*10000L)
                {
                    int actualCountFiles = GetDownloadedFileNames().Length;
                    if (actualCountFiles == expectedCountFiles)
                        logger.Info(string.Format("Скачано {0} файлов, ожидается {1}.", actualCountFiles,
                            expectedCountFiles));
                    throw new Exception("Время ожидания истекло");
                }

                if (GetDownloadedFileNames().Length == expectedCountFiles)
                    break;
                Thread.Sleep(sleepInterval);
            }
            logger.Info("Wait finished");
        }
    }
}