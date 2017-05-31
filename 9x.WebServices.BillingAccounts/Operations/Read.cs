using CorrigoServiceWebReference.CorrigoGA;
using System;

namespace _9x.WebServices.BillingAccounts.Operations
{
	internal static class Read
	{
        public static BillingAccount Retrieve(CorrigoService service, int id)
        {
            Console.WriteLine();
            Console.WriteLine($"Retrieve BillingAccount with id={id}");
            CorrigoEntity result = null;

            try
            {
                result = service.Retrieve(
                    new EntitySpecifier
                    {
                        EntityType = EntityType.BillingAccount,
                        Id = id
                    },
                    new PropertySet
                    {
                        Properties = new string[]
                        {
                            "Id",
                            "DisplayAs",
                            "PortalImageSetId",
                            "CpThemeId",
                            "IsBillAcct",
                            "Number",
                            "PayTerms",
                            "PayDays",
                            "PayInstr",
                            "IsCreditHold",
                            "AccrualMargin",
                            "SalesRep",
                            "IsTaxExempt",
                            "CorporateEntity.*",
                            "Address.*",
                            "IsInactive",
                            "PayDayWeekday",
                            "PayDayNumber",
                            "BillingContact.*"
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

            BillingAccount toReturn = result as BillingAccount;

            if (toReturn == null)
            {
                Console.WriteLine("Retrieve failed");
                return null;
            }

            int padRightNumber = 35;

            Console.WriteLine(string.Concat("BillingAccount.Id=".PadRight(padRightNumber), toReturn.Id.ToString()));
            Console.WriteLine(string.Concat("BillingAccount.DisplayAs=".PadRight(padRightNumber), toReturn.DisplayAs ?? ""));
            Console.WriteLine(string.Concat("BillingAccount.PortalImageSetId=".PadRight(padRightNumber), !toReturn.PortalImageSetId.HasValue ? "" : toReturn.PortalImageSetId.ToString()));

            Console.WriteLine(string.Concat("BillingAccount.CpThemeId=".PadRight(padRightNumber), !toReturn.CpThemeId.HasValue ? "" : toReturn.CpThemeId.ToString()));

            Console.WriteLine(string.Concat("BillingAccount.IsBillAcct=".PadRight(padRightNumber), toReturn.IsBillAcct));
            Console.WriteLine(string.Concat("BillingAccount.Number=".PadRight(padRightNumber), toReturn.Number ?? ""));

            Console.WriteLine(string.Concat("BillingAccount.PayTerms=".PadRight(padRightNumber), toReturn.PayTerms ?? ""));
            Console.WriteLine(string.Concat("BillingAccount.PayDays=".PadRight(padRightNumber), toReturn.PayDays));

            Console.WriteLine(string.Concat("BillingAccount.PayInstr=".PadRight(padRightNumber), toReturn.PayInstr ?? ""));

            Console.WriteLine(string.Concat("BillingAccount.IsCreditHold=".PadRight(padRightNumber), toReturn.IsCreditHold));
            Console.WriteLine(string.Concat("BillingAccount.AccrualMargin= ".PadRight(padRightNumber), toReturn.AccrualMargin));
            Console.WriteLine(string.Concat("BillingAccount.SalesRep= ".PadRight(padRightNumber), toReturn.SalesRep ?? ""));            
            Console.WriteLine(string.Concat("BillingAccount.IsTaxExempt=".PadRight(padRightNumber), toReturn.IsTaxExempt));


            if (toReturn.CorporateEntity != null)
            {
                Console.WriteLine(string.Concat("BillingAccount.CorporateEntity.Id=".PadRight(padRightNumber), toReturn.CorporateEntity.Id.ToString()));
                Console.WriteLine(string.Concat("BillingAccount.CorporateEntity.DisplayAs=".PadRight(padRightNumber), toReturn.CorporateEntity.DisplayAs ?? ""));
            }

            if (toReturn.Address != null)
            {
                Console.WriteLine(string.Concat("BillingAccount.Address.Id=".PadRight(padRightNumber), toReturn.Address.Id.ToString()));
                Console.WriteLine(string.Concat("BillingAccount.Address.Country=".PadRight(padRightNumber), toReturn.Address.Country ?? ""));
                Console.WriteLine(string.Concat("BillingAccount.Address.State=".PadRight(padRightNumber), toReturn.Address.State ?? ""));
                Console.WriteLine(string.Concat("BillingAccount.Address.City=".PadRight(padRightNumber), toReturn.Address.City ?? ""));
                Console.WriteLine(string.Concat("BillingAccount.Address.Street=".PadRight(padRightNumber), toReturn.Address.Street ?? ""));
            }

            Console.WriteLine(string.Concat("BillingAccount.IsInactive=".PadRight(padRightNumber), toReturn.IsInactive));
            Console.WriteLine(string.Concat("BillingAccount.PayDayWeekday=".PadRight(padRightNumber), toReturn.PayDayWeekday));
            Console.WriteLine(string.Concat("BillingAccount.PayDayNumber=".PadRight(padRightNumber), toReturn.PayDayNumber));

            if (toReturn.BillingContact != null)
            {
                Console.WriteLine(string.Concat("BillingAccount.BillingContact.Id=".PadRight(padRightNumber), toReturn.BillingContact.Id.ToString()));
                Console.WriteLine(string.Concat("BillingAccount.BillingContact.DisplayAs=".PadRight(padRightNumber), toReturn.BillingContact.DisplayAs ?? ""));
            }

            Console.WriteLine();

            return toReturn;
        }

        public static CorrigoEntity[] RetrieveMultiple(CorrigoService service)
        {
            var list = service.RetrieveMultiple(
                   new QueryByProperty
                   {
                       EntityType = EntityType.BillingAccount,
                       PropertySet = new AllProperties(),
                       Conditions = new PropertyValuePair[0]
                   });
            Console.WriteLine();
            Console.WriteLine("BillingAccounts: Retrieve Multiple");

            if (list == null || list.Length == 0) return list;

            Console.WriteLine(string.Concat("#".PadLeft(4), "|", "Id".PadLeft(4), "|", "Inactive".PadRight(8), "|", "DisplayAs".PadRight(50), "|", "Number".PadRight(10)));

            int i = 0;
            foreach (BillingAccount item in list)
            {
                i++;
                Console.WriteLine(string.Concat(i.ToString().PadLeft(4), "|", item.Id.ToString().PadLeft(4), "|", item.IsInactive.ToString().PadRight(8), "|", item.DisplayAs.PadRight(50), "|", item.Number.ToString().PadRight(10)));
            }

            Console.WriteLine();
            return list;
        }

        public static CorrigoEntity[] RetrieveByQuery(CorrigoService service)
        {
            var list = service.RetrieveMultiple(
                new QueryExpression
                {
                    EntityType = EntityType.BillingAccount,
                    PropertySet = new AllProperties(),
                    Criteria = new FilterExpression
                    {
                        Conditions = new ConditionExpression[]
                        {
                            new ConditionExpression
                            {
                                PropertyName = "IsInactive",
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
            Console.WriteLine();
            Console.WriteLine("BillingAccounts: Retrieve by query - IsInactive");

            if (list == null || list.Length == 0) return list;

            Console.WriteLine(string.Concat("#".PadLeft(4), "|", "Id".PadLeft(4), "|", "Inactive".PadRight(8), "|", "DisplayAs".PadRight(50), "|", "Number".PadRight(10)));

            int i = 0;
            foreach (BillingAccount item in list)
            {
                i++;
                Console.WriteLine(string.Concat(i.ToString().PadLeft(4), "|", item.Id.ToString().PadLeft(4), "|", item.IsInactive.ToString().PadRight(8), "|", item.DisplayAs.PadRight(50), "|", item.Number.ToString().PadRight(10)));
            }

            Console.WriteLine();
            return list;
        }

    }
}
