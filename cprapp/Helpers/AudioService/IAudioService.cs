using System;
namespace cprapp.Helpers.AudioService
{
    public interface IAudioService
    {
		void PlayMP3(int file);
        void StopMedia();
    }
}
