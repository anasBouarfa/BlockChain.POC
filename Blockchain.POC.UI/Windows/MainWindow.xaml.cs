using Blockchain.POC.Common;
using System;
using System.Windows;
using System.Windows.Input;

namespace Blockchain.POC.UI
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : BaseWindow
    {
        public MainWindow() : base()
        {
            InitializeComponent();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (!Username.Text.IsNullOrWhitespace() &&
               !Password.Password.IsNullOrWhitespace())
            {
                _chain = _globalManager.LoadLocalBlockChain();

                string userHash = _globalManager.IsAccountLoginValid(_chain, Username.Text, Password.Password);

                if (!userHash.IsNullOrWhitespace())
                {
                    App.Current.Properties[ApplicationPropertiesConstants.UserAddress] = userHash;

                    ChangeWindow(true, userHash);
                }
                else //not authorized
                {
                    Error.Redirect(this, "Incorrect credentials", false);
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
            if (!isAuthorized)
            {
                Blockchain.POC.UI.SignIn.Redirect(this);
            }
            else
            {
                Home.Redirect(this);
            }
        }
    }
}