using System;
using Android.Content;
using Android.Gms.Maps;
using Microsoft.Maui.Maps.Handlers;

namespace YellowClone.Controls
{
	public class PlatformCustomMap : Android.Gms.Maps.MapView
    {
        readonly CustomMap map;
        List<CustomPin> customPins;

        public PlatformCustomMap(Context context, CustomMap map) : base(context)
        {
            this.map = map;
            customPins = map.CustomPins;
            
        }

        GoogleMap fmap;

        public static void CreateCustomMap()
        {
            MapHandler.PlatformViewFactory = (handler) =>
            {
                return new PlatformCustomMap(handler.Context, (CustomMap)handler.VirtualView);
            };
        }

    }
}

