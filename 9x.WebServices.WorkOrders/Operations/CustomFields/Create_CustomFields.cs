using CorrigoServiceWebReference.CorrigoGA;
using System.Diagnostics;
using System.Linq;

namespace _9x.WebServices.WorkOrders.Operations.CustomFields
{
	internal class Create
	{
		public static int Execute(CorrigoService service, int woid)
		{
			var entity = new CustomField2
			{
				Descriptor = new CustomFieldDescriptor { Id = 9 },
				ObjectId = woid,
				ObjectTypeId = ActorType.WO,
				Value = "10"
			};
			var command = new CreateCommand { Entity = entity };
			var response = service.Execute(command) as OperationCommandResponse;

			var id = response?.EntitySpecifier?.Id ?? 0;
			Debug.Print(response.ErrorInfo?.Description ?? $"Successfully created custom field with id {id}");

			return id;
		}

		private static CustomFieldDescriptor GetCustomFieldDescriptorByName(CorrigoService service, string name)
		{
			var queryDescriptor = new QueryExpression
			{
				EntityType = EntityType.CustomFieldDescriptor,
				PropertySet = new AllProperties(),
				Criteria = new FilterExpression
				{
					Conditions = new[]
					{
						new ConditionExpression
						{
							PropertyName = "Name",
							Operator = ConditionOperator.Equal,
							Values = new object[] {name}
						}
					}
				}
			};

			return service.RetrieveMultiple(queryDescriptor).Cast<CustomFieldDescriptor>().FirstOrDefault();
		}
	}
}