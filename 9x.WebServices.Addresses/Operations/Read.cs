using CorrigoServiceWebReference.CorrigoGA;
using System;

namespace _9x.WebServices.Addresses.Operations
{
	internal static class Read
	{
        public static Address2 Retrieve(CorrigoService service, int id)
        {
            Console.WriteLine();
            Console.WriteLine($"Retrieve Address2 with id={id}");
            CorrigoEntity result = null;

            try
            {
                result = service.Retrieve(
                    new EntitySpecifier
                    {
                        EntityType = EntityType.Address2,
                        Id = id
                    },
                    new PropertySet
                    {
                        Properties = new string[]
                        {
                            "Id",
                            "ActorTypeId",
                            "ActorId",
                            "TypeId",
                            "Street",
                            "Street2",
                            "City",
                            "State",
                            "Zip",
                            "Country",
                            "GeoStatusId",
                            "Latitude",
                            "Longitude",
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

            Address2 toReturn = result as Address2;

            if (toReturn == null)
            {
                Console.WriteLine("Retrieve failed");
                return null;
            }

            int padRightNumber = 35;

            Console.WriteLine(string.Concat("Address2.Id=".PadRight(padRightNumber), toReturn.Id.ToString()));
            Console.WriteLine(string.Concat("Address2.ActorTypeId=".PadRight(padRightNumber), toReturn.ActorTypeId.ToString()));
            Console.WriteLine(string.Concat("Address2.ActorId=".PadRight(padRightNumber), toReturn.ActorId.ToString()));
            Console.WriteLine(string.Concat("Address2.TypeId=".PadRight(padRightNumber), toReturn.TypeId.ToString()));
            Console.WriteLine(string.Concat("Address2.Street=".PadRight(padRightNumber), toReturn.Street ?? ""));
            Console.WriteLine(string.Concat("Address2.Street2=".PadRight(padRightNumber), toReturn.Street2 ?? ""));
            Console.WriteLine(string.Concat("Address2.City=".PadRight(padRightNumber), toReturn.City ?? ""));
            Console.WriteLine(string.Concat("Address2.State=".PadRight(padRightNumber), toReturn.State ?? ""));
            Console.WriteLine(string.Concat("Address2.Zip=".PadRight(padRightNumber), toReturn.Zip ?? ""));
            Console.WriteLine(string.Concat("Address2.Country=".PadRight(padRightNumber), toReturn.Country ?? ""));
            Console.WriteLine(string.Concat("Address2.GeoStatusId=".PadRight(padRightNumber), toReturn.GeoStatusId.ToString()));
            Console.WriteLine(string.Concat("Address2.Latitude=".PadRight(padRightNumber), toReturn.Latitude.ToString()));
            Console.WriteLine(string.Concat("Address2.Longitude=".PadRight(padRightNumber), toReturn.Longitude.ToString()));

            Console.WriteLine();

            return toReturn;
        }

        public static CorrigoEntity[] RetrieveMultiple(CorrigoService service)
        {
            var list = service.RetrieveMultiple(
                new QueryByProperty
                {
                    EntityType = EntityType.Address2,
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
            Console.WriteLine("Addresses: Retrieve Multiple");

            if (list == null || list.Length == 0) return list;

            Console.WriteLine(string.Concat("#".PadLeft(4), "|", "Id".PadLeft(4), "|", "Country".PadRight(10), "|", "State".PadRight(6), "|", "City".PadRight(40), "|", "Street".PadRight(50), "|", "Zip".PadRight(10), "|", "Type".PadRight(15), "|", "ActorTypeId".PadRight(15)));

            int i = 0;
            foreach (Address2 item in list)
            {
                i++;
                Console.WriteLine(string.Concat(i.ToString().PadLeft(4), "|", item.Id.ToString().PadLeft(4), "|", item.Country.PadRight(10), "|", item.State.PadRight(6), "|", item.City.PadRight(40), "|", item.Street.PadRight(50), "|", item.Zip.PadRight(10), "|", item.TypeId.ToString().PadRight(15), "|", item.ActorTypeId.ToString().PadRight(15)));
            }

            Console.WriteLine();
            return list;
        }

        public static CorrigoEntity[] RetrieveByQuery(CorrigoService service)
        {
            var list = service.RetrieveMultiple(
                new QueryExpression
                {
                    EntityType = EntityType.Address2,
                    PropertySet = new AllProperties(),
                    Criteria = new FilterExpression
                    {
                        Conditions = new ConditionExpression[]
                        {
                            new ConditionExpression
                            {
                                PropertyName = "State",
                                Operator = ConditionOperator.Equal,
                                Values = new object[] {"CA"}
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
            Console.WriteLine("Addresses: Retrieve by query - State CA");

            if (list == null || list.Length == 0) return list;

            Console.WriteLine(string.Concat("#".PadLeft(4), "|", "Id".PadLeft(4), "|", "Country".PadRight(10), "|", "State".PadRight(6), "|", "City".PadRight(40), "|", "Street".PadRight(50), "|", "Zip".PadRight(10), "|", "Type".PadRight(15), "|", "ActorTypeId".PadRight(15)));

            int i = 0;
            foreach (Address2 item in list)
            {
                i++;
                Console.WriteLine(string.Concat(i.ToString().PadLeft(4), "|", item.Id.ToString().PadLeft(4), "|", item.Country.PadRight(10), "|", item.State.PadRight(6), "|", item.City.PadRight(40), "|", item.Street.PadRight(50), "|", item.Zip.PadRight(10), "|", item.TypeId.ToString().PadRight(15), "|", item.ActorTypeId.ToString().PadRight(15)));
            }

            Console.WriteLine();
            return list;
        }

    }
}
