using Blockchain.POC.Common;
using System.Windows;

namespace Blockchain.POC.UI
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : BaseWindow
    {
        private string address;

        public Home()
        {
            address = App.Current.Properties[ApplicationPropertiesConstants.UserAddress] as string;
            InitializeComponent();
        }

        private void MyBalance_Click(object sender, RoutedEventArgs e)
        {
            Redirect(nameof(MyBalance));
        }

        private void MyTransactions_Click(object sender, RoutedEventArgs e)
        {
            Redirect(nameof(History));
        }

        private void NewTransaction_Click(object sender, RoutedEventArgs e)
        {
            Redirect(nameof(NewTransaction));
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            Redirect(nameof(MainWindow));
        }

        private void ConnectToNode_Click(object sender, RoutedEventArgs e)
        {
            Redirect(nameof(AddNode));
        }

        private void Mine_Click(object sender, RoutedEventArgs e)
        {
            Redirect(nameof(Mine));
        }
    }
}