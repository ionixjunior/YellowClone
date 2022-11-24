using System;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Microsoft.Maui.Maps;
using Microsoft.Maui.Maps.Handlers;
using static Android.Icu.Text.Transliterator;

namespace YellowClone.Controls;

public class PlatformCustomMap : Android.Gms.Maps.MapView, GoogleMap.IInfoWindowAdapter
{
	readonly CustomMap map;
	List<CustomPin> customPins;
	GoogleMap gmap => ((MapHandler)map.Handler).Map;
	public PlatformCustomMap(Context context, CustomMap map) : base(context)
	{
		this.map = map;
		customPins = map.CustomPins;

		// Já que o método OnMapReady não existe mais nesse contexto
		// e nem é virtual o jeito é colocar a logica de inicialização nesse evento
		// https://github.com/dotnet/maui/blob/main/src/Core/maps/src/Handlers/Map/MapHandler.Android.cs#L275
		this.ViewAttachedToWindow += OnMapReady;
	}

	async void OnMapReady(object sender, ViewAttachedToWindowEventArgs e)
	{
		// Gambiarra pra esperar o gmap ser inicializado
		// N existe nenhum outro evento ou método que dê pra usar pra esse fim
		// No momento ele é sempre null, talvez seja por conta de não ter as chaves?
		while (gmap is null)
			await Task.Delay(500);

		gmap.SetInfoWindowAdapter(this);
	}

	public static void CreateCustomMap()
	{
		MapHandler.PlatformViewFactory = (handler) =>
		{
			return new PlatformCustomMap(handler.Context, (CustomMap)handler.VirtualView);
		};

		MapPinHandler.Mapper.AppendToMapping(nameof(IMapPin.Location), (handler, view) =>
		{
			if (view is not CustomPin pin)
				return;

			var marker = handler.PlatformView;
			marker.SetTitle(pin.Label);
			marker.SetSnippet(pin.Address);
			marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.map_pin));
		});
	}

	public Android.Views.View GetInfoContents(Marker marker)
	{
		return null;
	}

	public Android.Views.View GetInfoWindow(Marker marker)
	{
		var customPin = GetCustomPin(marker);
		if (customPin is null)
			return new Android.Views.View(Context);

		map.SelectPin(customPin);
		return new Android.Views.View(Context);
	}

	CustomPin GetCustomPin(Marker annotation)
	{
		var position = new Location(annotation.Position.Latitude, annotation.Position.Longitude);
		foreach (var pin in customPins)
		{
			if (pin.Location == position)
			{
				return pin;
			}
		}
		return null;
	}
}

