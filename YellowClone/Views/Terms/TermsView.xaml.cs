using System;
using System.Collections.Generic;

using Microsoft.Maui;
using Microsoft.Maui.Controls;
using YellowClone.ViewModels;

namespace YellowClone.Views.Terms
{
    public partial class TermsView : ContentPage
    {
        public TermsView()
        {
            InitializeComponent();
            BindingContext = new TermsViewModel();
        }
    }
}
