using CorrigoServiceWebReference.CorrigoGA;

namespace _9x.WebServices.Locations.Operations
{
	internal static class Read
	{
		public static Location Retrieve(CorrigoService service, int locationId)
		{
			var locationSpecifier = new EntitySpecifier
			{
				EntityType = EntityType.Location,
				Id = locationId
			};
			string[] properties =
			{
				nameof(Location.Id),
				nameof(Location.Address)+".*",
				nameof(Location.Info) + ".*",
				nameof(Location.IsTemplate),
				nameof(Location.ModelId),
				nameof(Location.Name),
				nameof(Location.Orphan),
				nameof(Location.ParentId),
				nameof(Location.RootId)
			};

			var response = service.Retrieve(locationSpecifier, new PropertySet { Properties = properties })
						as Location;

			return response;
		}
	}
}
