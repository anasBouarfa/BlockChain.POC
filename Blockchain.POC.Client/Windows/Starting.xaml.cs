using Blockchain.POC.Common;
using Blockchain.POC.Entities;
using Blockchain.POC.Manager;
using Blockchain.POC.P2PServer;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using WebSocketSharp;

namespace Blockchain.POC.Client
{
    /// <summary>
    /// Interaction logic for Starting.xaml
    /// </summary>
    public partial class Starting : Window
    {
        IGlobalManager _globalManager;

        public Starting()
        {
            InitializeComponent();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (!Port.Text.IsNullOrWhitespace()
                && !RemotePort.Text.IsNullOrWhitespace())
            {
                Server server = new Server();
                server.Start(int.Parse(Port.Text));

                _globalManager = new GlobalManager(int.Parse(Port.Text));

                App.Current.Properties[ApplicationPropertiesConstants.Port] = int.Parse(Port.Text);

                if(Port.Text != "12900")
                {
                    BlockChain chain = _globalManager.LoadLocalBlockChain();

                    P2PClient.Client client = new P2PClient.Client(_globalManager);

                    var dictionnary = new Dictionary<string, WebSocket>();
                    string url = $"ws://localhost:{int.Parse(RemotePort.Text)}/BlockchainPOC";
                    dictionnary.Add(url, client.Connect(url, chain));
                    client.urlwebSockets = dictionnary;
                    client.Send(url, "Show me your blockchain !");

                    App.Current.Properties[ApplicationPropertiesConstants.PortUrlWebSockets] = dictionnary;
                }

                MainWindow mainWindow = new MainWindow
                {
                    Left = this.Left,
                    Top = this.Top
                };

                mainWindow.Show();

                System.Threading.Thread.Sleep(200);

                this.Close();
            }
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            e.Handled = e.Text.IsNumeric();
        }
    }
}