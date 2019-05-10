using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Blockchain.POC.Client
{
    /// <summary>
    /// Interaction logic for Mine.xaml
    /// </summary>
    public partial class Mine : BaseWindow
    {
        public Mine() : base()
        {
            InitializeComponent();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            new Home()
            {
                Left = this.Left,
                Top = this.Top
            }.Show();

            System.Threading.Thread.Sleep(250);

            this.Close();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
