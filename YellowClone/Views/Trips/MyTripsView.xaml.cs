using System;
using System.Collections.Generic;

using Microsoft.Maui;
using Microsoft.Maui.Controls;
using YellowClone.Services;
using YellowClone.ViewModels;

namespace YellowClone.Views.Trips
{
    public partial class MyTripsView : ContentPage
    {
        public MyTripsView()
        {
            InitializeComponent();
            BindingContext = new MyTripsViewModel(Navigation, ApiService.Instance);
        }
    }
}
