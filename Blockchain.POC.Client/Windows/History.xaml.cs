using Blockchain.POC.Common;
using Blockchain.POC.Entities;
using System.Collections.Generic;
using System.Windows;

namespace Blockchain.POC.Client
{
    /// <summary>
    /// Interaction logic for History.xaml
    /// </summary>
    public partial class History : BaseWindow
    {
        public History() : base()
        {
            _chain = _globalManager.LoadLocalBlockChain();
            TransactionsDG.ItemsSource = LoadDGData();
            InitializeComponent();
        }

        private List<Transaction> LoadDGData()
        {
            return _globalManager.GetTransactionsByAddress(_chain, App.Current.Properties[ApplicationPropertiesConstants.UserAddress] as string);
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            new Home()
            {
                Left = this.Left,
                Top = this.Top
            }.Show();

            System.Threading.Thread.Sleep(250);

            this.Close();
        }
    }
}