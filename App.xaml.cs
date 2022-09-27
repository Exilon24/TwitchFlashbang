using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using PInvoke;

namespace TwitchFlashbang
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected unsafe override void OnStartup(StartupEventArgs e)
        {
            const long WS_EX_COMPOSITED = 0x02000000L;
            const long WS_EX_TRANSPARENT = 0x00000020L;

            MainWindow window = new();
            window.Show();
            IntPtr windowHandle = new WindowInteropHelper(window).Handle;

            nint currentStyles = User32.GetWindowLongPtr_IntPtr(windowHandle, User32.WindowLongIndexFlags.GWL_EXSTYLE);
            if (currentStyles == 0) throw new Win32Exception();

            nint result = (nint)User32.SetWindowLongPtr(windowHandle, User32.WindowLongIndexFlags.GWL_EXSTYLE, (void*)(currentStyles | WS_EX_COMPOSITED | WS_EX_TRANSPARENT));
            if (result == 0) throw new Win32Exception();

            base.OnStartup(e);
        }

    }
}
