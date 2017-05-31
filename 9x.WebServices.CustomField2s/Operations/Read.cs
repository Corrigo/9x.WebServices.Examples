using CorrigoServiceWebReference.CorrigoGA;
using System;
using System.Linq;

namespace _9x.WebServices.CustomField2s.Operations
{
    internal static class Read
    {
        public static CustomField2 Retrieve(CorrigoService service, CorrigoEntity entity,
            CustomFieldDescriptor customFieldDescriptor)
        {
            var list = service.RetrieveMultiple(
                new QueryExpression
                {
                    Count = 1,
                    EntityType = EntityType.CustomField2,
                    PropertySet = new PropertySet
                    {
                        Properties = new string[]
                        {
                            "Id",
                            "ObjectId",
                            "ObjectTypeId",
                            "Value",
                        }
                    },
                    Criteria = new FilterExpression
                    {
                        Conditions = new ConditionExpression[]
                        {
                            new ConditionExpression
                            {
                                PropertyName = "ObjectId",
                                Operator = ConditionOperator.Equal,
                                Values = new object[] {entity.Id}
                            },
                            new ConditionExpression
                            {
                                PropertyName = "Descriptor.Id",
                                Operator = ConditionOperator.Equal,
                                Values = new object[] {customFieldDescriptor.Id}
                            }
                        },
                        FilterOperator = LogicalOperator.And
                    }
                });

            return list.FirstOrDefault() as CustomField2;
        }

        public static CorrigoEntity[] RetrieveByQuery(CorrigoService service)
        {
            var list = service.RetrieveMultiple(
                new QueryExpression
                {
                    Count = 10,
                    EntityType = EntityType.CustomField2,
                    PropertySet = new PropertySet
                    {
                        Properties = new string[]
                        {
                            "Id",
                            "ObjectId",
                            "ObjectTypeId",
                            "Value",
                        }
                    },
                    Criteria = new FilterExpression
                    {
                        Conditions = new ConditionExpression[]
                        {
                            new ConditionExpression
                            {
                                PropertyName = "Descriptor.Type",
                                Operator = ConditionOperator.Equal,
                                Values = new object[] {CfType.Url}
                            }
                        },
                        FilterOperator = LogicalOperator.And
                    }
                });


            Console.WriteLine();
            Console.WriteLine("CustomField2s: Retrieve Multiple By Query");

            if (list == null || list.Length == 0) return list;

            Console.WriteLine(string.Concat("#".PadLeft(4), "|", "Id".PadLeft(4), "|", "ObjectId".PadLeft(8), "|",
                "ObjectTypeId".PadRight(20), "|", "Value".PadRight(250)));

            int i = 0;
            foreach (CustomField2 item in list)
            {
                i++;
                Console.WriteLine(string.Concat(i.ToString().PadLeft(4), "|", item.Id.ToString().PadLeft(4), "|",
                    item.ObjectId.ToString().PadLeft(8), "|", item.ObjectTypeId.ToString().PadRight(20), "|",
                    item.Value.PadRight(250)));
            }

            return list;
        }
    }
}
