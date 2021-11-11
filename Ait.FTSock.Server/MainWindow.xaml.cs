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
        IPEndPoint serverEndpoint;
        Socket serverSocket;
        bool serverOnline = false;
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

        private void BtnStartServer_Click(object sender, RoutedEventArgs e)
        {
            

        }

        private void BtnStopServer_Click(object sender, RoutedEventArgs e)
        {
            
        }
        private void StartListening()
        {
            IPAddress ip = IPAddress.Parse(cmbIPs.SelectedItem.ToString());
            int port = int.Parse(cmbPorts.SelectedItem.ToString());
            serverEndpoint = new IPEndPoint(ip, port);
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //try
            //{
            //    serverSocket.Bind(serverEndpoint);
            //    serverSocket.Listen(int.MaxValue);
            //    while (serverOnline)
            //    {
            //        DoEvents();
            //        if (serverSocket != null)
            //        {
            //            if (serverSocket.Poll(200000, SelectMode.SelectRead))
            //            {
            //                HandleClientCall(serverSocket.Accept());
            //            }
            //        }
            //    }
            //}
            //catch (Exception error)
            //{
            //    MessageBox.Show(error.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            //}
        }
        //private void HandleClientCall(Socket clientCall)
        //{
        //    byte[] clientRequest = new Byte[1024];
        //    string instruction = null;

        //    while (true)
        //    {
        //        int numByte = clientCall.Receive(clientRequest);
        //        instruction += Encoding.ASCII.GetString(clientRequest, 0, numByte);
        //        if (instruction.IndexOf("##EOM") > -1)
        //            break;
        //    }
        //    string serverResponseInText = ProcessClientCall(instruction);
        //    if (serverResponseInText != "")
        //    {
        //        byte[] serverResponse = Encoding.ASCII.GetBytes(serverResponseInText);
        //        clientCall.Send(serverResponse);
        //    }
        //    clientCall.Shutdown(SocketShutdown.Both);
        //    clientCall.Close();
        //}
        
        //private static void DoEvents()
        //{
        //    try
        //    {
        //        System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(delegate { }));
        //    }
        //    catch (Exception fout)
        //    {
        //        System.Windows.Application.Current.Dispatcher.DisableProcessing();
        //    }
        //}
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
            btnStartServer.Visibility = Visibility.Visible;
            btnStopServer.Visibility = Visibility.Hidden;
        }
        private void SaveConfig()
        {
            if (cmbIPs.SelectedItem == null) return;
            if (cmbPorts.SelectedItem == null) return;

            string ip = cmbIPs.SelectedItem.ToString();
            int port = int.Parse(cmbPorts.SelectedItem.ToString());
            AppConfig.WriteConfig(ip, port);
        }

        
    }
}
