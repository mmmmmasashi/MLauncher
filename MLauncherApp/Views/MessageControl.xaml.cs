using System.Windows.Controls;

namespace MLauncherApp.Views
{
    /// <summary>
    /// Interaction logic for MessageControl
    /// </summary>
    public partial class MessageControl : UserControl
    {
        public MessageControl()
        {
            InitializeComponent();
        }

        private void MessageControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            OKButton.Focus();
        }
    }
}
