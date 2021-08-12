using _9x.WebServices.WorkOrders.Operations;
using _9x.WebServices.WorkOrders.Operations.CustomFields;
using CorrigoServiceWebReference.CorrigoGA;
using Create = _9x.WebServices.WorkOrders.Operations.Create;

namespace _9x.WebServices.WorkOrders
{
	public static class WorkOrderOperationExamples
	{
		public static void CreateWorkOrderAndUpdateCustomFields(CorrigoService service)
		{
			WorkOrder createdWo = Create.Execute(service, true, true);
			var cfResponse = Update.SetCustomFieldValueToWO(service, createdWo);
		}

		public static WorkOrder RetrieveWorkOrder(CorrigoService service, int id)
		{
			return Read.Execute(service, id);
		}
		public static CorrigoEntity[] RetrieveAllWorkOrders(CorrigoService service)
		{
			return Read.GetAll(service);
		}

		public static WoActionResponse DeleteWorkOrder(CorrigoService service, int id)
		{
			return Delete.Execute(service, id);
		}

		public static int CreateCustomFieldForWorkOrder(CorrigoService service, int woid)
			=> Operations.CustomFields.Create.Execute(service, woid);
	}
}
