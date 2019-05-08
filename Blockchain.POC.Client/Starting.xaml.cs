﻿using Blockchain.POC.P2PServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            if(!Port.Text.IsNullOrWhitespace())
            {
                Server server = new Server();
                server.Start(int.Parse(Port.Text));

                App.Current.Properties["Port"] = int.Parse(Port.Text);

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
    }
}
