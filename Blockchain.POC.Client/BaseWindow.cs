using Blockchain.POC.Common;
using Blockchain.POC.Entities;
using Blockchain.POC.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Blockchain.POC.Client
{
    public class BaseWindow : Window
    {
        protected IGlobalManager _globalManager;
        protected BlockChain _chain;

        public BaseWindow() : base()
        {
            _globalManager = new GlobalManager((App.Current.Properties[ApplicationPropertiesConstants.Port] as int?).Value);
            _chain = _globalManager.LoadLocalBlockChain();
        }

    }
}
