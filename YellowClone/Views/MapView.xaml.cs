using System;
using System.Linq;

using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
//using Xamarin.Forms.Maps;
using YellowClone.Controls;

namespace YellowClone.Views
{
    public partial class MapView : ContentPage
    {
        private const string MapElement = "map";
        private const string PinNameElement = "pinName";
        private const string PinDescriptionElement = "pinDescription";
        private const string PinContentElement = "pinContent";

        private FlyoutPage _masterDetail;

        public MapView()
        {
            InitializeComponent();
            ConfigureMap();
        }

        private void ConfigureMap()
        {
            MoveToCurrentPosition();
            AddPins();
            SubscribeEvents();
        }

        private void MoveToCurrentPosition()
        {
            if (GetElementByName<CustomMap>(MapElement) is CustomMap map)
            {
                map.MoveToRegion(
                    MapSpan.FromCenterAndRadius(
                        new Location(-23.561453, -46.656300),
                        Distance.FromMeters(200)
                    )
                );
            }
        }

        private void AddPins()
        {
            if (GetElementByName<CustomMap>(MapElement) is CustomMap map)
            {
                var pin1 = new CustomPin
                {
                    Identifier = 1,
                    Type = PinType.Generic,
                    Location = new Location(-23.559949, -46.656182),
                    Label = "Bike",
                    Information = "R$ 1,00 a cada 15 min"
                };
                pin1.MarkerClicked += OnMarkerClicked;
                map.AddPin(pin1);

                var pin2 = new CustomPin
                {
                    Identifier = 2,
                    Type = PinType.Generic,
                    Location = new Location(-23.563066, -46.656933),
                    Label = "Bike",
                    Information = "R$ 1,30 a cada 15 min"
                };
                pin2.MarkerClicked += OnMarkerClicked;
                map.AddPin(pin2);
            }
        }

        private void OnMarkerClicked(object sender, PinClickedEventArgs e)
        {
            if (sender is CustomPin pin)
            {
                if (GetElementByName<CustomMap>(MapElement) is CustomMap map)
                {
                    map.MoveToRegion(
                        MapSpan.FromCenterAndRadius(
                            pin.Location,
                            Distance.FromMeters(200)
                        )
                    );
                }

                if (GetElementByName<Label>(PinNameElement) is Label pinName)
                    pinName.Text = pin.Label;

                if (GetElementByName<Label>(PinDescriptionElement) is Label pinDescription)
                    pinDescription.Text = pin.Information;

                if (GetElementByName<Frame>(PinContentElement) is Frame pinContent)
                    pinContent.IsVisible = true;
            }
        }

        private void SubscribeEvents()
        {
            if (GetElementByName<CustomMap>(MapElement) is CustomMap map)
            {
                map.PinSelected += OnPinSelected;
                map.MapMoved += OnMapMoved;
            }
        }

        private void OnPinSelected(object sender, CustomPin pin)
        {
            //if (GetElementByName<Label>(PinNameElement) is Label pinName)
            //    pinName.Text = pin.Label;

            if (GetElementByName<Label>(PinDescriptionElement) is Label pinDescription)
                pinDescription.Text = pin.Information;

            if (GetElementByName<Frame>(PinContentElement) is Frame pinContent)
                pinContent.IsVisible = true;
        }

        private void OnMapMoved(object sender, EventArgs e)
        {
            if (GetElementByName<Frame>(PinContentElement) is Frame pinContent)
                pinContent.IsVisible = false;
        }

        private void OnMenuClick(object sender, EventArgs e)
        {
            FindMasterDetail();
            ShowMenu();
        }

        private void FindMasterDetail()
        {
            if (_masterDetail == null)
            {
                var mainPage = Application.Current.MainPage as NavigationPage;
                _masterDetail = mainPage.Navigation.NavigationStack.FirstOrDefault() as FlyoutPage;
            }
        }

        private void ShowMenu()
        {
            _masterDetail.IsPresented = true;
        }

        private TElement GetElementByName<TElement>(string name) where TElement : class
        {
            return FindByName(name) as TElement;
        }
    }
}
