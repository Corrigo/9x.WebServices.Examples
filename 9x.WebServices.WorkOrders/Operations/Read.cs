using CorrigoServiceWebReference.CorrigoGA;

namespace _9x.WebServices.WorkOrders.Operations
{
	internal class Read
	{
		public static WorkOrder Execute(CorrigoService service, int workOrderId)
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

			var workOrder = (WorkOrder)service.Retrieve(
				new EntitySpecifier
				{
					EntityType = EntityType.WorkOrder,
					Id = workOrderId
				},
				new PropertySet { Properties = targetProperties });

			return workOrder;
		}

		public static CorrigoEntity[] GetAll(CorrigoService service)
		{
			return service.RetrieveMultiple(
				new QueryExpression
				{
					EntityType = EntityType.WorkOrder,
					PropertySet = new AllProperties(),
					Orders = new[]
					{
						new OrderExpression
						{
							OrderType = OrderType.Descending,
							PropertyName = "CreatedDateUtc"
						}
					},
				});
		}

        public static WorkOrder GetOrder(CorrigoService service, int woId)
        {
            return service.Retrieve(new EntitySpecifier { EntityType = EntityType.WorkOrder, Id = woId }, new AllProperties()) as WorkOrder;
        }
    }
}