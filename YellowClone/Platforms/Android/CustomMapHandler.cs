using System;
using System.Collections;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using Microsoft.Maui.Maps.Handlers;
using Microsoft.Maui.Platform;
using IMap = Microsoft.Maui.Maps.IMap;

namespace YellowClone.Platforms.Android
{
	public class CustomMapHandler : MapHandler
	{
        public static readonly IPropertyMapper<IMap, IMapHandler> CustomMapper =
            new PropertyMapper<IMap, IMapHandler>(Mapper)
            {
                [nameof(IMap.Pins)] = MapPins,
            };

        public CustomMapHandler() : base(CustomMapper, CommandMapper)
        {
        }

        public CustomMapHandler(IPropertyMapper? mapper = null, CommandMapper? commandMapper = null) : base(mapper ?? CustomMapper, commandMapper ?? CommandMapper)
        {
        }

        protected override void ConnectHandler(MapView platformView)
        {
            base.ConnectHandler(platformView);
            var mapReady = new MapCallbackHandler(this);
            PlatformView.GetMapAsync(mapReady);
        }

        private static new void MapPins(IMapHandler handler, IMap map)
        {
            if (handler is CustomMapHandler mapHandler)
            {
                mapHandler.AddPins(map.Pins);
            }
        }

        private void AddPins(IList<Microsoft.Maui.Maps.IMapPin> pins)
        {
            if (Map is null || MauiContext is null)
            {
                return;
            }

            foreach (var pin in pins)
            {
                var pinHandler = pin.ToHandler(MauiContext);
                if (pinHandler is IMapPinHandler mapPinHandler)
                {
                    var markerOption = mapPinHandler.PlatformView;
                    markerOption.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.map_pin));
                    Map.AddMarker(markerOption);
                }
            }
        }
    }
}

