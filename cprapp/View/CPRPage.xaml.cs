using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using cprapp.Helpers.AudioService;
using cprapp.Helpers.ShakeService;
using cprapp.Helpers.StatusBarService;
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
		StopWatch prepareTimer = new StopWatch();
        TickingWatch tickingWatch = new TickingWatch();
        CustomTimer timer = new CustomTimer(1);
        CustomTimer idleTimer = new CustomTimer(5);

        double speed = 0;
        bool isNotBusy = true, isActive = true, isPractice;
        bool isReady;
        int goodDepths = 0, scoreBad = 0, scoreGood = 0;
        int prepareTime = 5;
        int endTime = 30;

        public CPRPage(bool choosen)
        {
            InitializeComponent();
            isPractice = choosen;

			if (isPractice)
				stackTimeLeft.IsVisible = false;
        }

        private void ButtonStart_Clicked(object sender, EventArgs e)
        {
            ((Button)sender).Opacity = 0;
            ((Button)sender).IsEnabled = false;
			prepareTimer.Elapsed += PrepareUser;
            prepareTimer.Start();
            DependencyService.Get<IAudioService>().PlayMP3(6);
        }


        private void PrepareUser(object sender, int e)
        {
            labelPrepartion.Text = (prepareTime - e).ToString();
            if(e == 5)
            {
				prepareTimer.Stop();
                stackPreparation.IsVisible = false;
				Setup();
                isReady = true;
            }
        }

        private void Setup()
        {
			timer.Elapsed += CPRIdle;
			idleTimer.Elapsed += DelayIdleTimer;
			stopWatch.Elapsed += UpdateTimer;
			tickingWatch.Elapsed += PlayTick;
			idleWatch.Elapsed += IdleTimer;

			DependencyService.Get<IAudioService>().PlayMP3(1);

			stopWatch.Start();
			tickingWatch.Start();

			AnimateProgressBar();
			AnimateHands();
        }

        private void OnActive(bool isUserActive)
        {
            isActive = isUserActive;
        }

        private async void DelayIdleTimer(object sender, EventArgs e)
        {
            idleTimer.Stop();
            speed = 0;

            idleWatch.Start();
			OnActive(false);

			await progressBarSpeed.ProgressTo(0, 500, Easing.Linear);
			
		}

		private void PlayTick(object sender, int e)
		{
            DependencyService.Get<ITickingService>().PlayMP3(isActive);
		}

        private void IdleTimer(object sender, int e)
        {
            progressBarSpeed.SpeedResetUpdate.Invoke(this, true);
            TimeSpan result = TimeSpan.FromSeconds(e);
            labelIdle.Text = result.ToString("mm':'ss");
		}

        private void UpdateTimer(object sender, int e)
        {
            TimeSpan result = TimeSpan.FromSeconds(e);
            labelWatch.Text = result.ToString("mm':'ss");

            if (!isPractice)
            {
                endTime--;
                labelTimeLeft.Text = endTime.ToString();
            }

            if (endTime == 0 & !isPractice)
            {
                Processes_Disable();
                Navigation.PushAsync(new ChallengeResultPage(((scoreGood >= scoreBad) && (scoreGood >= 5)) ? true : false), false);
            }

            if (endTime == 21 & !isPractice)
                DependencyService.Get<IAudioService>().PlayMP3(9);
            if (endTime == 10 & !isPractice)
                DependencyService.Get<IAudioService>().PlayMP3(10);
        }

        async void CPRIdle(object sender, EventArgs e)
        {
            timer.Stop();
            speed = 0;
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
            if(isReady)
            {
                OnActive(true);
                idleWatch.Reset();
                idleWatch.Stop();

                if (isNotBusy)
                {
                    isNotBusy = false;
                    idleTimer.Stop();
                    timer.Stop();
                    if (e.speed*3.3 > speed)
                    {
                        speed = e.speed*3.3;

                        int lvl = 0;
                        if (speed <= 2000)
                        {
                            scoreBad++;
                            lvl = 1;
                            labelRemark.Text = "Push Harder";
                            goodDepths = 0;
                        }
                        else
                        {
                            scoreGood++;
                            switch (goodDepths)
                            {
                                case 2:
                                    lvl = 3;
                                    labelRemark.Text = "Good Depth";
                                    break;
                                default:
                                    lvl = 2;
                                    labelRemark.Text = "Push Faster";
                                    goodDepths++;
                                    break;
                            }
                        }

                        labelScoreBad.Text = scoreBad.ToString();
                        labelScoreGood.Text = scoreGood.ToString();

                        progressBarSpeed.CurrentSpeedUpdate.Invoke(this, lvl);
                        labelDisplay.Text = (Math.Round((speed / 1000), 2)).ToString();
                        await progressBarSpeed.ProgressTo((speed / 3000), 500, Easing.Linear);
    					DependencyService.Get<IAudioService>().PlayMP3(++lvl);
                    }

                    labelIdle.Text = "00:00";
                    await timer.Start();
                    isNotBusy = true;
                }
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

        public async void QuitButton_Clicked(Object sender, EventArgs e)
        {
            OnActive(false);
            DependencyService.Get<IAudioService>().PlayMP3(5);
            if (await DisplayActionSheet("Are you sure you want to quit?", "No", "Yes") == "Yes")
            {
                //DependencyService.Get<IStatusBarService>().ShowStatusBar();
                Processes_Disable();
                await Navigation.PopAsync();
            }
            else
                OnActive(true);
        }

        private void Processes_Disable()
        {
#if DEBUG
			Debug.WriteLine("[CPRPage.cs] Processes Disabled");
#endif
            OnActive(false);
            DependencyService.Get<IAudioService>().StopMedia();
            prepareTimer.Stop();
            prepareTimer.Elapsed -= PrepareUser;
            timer.Stop();
			timer.Elapsed -= CPRIdle;
            idleTimer.Stop();
			idleTimer.Elapsed -= DelayIdleTimer;
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
            double scale;

            while (true)
            {
                if (!isActive)
                    scale = 1;
                else
                    scale = 1.5;
                
                await imageHands.ScaleTo(scale, 250);
                await imageHands.ScaleTo(1, 250);
                
            }
		}
    }
}
