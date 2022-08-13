using Prism.Services.Dialogs;
using System;
using System.Windows.Controls;

namespace MLauncherApp.Views
{
    /// <summary>
    /// Interaction logic for ConfirmControl
    /// </summary>
    public partial class ConfirmControl : UserControl
    {
        
        public ConfirmControl()
        {
            InitializeComponent();
        }

        private void ConfirmControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            OKButton.Focus();
        }
    }
}
