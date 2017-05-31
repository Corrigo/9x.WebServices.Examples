using CorrigoServiceWebReference.CorrigoGA;

namespace _9x.WebServices.WorkOrders.Operations.ActionReasonLookup
{
	internal static class Read
	{
		public static int Retreive(CorrigoService service, int woid)
		{
			var entitySpecifier = new EntitySpecifier
			{
				EntityType = EntityType.WorkOrder,
				Id = woid
			};
			string[] properties = { "LastAction.*", "LastAction.Reason.*" };
			var workOrder = service.Retrieve(entitySpecifier, new PropertySet { Properties = properties })
							as WorkOrder;

			WoActionReasonLookup reason = workOrder?.LastAction?.Reason;

			return reason?.Id ?? 0;
		}
	}
}
