using _9x.WebServices.WorkOrders.Operations.Items;
using CorrigoServiceWebReference.CorrigoGA;
using System.Diagnostics;

namespace _9x.WebServices.WorkOrders.Operations.Costs
{
	internal static class Create
	{
		public static WorkOrderCostId Execute (CorrigoService service)
		{
			//var workOrder = CreateWorkOrder(service);
			var workOrder = Operations.Read.Execute(service, 583);
			workOrder.WorkOrderCost.CustomerNte.Value = 28.98m;
            workOrder.WorkOrderCost.CustomerNte.CurrencyTypeId = CurrencyType.USD;
            //workOrder.WorkOrderCost.CostState = CostState.Pending;
            workOrder.WorkOrderCost.BillingRule = BillingRule.NotBilled;
			//workOrder.WorkOrderCost.BillToType = BillToType.Customer;
			//workOrder.WorkOrderCost.BillingAccount = new BillingAccount
			//{

			//};
			//workOrder.WorkOrderCost.ChargeCode = new ChargeCodeLookup { Code = "some charge code", Id = 0 };
			//workOrder.WorkOrderCost.TaxStatus = TaxValidationStatus.Success;

			//workOrder.WorkOrderCost.Items = new[] { new FinancialItem
			//{
			//	CostCategoryId = CostCategory.Services,
			//	Quantity = 10,
			//	Amount = 56.23m,
			//} };

			//var workOrderCost = new WorkOrderCost
			//{
			//	CustomerNte = 28.98m,
			//	CostState = CostState.Pending,
			//	BillToType = BillToType.Customer,
			//	//ChargeCode = new ChargeCodeLookup
			//	//{
			//	//	DisplayAs = "display as charge code",
			//	//	Code = "some charge code",

			//	//},
			//	//Items = new[]
			//	//	{
			//	//		new FinancialItem
			//	//		{
			//	//			CostCategoryId = CostCategory.Services,
			//	//			Quantity = 10,
			//	//			Amount = 56.23m,
			//	//		}
			//	//	}
			//};


			var command = new UpdateCommand
			{
				Entity = workOrder,
				//PropertySet = new PropertySet { Properties = new[] { "WorkOrderCost.*" } }
				PropertySet = new PropertySet
				{
					Properties = new[]
					{
						"WorkOrderCost.CustomerNte",
						"WorkOrderCost.BillingRule",
						//"WorkOrderCost.ChargeCode.*",
						//"WorkOrderCost.Items.*",
					}
				}
			};

			var response = service.Execute(command) as OperationCommandResponse;

			var woCostId = response?.EntitySpecifier?.Id ?? 0;
			Debug.Print(response?.ErrorInfo?.Description
				?? $"Successfully created {nameof(WorkOrder.WorkOrderCost)} with id '{woCostId}'"
				+ $" for WorkOrder with id '{workOrder.Id}'");

			return new WorkOrderCostId { WoId = workOrder.Id, CostId = woCostId };
		}

		private static WorkOrder CreateWorkOrder (CorrigoService service)
		{
            var workZone = (WorkZone)service.Retrieve(
                new EntitySpecifier { Id = 28, EntityType = EntityType.WorkZone },
                new PropertySet { Properties = new[] { "Id", "TimeZone" } });

            var workOrder = new WorkOrder
			{
				Items = new[]
				{
					new WoItem
					{
						Asset = new Location {Id = 173},
						Task = new Task {Id = 14096}
					}
				},
				Customer = new Customer { Id = 14 },
                WorkZone = workZone,
				TimeZone = workZone.TimeZone,

				//WorkOrderCost = workOrderCost,

				Priority = new WoPriority { Id = 2 },
				MainAsset = new Location { Id = 173 },
				SubType = new WorkOrderType { Id = 4 },
				StatusId = WorkOrderStatus.New,
				ContactName = "Somerset Moehm",
				ContactAddress = new ContactInfo
				{
					Address = "San Francisco",
					ActorTypeId = ActorType.Asset,
					AddrTypeId = ContactAddrType.Contact
				},
				TypeCategory = WOType.Request, //required
			};

			var command = new WoCreateCommand
			{
				WorkOrder = workOrder,
				Comment = string.Empty,
				ComputeAssignment = true,
				ComputeSchedule = false,
				SkipBillToLogic = false
			};

			var response = service.Execute(command) as WoActionResponse;
			return response?.Wo;
		}
	}

	public sealed class WorkOrderCostId
	{
		public int WoId { get; set; }
		public int CostId { get; set; }
	}
}
