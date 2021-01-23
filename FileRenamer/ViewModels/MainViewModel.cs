using FileRenamer.Fetcher;
using FileRenamer.Files;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using TheRFramework.Utilities;
using Application = System.Windows.Application;

using WinForms = System.Windows.Forms;

namespace FileRenamer.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public ObservableCollection<FileViewModel> Files { get; set; }

        private FileViewModel _selectedFile;
        private string _newFileName;
        private string _newFileExtension;
        private int _selectedIndex;

        public FileViewModel SelectedFile
        {
            get => _selectedFile;
            set
            {
                if (value != null)
                {
                    NewFileExtension = Path.GetExtension(value.FilePath);
                }
                RaisePropertyChanged(ref _selectedFile, value);
            }
        }

        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                if ((_selectedIndex + 1) >= Files?.Count)
                {
                    value = 0;
                }
                RaisePropertyChanged(ref _selectedIndex, value);
            }
        }

        public string NewFileName
        {
            get => _newFileName;
            set => RaisePropertyChanged(ref _newFileName, value);
        }

        public string NewFileExtension
        {
            get => _newFileExtension;
            set => RaisePropertyChanged(ref _newFileExtension, value);
        }

        public Command OpenDirectoryCommand { get; }
        public Command RenameFileCommand { get; }

        public MainViewModel()
        {
            IconFetcher.Initialise();
            Files = new ObservableCollection<FileViewModel>();
            NewFileName = "";
            NewFileExtension = "";
            OpenDirectoryCommand = new Command(OpenDirectoryDialog);
            RenameFileCommand = new Command(RenameSelectedFile);
        }

        public void AddFile(FileViewModel file)
        {
            Application.Current?.Dispatcher?.Invoke(() =>
            {
                Files.Add(file);
            });
        }

        public void RemoveFile(FileViewModel file)
        {
            Application.Current?.Dispatcher?.Invoke(() =>
            {
                Files.Remove(file);
            });
        }

        public void LoadFilesFromDirectory(string directory)
        {
            Files.Clear();
            GC.Collect(4, GCCollectionMode.Forced, true, true);
            Task.Run(async () =>
            {
                foreach (string path in Directory.GetFiles(directory))
                {
                    FileInfo info = new FileInfo(path);
                    FileViewModel file = new FileViewModel()
                    {
                        FilePath = path,
                        FileSizeBytes = $"{Math.Round(info.Length / 1000000d, 3)} MB"
                    };

                    AddFile(file);
                    await Task.Delay(8);

                    IconFetcher.FetchIcon(file);
                }

                IconFetcher.StartFetching();
            });
        }

        public void OpenDirectoryDialog()
        {
            WinForms.FolderBrowserDialog browser = new WinForms.FolderBrowserDialog()
            {
                Description = "Select a folder to open all the files in",
                RootFolder = Environment.SpecialFolder.Desktop
            };

            if (browser.ShowDialog() == WinForms.DialogResult.OK)
            {
                LoadFilesFromDirectory(browser.SelectedPath);
            };
        }

        public void RenameSelectedFile()
        {
            if (string.IsNullOrEmpty(NewFileName.Trim()))
            {
                NewFileName = "";
                SelectedIndex++;
            }
            else
            {
                string oldDir = Path.GetDirectoryName(SelectedFile.FilePath);
                string newPath = Path.Combine(oldDir, NewFileName + NewFileExtension);

                File.Move(SelectedFile.FilePath, newPath);
                SelectedFile.FilePath = newPath;
                NewFileName = "";

                SelectedIndex++;
            }
        }
    }
}