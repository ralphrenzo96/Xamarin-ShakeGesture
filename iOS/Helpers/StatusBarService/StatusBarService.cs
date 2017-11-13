using System;
using cprapp.Helpers.StatusBarService;
using cprapp.iOS.Helpers.StatusBarService;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(StatusBarService))]
namespace cprapp.iOS.Helpers.StatusBarService
{
    public class StatusBarService : IStatusBarService
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
