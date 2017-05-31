using CorrigoServiceWebReference.CorrigoGA;
using System.Diagnostics;

namespace _9x.WebServices.WorkOrders.Operations.Estimate
{
	internal static class Update
	{
		public static void Execute (CorrigoService service, int woid, QuoteStatus newStatus)
		{
			var specifier = new EntitySpecifier
			{
				EntityType = EntityType.WorkOrder,
				Id = woid
			};
			string[] properties = { nameof(WorkOrder.Estimate) + ".*" };
			var workOrder = service.Retrieve(specifier, new PropertySet { Properties = properties }) as WorkOrder;

			var oldStatus = workOrder.Estimate.StatusId;
			workOrder.Estimate.StatusId = newStatus;

			var command = new UpdateCommand
			{
				Entity = workOrder,
				PropertySet = new PropertySet { Properties = new[] { "Estimate.StatusId" } }
			};

			var response = service.Execute(command) as OperationCommandResponse;

			Debug.Print(response?.ErrorInfo?.Description
				?? $"Successfully updated an Estimate from status {oldStatus} to {newStatus}");
		}
	}
}
