﻿using Blockchain.POC.Common;
using Blockchain.POC.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Blockchain.POC.UI
{
    /// <summary>
    /// Interaction logic for History.xaml
    /// </summary>
    public partial class History : BaseWindow
    {
        public History() : base()
        {
            _chain = _globalManager.LoadLocalBlockChain();
            InitializeComponent();
            TransactionsDG.ItemsSource = LoadDGData().Select(s => new
            {
                s.FromAddress,
                s.ToAddress,
                s.Amount,
                CreationDate = s.CreationDate.ToShortDateString()
            });
        }

        private List<Transaction> LoadDGData()
        {
            return _globalManager.GetTransactionsByAddress(_chain, App.Current.Properties[ApplicationPropertiesConstants.UserAddress] as string);
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            Redirect(nameof(Home));
        }
    }
}