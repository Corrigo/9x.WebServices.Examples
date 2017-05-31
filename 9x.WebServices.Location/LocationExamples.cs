using _9x.WebServices.Locations.Operations;
using CorrigoServiceWebReference.CorrigoGA;

namespace _9x.WebServices.Locations
{
	/// <summary>
	/// the class contains calls to entity represented in db with [dbo].[InvItemObject] table
	/// </summary>
	public static class LocationExamples
	{
		public static int CreateLocation(CorrigoService service)
			=> Create.Execute(service);
		public static Location ReadLocation(CorrigoService service, int locationId)
			=> Read.Retrieve(service, locationId);
		public static void UpdateLocation(CorrigoService service, int locationId)
			=> Update.Execute(service, locationId);
		public static void DeleteLocation(CorrigoService service, int locationId)
			=> Delete.Execute(service, locationId);
	}
}
