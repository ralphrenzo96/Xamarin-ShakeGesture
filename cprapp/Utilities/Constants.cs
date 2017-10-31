using System;
using Xamarin.Forms;

namespace cprapp.Utilities
{
    public class Constants
    {
        public static readonly Color THEME_COLOR = Color.FromHex("#e74c3c");
        public static Color THEME_COLOR_DARK = Color.FromHex("#2C3E50");
        public static double DEFAULT_TEMPLATE_HEIGHT = Device.RuntimePlatform == Device.Android ? 50 : 60;
    }
}
