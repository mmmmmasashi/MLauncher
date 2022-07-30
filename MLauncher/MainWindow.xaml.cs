using System;
using System.Collections.Generic;
using System.Diagnostics;
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

using LauncherModelLib;

namespace MLauncher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IFilePathRepository _repository;
        public MainWindow()
        {
            _repository = new FilePathRepository("path_list.txt");

            InitializeComponent();
            this.MainTextBox.Focus();
        }

        private void TextBox_DragEnter(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Copy;
            e.Handled = true;
        }

        private void TextBox_Drop(object sender, DragEventArgs e)
        {
            var textArray = (string[])e.Data.GetData(DataFormats.FileDrop);

            foreach (var text in textArray)
            {
                _repository.Save(new FilePath(text));
            }
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                FilePath matchedPath = _repository.Search(MainTextBox.Text);
                ProcessRunner.Run(matchedPath);
            }
        }
    }
}
