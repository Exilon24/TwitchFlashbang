using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using GameOverlay.Drawing;
using GameOverlay.Windows;

namespace TwitchFlashbang
{
	public class Overlay : IDisposable
	{
		private readonly GraphicsWindow _window;

		public bool canFlash = true;
		int alpha = 0;

		public int queue = 0;

		FlashbangBackend flashBack = new FlashbangBackend();

		public Overlay()
		{
			// Should hopefully adapt to primary monitor resolution
			_window = new GraphicsWindow(0, 0, (int)SystemParameters.FullPrimaryScreenWidth * 2, (int)SystemParameters.FullPrimaryScreenHeight + 300, null) // +300 to cover that taskbar
			{
				FPS = 60,
				IsTopmost = true,
				IsVisible = true
			};
			_window.DrawGraphics += _window_DrawGraphics;
			_window.SetupGraphics += _window_SetupGraphics;
		}

		private void _window_SetupGraphics(object sender, SetupGraphicsEventArgs e)
		{

			if (e.RecreateResources) return;
		}

		private void _window_DrawGraphics(object sender, DrawGraphicsEventArgs e)
		{
			var gfx = e.Graphics;
			if (queue > 0 && canFlash)
			{
				queue--;
				_ = this.CSGOflash();
			}

			if (alpha < 1) { canFlash = true; }

            var theBrush = gfx.CreateSolidBrush(255, 255, 255, alpha);
            gfx.ClearScene(theBrush); // What an odd name
		}

		public void Run()
		{
			_window.Create();
			_window.Join();
		}

		~Overlay()
		{
			Dispose(false);
		}

		#region IDisposable Support
		private bool disposedValue;

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				_window.Dispose();

				disposedValue = true;
			}
		}

		public async Task CSGOflash()
		{
			if (canFlash)
			{
				canFlash = false;
                await foreach (int tweenAlpha in flashBack.FlashbangAlphaAsync(5000))
                {
					alpha = tweenAlpha;
					Trace.WriteLine(alpha);
                }
            }
			else
			{
				queue += 1;
			}
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		#endregion
	}
}

