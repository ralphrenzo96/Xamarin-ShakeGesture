using System;
using CoreGraphics;
using cprapp.Controls;
using cprapp.iOS.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomProgressBar), typeof(CustomProgressBarRenderer))]
namespace cprapp.iOS.Controls
{
    public class CustomProgressBarRenderer : ProgressBarRenderer
    {
        CustomProgressBar progressBar;

        protected override void OnElementChanged(ElementChangedEventArgs<ProgressBar> e)
        {
            base.OnElementChanged(e);
			if (e.NewElement != null)
			{
				DefaultTint();
				progressBar = (CustomProgressBar)e.NewElement;
                //Control.TrackTintColor = Color.FromHex("E3E3E3").ToUIColor();

				progressBar.SpeedResetUpdate += ResetTint;
				progressBar.CurrentSpeedUpdate += ChangeTint;
			}

            if(e.OldElement != null)
            {
				progressBar.SpeedResetUpdate -= ResetTint;
				progressBar.CurrentSpeedUpdate -= ChangeTint;
            }
        }

        public override CGSize SizeThatFits(CGSize size)
        {
            return base.SizeThatFits(size);
        }

		public override void LayoutSubviews()
		{
			base.LayoutSubviews();

			var X = 1.0f;
			var Y = 2.0f; //This changes the height

			CGAffineTransform transform = CGAffineTransform.MakeScale(X, Y);
            this.Control.Transform = transform;
		}

		private void ResetTint(object sender, bool e)
		{
			if (e)
				DefaultTint();
		}

		private void DefaultTint()
		{
            Control.ProgressTintColor = Color.SkyBlue.ToUIColor();
		}

		private void ChangeTint(object sender, int e)
		{
			switch (e)
			{
				case 1:
                    Control.ProgressTintColor = Color.Red.ToUIColor(); 
                    break;
				case 2:
				case 3:
                    Control.ProgressTintColor = Color.YellowGreen.ToUIColor(); 
                    break;
			}
		}
    }
}
