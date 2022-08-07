using LauncherModelLib;
using Prism.Commands;
using Prism.Mvvm;
using System.Windows;
using System.Windows.Input;

namespace MLauncherApp.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Application";
        IFilePathRepository _repository;

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private string _textBoxTest;
        public string TextBoxText
        {
            get { return _textBoxTest; }
            set { SetProperty(ref _textBoxTest, value); }
        }

        public DelegateCommand<DragEventArgs> DragEnterCommand { get; }
        public DelegateCommand<DragEventArgs> DropCommand      { get; }
        public DelegateCommand<KeyEventArgs>  KeyDownCommand   { get; }

        public MainWindowViewModel()
        {
            _repository = new FilePathRepository("path_list.txt");

            DragEnterCommand = new DelegateCommand<DragEventArgs>(e =>
            {
                e.Effects = DragDropEffects.Copy;
                e.Handled = true;
            });

            DropCommand = new DelegateCommand<DragEventArgs>(e =>
            {
                var textArray = (string[])e.Data.GetData(DataFormats.FileDrop);

                foreach (var text in textArray)
                {
                    _repository.Save(new FilePath(text));
                }

            });

            KeyDownCommand = new DelegateCommand<KeyEventArgs>(e =>
            {
                if (e.Key == Key.Return)
                {
                    FilePath matchedPath = _repository.Search(TextBoxText);
                    ProcessRunner.Run(matchedPath);
                }
            });
        }
    }
}
