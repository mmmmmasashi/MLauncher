using Prism.Services.Dialogs;
using System.Windows;

namespace MLauncherApp.Views
{
    /// <summary>
    /// Interaction logic for DialogWindow.xaml
    /// </summary>
    public partial class DialogWindow : IDialogWindow
    {
        public DialogWindow()
        {
            InitializeComponent();
        }

        public IDialogResult Result { get; set; }
    }
}
