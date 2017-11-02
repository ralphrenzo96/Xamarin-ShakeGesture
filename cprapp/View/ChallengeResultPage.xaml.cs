using System;
using System.Collections.Generic;
using cprapp.Helpers.AudioService;
using Xamarin.Forms;

namespace cprapp.View
{
    public partial class ChallengeResultPage : ContentPage
    {
        void Handle_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PopToRootAsync();
        }

        public ChallengeResultPage(bool result)
        {
            InitializeComponent();

            if(result)
            {
                imageResult.Source = "alive.jpg";
                labelResult.Text = "Congratulations!";
                DependencyService.Get<IAudioService>().PlayMP3(7);
            }
            else
            {
                imageResult.Source = "dead.jpg";
                labelResult.Text = "Sad Life";
                DependencyService.Get<IAudioService>().PlayMP3(8);
            }
        }
    }
}
