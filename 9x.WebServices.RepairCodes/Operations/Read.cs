using CorrigoServiceWebReference.CorrigoGA;
using System.Diagnostics;

namespace _9x.WebServices.RepairCodes.Operations
{
	internal static class Read
	{
		public static RepairCode Retrieve (CorrigoService service, int id)
		{
			Debug.Print("Retrieving repair code entity:");

			var specifier = new EntitySpecifier { EntityType = EntityType.RepairCode, Id = id };
			string[] properties = { "Id", "DisplayAs", "ParentId", "IsRemoved", "Codes.*" };
			var response = service.Retrieve(specifier, new PropertySet { Properties = properties })
						 as RepairCode;

			Debug.Print(response == null ? "Failure!" : "Success!");

			return response;
		}
	}
}
