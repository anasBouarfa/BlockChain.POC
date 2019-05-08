using Blockchain.POC.Common;
using Blockchain.POC.Entities;
using Blockchain.POC.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

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

        protected void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

    }
}
