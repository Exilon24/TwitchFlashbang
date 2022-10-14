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
        CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
        CancellationToken token;
        public static Overlay? gameOverlay;
        TextBlock queueFlashText;

        // User settings
        public string APIToken;

        public MainWindow()
        {
            InitializeComponent();
            SocketAPIHandler.startConnection();
            queueFlashText = queuedFlashbangs;
            Closed += BehaviourLayer_Closed;


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
                _ = RefreshQueueTextAsync();
                ReadyButton.IsEnabled = false;
                SocketToken.IsEnabled = false;
                donoProviders.IsEnabled = false;
                TwitchEvents.IsEnabled = false;

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
                this.Dispatcher.Invoke(() =>
                {
                    gameOverlay.CSGOflash();
                });
            }
        }

        async Task RefreshQueueTextAsync()
        {
            while (true)
            {
                queueFlashText.Text = "Queued flashbangs: " + gameOverlay.queue;
                await Task.Delay(1000);
            }
        }
    }
}

