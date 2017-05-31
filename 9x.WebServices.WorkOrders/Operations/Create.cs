using CorrigoServiceWebReference.CorrigoGA;
using System.Diagnostics;

namespace _9x.WebServices.WorkOrders.Operations
{
	/// <summary>
	/// property bag with minimal data amount to allow WO creation in 9x
	/// </summary>
	internal static class Create
	{
		// take from link url of spaces for the customer below
		public static int SpaceUnitAssetId { get; set; } = 173;

		// take from link url of spaces for the customer below
		public static int SpaceUnitSubAssetId { get; set; } = 173;

		// take from exported tasks
		public static int TaskId { get; set; } = 14096;

		// take from url of customer details page
		public static int CustomerId { get; set; } = 14;

		// take from exported work zones or details page of customer
		public static int WorkZoneId { get; set; } = 28;

		// Reactive work order sub type - required -same for all companies
		public static int SubTypeId => 4;

		public static WorkOrder Execute(CorrigoService service, bool computeAssignment, bool computeSchedule)
		{
            //WorkOrder requires WorkZone for its creation
            //default time zone is UTC-12, therefore provide WorkOrder with time zone from WorkZone

            var workZone = (WorkZone)service.Retrieve(
                new EntitySpecifier { Id = WorkZoneId, EntityType = EntityType.WorkZone },
                new PropertySet { Properties = new[] { "Id", "TimeZone" } });

            var workOrder = new WorkOrder
			{
				Items = new[]	//required
				{
					new WoItem
					{
						Asset = new Location { Id = SpaceUnitSubAssetId },	//required
						Task = new Task { Id = TaskId }						//required
					}
				},
				Customer = new Customer { Id = CustomerId },//required
															//WorkZone = workZone,
															//TimeZone = workZone.TimeZone,

				//Priority = new WoPriority { Id = 1 },
				//MainAsset = new Location { Id = SpaceUnitAssetId },
				SubType = new WorkOrderType { Id = SubTypeId },//required
															   //StatusId = WorkOrderStatus.New,
															   //ContactName = "John Smith",
				ContactAddress = new ContactInfo            //required for request at least
				{
					Address = "San Francisco",//required
											  //ActorTypeId = ActorType.Asset,
					AddrTypeId = ContactAddrType.Contact//required
				},
				TypeCategory = WOType.Request,              //required

				//CreatedDateUtc = DateTime.UtcNow,
				//DueDateUtc = DateTime.UtcNow,
				//DtUtcOnSiteBy = DateTime.UtcNow,
				//DtScheduledStart = DateTime.UtcNow,
				//ScheduledStartUtc = DateTime.UtcNow,
			};

			var command = new WoCreateCommand
			{
				WorkOrder = workOrder,
				Comment = string.Empty,
				ComputeAssignment = computeAssignment,
				ComputeSchedule = computeSchedule,
				SkipBillToLogic = false
			};

			var response = service.Execute(command) as WoActionResponse;
			if (response.ErrorInfo != null)
				Debug.Print(response.ErrorInfo.Description);
			return response?.Wo;
		}
	}
}
