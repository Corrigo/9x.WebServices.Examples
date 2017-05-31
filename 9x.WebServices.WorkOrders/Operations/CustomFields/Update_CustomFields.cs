using System.Linq;
using CorrigoServiceWebReference.CorrigoGA;

namespace _9x.WebServices.WorkOrders.Operations.CustomFields
{
	internal class Update
	{
		public static OperationCommandResponse SetCustomFieldValueToWO(
			CorrigoService service,
			WorkOrder workOrder,
			string value = "51",
			string customFieldName = "CF Work Order")     // one of the custom fields that already exist in system
		{
			var descriptor = GetCustomFieldDescriptorByName(service, customFieldName);

			workOrder.CustomFields = new CustomField2[] {
				new CustomField2
				{
					Descriptor = descriptor,
					ObjectTypeId = ActorType.WO,
					Value = value				// as the Custom Field is of type Integer
				}
			};

			var result = (OperationCommandResponse) service.Execute(new UpdateCommand
			{
				Entity = workOrder,
				PropertySet = new PropertySet { Properties = new[] { "CustomFields.*" } }
			});

			return result;
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