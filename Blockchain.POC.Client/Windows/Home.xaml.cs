using Blockchain.POC.Entities;
using System.Collections.Generic;
using System.Windows;

namespace Blockchain.POC.Client
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : BaseWindow
    {
        private string address;

        public Home(string address)
        {
            this.address = address;
            FullName.Text = address;
            InitializeComponent();
        }

        public Home()
        {
            InitializeComponent();
        }

        private void MyBalance_Click(object sender, RoutedEventArgs e)
        {
            double balance = _globalManager.GetAccountBalance(_chain, address);
        }

        private void MyTransactions_Click(object sender, RoutedEventArgs e)
        {
            List<Transaction> transactions = _globalManager.GetTransactionsByAddress(_chain, address);
        }

        private void NewTransaction_Click(object sender, RoutedEventArgs e)
        {
            new NewTransaction
            {
                Left = this.Left,
                Top = this.Top
            }.Show();

            System.Threading.Thread.Sleep(250);

            this.Close();
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow
            {
                Left = this.Left,
                Top = this.Top
            }.Show();

            System.Threading.Thread.Sleep(250);

            this.Close();
        }

        private void ConnectToNode_Click(object sender, RoutedEventArgs e)
        {
            new AddNode
            {
                Left = this.Left,
                Top = this.Top
            }.Show();

            System.Threading.Thread.Sleep(250);

            this.Close();
        }

        private void Mine_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}