using System;
using Android.App;
using Android.Views;
using cprapp.Droid.Helpers.StatusBarService;
using cprapp.Helpers.StatusBarService;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(StatusBarService))]
namespace cprapp.Droid.Helpers.StatusBarService
{
    public class StatusBarService : IStatusBarService
    {
		WindowManagerFlags _originalFlags;

		#region IStatusBar implementation

		public void HideStatusBar()
		{
			var activity = (Activity)Forms.Context;
			var attrs = activity.Window.Attributes;
			_originalFlags = attrs.Flags;
			attrs.Flags |= Android.Views.WindowManagerFlags.Fullscreen;
			activity.Window.Attributes = attrs;
		}

		public void ShowStatusBar()
		{
			var activity = (Activity)Forms.Context;
			var attrs = activity.Window.Attributes;
			attrs.Flags = _originalFlags;
			activity.Window.Attributes = attrs;
		}

		#endregion
	}
}
