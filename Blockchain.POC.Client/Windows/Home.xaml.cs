using Blockchain.POC.Common;
using System.Windows;

namespace Blockchain.POC.Client
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
            new MyBalance
            {
                Left = this.Left,
                Top = this.Top
            }.Show();

            System.Threading.Thread.Sleep(200);

            this.Close();
        }

        private void MyTransactions_Click(object sender, RoutedEventArgs e)
        {
            new History
            {
                Left = this.Left,
                Top = this.Top
            }.Show();

            System.Threading.Thread.Sleep(200);

            this.Close();
        }

        private void NewTransaction_Click(object sender, RoutedEventArgs e)
        {
            new NewTransaction
            {
                Left = this.Left,
                Top = this.Top
            }.Show();

            System.Threading.Thread.Sleep(200);

            this.Close();
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow
            {
                Left = this.Left,
                Top = this.Top
            }.Show();

            System.Threading.Thread.Sleep(200);

            this.Close();
        }

        private void ConnectToNode_Click(object sender, RoutedEventArgs e)
        {
            new AddNode
            {
                Left = this.Left,
                Top = this.Top
            }.Show();

            System.Threading.Thread.Sleep(200);

            this.Close();
        }

        private void Mine_Click(object sender, RoutedEventArgs e)
        {
            new Mine
            {
                Left = this.Left,
                Top = this.Top
            }.Show();

            System.Threading.Thread.Sleep(200);

            this.Close();
        }
    }
}