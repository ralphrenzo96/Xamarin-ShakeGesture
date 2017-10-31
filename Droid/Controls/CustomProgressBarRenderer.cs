using System;
using cprapp.Controls;
using cprapp.Droid.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomProgressBar), typeof(CustomProgressBarRenderer))]
namespace cprapp.Droid.Controls
{
    public class CustomProgressBarRenderer : ProgressBarRenderer
    {
        CustomProgressBar progressBar;

        protected override void OnElementChanged(ElementChangedEventArgs<ProgressBar> e)
        {
            base.OnElementChanged(e);

			//Control.ProgressDrawable.SetColorFilter(Color.FromRgb(182, 231, 233).ToAndroid(), Android.Graphics.PorterDuff.Mode.SrcIn);
            //Control.ProgressTintList = Android.Content.Res.ColorStateList.ValueOf(Color.Pink.ToAndroid());
            Control.ScaleY = 10; // This changes the height

            if(e.NewElement != null)
            {
                DefaultTint();
                progressBar = (CustomProgressBar)e.NewElement;
                progressBar.SpeedResetUpdate += ResetTint;
                progressBar.CurrentSpeedUpdate += ChangeTint;
            }

		}

        private void ResetTint(object sender, bool e)
        {
            if(e)
                DefaultTint();
        }

        private void DefaultTint()
        {
			Control.ProgressDrawable.SetColorFilter(Color.SkyBlue.ToAndroid(), Android.Graphics.PorterDuff.Mode.SrcIn);
		}

        private void ChangeTint(object sender, int e)
        {
            switch(e)
            {
                case 1 :
                    Control.ProgressDrawable.SetColorFilter(Color.Red.ToAndroid(), Android.Graphics.PorterDuff.Mode.SrcIn);
                    break;
                case 2 :
                    Control.ProgressDrawable.SetColorFilter(Color.YellowGreen.ToAndroid(), Android.Graphics.PorterDuff.Mode.SrcIn);
                    break;
            }
		}
    }
}
