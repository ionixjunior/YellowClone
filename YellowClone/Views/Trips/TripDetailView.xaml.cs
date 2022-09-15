using System;
using System.Collections.Generic;

using Microsoft.Maui;
using Microsoft.Maui.Controls;
using YellowClone.Models;
using YellowClone.ViewModels;

namespace YellowClone.Views.Trips
{
    public partial class TripDetailView : ContentPage
    {
        public TripDetailView(Trip trip)
        {
            InitializeComponent();
            BindingContext = new TripDetailViewModel(trip);
        }

        private async void OnGoBackClick(object sender, System.EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
