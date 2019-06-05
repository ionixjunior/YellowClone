using System.Linq;
using System.Collections.Generic;
using CoreGraphics;
using MapKit;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.iOS;
using Xamarin.Forms.Platform.iOS;
using YellowClone.Controls;
using YellowClone.iOS.Renderers;
using Foundation;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace YellowClone.iOS.Renderers
{
    public class CustomMapRenderer : MapRenderer
    {
        private CustomMap _formsMap;
        private UIView _customPinView;
        private List<CustomPin> _customPins;

        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                if (Control is MKMapView nativeMap)
                {
                    nativeMap.RemoveAnnotations(nativeMap.Annotations);
                    nativeMap.GetViewForAnnotation = null;
                    nativeMap.DidSelectAnnotationView -= OnDidSelectAnnotationView;
                    nativeMap.DidDeselectAnnotationView -= OnDidDeselectAnnotationView;
                }
            }

            if (e.NewElement != null)
            {
                _formsMap = (CustomMap)e.NewElement;
                _customPins = _formsMap.CustomPins;

                if (Control is MKMapView nativeMap)
                {
                    nativeMap.GetViewForAnnotation = GetViewForAnnotation;
                    nativeMap.DidSelectAnnotationView += OnDidSelectAnnotationView;
                    nativeMap.DidDeselectAnnotationView += OnDidDeselectAnnotationView;
                }
            }
        }

        protected override MKAnnotationView GetViewForAnnotation(MKMapView mapView, IMKAnnotation annotation)
        {
            MKAnnotationView annotationView = null;

            if (annotation is MKUserLocation)
                return null;

            var customPin = GetCustomPin(annotation as MKPointAnnotation);
            if (customPin == null)
            {
                return null;
            }

            annotationView = mapView.DequeueReusableAnnotation(customPin.Id.ToString());
            if (annotationView == null)
            {
                annotationView = new CustomMKAnnotationView(annotation, customPin.Id.ToString());
                annotationView.Image = UIImage.FromFile("map_pin.png");
                annotationView.CalloutOffset = new CGPoint(0, 0);
                ((CustomMKAnnotationView)annotationView).Id = customPin.Identifier.ToString();
                ((CustomMKAnnotationView)annotationView).Information = customPin.Information;
            }
            annotationView.CanShowCallout = false;

            return annotationView;
        }

        private void OnDidSelectAnnotationView(object sender, MKAnnotationViewEventArgs e)
        {
            if (e.View is CustomMKAnnotationView customView)
            {
                var customPin = _customPins.FirstOrDefault(p => p.Identifier.ToString() == customView.Id);
                if (customPin == null)
                    return;

                _formsMap.SelectPin(customPin);
            }
        }

        private void OnDidDeselectAnnotationView(object sender, MKAnnotationViewEventArgs e)
        {
            if (!e.View.Selected)
            {
                if (_customPinView == null)
                    return;

                _customPinView.RemoveFromSuperview();
                _customPinView.Dispose();
                _customPinView = null;
            }
        }

        private CustomPin GetCustomPin(MKPointAnnotation annotation)
        {
            if (annotation == null)
                return null;

            var position = new Position(annotation.Coordinate.Latitude, annotation.Coordinate.Longitude);
            foreach (var pin in _customPins)
            {
                if (pin.Position == position)
                {
                    return pin;
                }
            }
            return null;
        }

        public override void TouchesMoved(NSSet touches, UIEvent evt)
        {
            _formsMap.MoveMap();
            base.TouchesMoved(touches, evt);
        }
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
