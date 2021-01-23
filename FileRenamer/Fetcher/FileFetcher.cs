using FileRenamer.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FileRenamer.Fetcher
{
    public static class FileFetcher
    {
        //private static Thread FetchThread { get; set; }
        //
        //private static bool CanRunFetcher { get; set; }
        //private static bool CanStartSearching { get; set; }
        //
        //private static string ActivePath { get; set; }
        //
        //public static Action FilesFetchedCallback { get; set; }
        //public static Action DirectoriesFetchedCallback { get; set; }
        //
        //public static void Initialise()
        //{
        //    StopFetching();
        //    CanRunFetcher = true;
        //    FetchThread = new Thread(FetchDirectories);
        //    FetchThread.Start();
        //}
        //
        //[MTAThread]
        //private static void FetchDirectories()
        //{
        //    while (CanRunFetcher)
        //    {
        //        if (CanStartSearching)
        //        {
        //            foreach (string path in Directory.GetDirectories(ActivePath))
        //            {
        //                DirectoryInfo folder = new DirectoryInfo(path);
        //                FileModel model =
        //                    new FileModel(
        //                        path,
        //                        folder.LastWriteTime,
        //                        FileType.Directory,
        //                        int.MaxValue);
        //
        //                DirectoryFetchedCallback?.Invoke(model);
        //                Thread.Sleep(1);
        //            }
        //
        //            foreach (string path in Directory.GetFiles(ActivePath))
        //            {
        //                FileInfo file = new FileInfo(path);
        //                FileModel model =
        //                    new FileModel(
        //                        path,
        //                        file.LastWriteTime,
        //                        FileType.File,
        //                        file.Length);
        //
        //                FileFetchedCallback?.Invoke(model);
        //                Thread.Sleep(1);
        //            }
        //
        //            StopFetching();
        //        }
        //
        //        Thread.Sleep(1);
        //    }
        //}
        //
        //public static void StartFetching(string path)
        //{
        //    ActivePath = path;
        //    CanStartSearching = true;
        //}
        //
        //public static void StopFetching()
        //{
        //    CanStartSearching = false;
        //    ActivePath = "";
        //}
        //
        //public static void ForceStop()
        //{
        //    CanRunFetcher = false;
        //}
    }
}
