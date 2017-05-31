using _9x.WebServices.WorkOrders.Operations.Items;
using CorrigoServiceWebReference.CorrigoGA;

namespace _9x.WebServices.WorkOrders
{
	public static class WorkOrderItemExamples
	{
		public static EntitySpecifier[] CreateItem(CorrigoService service, int workOrderId)
			=> Create.Execute(service, workOrderId);
		//public static WoAssignment ReadAssignment(CorrigoService service, int? assignmentId)
		//	=> Read.Retrieve(service, assignmentId);
		//public static void UpdateAssignment(CorrigoService service, int? assignmentId)
		//	=> Update.Execute(service, assignmentId ?? 0);
		//public static void DeleteAssignment(CorrigoService service, int? assignmentId)
		//	=> Delete.Execute(service, assignmentId ?? 0);
	}
}