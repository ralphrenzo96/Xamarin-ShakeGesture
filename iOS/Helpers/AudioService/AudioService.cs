using System;
using AVFoundation;
using cprapp.Helpers.AudioService;
using cprapp.iOS.Helpers.AudioService;
using Foundation;
using Xamarin.Forms;

[assembly: Dependency(typeof(AudioService))]
namespace cprapp.iOS.Helpers.AudioService
{
    public class AudioService : IAudioService
    {
		AVAudioPlayer soundEffect;
		NSUrl songURL;
        NSError err;

        public void PlayMP3(int file)
        {
			switch (file)
			{
				case 1:
					songURL = new NSUrl("raw/start.mp3");
					break;
				case 2:
					songURL = new NSUrl("raw/harder.mp3");
					break;
				case 3:
					songURL = new NSUrl("raw/faster.mp3");
					break;
				case 4:
					songURL = new NSUrl("raw/good.mp3");
					break;
				case 5:
					songURL = new NSUrl("raw/end.mp3");
					break;
				case 6:
					songURL = new NSUrl("raw/count.mp3");
					break;
				case 7:
					songURL = new NSUrl("raw/alive.mp3");
					break;
				case 8:
					songURL = new NSUrl("raw/dead.mp3");
					break;
				case 9:
					songURL = new NSUrl("raw/twenty_seconds.mp3");
					break;
				case 10:
					songURL = new NSUrl("raw/countdown.mp3");
					break;

			}


            if (file != 0)
            {
                soundEffect = new AVAudioPlayer(songURL, "mp3", out err);
                soundEffect.Play();
            }
        }

        public void StopMedia()
        {
            soundEffect.Stop();
        }
    }
}
