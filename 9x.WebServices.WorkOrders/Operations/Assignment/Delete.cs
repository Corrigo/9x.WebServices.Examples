using CorrigoServiceWebReference.CorrigoGA;
using System.Diagnostics;

namespace _9x.WebServices.WorkOrders.Operations.Assignment
{
	internal static class Delete
	{
		public static void Execute(CorrigoService service, int id)
		{
			var command = new DeleteCommand
			{
				EntitySpecifier = new EntitySpecifier
				{
					EntityType = EntityType.WoAssignment,
					Id = id
				}
			};
			var response = service.Execute(command) as OperationCommandResponse;

			Debug.Print(response.ErrorInfo?.Description ?? "Successfully deleted WoAssignment!");
		}
	}
}
