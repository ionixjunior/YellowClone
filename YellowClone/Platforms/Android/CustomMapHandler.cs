using System;
using Microsoft.Maui.Maps.Handlers;
using IMap = Microsoft.Maui.Maps.IMap;

namespace YellowClone.Platforms.Android
{
	public class CustomMapHandler : MapHandler
	{
        public static readonly IPropertyMapper<IMap, IMapHandler> CustomMapper =
            new PropertyMapper<IMap, IMapHandler>(Mapper)
            {
            };

        public CustomMapHandler() : base(CustomMapper, CommandMapper)
        {
        }

        public CustomMapHandler(IPropertyMapper? mapper = null, CommandMapper? commandMapper = null) : base(mapper ?? CustomMapper, commandMapper ?? CommandMapper)
        {
        }
    }
}

