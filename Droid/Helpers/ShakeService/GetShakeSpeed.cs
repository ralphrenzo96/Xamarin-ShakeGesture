using System;
using Android.Content;
using Android.Hardware;
using Android.Runtime;
using cprapp.Droid.Helpers.ShakeService;
using cprapp.Helpers.ShakeService;
using Xamarin.Forms;

[assembly: Dependency(typeof(GetShakeSpeed))]
namespace cprapp.Droid.Helpers.ShakeService
{
    public class ShakeSpeedEventArgs : EventArgs, IShakeServiceEventArgs
    {
        public double speed { get; set; }
    }

    public class GetShakeSpeed : Java.Lang.Object, IShakeService, Android.Hardware.ISensorEventListener
    {
        ShakeSpeedEventArgs args = new ShakeSpeedEventArgs();

        public event EventHandler<IShakeServiceEventArgs> speedObtained;
        Android.Hardware.SensorManager sensorManager;
        Sensor sensor;

		bool hasUpdated = false;
		DateTime lastUpdate;
		double last_x = 0.0f;
		double last_y = 0.0f;
		double last_z = 0.0f;

		const int ShakeDetectionTimeLapse = 250;
		const double ShakeThreshold = 100;

		//public IntPtr Handle => throw new NotImplementedException();

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public void OnAccuracyChanged(Sensor sensor, [GeneratedEnum] SensorStatus accuracy)
        {
            //throw new NotImplementedException();
        }

		public void OnSensorChanged(SensorEvent e)
		{
			if (e.Sensor.Type == Android.Hardware.SensorType.Accelerometer)
			{
				double x = e.Values[0];
				double y = e.Values[1];
				double z = e.Values[2];

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
					if ((curTime - lastUpdate).TotalMilliseconds > ShakeDetectionTimeLapse)
					{
						double diffTime = (double)(curTime - lastUpdate).TotalMilliseconds;
						lastUpdate = curTime;
						double total = x + y + z - last_x - last_y - last_z;
						double getSpeed = Math.Abs(total) / diffTime * 10000;

						if (getSpeed > ShakeThreshold)
						{
                            
                            args.speed = getSpeed;
                            speedObtained(this, args);

                            System.Diagnostics.Debug.WriteLine("[GetShakeSpeed.Android] Speed : " + getSpeed);
						}

						last_x = x;
						last_y = y;
						last_z = z;
					}
				}
			}
		}

        public void GetSpeed()
        {
			// Register this as a listener with the underlying service.
			sensorManager = (Android.Hardware.SensorManager)Forms.Context.GetSystemService(Context.SensorService);
			sensor = sensorManager.GetDefaultSensor(Android.Hardware.SensorType.Accelerometer);
			sensorManager.RegisterListener(this, sensor, Android.Hardware.SensorDelay.Game);
        }

        public void Stop()
        {
            sensorManager.UnregisterListener(this);
        }
    }
}
