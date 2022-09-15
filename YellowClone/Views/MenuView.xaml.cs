using Microsoft.Maui;
using Microsoft.Maui.Controls;
using YellowClone.Services;
using YellowClone.ViewModels;

namespace YellowClone.Views
{
    public partial class MenuView : ContentPage
    {
        public MenuView()
        {
            InitializeComponent();
            BindingContext = new MenuViewModel(Navigation, ApiService.Instance);
        }
    }
}
