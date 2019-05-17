using Blockchain.POC.Common;
using System.Collections.Generic;
using System.Windows;
using WebSocketSharp;

namespace Blockchain.POC.UI
{
    /// <summary>
    /// Interaction logic for Mine.xaml
    /// </summary>
    public partial class Mine : BaseWindow
    {
        public Mine() : base()
        {
            _chain = _globalManager.LoadLocalBlockChain();
            InitializeComponent();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            new Home()
            {
                Left = this.Left,
                Top = this.Top
            }.Show();

            System.Threading.Thread.Sleep(150);

            this.Close();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            _chain = _globalManager.AddBlock(_chain);
            _globalManager.SaveBlockChain(_chain);

            P2PClient.Client client = new P2PClient.Client(_globalManager)
            {
                urlwebSockets = App.Current.Properties[ApplicationPropertiesConstants.PortUrlWebSockets] as Dictionary<string, WebSocket>
            };
            client.BroadcastChain(_chain);
            _globalManager.SaveBlockChain(_chain);

            new Home()
            {
                Left = this.Left,
                Top = this.Top
            }.Show();

            System.Threading.Thread.Sleep(150);

            this.Close();
        }
    }
}