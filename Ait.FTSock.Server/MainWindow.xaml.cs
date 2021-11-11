using Ait.FTSock.Server.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Ait.FTSock.Server
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
      
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            StartupConfig();
        }
        private void CmbIPs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SaveConfig();
        }
        private void CmbPorts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SaveConfig();
        }

     

        private void BtnStopServer_Click(object sender, RoutedEventArgs e)
        {
            
        }
        
        private void StartupConfig()
        {
            cmbIPs.ItemsSource = IPv4Helper.GetActiveIP4s();
            for (int port = 49200; port <= 49500; port++)
            {
                cmbPorts.Items.Add(port);
            }
            AppConfig.GetConfig(out string savedIP, out int savedPort);
            try
            {
                cmbIPs.SelectedItem = savedIP;
            }
            catch
            {
                cmbIPs.SelectedItem = "127.0.0.1";
            }
            try
            {
                cmbPorts.SelectedItem = savedPort;
            }
            catch
            {
                cmbPorts.SelectedItem = 49200;
            }
            
        }
        private void SaveConfig()
        {
            if (cmbIPs.SelectedItem == null) return;
            if (cmbPorts.SelectedItem == null) return;

            string ip = cmbIPs.SelectedItem.ToString();
            int port = int.Parse(cmbPorts.SelectedItem.ToString());
            AppConfig.WriteConfig(ip, port);
        }

        private void txtBasePath_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btnPath_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
