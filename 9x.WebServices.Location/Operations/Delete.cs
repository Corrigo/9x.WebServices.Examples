using System.Diagnostics;
using CorrigoServiceWebReference.CorrigoGA;

namespace _9x.WebServices.Locations.Operations
{
	internal static class Delete
	{
		public static void Execute(CorrigoService service, int locationId)
		{
			var command = new DeleteCommand
			{
				EntitySpecifier = new EntitySpecifier
				{
					EntityType = EntityType.Location,
					Id = locationId
				}
			};
			var response = service.Execute(command) as OperationCommandResponse;
			Debug.Print(response.ErrorInfo?.Description ?? $"Successfully deleted Location with id {locationId}");
		}
	}
}