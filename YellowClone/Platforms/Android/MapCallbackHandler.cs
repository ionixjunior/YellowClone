using System;
using Android.Gms.Maps;
using Microsoft.Maui.Maps.Handlers;
using IMap = Microsoft.Maui.Maps.IMap;

namespace YellowClone.Platforms.Android
{
	public class MapCallbackHandler : Java.Lang.Object, IOnMapReadyCallback
    {
        private readonly IMapHandler _mapHandler;

        public MapCallbackHandler(IMapHandler mapHandler)
		{
            _mapHandler = mapHandler;
		}

        public void OnMapReady(GoogleMap googleMap)
        {
            _mapHandler.UpdateValue(nameof(IMap.Pins));
        }
    }
}

