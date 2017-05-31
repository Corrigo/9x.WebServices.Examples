using CorrigoServiceWebReference.CorrigoGA;
using System.Diagnostics;

namespace _9x.WebServices.WorkOrders.Operations.Assignment
{
	internal static class Create
	{
		public static int? Execute(CorrigoService service, int woid)
		{
			var assignment = new WoAssignment
			{
				WorkOrderId = woid,
				EmployeeId = 4,
				IsPrimary = true
			};
			var command = new CreateCommand { Entity = assignment };
			var response = service.Execute(command) as OperationCommandResponse;

			Debug.Print(response.ErrorInfo?.Description
				?? $"Successfully created WoAssignment for Wo with id {woid}");

			return response.EntitySpecifier?.Id;
		}
	}
}
