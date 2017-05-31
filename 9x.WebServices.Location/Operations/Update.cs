using CorrigoServiceWebReference.CorrigoGA;
using System.Diagnostics;

namespace _9x.WebServices.Locations.Operations
{
	internal static class Update
	{
		public static void Execute(CorrigoService service, int locationId)
		{
			var location = Read.Retrieve(service, locationId);
			location.ModelId = 1215;

			string[] properties = { nameof(Location.ModelId) };

			var command = new UpdateCommand
			{
				Entity = location,
				PropertySet = new PropertySet { Properties = properties }
			};
			var response = service.Execute(command) as OperationCommandResponse;
			Debug.Print(response.ErrorInfo?.Description ?? $"Successfully updated Location with id {locationId}");
		}
	}
}