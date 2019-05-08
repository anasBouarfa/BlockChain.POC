using Blockchain.POC.Entities;
using Blockchain.POC.Common;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for History.xaml
    /// </summary>
    public partial class History : BaseWindow
    {
        public History() : base()
        {
            TransactionsDG.ItemsSource = LoadDGData();
            InitializeComponent();
        }

        private List<Transaction> LoadDGData()
        {
            return _globalManager.GetTransactionsByAddress(_chain, App.Current.Properties[ApplicationPropertiesConstants.UserAddress] as string);
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
