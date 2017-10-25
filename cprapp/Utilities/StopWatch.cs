using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace cprapp.Utilities
{
    public class StopWatch
    {
        int seconds = 0;
		private bool _isRunning;
		public bool IsRunning
		{
			get { return _isRunning; }
			set { _isRunning = value; }
		}

		public event EventHandler<int> Elapsed;
		protected virtual void OnTimerElapsed()
		{
			if (Elapsed != null)
			{
                Elapsed(this, seconds);
			}
		}

        public StopWatch()
		{

		}

        public void Reset()
        {
            IsRunning = false;
            seconds = 0;
        }

		public async Task Start()
		{
			IsRunning = true;
			while (IsRunning)
			{
				if (seconds != 0)
				{
					OnTimerElapsed();
				}
				await Task.Delay(1000);

				seconds++;
#if DEBUG
				Debug.WriteLine("[StopWatch.cs] StopWatch Seconds : " + seconds);
#endif
			}
		}

		public void Stop()
		{
#if DEBUG
			Debug.WriteLine("[StopWatch.cs] StopWatch Stopped");
#endif
			IsRunning = false;
		}
    }
}
