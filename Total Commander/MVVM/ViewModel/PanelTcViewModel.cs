using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Input;
using Total_Commander.Core;

namespace Total_Commander.MVVM.ViewModel
{
    public class PanelTcViewModel : ObservableObject
    {
        private string _currentPath;
        private string[] _drives;
        private string _currentDrive;
        private string _selectedDirectory;
        private List<string> _directoryContent;

        public PanelTcViewModel()
        {
            GetDrives();
            ChangeDirectoryCommand = new RelayCommand(ChangeDirectory, ChangeDirectoryCheck);
        }

        public ICommand ChangeDirectoryCommand { get; set; }

        public string CurrentPath
        {
            get => _currentPath;
            set
            {
                _currentPath = value;
                OnPropertyChanged(nameof(CurrentPath));
                UpdateDisplayContent();
            }
        }
        public string[] Drives
        {
            get => _drives;
            set
            {
                _drives = value;
                OnPropertyChanged(nameof(Drives));
            }
        }

        public string CurrentDrive
        {
            get => _currentDrive;
            set
            {
                _currentDrive = value;
                OnPropertyChanged(nameof(CurrentDrive));
                UpdatePath();
            }
        }
        public string SelectedDirectory
        {
            get => _selectedDirectory;
            set
            {
                _selectedDirectory = value;
                OnPropertyChanged(nameof(SelectedDirectory));
            }
        }

        public List<string> DirectoryContent
        {
            get => _directoryContent;
            set
            {
                _directoryContent = value;
                OnPropertyChanged(nameof(DirectoryContent));
            }
        }
        private void GetDrives()
        {
            Drives = Directory.GetLogicalDrives();
        }

        private void ChangeDirectory(object o)
        {
            if (_selectedDirectory == "..")
                CurrentPath = Directory.GetParent(CurrentPath).FullName;
            else
            {
                if (CurrentPath.EndsWith("\\"))
                    CurrentPath += _selectedDirectory.Replace("<D>", "");
                else
                    CurrentPath += "\\" + _selectedDirectory.Replace("<D>", "");
            }
        }

        private bool ChangeDirectoryCheck(object o)
        {
            if (_selectedDirectory == "..") return true;

            return _selectedDirectory != null && _selectedDirectory.Contains("<D>");
        }

        private void UpdatePath()
        {
            CurrentPath = _currentDrive;
        }

        private void UpdateDisplayContent()
        {
            var content = new List<string>();

            var files = Directory.GetFiles(CurrentPath);
            var directories = Directory.GetDirectories(CurrentPath);

            if (Directory.GetParent(CurrentPath) != null)
                content.Add("..");

            content.AddRange(directories.Select(d => "<D>" + Path.GetFileName(d)));

            content.AddRange(files.Select(Path.GetFileName));

            DirectoryContent = content;
        }
    }
}

