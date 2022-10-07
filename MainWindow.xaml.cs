﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
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
        

        public MainWindow()
        {
            InitializeComponent();
            behaviourLayer.WindowState = WindowState.Maximized;
            behaviourLayer.WindowStyle = WindowStyle.None;
            behaviourLayer.AllowsTransparency = true;
            behaviourLayer.Topmost = true;
            behaviourLayer.Background = null;
            behaviourLayer.Closed += BehaviourLayer_Closed;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var gameOverlay = new Overlay();
            if (donoProviders.SelectedItem != null)
            { 
                provider = donoProviders.SelectedItem.ToString();
                Task.Run(() => gameOverlay.Run());
                behaviourLayer.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Please select a donation handler.", "ERROR");
            }
            gameOverlay.CSGOflash();
        }

        private void BehaviourLayer_Closed(object? sender, EventArgs e)
        {

        }
    }
}
