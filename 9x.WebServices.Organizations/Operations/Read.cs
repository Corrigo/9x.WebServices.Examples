using CorrigoServiceWebReference.CorrigoGA;
using System;

namespace _9x.WebServices.Organizations.Operations
{
    internal static class Read
    {
        public static Organization Retrieve(CorrigoService service, int id)
        {
            Console.WriteLine();
            Console.WriteLine($"Retrieve Organization with id={id}");
            CorrigoEntity result = null;

            try
            {
                result = service.Retrieve(
                    new EntitySpecifier
                    {
                        EntityType = EntityType.Organization,
                        Id = id
                    },
                    new PropertySet
                    {
                        Properties = new string[]
                        {
                            "Id",
                            "DisplayAs",
                            "Number",
                            "Address.*",
                            "ContactAddresses.*",
                            "CustomFields.*",
                            "Notes.*",
                        }
                    }
                    //new AllProperties()
                    );
            }
            catch (Exception e)
            {
                if (!string.IsNullOrEmpty(e.Message)) Console.WriteLine(e.Message);
            }

            if (result == null)
            {
                Console.WriteLine("Retrieve failed");
                return null;
            }

            Organization toReturn = result as Organization;

            if (toReturn == null)
            {
                Console.WriteLine("Retrieve failed");
                return null;
            }

            int padRightNumber = 55;

            Console.WriteLine(string.Concat("Organization.Id=".PadRight(padRightNumber), toReturn.Id.ToString()));
            Console.WriteLine(string.Concat("Organization.DisplayAs=".PadRight(padRightNumber), toReturn.DisplayAs ?? ""));
            Console.WriteLine(string.Concat("Organization.Number=".PadRight(padRightNumber), toReturn.Number ?? ""));

            if (toReturn.Address != null)
            {
                Console.WriteLine(string.Concat("Organization.Address.City=".PadRight(padRightNumber), toReturn.Address.City??""));
            }

           
            Console.WriteLine();

            return toReturn;
        }

        public static CorrigoEntity[] RetrieveMultiple(CorrigoService service, int countNumber = 200)
        {
            var list = service.RetrieveMultiple(
                new QueryByProperty
                {
                    Count = countNumber,
                    EntityType = EntityType.Organization,
                    PropertySet = new AllProperties(),
                    Conditions = new PropertyValuePair[0],
                    Orders = new[]
                    {
                        new OrderExpression
                        {
                            OrderType = OrderType.Ascending,
                            PropertyName = "Id"
                        }
                    },
                });
            Console.WriteLine();
            Console.WriteLine($"Organizations: Retrieve Multiple Top {countNumber} Organizations");

            if (list == null || list.Length == 0) return list;

            Console.WriteLine(string.Concat("#".PadLeft(4), "|", "Id".PadLeft(4), "|", "Number".PadRight(10), "|", "DisplayAs".PadRight(60)));

            int i = 0;
            foreach (Organization item in list)
            {
                i++;
                Console.WriteLine(string.Concat(i.ToString().PadLeft(4), "|", item.Id.ToString().PadLeft(4), "|", item.Number.PadRight(10), "|", item.DisplayAs.PadRight(60)));
            }

            Console.WriteLine();
            return list;
        }

        public static CorrigoEntity[] RetrieveByQuery(CorrigoService service, int countNumber = 500)
        {
            var list = service.RetrieveMultiple(
                new QueryExpression
                {
                    Count = countNumber,
                    EntityType = EntityType.Organization,
                    PropertySet = new PropertySet
                    {
                        Properties = new string[]
                        {
                            "Id",
                            "DisplayAs",
                            "Number",
                        }
                    },
                    Criteria = new FilterExpression
                    {
                        Conditions = new ConditionExpression[]
                        {
                            new ConditionExpression
                            {
                                PropertyName = "Id",
                                Operator = ConditionOperator.GreaterOrEqual,
                                Values = new object[] {3}
                            },
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
            Console.WriteLine();
            Console.WriteLine($"Organizations: Retrieve by query Top {countNumber} Organizations");

            if (list == null || list.Length == 0) return list;

            Console.WriteLine(string.Concat("#".PadLeft(4), "|", "Id".PadLeft(4), "|", "Number".PadRight(10), "|", "DisplayAs".PadRight(60)));

            int i = 0;
            foreach (Organization item in list)
            {
                i++;
                Console.WriteLine(string.Concat(i.ToString().PadLeft(4), "|", item.Id.ToString().PadLeft(4), "|", item.Number.PadRight(10), "|", item.DisplayAs.PadRight(60)));
            }

            Console.WriteLine();
            return list;
        }

    }
}
