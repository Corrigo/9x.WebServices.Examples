using CorrigoServiceWebReference.CorrigoGA;
using System.Diagnostics;

namespace _9x.WebServices.WorkOrders.Operations.LastAction
{
	internal static class Create
	{
		public static int? Execute(CorrigoService service, int woid)
		{

			var action = new WoLastAction
			{
				WorkOrderId = woid,
				LastAction = new WoActionLog { Id = 5 },
				EmergencyReason = new WoActionReasonLookup { Id = 1289 },
				Reason = new WoActionReasonLookup { Id = 1289 },
				BillStatus = BillStatus.NotBilled,
				Invoice = new Invoice { Id = 269 },
                BilledTotal = new MoneyValue {Value = 156m, CurrencyTypeId = CurrencyType.USD},
                XNumber = "123"
			};

			//WoLastModificationCommand ?
			var command = new CreateCommand { Entity = action };
			var response = service.Execute(command) as OperationCommandResponse;

			var printOut = response?.ErrorInfo?.Description;
			printOut = !string.IsNullOrWhiteSpace(printOut)
				? $"{nameof(WoLastAction)} creation failure reason: {printOut}"
				: $"Successfully created {nameof(WoLastAction)} instance for WorkOrder with id {woid}.";
			Debug.Print(printOut);

			return response?.EntitySpecifier?.Id;
		}
	}
}
