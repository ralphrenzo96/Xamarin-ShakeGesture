using System;
using cprapp.Helpers.ShakeService;
using cprapp.iOS.Helpers.ShakeGesture;
using Xamarin.Forms;

[assembly: Dependency(typeof(GetShakeSpeed))]
namespace cprapp.iOS.Helpers.ShakeGesture
{
	public class ShakeSpeedEventArgs : EventArgs, IShakeServiceEventArgs
	{
		public double speed { get; set; }
	}

    public class GetShakeSpeed : IShakeService
    {
        public event EventHandler<IShakeServiceEventArgs> speedObtained;

        public void GetSpeed()
        {
            //throw new NotImplementedException();
        }

        public void Stop()
        {
            //throw new NotImplementedException();
        }
    }
}
