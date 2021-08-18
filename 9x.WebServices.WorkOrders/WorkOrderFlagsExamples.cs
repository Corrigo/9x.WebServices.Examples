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
			=> Create.ExecuteCreateCommand(service, workOrderId);

		/// <summary>
		/// Creates Flag via WoFlagCommand, links to WO
		/// </summary>
		/// <param name="service"></param>
		/// <param name="workOrderId"></param>
		/// <returns></returns>
		public static WorkOrder CreateFlagWo(CorrigoService service, int workOrderId, int flagId)
			=> Create.ExecuteWoFlagCommand(service, workOrderId, flagId);

		/// <summary>
		/// Returns all flags
		/// Ordered - latest is 1st
		/// 
		/// </summary>
		/// <param name="service"></param>
		/// <returns></returns>
		public static CorrigoEntity[] ReadFlags(CorrigoService service)
			=> Read.RetrieveMultiple(service);

		/// <summary>
		/// Links set of flags to WO
		/// </summary>
		/// <param name="service"></param>
		/// <param name="workOrderId"></param>
		/// <param name="flagIds">Reason Ids</param>
		/// <returns>Updated WO</returns>
		public static WorkOrder SetupFlags(CorrigoService service, int workOrderId, int[] flagIds)
			=> Update.SetFlags(service, workOrderId, flagIds);

		/// <summary>
		/// Flags property is readonly, set and clear by separate command
		/// </summary>
		/// <param name="service"></param>
		/// <param name="workOrderId"></param>
		/// <param name="flagIds">Reason Ids</param>
		/// <returns>Updated WO</returns>
		public static WorkOrder ClearFlags(CorrigoService service, int workOrderId, int[] flagIds)
			=> Update.ClearFlags(service, workOrderId, flagIds);

	}
}
