using CorrigoServiceWebReference.CorrigoGA;
using System.Diagnostics;

namespace _9x.WebServices.Locations.Operations
{
	internal static class Create
	{
		public static int Execute(CorrigoService service)
		{
			var asset = new Location
			{
				Name = "test location",
				Address = new Address2 { Id = 15 },    //?
				ModelId = 1221,      //?
				Orphan = true,
				TypeId = AssetType.Building,
                ParentId = 6,     //?
                RootId = 6,       //?
                IsTemplate = false,
				//Info = new AssetInfo { Id = 111 }       //?
			};
			var command = new CreateCommand { Entity = asset };
			var response = service.Execute(command) as OperationCommandResponse;

			var id = response.EntitySpecifier.Id ?? 0;
			Debug.Print(response.ErrorInfo?.Description ?? $"Successfully created Location with id {id}");

			return id;
		}
	}
}