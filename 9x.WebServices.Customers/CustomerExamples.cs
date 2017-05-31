using _9x.WebServices.Customers.Operations;
using CorrigoServiceWebReference.CorrigoGA;

namespace _9x.WebServices.Customers
{
	public static class CustomerExamples
	{
		public static int CreateRetreive (CorrigoService service)
		{
			var createdCustomerId = Create.Execute(service);

			var customer = Read.Execute(service, createdCustomerId);

			return customer.Id;
		}

		public static void RetreiveUpdate (CorrigoService service, int id)
		{
			var customer = Read.Execute(service, id);

			//retrieved customer has right customer.ConcurrencyId to pass optimistic lock check on update
			var updatedCustomer = Update.Execute(service, customer);
		}

		public static void Delete (CorrigoService service, int id)
		{
			var response = Operations.Delete.Execute(service, id);
		}
	}
}
