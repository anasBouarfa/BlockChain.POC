using System.Windows;

namespace Blockchain.POC.UI
{
    /// <summary>
    /// Interaction logic for Error.xaml
    /// </summary>
    public partial class Error : Window
    {
        public string Message { get; set; }

        public bool IsAuthorized { get; set; }

        public Error()
        {
            InitializeComponent();
        }

        public Error(string message, bool isAuthorized)
        {
            InitializeComponent();
            IsAuthorized = isAuthorized;
            DataContext = new { message };
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            if(IsAuthorized)
            {
                new Home
                {
                    Left = this.Left,
                    Top = this.Top
                }.Show();
            }
            else
            {
                new MainWindow
                {
                    Left = this.Left,
                    Top = this.Top
                }.Show();
            }

            System.Threading.Thread.Sleep(150);

            this.Close();
        }
    }
}