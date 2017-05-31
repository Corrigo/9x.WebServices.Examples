using CorrigoServiceWebReference.CorrigoGA;
using System;

namespace _9x.WebServices.Contracts.Operations
{
	internal static class Read
	{
        public static Contract Retrieve(CorrigoService service, int id)
        {
            Console.WriteLine();
            Console.WriteLine($"Retrieve Contract with id={id}");
            CorrigoEntity result = null;

            try
            {
                result = service.Retrieve(
                    new EntitySpecifier
                    {
                        EntityType = EntityType.Contract,
                        Id = id
                    },
                    new PropertySet
                    {
                        Properties = new string[]
                        {
                            "Id",
                            "DisplayAs"
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

            Contract toReturn = result as Contract;

            if (toReturn == null)
            {
                Console.WriteLine("Retrieve failed");
                return null;
            }

            int padRightNumber = 35;

            Console.WriteLine(string.Concat("Contract.Id=".PadRight(padRightNumber), toReturn.Id.ToString()));
            Console.WriteLine(string.Concat("Contract.DisplayAs=".PadRight(padRightNumber), toReturn.DisplayAs ?? ""));

            Console.WriteLine();

            return toReturn;
        }

        public static CorrigoEntity[] RetrieveMultiple(CorrigoService service)
        {
            var list = service.RetrieveMultiple(
                   new QueryByProperty
                   {
                       EntityType = EntityType.Contract,
                       PropertySet = new AllProperties(),
                       Conditions = new PropertyValuePair[0]
                   });
            Console.WriteLine();
            Console.WriteLine("Contracts: Retrieve Multiple");

            if (list == null || list.Length == 0) return list;

            Console.WriteLine(string.Concat("#".PadLeft(4), "|", "Id".PadLeft(4),  "|", "DisplayAs".PadRight(50)));

            int i = 0;
            foreach (Contract item in list)
            {
                i++;
                Console.WriteLine(string.Concat(i.ToString().PadLeft(4), "|", item.Id.ToString().PadLeft(4), "|", item.DisplayAs.PadRight(50)));
            }

            Console.WriteLine();
            return list;
        }

        public static CorrigoEntity[] RetrieveByQuery(CorrigoService service)
        {
            var list = service.RetrieveMultiple(
                new QueryExpression
                {
                    EntityType = EntityType.Contract,
                    PropertySet = new AllProperties(),
                    Criteria = new FilterExpression
                    {
                        Conditions = new ConditionExpression[]
                        {
                            new ConditionExpression
                            {
                                PropertyName = "DisplayAs",
                                Operator = ConditionOperator.Like,
                                Values = new object[] {"%#%"}
                            }
                        },
                        FilterOperator = LogicalOperator.Or
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
            Console.WriteLine("Contracts: Retrieve by query - like '%#%'");

            if (list == null || list.Length == 0) return list;

            Console.WriteLine(string.Concat("#".PadLeft(4), "|", "Id".PadLeft(4), "|", "DisplayAs".PadRight(50)));

            int i = 0;
            foreach (Contract item in list)
            {
                i++;
                Console.WriteLine(string.Concat(i.ToString().PadLeft(4), "|", item.Id.ToString().PadLeft(4), "|", item.DisplayAs.PadRight(50)));
            }

            Console.WriteLine();
            return list;
        }

    }
}
