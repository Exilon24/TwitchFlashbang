﻿using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;


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

        // UI SETTINGS
        TextBlock queueFlashText;
        public static string socketAPIToken;
        public static TextBlock socketConnectionStatus;
        public static bool? invokeOnSubscription;
        public static bool? invokeOnFollow;
        public static bool? invokeOnDonate;
        public static bool? waitForMessageEndBool;
        public static int minimumDonationAmount;
        public static string setTriggerCommand;
        public static bool? warnOnFlash;

        public MainWindow()
        {
            InitializeComponent();
            Closed += BehaviourLayer_Closed;

            // UI assigns
            queueFlashText = queuedFlashbangs;

            token = cancelTokenSource.Token;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            gameOverlay = new Overlay();
            if (donoProviders.SelectedItem != null && SocketToken.Password != "")
            {
                provider = donoProviders.SelectedItem.ToString();
                Task.Run(() => gameOverlay.Run(), token);
                _ = RefreshQueueTextAsync();
                ReadyButton.IsEnabled = false;
                SocketToken.IsEnabled = false;
                donoProviders.IsEnabled = false;
                TwitchEvents.IsEnabled = false;
                minDonationStackPanel.IsEnabled = false;
                waitForMessageEnd.IsEnabled = false;

                warnOnFlash = WarnMe.IsChecked;
                invokeOnDonate = onDonation.IsChecked;
                invokeOnFollow = onFollow.IsChecked;
                invokeOnSubscription = onSubscription.IsChecked;
                socketAPIToken = SocketToken.Password;
                waitForMessageEndBool = waitForMessageEnd.IsChecked;

                if (triggerCommand.Text == "")
                {
                    triggerCommand.Text = "None";
                }
                if (minDonationAmount.Text == "")
                {
                    minDonationAmount.Text = "0";
                }

                setTriggerCommand = triggerCommand.Text;
                minimumDonationAmount = Int32.Parse(minDonationAmount.Text);

                Trace.WriteLine(setTriggerCommand);

                SocketAPIHandler.startConnection();

            }
            else
            {
                MessageBox.Show("Please select a donation handler and enter your SocketAPI token.", "ERROR");
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

