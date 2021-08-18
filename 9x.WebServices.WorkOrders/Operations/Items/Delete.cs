using CorrigoServiceWebReference.CorrigoGA;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace _9x.WebServices.WorkOrders.Operations.Items
{
	internal static class Delete
	{
		public static void Execute(CorrigoService service, int woid)
		{
			var wo = Operations.Read.Execute(service, 583);

			if (!wo.Items.Any()) return;

			wo.Items = new List<WoItem>(wo.Items.Take(wo.Items.Length - 1)).ToArray();

			var command = new UpdateCommand
			{
				Entity = wo,
				PropertySet = new PropertySet { Properties = new[] { nameof(WorkOrder.Items) + ".*" } }
			};

			var response = service.Execute(command) as OperationCommandResponse;
			Debug.Print(response.ErrorInfo?.Description ?? "Successfully updated items for a work order");

			//return response.NestedEntitiesOperationResults.Select(r => r.EntitySpecifier).ToArray();
		}
	}
}
