using CorrigoServiceWebReference.CorrigoGA;
using System.Diagnostics;

namespace OneTransactionCommands
{
	public static class UnitSpaceContactWorkOrder
	{
		public static void Create(CorrigoService service, int customerId, string unitFloorPlan)
		{
			var command = new CompositeCommand
			{
				IsTransactional = true,
				Commands = new CommandRequest[]
				{
					CreateSpaceCommand(customerId, unitFloorPlan),
					CreateContactCommand(customerId)
				}
			};

			var response = service.Execute(command) as CompositeCommandResponse;
			foreach (CommandResponse commandResponse in response.Responses)
			{
				Debug.Print(commandResponse.ErrorInfo?.Description ?? "Success");
				Debug.Print(commandResponse.GetType().ToString());
			}
		}

		/// <summary> creates space and unit for it simultaneously </summary>
		/// <param name="customerId">requires Customer to be pre-existing in 9x</param>
		/// <param name="unitFloorPlan">as Space cannot be created without Unit - requires UnitFloorPlan value</param>
		/// <returns>space create command</returns>
		private static CommandRequest CreateSpaceCommand(int customerId, string unitFloorPlan)
		{
			return new SpaceCreateCommand
			{
				CustomerId = customerId,
				Instructions = "please clean up everything",
				NewUnitSpecifier = new NewUnitSpecifier
				{
					UnitName = "test unit 1 name 2",                     //IMPORTANT: UnitName.Length <= 64
																		 //unit name should be unique
					UnitFloorPlan = unitFloorPlan,
					StreetAddress = new Address2 { ActorTypeId = ActorType.CommLeaseSpace, },
					UnitInfo = new AssetInfo()
				}
			};
		}

		/// <summary> composes command for contact creation </summary>
		/// <param name="customerId">requires Customer to be pre-existing in 9x</param>
		/// <returns>create command</returns>
		private static CommandRequest CreateContactCommand(int customerId)
		{
			return new CreateCommand
			{
				Entity = new Contact
				{
					DisplayAs = "test 1 contact",           // should be unique
					LastName = "Smith",
					Number = "19191996",                    // should be unique
					Comment = "test contact",
					TypeId = LeaseContactType.Primary,
					CustomerId = customerId,
					CanViewAnyRequest = true,
					CanCreateRequest = true,
					MustResetPassword = false,
					Username = "test 1 contact"             // should be unique
				}
			};
		}
	}
}
