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
            BaseWindow baseWindow = new BaseWindow
            {
                Left = this.Left,
                Top = this.Top
            };

            if (IsAuthorized)
            {
                baseWindow.Redirect(nameof(Home));
            }
            else
            {
                baseWindow.Redirect(nameof(MainWindow));
            }
        }

        public static void View(Window window, string message, bool isAuthorized)
        {
            new Error(message, isAuthorized)
            {
                Top = window.Top,
                Left = window.Left
            }.Show();

            System.Threading.Thread.Sleep(200);

            window.Close();
        }
    }
}