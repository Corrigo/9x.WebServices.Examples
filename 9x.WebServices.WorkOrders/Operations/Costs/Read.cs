using CorrigoServiceWebReference.CorrigoGA;
using System.Diagnostics;

namespace _9x.WebServices.WorkOrders.Operations.Costs
{
	internal static class Read
	{
		public static WorkOrderCost Retrieve (CorrigoService service, int workOrderId)
		{
			Debug.Print($"Retrieving WorkOrderCost object for WorkOrder with id '{workOrderId}': ");

			var specifier = new EntitySpecifier { EntityType = EntityType.WorkOrderCost, Id = workOrderId };
			string[] properties =
			{
				"CustomerNte",
				"ChargeCode.Id",
				"Items.CostCategoryId",
				"Items.Quantity",
				"Items.Amount"
			};

			var response = service.Retrieve(specifier, new PropertySet { Properties = properties })
						as WorkOrderCost;

			Debug.Print(response == null ? "Failure!" : "Success!");

			return response;
		}
	}
}
