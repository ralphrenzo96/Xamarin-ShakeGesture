using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using cprapp.Helpers.ShakeService;
using cprapp.Helpers.TickingService;
using cprapp.Utilities;
using Xamarin.Forms;

namespace cprapp.View
{
    public partial class CPRPage : ContentPage
    {
        IShakeService shake = DependencyService.Get<IShakeService>();
        StopWatch stopWatch = new StopWatch();
        StopWatch idleWatch = new StopWatch();
        TickingWatch tickingWatch = new TickingWatch();
        CustomTimer timer = new CustomTimer(1);
        CustomTimer idleTimer = new CustomTimer(5);

        double speed;
        bool isNotBusy = true;

        public CPRPage()
        {
            InitializeComponent();

			AnimateProgressBar();


            timer.Elapsed += CPRIdle;
            idleTimer.Elapsed += delayIdleTimer;
            stopWatch.Elapsed += UpdateTimer;
            tickingWatch.Elapsed += PlayTick;
            idleWatch.Elapsed += IdleTimer;

			// Starting Audio
            //DependencyService.Get<ITickingService>().PlayMP3(true);

            stopWatch.Start();
            tickingWatch.Start();
            AnimateHands();
            //StartWatch();
        }

        //public async void StartWatch()
        //{
        //    await stopWatch.Start();
        //    await tickingWatch.Start();
        //}

        private async void delayIdleTimer(object sender, EventArgs e)
        {
            idleTimer.Stop();
            await idleWatch.Start();
        }

		private void PlayTick(object sender, int e)
		{
			DependencyService.Get<ITickingService>().PlayMP3(true);
		}

        private void IdleTimer(object sender, int e)
        {
            TimeSpan result = TimeSpan.FromSeconds(e);
            labelIdle.Text = result.ToString("mm':'ss");

//#if DEBUG
//            Debug.WriteLine("[CPRPage.cs] Idle Time : " + e);
//#endif
        }

        private void UpdateTimer(object sender, int e)
        {
            TimeSpan result = TimeSpan.FromSeconds(e);
            labelWatch.Text = result.ToString("mm':'ss");
//#if DEBUG
//            Debug.WriteLine("[CPRPage.cs] Update Time : " + e);
//#endif
        }

        async void CPRIdle(object sender, EventArgs e)
        {
            timer.Stop();
            await progressBarSpeed.ProgressTo(0, 500, Easing.Linear);
            speed = 0;
            progressBarSpeed.SpeedResetUpdate.Invoke(this, true);
            await idleTimer.Start();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            shake.speedObtained += OnSpeedChanged;
			shake.GetSpeed();
		}

        async void OnSpeedChanged(object sender, IShakeServiceEventArgs e)
        {
            if (isNotBusy)
            {
                isNotBusy = false;
                timer.Stop();
                if (e.speed > speed)
                {
                    speed = e.speed;

                    int lvl = 0;
                    if (speed <= 2000)
                    {
                        lvl = 1;
                        //DependencyService.Get<ITickingService>().PlayMP3(3);
                    }
                    else
                    {
                        lvl = 2;
                        //DependencyService.Get<ITickingService>().PlayMP3(4);
                    }

                    progressBarSpeed.CurrentSpeedUpdate.Invoke(this, lvl);
                    labelDisplay.Text = (Math.Round((speed / 1000), 2)).ToString();
                    await progressBarSpeed.ProgressTo((speed / 3000), 500, Easing.Linear);
                }

                idleWatch.Reset();
                labelIdle.Text = "00:00";
                await timer.Start();
                isNotBusy = true;
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            shake.speedObtained -= OnSpeedChanged;
            shake.Stop();
        }

        protected override bool OnBackButtonPressed()
        {
			Processes_Disable();
            return base.OnBackButtonPressed();
        }

        public void QuitButton_Clicked(Object sender, EventArgs e)
        {
            Processes_Disable();
            Navigation.PopAsync();
        }

        private void Processes_Disable()
        {
#if DEBUG
			Debug.WriteLine("[CPRPage.cs] Processes Disabled");
#endif
            timer.Stop();
			timer.Elapsed -= CPRIdle;
            idleTimer.Stop();
			idleTimer.Elapsed -= delayIdleTimer;
            stopWatch.Stop();
			stopWatch.Elapsed -= UpdateTimer;
            tickingWatch.Stop();
			tickingWatch.Elapsed -= UpdateTimer;
            idleWatch.Stop();
			idleWatch.Elapsed -= IdleTimer;

            DependencyService.Get<ITickingService>().PlayMP3(false);
        }

        public async void AnimateProgressBar()
        {
            while(true)
            {
                progressBarSpeed.Opacity = 0;
                await progressBarSpeed.FadeTo(1, 500);
                await progressBarSpeed.FadeTo(0, 1000);
            }
        }

		public async void AnimateHands()
		{
			while (true)
			{
                await imageHands.ScaleTo(1.5, 250);
                await imageHands.ScaleTo(1, 250);
			}
		}
    }
}
