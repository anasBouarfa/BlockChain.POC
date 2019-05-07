using Blockchain.POC.Entities;
using Blockchain.POC.Manager;
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
    /// Interaction logic for SignIn.xaml
    /// </summary>
    public partial class SignIn : Window
    {
        private IGlobalManager _globalManager;
        private BlockChain _chain;

        public SignIn()
        {
            _globalManager = new GlobalManager((App.Current.Properties["Port"] as int?).Value);
            _chain = _globalManager.LoadLocalBlockChain();

            InitializeComponent();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            var account  = _globalManager.CreateAccount(Username.Text, Password.Password, Firstname.Text, Lastname.Text, DateTime.Parse(DateOfBirth.Text));

            _chain.PendingTransactions?.Add(new Transaction(null, account.Address, 10)

            );
        }
    }
}
