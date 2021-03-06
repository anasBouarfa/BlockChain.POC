﻿using Blockchain.POC.Common;
using Blockchain.POC.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using WebSocketSharp;

namespace Blockchain.POC.UI
{
    /// <summary>
    /// Interaction logic for Mine.xaml
    /// </summary>
    public partial class Mine : BaseWindow
    {
        public Mine() : base()
        {
            InitializeComponent();
            DataContext = new { PTCount = _chain.PendingTransactions.Count, Reward = BlockChain.Reward };
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Redirect(nameof(Home));
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if(_chain.PendingTransactions.Any())
            {
                _chain = _globalManager.AddBlock(_chain, App.Current.Properties[ApplicationPropertiesConstants.UserAddress] as string);

                P2PClient.Client client = new P2PClient.Client(_globalManager)
                {
                    urlwebSockets = App.Current.Properties[ApplicationPropertiesConstants.PortUrlWebSockets] as Dictionary<string, WebSocket>
                };
                client.BroadcastChain(_chain);
                _globalManager.SaveBlockChain(_chain);

                Redirect(nameof(Home));
            }
            else
            {
                Error.View(this, "There are no pending transactions", true);
            }
        }
    }
}