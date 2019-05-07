using Blockchain.POC.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using WebSocketSharp;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using Blockchain.POC.Common;

namespace Blockchain.POC.Client
{
    /// <summary>
    /// Interaction logic for AddNode.xaml
    /// </summary>
    public partial class AddNode : Window
    {
        private IGlobalManager _globalManager;

        public AddNode()
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
            if (!Port.Text.IsNullOrWhitespace())
            {
                P2PClient.Client client = new P2PClient.Client(_globalManager);

                string url = $"ws://localhost:{int.Parse(Port.Text)}/BlockchainPOC";

                var dictionnary = App.Current.Properties[ApplicationPropertiesConstants.PortUrlWebSockets] as Dictionary<string, WebSocket> ?? new Dictionary<string, WebSocket>();

                dictionnary.Add(url, client.Connect(url, _globalManager.LoadLocalBlockChain()));

                App.Current.Properties[ApplicationPropertiesConstants.PortUrlWebSockets] = dictionnary;

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
    }
}
