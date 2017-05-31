using CorrigoServiceWebReference.CorrigoGA;

namespace _9x.WebServices.Customers.Operations
{
	internal static class Create
	{
		public static int Execute (CorrigoService service)
		{
			var entity = new Customer
			{
				Name = "John name",                                         // IMPORTANT: Name.Length <= 64
				Dba = "jet brains",                                         // IMPORTANT: DBA.Length <= 64

				DisplayAs = "John",
                WorkZone = new WorkZone { Id = 100 },                       // WorkZone Id - from export

				Instructions = "Add the garlic and onion, and sprinkle in the cumin, chili powder, salt, and pepper. Set the cooker on Low, and cook until the chicken is very tender, 4 to 5 hours. Shred the chicken with two forks for serving.",
				TenantCode = "3198767",                                     //should be unique
				TaxExempt = false, //what is it?
				Spaces = new Space[] { },
				Contacts = new Contact[] { },
				CustomFields = new CustomField2[] { },
				Notes = new Note[] { },
				Addresses = new Address2[] { },
				Contract = new Contract { Id = 4 },
				GroupsBridge = new LinkCustomerAndGroup[] { }
			};
			var command = new CreateCommand { Entity = entity };
			var response = service.Execute(command) as OperationCommandResponse;

			var newCustomerId = response.EntitySpecifier.Id;

			//Customer is a CorrigoEntityWithOptimisticLock,
			//response EntitySpecifier is of EntityWithOptimisticLockSpecifier type
			//that has ConcurrencyId field
			var responseWithOptimisticLock = response.EntitySpecifier as EntityWithOptimisticLockSpecifier;

			int concurrencyId = responseWithOptimisticLock.ConcurrencyId;

			return newCustomerId.Value;
		}
	}
}
