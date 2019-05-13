using Blockchain.POC.Common;
using System.Windows;

namespace Blockchain.POC.Client
{
    /// <summary>
    /// Interaction logic for MyBalance.xaml
    /// </summary>
    public partial class MyBalance : BaseWindow
    {
        public string balance { get; set; }

        public MyBalance()
        {
            balance = _globalManager.GetAccountBalance(_chain, App.Current.Properties[ApplicationPropertiesConstants.UserAddress] as string).ToString();
            InitializeComponent();
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            new Home()
            {
                Left = this.Left,
                Top = this.Top
            }.Show();

            System.Threading.Thread.Sleep(250);

            this.Close();
        }
    }
}