using System;
using System.Collections.Generic;

using Xamarin.Forms;
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
