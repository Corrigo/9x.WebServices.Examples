using CorrigoServiceWebReference.CorrigoGA;

namespace _9x.WebServices.WorkOrders.Operations.Items
{
	public static class WorkOrderItemExamples
	{
		public static EntitySpecifier[] CreateItem(CorrigoService service, int workOrderId)
			=> Create.Execute(service, workOrderId);

		public static CorrigoEntity[] ReadItems(CorrigoService service)
			=> Read.RetrieveAll(service);

		public static void UpdateItem(CorrigoService service, int workOrderId)
			=> Update.Execute(service, workOrderId);

		public static void DeleteItem(CorrigoService service, int workOrderId)
			=> Delete.Execute(service, workOrderId);

		//----
		public static WorkOrder GetOrder(CorrigoService service, int workOrderId)
			=> Operations.Read.Execute(service, workOrderId);
		//public static WoAssignment ReadAssignment(CorrigoService service, int? assignmentId)
		//	=> Read.Retrieve(service, assignmentId);
		//public static void UpdateAssignment(CorrigoService service, int? assignmentId)
		//	=> Update.Execute(service, assignmentId ?? 0);
		//public static void DeleteAssignment(CorrigoService service, int? assignmentId)
		//	=> Delete.Execute(service, assignmentId ?? 0);
	}
}