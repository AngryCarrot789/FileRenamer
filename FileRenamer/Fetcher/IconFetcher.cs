using FileRenamer.Files;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FileRenamer.Fetcher
{
    public static class IconFetcher
    {
        private static Thread IconReceiverThread { get; set; }

        private static bool CanRun { get; set; }

        private static List<FileViewModel> FetcherPool { get; set; }
        public static bool CanFetchIcons { get; private set; }

        public static void Initialise()
        {
            CanRun = true;
            FetcherPool = new List<FileViewModel>();
            IconReceiverThread = new Thread(ReceiveIcons);
            IconReceiverThread.Name = "Icon Receiver";
            IconReceiverThread.Start();
        }

        private static void ReceiveIcons()
        {
            while (CanRun)
            {
                if (CanFetchIcons)
                {
                    foreach (FileViewModel file in FetcherPool)
                    {
                        if (CanFetchIcons)
                        {
                            if (IconHelper.IsImageFile(file.FilePath))
                            {
                                if (CanFetchIcons)
                                {
                                    ImageSource source = IconHelper.LoadBitmapImage(file.FilePath);
                                    source.Freeze();
                                    Application.Current?.Dispatcher?.Invoke(() =>
                                    {
                                        file.Icon = source.Clone();
                                    });
                                }
                                else break;
                            }
                            else
                            {
                                Icon icon = IconHelper.GetIconOfFile(file.FilePath, false, false);
                                if (CanFetchIcons)
                                {
                                    ImageSource source = IconHelper.ToImageSource(icon);
                                    source.Freeze();
                                    Application.Current?.Dispatcher?.Invoke(() =>
                                    {
                                        file.Icon = source.Clone();
                                    });
                                }
                                else break;
                            }

                            Thread.Sleep(1);
                        }
                        else break;
                    }

                    FetcherPool.Clear();
                    StopFetching();
                }
            }

            Thread.Sleep(1);
        }

        public static void FetchIcon(FileViewModel file)
        {
            FetcherPool.Add(file);
        }

        public static void StartFetching()
        {
            CanFetchIcons = true;
        }

        public static void StopFetching()
        {
            CanFetchIcons = false;
        }

        public static void ClearPool()
        {
            FetcherPool.Clear();
        }

        public static void ForceShutdown()
        {
            CanRun = false;
        }
    }
}
