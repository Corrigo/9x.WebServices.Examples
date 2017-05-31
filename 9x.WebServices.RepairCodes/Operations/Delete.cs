using CorrigoServiceWebReference.CorrigoGA;
using System.Diagnostics;

namespace _9x.WebServices.RepairCodes.Operations
{
	internal static class Delete
	{
		public static void Execute (CorrigoService service, int repairCodeId)
		{
			var command = new DeleteCommand
			{
				EntitySpecifier = new EntitySpecifier
				{
					EntityType = EntityType.RepairCode,
					Id = repairCodeId
				}
			};
			var response = service.Execute(command) as OperationCommandResponse;

			Debug.Print(response?.ErrorInfo?.Description
				?? $"Successfully deleted repair code with id '{repairCodeId}'.");
		}
	}
}
