﻿using Blockchain.POC.Common;
using Blockchain.POC.Entities;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using WebSocketSharp;

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
            if (_globalManager.IsAccountAddressValid(_chain, Address.Text) && Address.Text != App.Current.Properties[ApplicationPropertiesConstants.UserAddress] as string)
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

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            e.Handled = e.Text.IsNumeric();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
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