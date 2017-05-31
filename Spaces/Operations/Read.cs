using CorrigoServiceWebReference.CorrigoGA;

namespace _9xWebServices.Spaces.Operations
{
	internal static class Read
	{
		public static Space Execute (CorrigoService service, int id)
		{
			var entitySpecifier = new EntitySpecifier
			{
				EntityType = EntityType.Space,
				Id = id
			};

			var properies = new PropertySet
			{
				Properties = new[]
				{
					"CustomerId",
					"Id",
					"StatusId",
					"StartDate",
					"EndDate",
					"MoveOutDate",
					"Instructions",
					"Asset.*",
					"Community.*",
					"Addresses.*",
				}
			};

			var result = service.Retrieve(entitySpecifier, properies);
			return result as Space;
		}
	}
}
