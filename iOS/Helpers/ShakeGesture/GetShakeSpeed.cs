using System;
using CoreMotion;
using cprapp.Helpers.ShakeService;
using cprapp.iOS.Helpers.ShakeGesture;
using Foundation;
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
		ShakeSpeedEventArgs args = new ShakeSpeedEventArgs();

		public event EventHandler<IShakeServiceEventArgs> speedObtained;
        CMMotionManager motionManager = new CMMotionManager();

		bool hasUpdated = false;
		DateTime lastUpdate;
		double last_x = 0.0f;
		double last_y = 0.0f;
		double last_z = 0.0f;

		const int ShakeDetectionTimeLapse = 250;
		const double ShakeThreshold = 100;

        public void GetSpeed()
        {
            motionManager.StartAccelerometerUpdates(NSOperationQueue.CurrentQueue, (data, error) =>
			{
                double x = data.Acceleration.X;
                double y = data.Acceleration.Y;
                double z = data.Acceleration.Z;

                DateTime curTime = System.DateTime.Now;

                if (hasUpdated == false)
                {
                    hasUpdated = true;
                    lastUpdate = curTime;

                    last_x = x;
                    last_y = y;
                    last_z = z;
                }
                else
                {
                    if((curTime - lastUpdate).TotalMilliseconds > ShakeDetectionTimeLapse)
                    {
                        double diffTime = (double)(curTime - lastUpdate).TotalMilliseconds;
                        lastUpdate = curTime;

                        double total = x + y + z - last_x - last_y - last_z;
                        double getSpeed = (Math.Abs(total) / diffTime * 10000)*4;

                        if(getSpeed > ShakeThreshold)
                        {
                            args.speed = getSpeed;
                            speedObtained(this, args);

                            System.Diagnostics.Debug.WriteLine("[GetShakeSpeed.iOS] Speed : " + getSpeed);
                        }

						last_x = x;
						last_y = y;
						last_z = z;
                    }
                }
			});

        }

        public void Stop()
        {
            motionManager.StopAccelerometerUpdates();
        }
    }
}
