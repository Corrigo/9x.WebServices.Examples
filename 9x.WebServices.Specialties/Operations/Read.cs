using CorrigoServiceWebReference.CorrigoGA;
using System;
using System.Linq;

namespace _9x.WebServices.Specialties.Operations
{
	internal static class Read
	{

        public static Specialty Retrieve(CorrigoService service, int id)
        {
            Console.WriteLine();
            Console.WriteLine($"Retrieve Specialty with id={id}");
            CorrigoEntity result = null;

            try
            {
                result = service.Retrieve(
                    new EntitySpecifier
                    {
                        EntityType = EntityType.Specialty,
                        Id = id
                    },
                    new PropertySet
                    {
                        Properties = new string[]
                        {
                            "Id",
                            "DisplayAs",
                            "WONServiceId",
                            "Instructions",
                            "TaxCode.*",
                            "Currencies.*"
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

            Specialty toReturn = result as Specialty;

            if (toReturn == null)
            {
                Console.WriteLine("Retrieve failed");
                return null;
            }

            int padRightNumber = 35;

            Console.WriteLine(string.Concat("Specialty.Id=".PadRight(padRightNumber), toReturn.Id.ToString()));
            Console.WriteLine(string.Concat("Specialty.DisplayAs=".PadRight(padRightNumber), toReturn.DisplayAs ?? ""));
            Console.WriteLine(string.Concat("Specialty.WONServiceId=".PadRight(padRightNumber), toReturn.WONServiceId.ToString()));
            Console.WriteLine(string.Concat("Specialty.Instructions=".PadRight(padRightNumber), toReturn.Instructions ?? ""));

            if (toReturn.TaxCode != null)
            {
                Console.WriteLine(string.Concat("Specialty.TaxCode.Id=".PadRight(padRightNumber), toReturn.TaxCode.Id.ToString()));
                Console.WriteLine(string.Concat("Specialty.TaxCode.DisplayAs=".PadRight(padRightNumber), toReturn.TaxCode.DisplayAs ?? ""));                
            }

            if (toReturn.Currencies != null)
            {
                for (int i = 0; i < toReturn.Currencies.Length; i++)
                {
                    Console.WriteLine(string.Concat($"Specialty.Currencies[{i}].Nte=".PadRight(padRightNumber), toReturn.Currencies[i].Nte?.ToString() ?? ""));
                    Console.WriteLine(string.Concat($"Specialty.Currencies[{i}].AvgCostAll=".PadRight(padRightNumber), toReturn.Currencies[i].AvgCostAll?.ToString() ?? ""));
                    Console.WriteLine(string.Concat($"Specialty.Currencies[{i}].AvgCostTech=".PadRight(padRightNumber), toReturn.Currencies[i].AvgCostTech?.ToString() ?? ""));
                    Console.WriteLine(string.Concat($"Specialty.Currencies[{i}].AvgCostVendor=".PadRight(padRightNumber), toReturn.Currencies[i].AvgCostVendor?.ToString() ?? ""));
                }
            }

            Console.WriteLine();

            return toReturn;
        }

        public static CorrigoEntity[] RetrieveMultiple(CorrigoService service)
        {

            var list = service.RetrieveMultiple(
                new QueryByProperty
                {
                    EntityType = EntityType.Specialty,
                    PropertySet = new PropertySet
                    {
                        Properties = new string[]
                        {
                            "*", "TaxCode.*"
                        }
                    },
                    Conditions = new PropertyValuePair[0],
                    Orders = new[]
                    {
                        //new OrderExpression
                        //{
                        //    OrderType = OrderType.Ascending,
                        //    PropertyName = "DisplayAs"
                        //},
                        new OrderExpression
                        {
                            OrderType = OrderType.Ascending,
                            PropertyName = "Id"
                        }
                    },
                });

            Console.WriteLine("Specialties: Retrieve Multiple");

            if (list == null || list.Length == 0) return list;

            Console.WriteLine(string.Concat("#".PadLeft(6), "|", "Id".PadLeft(6), "|", "DisplayAs".PadRight(50), "|", "Instructions".PadRight(25), "|", "WONServiceId".PadRight(15), "|",  "TaxCode".PadRight(15)));

            int i = 0;
            foreach (Specialty item in list)
            {
                string taxCode = (item.TaxCode == null) ? "" : (item.TaxCode.DisplayAs ?? "");

                i++;
                Console.WriteLine(string.Concat(i.ToString().PadLeft(6), "|", item.Id.ToString().PadLeft(6), "|", item.DisplayAs.PadRight(50), "|", item.Instructions.PadLeft(25), "|", item.WONServiceId.ToString().PadLeft(15), "|", taxCode.PadLeft(15)));
            }

            Console.WriteLine();
            return list;
        }

        public static CorrigoEntity[] RetrieveByQuery(CorrigoService service)
        {

            Console.WriteLine();


            var list = service.RetrieveMultiple(
                new QueryExpression
                {
                    EntityType = EntityType.Specialty,
                    PropertySet = new PropertySet
                    {
                        Properties = new string[]
                        {
                            "*", "TaxCode.*"
                        }
                    },
                    //PropertySet = new AllProperties(),

                    Criteria = new FilterExpression
                    {
                        Conditions = new ConditionExpression[]
                        {
                            new ConditionExpression
                            {
                                PropertyName = "IsRemoved",
                                Operator = ConditionOperator.In,
                                Values = new Object[]{ true }
                            }
                        },
                        FilterOperator = LogicalOperator.Or
                    },
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
            Console.WriteLine($"Specialties: Retrieve by query - Removed");

            if (list == null || list.Length == 0) return list;

            Console.WriteLine(string.Concat("#".PadLeft(6), "|", "Id".PadLeft(6), "|", "DisplayAs".PadRight(50), "|", "Instructions".PadRight(25), "|", "WONServiceId".PadRight(15), "|", "TaxCode".PadRight(15)));

            int i = 0;
            foreach (Specialty item in list)
            {
                string taxCode = (item.TaxCode == null) ? "" : (item.TaxCode.DisplayAs ?? "");

                i++;
                Console.WriteLine(string.Concat(i.ToString().PadLeft(6), "|", item.Id.ToString().PadLeft(6), "|", item.DisplayAs.PadRight(50), "|", item.Instructions.PadLeft(25), "|", item.WONServiceId.ToString().PadLeft(15), "|", taxCode.PadLeft(15)));
            }

            Console.WriteLine();
            return list;
        }

    }


}
