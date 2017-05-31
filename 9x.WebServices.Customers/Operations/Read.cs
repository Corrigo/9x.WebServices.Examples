using CorrigoServiceWebReference.CorrigoGA;

namespace _9x.WebServices.Customers.Operations
{
	internal static class Read
	{
        public static Customer Execute(CorrigoService service, int id)
		{
			var entity = new EntitySpecifier
			{
				EntityType = EntityType.Customer,
				Id = id
			};

			var properties = new[]
			{
				"DisplayAs",
				"Name",
				"Dba",
				"Instructions",
				"TenantCode",
				"TaxExempt",
				"Spaces.*",
				"CustomFields.*",
				"Notes.*",
				"Addresses.*",
				"GroupsBridge.*",
				"Contacts.*",
				"Contract.*",
                "WorkZone.*"
            };

			var response = service.Retrieve(entity, new PropertySet { Properties = properties });
			return response as Customer;
		}
	}
}
