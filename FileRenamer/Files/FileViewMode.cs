using System.IO;
using System.Windows.Media;
using TheRFramework.Utilities;

namespace FileRenamer.Files
{
    public class FileViewModel : BaseViewModel
    {
        private ImageSource _icon;
        private string _filePath;
        private string _fileName;
        private string _dateModified;
        private string _fileSizeBytes;

        public ImageSource Icon
        {
            get => _icon;
            set => RaisePropertyChanged(ref _icon, value);
        }

        public string FilePath
        {
            get => _filePath;
            set => RaisePropertyChanged(ref _filePath, value, UpdateFileName);
        }

        public string FileName
        {
            get => _fileName;
            set => RaisePropertyChanged(ref _fileName, value);
        }

        public string FileSizeBytes
        {
            get => _fileSizeBytes;
            set => RaisePropertyChanged(ref _fileSizeBytes, value);
        }

        public FileViewModel()
        {

        }

        private void UpdateFileName()
        {
            FileName = Path.GetFileName(FilePath);
        }
    }
}