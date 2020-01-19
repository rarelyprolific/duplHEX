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

            // Sets up the width for the hex view for a specific font size
            // TODO: Adjust and re-calculate this when the font size changes.
            this.MinWidth = 900;
            this.MaxWidth = 900;
        }

        /// <summary>
        /// Captures a file dropped onto the hex viewer control and loads it
        /// </summary>
        private async void HexViewer_FileDrop(object sender, DragEventArgs e)
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
                LoadedFileLabel.Content = $"Loaded: {openFileDialog.FileName}";
                DetectedFileTypeSeparator.Visibility = Visibility.Visible;
                DetectedFileTypeLabel.Content = "Unknown file type - XXMB [xxxxxxx bytes]";
            }
        }

        /// <summary>
        /// Loads a file and displays the hex in the text editor
        /// </summary>
        /// <param name="pathToFile">The full path to the file to load</param>
        private async Task LoadFileAndDisplayHex(string pathToFile)
        {
            HexViewer.Clear();

            byte[] fileBytes = await new FileLoader().LoadFile(pathToFile);
            HexViewer.AppendText(new HexBuilder().BuildHex(fileBytes));
        }

        /// <summary>
        /// Shows the About window
        /// </summary>
        private void AboutMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var aboutWindow = new AboutWindow();
            aboutWindow.Owner = this;
            aboutWindow.ShowDialog();
        }


        /// <summary>
        /// Exits the application
        /// </summary>
        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
