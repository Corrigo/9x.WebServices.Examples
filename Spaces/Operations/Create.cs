using CorrigoServiceWebReference.CorrigoGA;

namespace _9xWebServices.Spaces.Operations
{
	internal static class Create
	{
		public static SpaceCreateCommandResponse ExecuteByUnitId (CorrigoService service, int? unitId = null)
		{
			var command = new SpaceCreateCommand
			{
				CustomerId = 51,
				Instructions = "please clean up everything J",
				UnitId = unitId ?? 73             //from xlsx file - some record with Asset category type = unit
            };
			var response = service.Execute(command);
			return response as SpaceCreateCommandResponse;
		}

		public static SpaceCreateCommandResponse ExecuteWithNewUnit (CorrigoService service)
		{
			var command = new SpaceCreateCommand
			{
				CustomerId = 51,
				Instructions = "please clean up everything",
				NewUnitSpecifier = new NewUnitSpecifier
				{
					//BuildingName = "some building",
					//BuildingFloorPlan = "some floor plan",
					//FloorName = "floor name 1",
					//FloorFloorPlan = "floor floor floor plan",
					UnitName = "test unit 1 name",                              //IMPORTANT: UnitName.Length <= 64
					UnitFloorPlan = "Unit1",
					StreetAddress = new Address2 { ActorTypeId = ActorType.CommLeaseSpace, },
					UnitInfo = new AssetInfo { }
				}
			};
			var response = service.Execute(command);
			return response as SpaceCreateCommandResponse;
		}

	}
}
