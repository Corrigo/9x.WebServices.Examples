using _9x.WebServices.WorkOrders.Operations;
using CorrigoServiceWebReference.CorrigoGA;
using System.Diagnostics;

namespace _9x.WebServices.WorkOrders.Workflows
{
	public static class WoAutoAssignExample
	{
		public static int Execute(CorrigoService service, int woid)
		{
			var wo = Read.Execute(service, woid);

			var routine = new WoAutoAssignRoutine { WorkOrder = wo, Zip = "95814" };

			var response = service.Execute(routine) as EmployeeResponse;

			Debug.Print(response.ErrorInfo?.Description
					?? $"Successfully executed {nameof(WoAutoAssignRoutine)}");
			return response.EmployeeId;
		}
	}
}
