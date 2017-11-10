using System;
using AVFoundation;
using cprapp.Helpers.TickingService;
using cprapp.iOS.Helpers.TickingService;
using Foundation;
using Xamarin.Forms;

[assembly: Dependency(typeof(TickingService))]
namespace cprapp.iOS.Helpers.TickingService
{
    public class TickingService : ITickingService
    {
        AVAudioPlayer soundEffect;
		NSUrl songURL;
        NSError err;

		public void PlayMP3(bool isUsing)
        {
			if (isUsing)
			{
                songURL = new NSUrl("raw/tick.mp3");
                soundEffect = new AVAudioPlayer(songURL, "mp3", out err);
                soundEffect.Play();
			}
			else
				soundEffect.Stop();
        }
    }
}
