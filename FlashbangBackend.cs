using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Media;

public class FlashbangBackend
{
    MediaPlayer flashSound;

    public FlashbangBackend()
    {
        flashSound = new MediaPlayer();
        flashSound.Open(new Uri(@"FlashBangSound.mp3", UriKind.RelativeOrAbsolute));
    } 
    public void FlashbangAlpha(ref int alpha, float fadeTime)
    {
        alpha = 255;
        int fadeDelayPerFrame = (int) Math.Floor(fadeTime / 255);
        Task.Delay(8000);
        for (int i = alpha; i >= 0; i--)
        {
            alpha = i;
            Task.Delay(fadeDelayPerFrame); 
        }
    }

    public async IAsyncEnumerable<int> FlashbangAlphaAsync(float fadeTime)
    {
        flashSound.Position = TimeSpan.Zero;
        flashSound.Play();
        int alpha = 255;
        yield return alpha;
        await Task.Delay(8000);

        int fadeDelayPerFrame = (int)(fadeTime / 255);

        for (int i = 255; i >= 0; i--)
        {
            alpha = i;
            yield return alpha;
            await Task.Delay(fadeDelayPerFrame);
        }
    }
}

