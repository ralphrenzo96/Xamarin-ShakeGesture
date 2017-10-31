using System;
using Android.Media;
using cprapp.Droid.Helpers.TickingService;
using cprapp.Helpers.TickingService;
using Xamarin.Forms;

[assembly: Dependency(typeof(TickingService))]
namespace cprapp.Droid.Helpers.TickingService
{
    public class TickingService : ITickingService
    {
        private MediaPlayer _mediaPlayer;

        public bool PlayMP3(int file)
        {
            switch (file)
            {
                case 1 : _mediaPlayer = MediaPlayer.Create(global::Android.App.Application.Context, Resource.Raw.tick);
                    break;
            }
                
			_mediaPlayer.Start();

			return true;
        }
    }
}
