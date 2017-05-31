using CorrigoServiceWebReference.CorrigoGA;

namespace _9x.WebServices.WorkOrders.Operations
{
	internal class Read
	{
		public static WorkOrder Execute (CorrigoService service, int workOrderId)
		{
			var targetProperties = new string[]
			{
				"Id",
				"Number",
				"PoNumber",
				//"StatusId",
				"CompletionNote.Body",
				"Notes.*",
				"RepairCode.*",
				"LastAction.Reason.DisplayAs",
				"LastAction.LastAction.Comment",
				"LastAction.LastAction.Actor.DisplayAs",
				"LastActionDate",
				"CustomFields.Value",
				"CustomFields.Descriptor.Id",
				"CustomFields.Descriptor.Name",

				//"Employee",
				"Employee.ActorTypeId",
                "Items.*"
			};

			var workOrder = (WorkOrder) service.Retrieve(
				new EntitySpecifier
				{
					EntityType = EntityType.WorkOrder,
					Id = workOrderId
				},
				new PropertySet { Properties = targetProperties });

			return workOrder;
		}
	}
}