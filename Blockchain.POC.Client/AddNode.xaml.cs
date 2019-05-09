using Blockchain.POC.Manager;
using System;
using System.Collections.Generic;
using WebSocketSharp;
using System.Windows;
using System.Windows.Input;
using Blockchain.POC.Common;

namespace Blockchain.POC.Client
{
    /// <summary>
    /// Interaction logic for AddNode.xaml
    /// </summary>
    public partial class AddNode : BaseWindow
    {
        public AddNode()
        {
            InitializeComponent();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            e.Handled = e.Text.IsNumeric();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (!Port.Text.IsNullOrWhitespace())
            {
                P2PClient.Client client = new P2PClient.Client(_globalManager);

                string url = $"ws://localhost:{int.Parse(Port.Text)}/BlockchainPOC";

                var dictionnary = App.Current.Properties[ApplicationPropertiesConstants.PortUrlWebSockets] as Dictionary<string, WebSocket> ?? new Dictionary<string, WebSocket>();

                dictionnary.Add(url, client.Connect(url, _globalManager.LoadLocalBlockChain()));

                App.Current.Properties[ApplicationPropertiesConstants.PortUrlWebSockets] = dictionnary;

                Home homeWindow = new Home
                {
                    Left = this.Left,
                    Top = this.Top
                };

                homeWindow.Show();

                System.Threading.Thread.Sleep(200);

                this.Close();
            }
        }
    }
}
