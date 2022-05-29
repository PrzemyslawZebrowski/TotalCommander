using System.IO;
using System.Windows.Input;
using Total_Commander.Core;

namespace Total_Commander.MVVM.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        private PanelTcViewModel _leftPanel;
        private PanelTcViewModel _rightPanel;

        public MainViewModel()
        {
            LeftPanel = new PanelTcViewModel();
            RightPanel = new PanelTcViewModel();
            CopyCommand = new RelayCommand(Copy, CopyCheck);
        }
        public ICommand CopyCommand { get; set; }

        public PanelTcViewModel LeftPanel
        {
            get => _leftPanel;
            set
            {
                _leftPanel = value;
                OnPropertyChanged(nameof(LeftPanel));
            }
        }

        public PanelTcViewModel RightPanel
        {
            get => _rightPanel;
            set
            {
                _rightPanel = value;
                OnPropertyChanged(nameof(RightPanel));
            }
        }

        private void Copy(object o)
        {
            string filePath, directoryPath, fileName;
            if (LeftPanel.SelectedDirectory != null)
            {
                filePath = LeftPanel.CurrentPath + "\\" + LeftPanel.SelectedDirectory.Trim();
                directoryPath = RightPanel.CurrentPath;
                fileName = Path.GetFileName(filePath);
            }
            else
            {
                filePath = RightPanel.CurrentPath + "\\" + RightPanel.SelectedDirectory.Trim();
                directoryPath = LeftPanel.CurrentPath;
                fileName = Path.GetFileName(filePath);
            }
            File.Copy(filePath, directoryPath + "\\" + fileName);
            LeftPanel.CurrentPath = LeftPanel.CurrentPath;
            RightPanel.CurrentPath = RightPanel.CurrentPath;
        }

        private bool CopyCheck(object o)
        {
            if (LeftPanel.CurrentPath == RightPanel.CurrentPath)
                return false;

            if (LeftPanel.SelectedDirectory != null
                && RightPanel.DirectoryContent != null
                && RightPanel.DirectoryContent.Contains(LeftPanel.SelectedDirectory))
                return false;

            if (RightPanel.SelectedDirectory != null
                && LeftPanel.DirectoryContent != null
                && LeftPanel.DirectoryContent.Contains(RightPanel.SelectedDirectory))
                return false;

            if (LeftPanel.SelectedDirectory != null
                && !LeftPanel.SelectedDirectory.Contains("<D>")
                && LeftPanel.SelectedDirectory != ".." && RightPanel.CurrentPath != null)
                return true;

            return RightPanel.SelectedDirectory != null
                   && !RightPanel.SelectedDirectory.Contains("<D>")
                   && RightPanel.SelectedDirectory != ".." && LeftPanel.CurrentPath != null;
        }
    }
}
