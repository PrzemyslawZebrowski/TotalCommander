using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Total_Commander.MVVM.View
{
    /// <summary>
    /// Interaction logic for PanelTC.xaml
    /// </summary>
    public partial class PanelTcView : UserControl
    {
        public PanelTcView()
        {
            InitializeComponent();
        }

        private static readonly DependencyProperty DoubleClickCommandProperty =
            DependencyProperty.Register("DoubleClickCommand", typeof(ICommand), typeof(PanelTcView),
                new FrameworkPropertyMetadata(null));

        private static readonly DependencyProperty CurrentPathProperty =
            DependencyProperty.Register("CurrentPath", typeof(string), typeof(PanelTcView),
                new FrameworkPropertyMetadata(null));

        private static readonly DependencyProperty CurrentDriveProperty =
            DependencyProperty.Register("CurrentDrive", typeof(string), typeof(PanelTcView),
                new FrameworkPropertyMetadata(null));

        private static readonly DependencyProperty DrivesProperty =
            DependencyProperty.Register("Drives", typeof(string[]), typeof(PanelTcView),
                new FrameworkPropertyMetadata(null));

        private static readonly DependencyProperty DirectoryContentProperty =
            DependencyProperty.Register("DirectoryContent", typeof(List<string>), typeof(PanelTcView),
                new FrameworkPropertyMetadata(null));

        private static readonly DependencyProperty SelectedDirectoryProperty =
            DependencyProperty.Register("SelectedDirectory", typeof(string), typeof(PanelTcView),
                new FrameworkPropertyMetadata(null));

        public ICommand DoubleClickCommand
        {
            get => (ICommand)GetValue(DoubleClickCommandProperty);
            set => SetValue(DoubleClickCommandProperty, value);
        }

        public string CurrentPath
        {
            get => (string)GetValue(CurrentPathProperty);
            set => SetValue(CurrentPathProperty, value);
        }

        public string CurrentDrive
        {
            get => (string)GetValue(CurrentDriveProperty);
            set => SetValue(CurrentDriveProperty, value);
        }

        public string[] Drives
        {
            get => (string[])GetValue(DrivesProperty);
            set => SetValue(DrivesProperty, value);
        }

        public List<string> DirectoryContent
        {
            get => (List<string>)GetValue(DirectoryContentProperty);
            set => SetValue(DirectoryContentProperty, value);
        }

        public string SelectedDirectory
        {
            get => (string)GetValue(SelectedDirectoryProperty);
            set => SetValue(SelectedDirectoryProperty, value);
        }
    }
}

