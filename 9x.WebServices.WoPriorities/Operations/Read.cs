using CorrigoServiceWebReference.CorrigoGA;
using System;

namespace _9x.WebServices.WoPriorities.Operations
{
	internal static class Read
	{
        public static WoPriority Retrieve(CorrigoService service, int id)
        {
            Console.WriteLine($"Retrieve WoPriority with id={id.ToString()}");
            CorrigoEntity result = null;

            try
            {
                result = service.Retrieve(
                    new EntitySpecifier
                    {
                        EntityType = EntityType.WoPriority,
                        Id = id
                    },
                    new PropertySet
                    {
                        Properties =
                            new string[]
                            {
                                "Id", "DisplayAs", "IsEmergency", "RespondInMinutes", "DueInMinutes",
                                "AcknowledgeInMinutes"
                            }
                    }
                    //new AllProperties()
                    );
            }
            catch (Exception e)
            {
                if (!String.IsNullOrEmpty(e.Message)) Console.WriteLine(e.Message);
            }

            if (result == null)
            {
                Console.WriteLine("Retrieve failed");
                return null;
            }

            WoPriority toReturn = result as WoPriority;

            if (toReturn == null)
            {
                Console.WriteLine("Retrieve failed");
                return null;
            }

            Console.WriteLine(String.Concat("WoPriority.Id=".PadRight(30), toReturn.Id.ToString()));
            Console.WriteLine(String.Concat("WoPriority.DisplayAs=".PadRight(30), String.IsNullOrEmpty(toReturn.DisplayAs) ? "" : toReturn.DisplayAs));
            Console.WriteLine(String.Concat("WoPriority.IsEmergency=".PadRight(30), toReturn.IsEmergency.ToString(), "  //Escalate"));
            Console.WriteLine(String.Concat("WoPriority.RespondInMinutes =".PadRight(30), toReturn.RespondInMinutes.ToString(), "  //Response minutes."));
            Console.WriteLine(String.Concat("WoPriority.DueInMinutes=".PadRight(30), toReturn.DueInMinutes.ToString(), "  //Complete minutes."));
            Console.WriteLine(String.Concat("WoPriority.AcknowledgeInMinutes=".PadRight(30), toReturn.AcknowledgeInMinutes.ToString(), "  //Complete minutes."));
            Console.WriteLine();

            return toReturn;
        }

        public static CorrigoEntity[] RetrieveMultiple(CorrigoService service)
        {
            var list = service.RetrieveMultiple(
                   new QueryByProperty
                   {
                       EntityType = EntityType.WoPriority,
                       PropertySet = new AllProperties(),
                       Conditions = new PropertyValuePair[0]
                   });
            Console.WriteLine("WoPriorities: Retrieve Multiple");

            if (list == null || list.Length == 0) return list;

            Console.WriteLine(String.Concat("#".PadLeft(4), "|", "Id".PadLeft(4), "|", "Label".PadRight(60), "|", "Response mins.".PadRight(15), "|", "Complete mins.".PadRight(15), "|", "Escalated".PadRight(10)));

            int i = 0;
            foreach (WoPriority item in list)
            {
                i++;
                Console.WriteLine(String.Concat(i.ToString().PadLeft(4), "|", item.Id.ToString().PadLeft(4), "|", item.DisplayAs.PadRight(60), "|", item.RespondInMinutes.ToString().PadLeft(15), "|", item.DueInMinutes.ToString().PadLeft(15), "|", item.IsEmergency.ToString().PadLeft(10)));
            }

            Console.WriteLine();
            return list;
        }

        public static CorrigoEntity[] RetrieveByQuery(CorrigoService service)
        {
            var list = service.RetrieveMultiple(
                new QueryExpression
                {
                    EntityType = EntityType.WoPriority,
                    PropertySet = new AllProperties(),
                    Criteria = new FilterExpression
                    {
                        Conditions = new ConditionExpression[]
                        {
                            new ConditionExpression
                            {
                                PropertyName = "IsEmergency",
                                Operator = ConditionOperator.Equal,
                                Values = new object[] {"True"}
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
            Console.WriteLine("WoPriorities: Retrieve by query - Escalated");

            if (list == null || list.Length == 0) return list;

            Console.WriteLine(String.Concat("#".PadLeft(4), "|", "Id".PadLeft(4), "|", "Label".PadRight(60), "|", "Response mins.".PadRight(15), "|", "Complete mins.".PadRight(15), "|", "Escalated".PadRight(10)));

            int i = 0;
            foreach (WoPriority item in list)
            {
                i++;
                Console.WriteLine(String.Concat(i.ToString().PadLeft(4), "|", item.Id.ToString().PadLeft(4), "|", item.DisplayAs.PadRight(60), "|", item.RespondInMinutes.ToString().PadLeft(15), "|", item.DueInMinutes.ToString().PadLeft(15), "|", item.IsEmergency.ToString().PadLeft(10)));
            }

            Console.WriteLine();
            return list;
        }

    }
}
