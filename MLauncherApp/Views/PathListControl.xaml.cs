using LauncherModelLib;
using System.Collections.Generic;
using System.Windows.Controls;

namespace MLauncherApp.Views
{
    /// <summary>
    /// Interaction logic for PathListControl
    /// </summary>
    public partial class PathListControl : UserControl
    {
        public PathListControl()
        {
            InitializeComponent();
        }

        private void LoadedEvent(object sender, System.Windows.RoutedEventArgs e)
        {
            MainDataGrid.Focus();
        }
    }
}
