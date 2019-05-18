using Blockchain.POC.Common;
using System.Linq;
using System;
using System.Windows;

namespace Blockchain.POC.UI
{
    /// <summary>
    /// Interaction logic for MyBalance.xaml
    /// </summary>
    public partial class MyBalance : BaseWindow
    {
        public MyBalance()
        {
            InitializeComponent();

            var balance = _globalManager.GetAccountBalance(_chain, App.Current.Properties[ApplicationPropertiesConstants.UserAddress] as string).ToString();
            var pendingTransactions = _globalManager.GetPendingTransactionsByAddress(_chain, App.Current.Properties[ApplicationPropertiesConstants.UserAddress] as string);
            PendingTransactionsDG.ItemsSource = pendingTransactions;
            DataContext = new
            {
                balance,
                finalBalance = int.Parse(balance) + (pendingTransactions.IsNullOrEmpty() ? 0 : pendingTransactions.Sum(s => s.ToAddress == "You" ? s.Amount : s.FromAddress == "You" ? -s.Amount : s.Amount))
            };
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            new Home()
            {
                Left = this.Left,
                Top = this.Top
            }.Show();

            System.Threading.Thread.Sleep(200);

            this.Close();
        }
    }
}