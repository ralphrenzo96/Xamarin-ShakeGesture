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
        private MediaPlayer _mediaPlayer = MediaPlayer.Create(Android.App.Application.Context, Resource.Raw.tick);

        public bool PlayMP3(bool isUsing)
        {
            if (isUsing)
            {
                System.Diagnostics.Debug.WriteLine("Start");
                _mediaPlayer.Start();
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Stop");
                _mediaPlayer.Stop();
            }

			return true;
        }
    }
}
