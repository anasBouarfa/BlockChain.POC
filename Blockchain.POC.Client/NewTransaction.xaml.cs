using Blockchain.POC.Common;
using Blockchain.POC.Entities;
using Blockchain.POC.Manager;
using System;
using System.Collections.Generic;
using WebSocketSharp;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Blockchain.POC.Client
{
    /// <summary>
    /// Interaction logic for NewTransaction.xaml
    /// </summary>
    public partial class NewTransaction : BaseWindow
    {
        public NewTransaction()
        {
            InitializeComponent();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if(_globalManager.IsAccountAddressValid(_chain, Address.Text) && Address.Text != App.Current.Properties[ApplicationPropertiesConstants.UserAddress] as string)
            {
                if (!_chain.PendingTransactions.IsNullOrEmpty())
                    _chain.PendingTransactions.Add(new Transaction(App.Current.Properties[ApplicationPropertiesConstants.UserAddress] as string, Address.Text, double.Parse(Amount.Text)));
                else
                    _chain.PendingTransactions = new List<Transaction> { new Transaction(App.Current.Properties[ApplicationPropertiesConstants.UserAddress] as string, Address.Text, double.Parse(Amount.Text)) };

                P2PClient.Client client = new P2PClient.Client(_globalManager)
                {
                    urlwebSockets = App.Current.Properties[ApplicationPropertiesConstants.PortUrlWebSockets] as Dictionary<string, WebSocket>
                };
                client.BroadcastChain(_chain);
            }

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Home homePage = new Home
            {
                Left = this.Left,
                Top = this.Top
            };

            homePage.Show();
        }
    }
}
