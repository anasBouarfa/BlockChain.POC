using Blockchain.POC.Common;
using Blockchain.POC.Entities;
using Blockchain.POC.Manager;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace Blockchain.POC.UI
{
    public class BaseWindow : Window
    {
        protected IGlobalManager _globalManager;
        protected BlockChain _chain;
        protected int _port;
        protected string _message;

        public BaseWindow() : base()
        {
            _port = (App.Current.Properties[ApplicationPropertiesConstants.Port] as int?).Value;
            _globalManager = new GlobalManager(_port);
            _chain = _globalManager.LoadLocalBlockChain();
            _message = string.Empty;
            Title = $"MyWallet - {_port}";
        }

        public void Redirect(string toWindow)
        {
            var window = (Window)Activator.CreateInstance(TypeProvider.DefinedTypes.FirstOrDefault(t => t.Name == toWindow));

            window.Left = this.Left;
            window.Top = this.Top;

            window.Show();

            System.Threading.Thread.Sleep(200);

            this.Close();
        }
    }
}