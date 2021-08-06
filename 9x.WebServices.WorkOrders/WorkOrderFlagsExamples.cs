using _9x.WebServices.WorkOrders.Operations.Flags;
using CorrigoServiceWebReference.CorrigoGA;

namespace _9x.WebServices.WorkOrders
{
    public static class WorkOrderFlagsExamples
    {
		/// <summary>
		/// Create, Update, Delete aren't supported
		/// </summary>
		/// <param name="service"></param>
		/// <param name="workOrderId"></param>
		/// <returns></returns>
		public static int? CreateFlag(CorrigoService service, int workOrderId)
			=> Create.Execute(service, workOrderId);
		/// <summary>
		/// Flags property is readonly
		/// </summary>
		/// <param name="service"></param>
		/// <returns></returns>
		public static WoFlag[] ReadFlags(CorrigoService service)
			=> Read.RetrieveMultiple(service);

	}
}
