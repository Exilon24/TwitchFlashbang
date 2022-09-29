using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace TwitchFlashbang
{
    /// <summary>
    /// Interaction logic for overlay.xaml
    /// </summary>
    public partial class overlay : Window
    {
        public static overlay? thisOverlay;
        public overlay()
        {
            InitializeComponent();
            this.Loaded += Overlay_Loaded;
        }

        private void Overlay_Loaded(object sender, RoutedEventArgs e)
        {
            thisOverlay = this;
        }

        public void showOverlay()
        { 
            this.Show();
        }
    }
}
