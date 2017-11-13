using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace cprapp.View
{
    public partial class MainPage : ContentPage
    {
        private bool choice;

        public MainPage()
        {
            InitializeComponent();
        }

        public async void OnClicked_Navigate(Object sender, EventArgs e)
        {
            Button button = (Button)sender;

            switch(Convert.ToInt32(button.CommandParameter.ToString()))
            {
				case 1: choice = false; break;
                case 2: choice = false; break;
                case 3: choice = true; break;
            }
            await Navigation.PushAsync(new CPRPage(choice));
        }
    }
}
