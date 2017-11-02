using System;
using Android.Media;
using cprapp.Droid.Helpers.AudioService;
using cprapp.Helpers.AudioService;
using Xamarin.Forms;

[assembly: Dependency(typeof(AudioService))]
namespace cprapp.Droid.Helpers.AudioService
{
    public class AudioService : IAudioService
    {
		private MediaPlayer _mediaPlayer = MediaPlayer.Create(Android.App.Application.Context, Resource.Raw.tick);

		public void PlayMP3(int file)
		{
            switch (file)
			{
                case 1:
                    _mediaPlayer = MediaPlayer.Create(Android.App.Application.Context, Resource.Raw.start);
                    break;
                case 2:
                    _mediaPlayer = MediaPlayer.Create(Android.App.Application.Context, Resource.Raw.harder);
                    break;
				case 3:
                    _mediaPlayer = MediaPlayer.Create(Android.App.Application.Context, Resource.Raw.faster);
					break;
				case 4:
                    _mediaPlayer = MediaPlayer.Create(Android.App.Application.Context, Resource.Raw.good);
					break;
				case 5:
					_mediaPlayer = MediaPlayer.Create(Android.App.Application.Context, Resource.Raw.end);
					break;
				case 6:
                    _mediaPlayer = MediaPlayer.Create(Android.App.Application.Context, Resource.Raw.count);
					break;
				case 7:
                    _mediaPlayer = MediaPlayer.Create(Android.App.Application.Context, Resource.Raw.alive);
					break;
				case 8:
                    _mediaPlayer = MediaPlayer.Create(Android.App.Application.Context, Resource.Raw.dead);
					break;
			}


            if (file != 0)
                _mediaPlayer.Start();
		}
    }
}
