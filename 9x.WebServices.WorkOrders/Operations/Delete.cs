using CorrigoServiceWebReference.CorrigoGA;

namespace _9x.WebServices.WorkOrders.Operations
{
	internal class Delete
	{
		public static WoActionResponse Execute (CorrigoService service, int woId)
		{
			var command = new WoCancelCommand
			{
				// ActionReason Id take from [DkEwiJllMes].[dbo].[WOActionReasonLookup] on devsql03.dev.local\inst2
				ActionReasonId = 1796,
				Comment = "need to delete the work order",
				WorkOrderId = woId
			};

			var response = service.Execute(command) as WoActionResponse;

			return response;
		}
	}
}