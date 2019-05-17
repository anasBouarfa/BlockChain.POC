using Blockchain.POC.Common;
using System;
using System.Collections.Generic;
using System.Windows;
using WebSocketSharp;

namespace Blockchain.POC.UI
{
    /// <summary>
    /// Interaction logic for SignIn.xaml
    /// </summary>
    public partial class SignIn : BaseWindow
    {
        public SignIn() : base()
        {
            _chain = _globalManager.LoadLocalBlockChain();
            InitializeComponent();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            _chain = _globalManager.CreateAccount(_chain, Username.Text, Password.Password, Firstname.Text, Lastname.Text, DateTime.Parse(DateOfBirth.Text));

            if (_chain != null)
            {
                P2PClient.Client client = new P2PClient.Client(_globalManager)
                {
                    urlwebSockets = App.Current.Properties[ApplicationPropertiesConstants.PortUrlWebSockets] as Dictionary<string, WebSocket>
                };

                client.BroadcastChain(_chain);
                _globalManager.SaveBlockChain(_chain);
            }

            new Home
            {
                Top = this.Top,
                Left = this.Left
            }.Show();

            System.Threading.Thread.Sleep(200);

            this.Close();
        }
    }
}