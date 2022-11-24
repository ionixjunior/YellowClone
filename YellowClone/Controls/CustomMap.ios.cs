using System;
using Foundation;
using MapKit;
using Microsoft.Maui.Maps;
using Microsoft.Maui.Maps.Handlers;
using UIKit;

namespace YellowClone.Controls;

public class PlatformCustomMap : Microsoft.Maui.Maps.Platform.MauiMKMapView
{
    readonly List<CustomPin> customPins;
    readonly Microsoft.Maui.Maps.IMap map;
    UIView customPinView;

    public PlatformCustomMap(IMapHandler handler) : base(handler)
    {
        map = handler.VirtualView;

        if (map is CustomMap customMap)
        {
            customPins = customMap.CustomPins;
        }

        RemoveAnnotations(this.Annotations);
        base.GetViewForAnnotation = GetViewForAnnotation;
        DidSelectAnnotationView += OnDidSelectAnnotationView;
        DidDeselectAnnotationView += OnDidDeselectAnnotationView;
    }

    public override void TouchesMoved(NSSet touches, UIEvent evt)
    {
        if (map is not CustomMap customMap)
            return;

        customMap.MoveMap();
        base.TouchesMoved(touches, evt);
    }

    new MKAnnotationView GetViewForAnnotation(MKMapView mapView, IMKAnnotation annotation)
    {
        if (annotation is MKUserLocation)
            return null;

        var customPin = GetCustomPin(annotation as MKPointAnnotation);

        if (customPin is null)
            return null;

        var markerId = customPin.MarkerId.ToString();

        var annotationView = mapView.DequeueReusableAnnotation(markerId);
        if (annotationView is null)
        {
            annotationView = new CustomMKAnnotationView(annotation, markerId)
            {
                Image = UIImage.FromFile("map_pin.png"),
                CalloutOffset = new CoreGraphics.CGPoint(0, 0)
            };

            ((CustomMKAnnotationView)annotationView).Id = customPin.Identifier.ToString();
            ((CustomMKAnnotationView)annotationView).Information = customPin.Information;
        }

        annotationView.CanShowCallout = false;

        return annotationView;
    }

    CustomPin GetCustomPin(MKPointAnnotation annotation)
    {
        if (annotation is null)
            return null;

        var position = new Location(annotation.Coordinate.Latitude, annotation.Coordinate.Longitude);
        foreach (var pin in customPins)
        {
            if (pin.Location == position)
            {
                return pin;
            }
        }
        return null;
    }


    void OnDidDeselectAnnotationView(object sender, MKAnnotationViewEventArgs e)
    {
        if (!e.View.Selected)
        {
            if (customPinView is null)
                return;

            customPinView.RemoveFromSuperview();
            customPinView.Dispose();
            customPinView = null;
        }
    }

    void OnDidSelectAnnotationView(object sender, MKAnnotationViewEventArgs e)
    {
        if (e.View is not CustomMKAnnotationView customView || map is not CustomMap customMap)
            return;

        var customPin = customPins.FirstOrDefault(x => x.Identifier.ToString() == customView.Id);
        if (customPin is null)
            return;

        customMap.SelectPin(customPin);
    }

    public static void CreateCustomMap()
    {
        MapHandler.PlatformViewFactory = (handler) => new PlatformCustomMap((IMapHandler)handler);
    }


    public class CustomMKAnnotationView : MKAnnotationView
    {
        public string Id { get; set; }
        public string Information { get; set; }

        public CustomMKAnnotationView(IMKAnnotation annotation, string id)
            : base(annotation, id)
        {
        }
    }
}