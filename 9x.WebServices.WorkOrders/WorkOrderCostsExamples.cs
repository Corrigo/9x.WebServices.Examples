using _9x.WebServices.WorkOrders.Operations.Costs;
using CorrigoServiceWebReference.CorrigoGA;

namespace _9x.WebServices.WorkOrders
{
	public static class WorkOrderCostsExamples
	{
		public static WorkOrderCostId CreateCosts (CorrigoService service)
			=> Create.Execute(service);
		public static WorkOrderCost ReadCosts (CorrigoService service, int workOrderId)
			=> Read.Retrieve(service, workOrderId);
	}
}
