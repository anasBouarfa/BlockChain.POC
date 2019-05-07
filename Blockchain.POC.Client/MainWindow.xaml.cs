using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Blockchain.POC.Entities;
using Blockchain.POC.Manager;
using Blockchain.POC.P2PServer;
using Blockchain.POC.P2PClient;
using WebSocketSharp;

namespace Blockchain.POC.Client
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IGlobalManager _globalManager;

        public MainWindow()
        {
            _globalManager = new GlobalManager((App.Current.Properties["Port"] as int?).Value);

            InitializeComponent();
        }
        

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if(!Username.Text.IsNullOrWhitespace() &&
               !Password.Password.IsNullOrWhitespace())
            {
                BlockChain chain = _globalManager.LoadLocalBlockChain();

                P2PClient.Client client = new P2PClient.Client(_globalManager);

                if(RemotePort.Text.IsNullOrWhitespace())
                {
                    //handle it 
                }

                var dictionnary = new Dictionary<string, WebSocket>();

                string url = $"ws://localhost:{int.Parse(RemotePort.Text)}/BlockchainPOC";

                dictionnary.Add(url, client.Connect(url, chain));

                App.Current.Properties["urlWebSockets"] = dictionnary;

                chain = _globalManager.LoadLocalBlockChain();

                string userHash = _globalManager.IsAccountLoginValid(chain, Username.Text, Password.Password);

                if(!userHash.IsNullOrWhitespace())
                {
                    App.Current.Properties["User"] = userHash;

                    ChangeWindow(true, userHash);
                }
                else //not authorized
                {

                }
            }
        }

        private void SignIn_Click(object sender, RoutedEventArgs e)
        {
            ChangeWindow(false);
        }

        private void ChangeWindow(bool isAuthorized, string address = null)
        {
            if(!isAuthorized)
            {
                SignIn signInPage = new SignIn
                {
                    Left = this.Left,
                    Top = this.Top
                };

                signInPage.Show();
            }
            else
            {
                Home homePage = new Home
                {
                    Left = this.Left,
                    Top = this.Top
                };
                homePage.Show();
            }

            System.Threading.Thread.Sleep(200);

            this.Close();
        }
    }
}
