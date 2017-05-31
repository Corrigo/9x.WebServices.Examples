using CorrigoServiceWebReference.CorrigoGA;
using System.Diagnostics;

namespace _9x.WebServices.WorkOrders.Operations.Estimate
{
	internal static class Delete
	{
		public static void Execute (CorrigoService service, int woid)
		{
			var specifier = new EntitySpecifier
			{
				EntityType = EntityType.WorkOrder,
				Id = woid
			};
			string[] properties = { nameof(WorkOrder.Estimate) + ".*" };
			var workOrder = service.Retrieve(specifier, new PropertySet { Properties = properties }) as WorkOrder;

			workOrder.Estimate = null;
			var command = new UpdateCommand
			{
				Entity = workOrder,
				PropertySet = new PropertySet { Properties = properties }
			};

			var response = service.Execute(command) as OperationCommandResponse;

			Debug.Print(response?.ErrorInfo?.Description
				?? $"Successfully deleted WoEstimate for WorkOrder with id '{woid}'");
		}
	}
}
