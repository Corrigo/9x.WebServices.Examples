using _9x.WebServices.WorkOrders.Operations.LastAction;
using CorrigoServiceWebReference.CorrigoGA;

namespace _9x.WebServices.WorkOrders
{
	public static class WorkOrderLastActionExamples
	{
		public static int? CreateWoLastAction(CorrigoService service, int woid) => Create.Execute(service, woid);
	}
}
