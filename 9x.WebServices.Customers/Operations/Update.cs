using CorrigoServiceWebReference.CorrigoGA;

namespace _9x.WebServices.Customers.Operations
{
	internal static class Update
	{
		public static Customer Execute (CorrigoService service, Customer customer)
		{
			customer.Name = "Updated Customer";

			var command = new UpdateCommand
			{
				Entity = customer,
				PropertySet = new PropertySet
				{
					Properties = new[] { nameof(customer.Addresses) + ".*" }
				}
			};

			var response = service.Execute(command) as OperationCommandResponse;

			//Customer is a CorrigoEntityWithOptimisticLock,
			//response EntitySpecifier is of EntityWithOptimisticLockSpecifier type
			//that has ConcurrencyId field
			var responseWithOptimisticLock = response.EntitySpecifier as EntityWithOptimisticLockSpecifier;

			int concurrencyId = responseWithOptimisticLock.ConcurrencyId;
			customer.ConcurrencyId = concurrencyId;//updating concurrencyId to perform further changes
			return customer;
		}
	}
}
