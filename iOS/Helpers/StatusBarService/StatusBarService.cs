using System;
using cprapp.iOS.Helpers.StatusBarService;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(StatusBarService))]
namespace cprapp.iOS.Helpers.StatusBarService
{
    public class StatusBarService
    {
		#region IStatusBar implementation

		public void HideStatusBar()
		{
			UIApplication.SharedApplication.StatusBarHidden = true;
		}

		public void ShowStatusBar()
		{
			UIApplication.SharedApplication.StatusBarHidden = false;
		}

		#endregion
	}
}
