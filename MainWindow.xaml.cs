using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace TwitchFlashbang
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string? provider;
        Window behaviourLayer = new Window();
        CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
        CancellationToken token;
        Overlay? gameOverlay;
        public static TextBlock queueFlashText;

        // User settings
        public string APIToken;



        public MainWindow()
        {
            InitializeComponent();
            behaviourLayer.WindowState = WindowState.Maximized;
            behaviourLayer.WindowStyle = WindowStyle.None;
            behaviourLayer.AllowsTransparency = true;
            behaviourLayer.Topmost = true;
            behaviourLayer.Background = null;
            queueFlashText = queuedFlashbangs;
            behaviourLayer.Closed += BehaviourLayer_Closed;

            APIToken = SocketToken.Password;

            token = cancelTokenSource.Token;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            gameOverlay = new Overlay();
            if (donoProviders.SelectedItem != null)
            { 
                provider = donoProviders.SelectedItem.ToString();
                Task.Run(() => gameOverlay.Run(), token);
                behaviourLayer.Show();

            }
            else
            {
                MessageBox.Show("Please select a donation handler.", "ERROR");
            }
           
        }

        private void BehaviourLayer_Closed(object? sender, EventArgs e)
        {
            cancelTokenSource.Cancel();
            cancelTokenSource.Dispose();
        }

        private void TestButton_Click(object sender, RoutedEventArgs e)
        {
            if (gameOverlay != null)
            {
                gameOverlay.CSGOflash();
            }
        }
    }
}
