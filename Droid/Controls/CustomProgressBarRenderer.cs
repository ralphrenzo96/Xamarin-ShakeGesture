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
        protected override void OnElementChanged(ElementChangedEventArgs<ProgressBar> e)
        {
            base.OnElementChanged(e);

			//Control.ProgressDrawable.SetColorFilter(Color.FromRgb(182, 231, 233).ToAndroid(), Android.Graphics.PorterDuff.Mode.SrcIn);
            //Control.ProgressTintList = Android.Content.Res.ColorStateList.ValueOf(Color.Pink.ToAndroid());
			Control.ScaleY = 10; // This changes the height
		}


    }
}
