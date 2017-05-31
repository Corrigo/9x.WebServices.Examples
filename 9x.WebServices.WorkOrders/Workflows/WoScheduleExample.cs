using CorrigoServiceWebReference.CorrigoGA;
using System.Diagnostics;
using System.Linq;

namespace _9x.WebServices.WorkOrders.Workflows
{
	public static class WoScheduleExample
	{
		public static void InitializeSchedule(CorrigoService service)
		{
			//var workOrder = Read.Execute(service, woid);

			string[] inputProperties = { };
			string[] outputProperties = { };

			var routine = new WoScheduleRoutine
			{
				WorkOrder = new WorkOrder { WorkZone = new WorkZone { Id = 100 } },
				Options = WoScheduleOptions.SkipFinancial,
				InputPropertySet = new PropertySet { Properties = inputProperties },
				OutputPropertySet = new PropertySet { Properties = outputProperties }
			};

			var response = service.Execute(routine) as WoActionResponse;

			Debug.Print(response.ErrorInfo?.Description
						?? $"Successfully executed {nameof(WoScheduleRoutine)} command.");
			response.UpdatedPropertySet.Properties.ToList().ForEach(p => Debug.Print(p));
		}
	}
}
