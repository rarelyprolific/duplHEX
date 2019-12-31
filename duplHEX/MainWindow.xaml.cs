using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace duplHEX
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void DropAFileInEditor_Drop(object sender, DragEventArgs e)
        {
            // Check we have a file to read
            if (e.Data.GetDataPresent(DataFormats.FileDrop, true))
            {
                // Get the file data
                var fileDetails = e.Data.GetData(DataFormats.FileDrop, true) as string[];
                await LoadFileAndDisplayHex(fileDetails[0]);
            }
        }

        /// <summary>
        /// Loads a file and populates the viewer
        /// </summary>
        private async void LoadFile_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == true)
            {
                await LoadFileAndDisplayHex(openFileDialog.FileName);
                lblLoadedFile.Content = $"Loaded: {openFileDialog.FileName}";
                sepDetectedFileType.Visibility = Visibility.Visible;
                lblDetectedFileType.Content = "Unknown file type";
            }
        }

        /// <summary>
        /// Exits the application
        /// </summary>
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        /// <summary>
        /// Loads a file and displays the hex in the text editor
        /// </summary>
        /// <param name="pathToFile">The full path to the file to load</param>
        private async Task LoadFileAndDisplayHex(string pathToFile)
        {
            hexViewer.Clear();

            byte[] fileBytes = await new FileLoader().LoadFile(pathToFile);
            hexViewer.AppendText(new HexBuilder().BuildHex(fileBytes));
        }

        private void AboutMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var aboutWindow = new AboutWindow();
            aboutWindow.Owner = this;
            aboutWindow.ShowDialog();
        }
    }
}
