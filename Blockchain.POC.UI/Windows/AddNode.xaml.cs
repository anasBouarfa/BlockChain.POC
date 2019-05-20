using Blockchain.POC.Common;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using WebSocketSharp;

namespace Blockchain.POC.UI
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

                var dictionnary = new Dictionary<string, WebSocket>();
                string url = $"ws://localhost:{int.Parse(Port.Text)}/BlockchainPOC";
                dictionnary.Add(url, client.Connect(url, _chain));
                client.urlwebSockets = dictionnary;
                client.Send(url, "Show me your blockchain !");

                App.Current.Properties[ApplicationPropertiesConstants.PortUrlWebSockets] = dictionnary;

                Redirect(nameof(Home));
            }
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            Redirect(nameof(Home));
        }
    }
}