using CorrigoServiceWebReference.CorrigoGA;

namespace _9x.WebServices.WorkOrders.Operations.Assignment
{
	internal static class Read
	{
		public static WoAssignment Retrieve(CorrigoService service, int? id)
		{
			var entitySpecifier = new EntitySpecifier
			{
				EntityType = EntityType.WoAssignment,
				Id = id
			};

			string[] properties = { "WorkOrderId", "EmployeeId", "IsPrimary" };
			var response = service.Retrieve(entitySpecifier, new PropertySet { Properties = properties })
						 as WoAssignment;

			return response;
		}
	}
}
