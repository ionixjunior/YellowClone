using System.Linq;
using System.Collections.Generic;
using Map = Microsoft.Maui.Controls.Maps.Map;
using System;
using Microsoft.Maui.Controls.Maps;

namespace YellowClone.Controls
{
    public partial class CustomMap : Map
    {
        public event EventHandler<CustomPin> PinSelected;
        public event EventHandler MapMoved;
        public List<CustomPin> CustomPins { get; set; }

        public CustomMap()
        {
            CustomPins = new List<CustomPin>();
        }

        static partial void CreateCustomMap();

        public void AddPin(CustomPin pin)
        {
            Pins.Add(pin);
            CustomPins.Add(pin);
        }

        public void SelectPin(CustomPin pin)
        {
            PinSelected?.Invoke(this, pin);
        }

        public void MoveMap()
        {
            MapMoved?.Invoke(this, EventArgs.Empty);
        }
    }

    public class CustomPin : Pin
    {
        public int Identifier { get; set; }
        public string Information { get; set; }
    }
}
