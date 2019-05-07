using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Blockchain.POC.Manager;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Blockchain.POC.Entities;

namespace Blockchain.POC.Client
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Window
    {
        private string address;
        private int myPort;
        private IGlobalManager _globalManager;
        private BlockChain _chain;

        public Home(string address)
        {
            this.address = address;
            FullName.Text = address;
            _globalManager = new GlobalManager((App.Current.Properties["Port"] as int?).Value);
            LoadBlockChain();
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
            //TODO 


        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow
            {
                Left = this.Left,
                Top = this.Top
            };
            mainWindow.Show();

            System.Threading.Thread.Sleep(250);

            this.Close();
        }

        private void LoadBlockChain()
        {
            this._chain = _globalManager.LoadLocalBlockChain();
            
        }
    }
}
