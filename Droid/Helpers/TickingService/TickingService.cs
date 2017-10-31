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
        private MediaPlayer _mediaPlayer = new MediaPlayer();
        string outputFile;

        public TickingService()
        {
            //_mediaPlayer.SetAudioStreamType(Stream.);

        }

        public void PlayMP3(bool isUsing)
        {
            if (isUsing)
            {
                _mediaPlayer.Reset();
                _mediaPlayer = MediaPlayer.Create(Android.App.Application.Context, Resource.Raw.tick);
                _mediaPlayer.Start();
            }
            else
                _mediaPlayer.Stop();


        }
    }
}
