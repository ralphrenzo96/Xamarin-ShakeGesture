using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using cprapp.Helpers.ShakeService;
using cprapp.Utilities;
using Xamarin.Forms;

namespace cprapp.View
{
    public partial class CPRPage : ContentPage
    {
        IShakeService shake = DependencyService.Get<IShakeService>();
        CustomTimer timer = new CustomTimer(2);
        double speed;
        public CPRPage()
        {
            InitializeComponent();
            timer.Elapsed += CPRIdle;
        }

        async void CPRIdle(object sender, EventArgs e)
        {
            await progressBarSpeed.ProgressTo(0, 500, Easing.Linear);
            speed = 0;
            timer.Stop();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            shake.speedObtained += OnSpeedChanged;
			shake.GetSpeed();

			
		}

        async void OnSpeedChanged(object sender, IShakeServiceEventArgs e)
        {
            if(speed < e.speed)
                speed = e.speed;
            labelDisplay.Text = speed.ToString() + "\n" + labelDisplay.Text;

            await progressBarSpeed.ProgressTo((speed/3000), 500, Easing.Linear);

            timer.Stop();
            await timer.Start();
        }



        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            shake.speedObtained -= OnSpeedChanged;
            shake.Stop();
        }



        public void GetSpeed_Clicked(Object s, EventArgs args)
        {
			//shake.speedObtained += (object sender, IShakeServiceEventArgs e) => speed = e.speed;
			//shake.GetSpeed();

			//labelDisplay.Text = speed.ToString();
        }
    }
}
