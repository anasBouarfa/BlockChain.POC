﻿using Blockchain.POC.Common;
using Blockchain.POC.Entities;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using WebSocketSharp;

namespace Blockchain.POC.UI
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
            var address = App.Current.Properties[ApplicationPropertiesConstants.UserAddress] as string;

            if (_globalManager.IsAccountUsernameValid(_chain, Address.Text) && Address.Text != address)
            {
                var account = _globalManager.GetAccountByUsername(_chain, Address.Text);

                int balance = _globalManager.GetAccountBalance(_chain, address);
                int currentBalance = balance + _globalManager.GetPendingTransactionsBaluByAddress(_chain, address);

                if(balance <= currentBalance && balance >= int.Parse(Amount.Text))
                {
                    if (!_chain.PendingTransactions.IsNullOrEmpty())
                        _chain.PendingTransactions.Add(new Transaction(address, account.Address, int.Parse(Amount.Text)));
                    else
                        _chain.PendingTransactions = new List<Transaction> { new Transaction(address, account.Address, int.Parse(Amount.Text)) };

                    P2PClient.Client client = new P2PClient.Client(_globalManager)
                    {
                        urlwebSockets = App.Current.Properties[ApplicationPropertiesConstants.PortUrlWebSockets] as Dictionary<string, WebSocket>
                    };

                    client.BroadcastChain(_chain);
                    _globalManager.SaveBlockChain(_chain);
                }
                else
                {
                    //TODO: balance insufficiant
                }
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