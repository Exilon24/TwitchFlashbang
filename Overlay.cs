using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using NAudio.Wave;
using GameOverlay.Drawing;
using GameOverlay.Windows;

namespace TwitchFlashbang
{
    public class Overlay : IDisposable
	{
		private readonly GraphicsWindow _window;

		private readonly Dictionary<string, SolidBrush> _brushes;
		private readonly Dictionary<string, Font> _fonts;

		public bool flashbanged = false;
		public bool canFlash = true;
		int alpha = 255;

		public int queue = 0;

		bool hasSet = true;
		int blindFrames = 540;
		int fadeFrames = 540;
		int currentBlindFrames = 0;
		int currentFadeFrames = 0;

		public WaveOutEvent outputDevice;
		AudioFileReader audioFile;

		public Overlay()
		{
			_brushes = new Dictionary<string, SolidBrush>();
			_fonts = new Dictionary<string, Font>();

			outputDevice = new WaveOutEvent();
			outputDevice.Volume = 1f;

			audioFile = new AudioFileReader(@"C:\Users\Pasha\source\repos\TwitchFlashbang\FlashBangSound.mp3");
			outputDevice.Init(audioFile);

			// Should hopefully adapt to primary monitor resolution
			_window = new GraphicsWindow(0, 0, (int)SystemParameters.FullPrimaryScreenWidth, (int)SystemParameters.FullPrimaryScreenHeight + 300, null) // +300 to cover that taskbar
			{
				FPS = 60,
				IsTopmost = true,
				IsVisible = true
			};

			_window.DestroyGraphics += _window_DestroyGraphics;
			_window.DrawGraphics += _window_DrawGraphics;
			_window.SetupGraphics += _window_SetupGraphics;
		}

        private void _window_SetupGraphics(object sender, SetupGraphicsEventArgs e)
		{
			var gfx = e.Graphics;

			if (e.RecreateResources)
			{
				foreach (var pair in _brushes) pair.Value.Dispose();
			}

			_brushes["black"] = gfx.CreateSolidBrush(0, 0, 0);
			_brushes["white"] = gfx.CreateSolidBrush(255, 255, 255);
			_brushes["red"] = gfx.CreateSolidBrush(255, 0, 0);
			_brushes["green"] = gfx.CreateSolidBrush(0, 255, 0);
			_brushes["blue"] = gfx.CreateSolidBrush(0, 0, 255);
			_brushes["background"] = gfx.CreateSolidBrush(0x33, 0x36, 0x3F);
			_brushes["grid"] = gfx.CreateSolidBrush(255, 255, 255, 0.2f);
			_brushes["random"] = gfx.CreateSolidBrush(0, 0, 0);

			if (e.RecreateResources) return;

			_fonts["arial"] = gfx.CreateFont("Arial", 12);
			_fonts["consolas"] = gfx.CreateFont("Consolas", 14);
		}

		private void _window_DestroyGraphics(object sender, DestroyGraphicsEventArgs e)
		{
			foreach (var pair in _brushes) pair.Value.Dispose();
			foreach (var pair in _fonts) pair.Value.Dispose();
		}

		private void _window_DrawGraphics(object sender, DrawGraphicsEventArgs e)
		{
			var gfx = e.Graphics;
			var theBrush = gfx.CreateSolidBrush(255, 255, 255, 0);
			if (queue > 0 && !flashbanged)
			{
				flashbanged = true;
				queue--;
				outputDevice.Play();
				Trace.WriteLine("PlayedAudio");
			}

			if (flashbanged)
			{
				if (hasSet)
				{
					currentBlindFrames = blindFrames;
					currentFadeFrames = fadeFrames;
					hasSet = false;
					alpha = 255;
				}

				if (currentBlindFrames > 0)
				{
					currentBlindFrames = currentBlindFrames - 1;
				}
				else if (currentFadeFrames > 0)
				{
					currentFadeFrames = currentFadeFrames - 1;
					if (alpha > 0)
					{
						alpha = alpha - 1;
					}
				}

				if (alpha < 100 && currentBlindFrames == 0)
				{
					outputDevice.Pause();
					audioFile.Position = 0;
					flashbanged = false;
					hasSet = true;
				}


				theBrush = gfx.CreateSolidBrush(255, 255, 255, alpha);
			}


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

		public void CSGOflash()
		{
			if (!flashbanged)
			{
				outputDevice.Play();
				flashbanged = true;
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

