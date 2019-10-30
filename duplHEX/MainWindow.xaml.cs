﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
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

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnLoadFile_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == true)
            {
                lblLoadedFile.Content = $"Loading.. {openFileDialog.FileName}";

                // TODO: Load this file and print the hex in AvalonEditor
            }

        }
    }
}
