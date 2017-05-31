
using CorrigoServiceWebReference.CorrigoGA;
using _9x.WebServices.CustomField2s.Operations;
using System;
using System.Linq;

namespace _9x.WebServices.CustomField2s
{
    public static class CustomField2Examples
    {
        /// <summary>
        /// For creation of new Custom Field is used latest WorkOrder.
        /// By default creation, update, deletion of Custom Fields is disabled.
        /// To enable creation, update, deletion mode for Custom Fields set isCreateUpdateDelete = true.
        /// </summary>
        /// <param name="service"></param>
        /// <param name="isCreateUpdateDelete">by default is false</param>
        public static void CRUDExample(CorrigoService service,
            bool isCreateUpdateDelete = false)
        {
            if (service == null) return;

            //
            // Get Custom Field Descriptor with properties (Custom Field: JIRA LINK; Domain: Work Order; Data Type : URL)
            //
            CustomFieldDescriptor customFieldDescriptor = GetCustomFieldDescriptor(service, "JIRA LINK", ActorType.WO, CfType.Url);
            if (customFieldDescriptor == null || !(customFieldDescriptor.Id > 0))
            {
                Console.WriteLine("Custom Field Descriptor is undefined");
                return;
            }

            //Get latest WO
            CorrigoEntity[] latestWO = GetLatestWOs(service, 1);
            if (!latestWO.Any())
            {
                Console.WriteLine("No existing Work Order was found");
                return;
            }

            //
            // Retrieve Custom field specified by descriptor for given Work Order.
            //
            CustomField2 cf = Read.Retrieve(service, latestWO[0], customFieldDescriptor);

            if (cf == null && isCreateUpdateDelete)
            {
                //
                // Create Custom field specified by descriptor for given Work Order.
                //
                cf = Create.Execute(service, latestWO[0], customFieldDescriptor);
            }

            if (cf != null && isCreateUpdateDelete)
            {
                cf = Read.Retrieve(service, latestWO[0], customFieldDescriptor);

                //
                // Update Custom field
                //
                Update.Execute(service, cf);

                //
                // Delete Custom Field
                //
                Delete.Execute(service, cf.Id);

                //Update.Restore(service, cf); // CustomField2 is not restorable.             
            }

            //
            // Retrive 10 Custom Fields with Data Type : URL
            //
            Read.RetrieveByQuery(service); // retrieve is limited to 10 records
        }


        /// <summary>
        /// Get Custom Field Descriptor by specified field name, domain(entity) object type, and Custom Field Type.
        /// When Custom Field Descriptor by specified parameters is not found, the new descriptor is going to be created.
        /// </summary>
        /// <param name="service"></param>
        /// <param name="fieldName"></param>
        /// <param name="actorType"></param>
        /// <param name="cfType"></param>
        /// <returns>CustomFieldDescriptor</returns>
        private static CustomFieldDescriptor GetCustomFieldDescriptor(CorrigoService service, string fieldName,
            ActorType actorType, CfType cfType)
        {
            var list = service.RetrieveMultiple(
                new QueryExpression
                {
                    Count = 1,
                    EntityType = EntityType.CustomFieldDescriptor,
                    PropertySet = new PropertySet
                    {
                        Properties = new string[]
                        {
                            "*"
                        }
                    },
                    Criteria = new FilterExpression
                    {
                        Conditions = new ConditionExpression[]
                        {
                            new ConditionExpression
                            {
                                PropertyName = "Type",
                                Operator = ConditionOperator.Equal,
                                Values = new object[] {cfType}
                            },
                            new ConditionExpression
                            {
                                PropertyName = "ActorTypeId",
                                Operator = ConditionOperator.Equal,
                                Values = new object[] {actorType}
                            },
                            new ConditionExpression
                            {
                                PropertyName = "Name",
                                Operator = ConditionOperator.Equal,
                                Values = new object[] {fieldName}
                            },
                            new ConditionExpression
                            {
                                PropertyName = "IsRemoved",
                                Operator = ConditionOperator.Equal,
                                Values = new object[] {false}
                            }
                        },
                        FilterOperator = LogicalOperator.And
                    },
                    Orders = new[]
                    {
                        new OrderExpression
                        {
                            OrderType = OrderType.Descending,
                            PropertyName = "Id"
                        }
                    },
                });

            if (list.Any()) return list[0] as CustomFieldDescriptor;

            var entity = new CustomFieldDescriptor
            {
                ActorTypeId = actorType,
                Name = fieldName,
                Type = cfType
            };
            var command = new CreateCommand {Entity = entity};
            var response = service.Execute(command) as OperationCommandResponse;

            entity.Id = response?.EntitySpecifier?.Id ?? 0;

            return entity;
        }


        // Returns specified number of latest Work Orders
        public static CorrigoEntity[] GetLatestWOs(CorrigoService service, int numberOfWOs)
        {
            var list = service.RetrieveMultiple(
                new QueryExpression
                {
                    EntityType = EntityType.WorkOrder,
                    PropertySet = new AllProperties(),
                    Orders = new[]
                    {
                        new OrderExpression
                        {
                            OrderType = OrderType.Descending,
                            PropertyName = "CreatedDateUtc"
                        }
                    },
                    Count = numberOfWOs
                });

            return list;
        }
    }
}
