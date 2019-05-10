using Blockchain.POC.Common;
using Blockchain.POC.P2PServer;
using System;
using System.Windows;
using System.Windows.Input;

namespace Blockchain.POC.Client
{
    /// <summary>
    /// Interaction logic for Starting.xaml
    /// </summary>
    public partial class Starting : Window
    {
        public Starting()
        {
            InitializeComponent();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (!Port.Text.IsNullOrWhitespace())
            {
                Server server = new Server();
                server.Start(int.Parse(Port.Text));

                App.Current.Properties[ApplicationPropertiesConstants.Port] = int.Parse(Port.Text);

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

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            e.Handled = e.Text.IsNumeric();
        }
    }
}