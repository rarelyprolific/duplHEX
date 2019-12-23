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

        /// <summary>
        /// Loads a file and populates the viewer
        /// </summary>
        private async void LoadFile_Click(object sender, RoutedEventArgs e)
        {
            textEditor.Clear();

            var openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == true)
            {
                lblLoadedFile.Content = $"Loading.. {openFileDialog.FileName}";

                byte[] fileBytes = await File.ReadAllBytesAsync(openFileDialog.FileName);
                textEditor.AppendText(new HexBuilder().BuildHex(fileBytes));

                lblDetectedFileType.Content = "Detected file type: Unknown :(";
            }
        }

        /// <summary>
        /// Exits the application
        /// </summary>
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
