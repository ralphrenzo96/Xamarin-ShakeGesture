using System;
using cprapp.View;
using Xamarin.Forms;

namespace cprapp.Controls
{
    public class CustomProgressBar : ProgressBar
    {
		public static readonly BindableProperty CurrentSpeedProperty = BindableProperty.Create("CurrentSpeedUpdate", typeof(EventHandler<int>), typeof(CPRPage), null);

		public EventHandler<int> CurrentSpeedUpdate
		{
			get { return (EventHandler<int>)GetValue(CurrentSpeedProperty); }
			set { SetValue(CurrentSpeedProperty, value); }
		}

        public static readonly BindableProperty SpeedResetProperty = BindableProperty.Create("SpeedResetUpdate", typeof(EventHandler<bool>), typeof(CPRPage), null);

        public EventHandler<bool> SpeedResetUpdate
		{
			get { return (EventHandler<bool>)GetValue(SpeedResetProperty); }
			set { SetValue(SpeedResetProperty, value); }
		}

    }
}
