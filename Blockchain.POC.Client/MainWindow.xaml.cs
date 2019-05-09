using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Blockchain.POC.Entities;
using Blockchain.POC.Manager;
using Blockchain.POC.Common;
using WebSocketSharp;

namespace Blockchain.POC.Client
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : BaseWindow
    {
        public MainWindow()
        {
            InitializeComponent();
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

                App.Current.Properties[ApplicationPropertiesConstants.PortUrlWebSockets] = dictionnary;

                chain = _globalManager.LoadLocalBlockChain();

                string userHash = _globalManager.IsAccountLoginValid(chain, Username.Text, Password.Password);

                if(!userHash.IsNullOrWhitespace())
                {
                    App.Current.Properties[ApplicationPropertiesConstants.UserAddress] = userHash;

                    ChangeWindow(true, userHash);
                }
                else //not authorized
                {

                }
            }
        }

        protected void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            e.Handled = e.Text.IsNumeric();
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
