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
                    _message = "Incorrect credentials";

                    new Error(_message, false)
                    {
                        Left = this.Left,
                        Top = this.Top
                    }.Show();

                    System.Threading.Thread.Sleep(200);

                    this.Close();
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
                new SignIn
                {
                    Left = this.Left,
                    Top = this.Top
                }.Show();
            }
            else
            {
                new Home
                {
                    Left = this.Left,
                    Top = this.Top
                }.Show();
            }

            System.Threading.Thread.Sleep(200);

            this.Close();
        }
    }
}