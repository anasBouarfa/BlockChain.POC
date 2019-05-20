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
            MyBalance.Redirect(this);
        }

        private void MyTransactions_Click(object sender, RoutedEventArgs e)
        {
            History.Redirect(this);
        }

        private void NewTransaction_Click(object sender, RoutedEventArgs e)
        {
            NewTransaction.Redirect(this);
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Redirect(this);
        }

        private void ConnectToNode_Click(object sender, RoutedEventArgs e)
        {
            AddNode.Redirect(this);
        }

        private void Mine_Click(object sender, RoutedEventArgs e)
        {
            Mine.Redirect(this);
        }
    }
}