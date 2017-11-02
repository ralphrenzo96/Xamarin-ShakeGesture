using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace cprapp.View
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        public async void OnClicked_Navigate(Object sender, EventArgs e)
        {
            Button button = (Button)sender;

            switch(Convert.ToInt32(button.CommandParameter.ToString()))
            {
                case 1: await Navigation.PushAsync(new CPRPage()); break;
                case 2: await Navigation.PushAsync(new CPRPage()); break;
            }
        }
    }
}
