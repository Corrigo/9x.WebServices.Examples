using CorrigoServiceWebReference.CorrigoGA;
using System.Diagnostics;

namespace _9x.WebServices.WorkOrders.Operations.Assignment
{
	internal static class Update
	{
		public static void Execute(CorrigoService service, int id)
		{
			var entity = new WoAssignment
			{
				Id = id,
				EmployeeId = 3,
				IsPrimary = false
			};

			string[] properties = { "EmployeeId", "IsPrimary" };
			var command = new UpdateCommand
			{
				Entity = entity,
				PropertySet = new PropertySet { Properties = properties }
			};
			var response = service.Execute(command) as OperationCommandResponse;

			Debug.Print(response.ErrorInfo?.Description ?? "Successfully updated WoAssignment!");
		}
	}
}
