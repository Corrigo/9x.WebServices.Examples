using _9x.WebServices.WorkOrders.Operations.ActionReasonLookup;
using CorrigoServiceWebReference.CorrigoGA;

namespace _9x.WebServices.WorkOrders
{
	public static class WorkOrderActionReasonLookupExamples
	{
		public static int? ReadActionReasonLookup(CorrigoService service, int workOrderId)
			=> Read.Retreive(service, workOrderId);
	}
}
