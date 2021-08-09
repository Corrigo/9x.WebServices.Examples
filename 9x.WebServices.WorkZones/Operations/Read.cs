using CorrigoServiceWebReference.CorrigoGA;
using System;

namespace _9x.WebServices.WorkZones.Operations
{
	internal static class Read
	{
	    public static WorkZone Retrieve(CorrigoService service, int id)
	    {
	        Console.WriteLine();
	        Console.WriteLine($"Retrieve WorkZone with id={id}");
	        CorrigoEntity result = null;

	        try
	        {
	            result = service.Retrieve(
	                new EntitySpecifier
	                {
	                    EntityType = EntityType.WorkZone,
	                    Id = id
	                },
	                new PropertySet
	                {
	                    Properties = new string[]
	                    {
	                        "Id",
	                        "DisplayAs",
	                        "TimeZone",
                            "AccessOptionsMask",
                            "AdvanceNotice",
                            "Asset.*",
                            "AutoAssignEnabled",
                            "BackupRoutingId",
                            "BillingAccount.*",
                            "BizHours.*",
                            "ContactAddresses.*",
                            "Contract.*",
                            "CpThemeId",
                            "CurrencyTypeId",
                            "CustomFields.*",
                            "DefaultAccess",
                            "Entity",
                            "EscalationRules.*",
                            "IsOffline",
                            "LanguageId",
                            "NoIncompleteItem",
                            //"NoIncompletePunchList",
                            //WorkZone entity does not expose "NoIncompletePunchList" property.
                            "Number",
                            "OnCallRules.*",
                            "Portfolios.*",
                            "Responsibilities.*",
                            "RoundApptTimeTo",
                            "SchedulingWindow",
                            "SpecDispatchRules.*",
                            "TaxRegion.*",
                            "Teams.*",
                            "UiShowProvidersFirst",
                            "UseBizHours",
                            "UseEscalation",
                            "UseHolidays",
                            "UseOnCall",
                            "WoNumberDigits",
                            "WoNumberPrefix",
                            "WorkPlanAutoCancel",
                            "WorkPlanAutoDependency",
                            "WorkPlanChildResolution",


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

	        WorkZone toReturn = result as WorkZone;

	        if (toReturn == null)
	        {
	            Console.WriteLine("Retrieve failed");
	            return null;
	        }

	        int padRightNumber = 35;

	        Console.WriteLine(string.Concat("WorkZone.Id=".PadRight(padRightNumber), toReturn.Id.ToString()));
	        Console.WriteLine(string.Concat("WorkZone.DisplayAs=".PadRight(padRightNumber), toReturn.DisplayAs ?? ""));
            Console.WriteLine(string.Concat("WorkZone.Asset.Id=".PadRight(padRightNumber), toReturn.Asset?.Id.ToString() ?? ""));
            Console.WriteLine(string.Concat("WorkZone.TimeZone=".PadRight(padRightNumber), toReturn.TimeZone.ToString()));



            Console.WriteLine(string.Concat("WorkZone.Number=".PadRight(padRightNumber), toReturn.Number ?? ""));


            if (toReturn.Contract != null)
            {
                Console.WriteLine(string.Concat("WorkZone.Contract.Id=".PadRight(padRightNumber),
                    toReturn.Contract.Id.ToString()));
                Console.WriteLine(string.Concat("WorkZone.Contract.DisplayAs=".PadRight(padRightNumber),
                    toReturn.Contract.DisplayAs));
            }

            if (toReturn.TaxRegion != null)
            {
                Console.WriteLine(string.Concat("WorkZone.TaxRegion.Id=".PadRight(padRightNumber),
                    toReturn.TaxRegion.Id.ToString()));
                Console.WriteLine(string.Concat("WorkZone.TaxRegion.DisplayAs=".PadRight(padRightNumber),
                    toReturn.TaxRegion.DisplayAs));
            }

            Console.WriteLine();

            return toReturn;
        }

        public static CorrigoEntity[] RetrieveMultiple(CorrigoService service)
        {
            var list = service.RetrieveMultiple(
                new QueryByProperty
                {
                    EntityType = EntityType.WorkZone,
                    //PropertySet = new AllProperties(),
                    PropertySet = new PropertySet
                    {
                        Properties = new string[]
                        {
                            "Id",
                            "DisplayAs",
                            "TimeZone",
                            "Contract.*",
                            "TaxRegion.*",
                            "Asset.*",
                            "Number"
                        }
                    },
                    Conditions = new PropertyValuePair[0]
                });
            Console.WriteLine();
            Console.WriteLine("WorkZones: Retrieve Multiple");

            if (list == null || list.Length == 0) return list;

            Console.WriteLine(string.Concat("#".PadLeft(4), "|", "Id".PadLeft(4), "|", "DisplayAs".PadRight(50), "|", "Asset.Id".PadRight(10), "|", "TimeZone".PadRight(10), "|", "Number".PadRight(40), "|", "Contract.Id".PadRight(20), "|", "TaxRegion.Id".PadRight(20)));

            int i = 0;
            foreach (WorkZone item in list)
            {
                i++;
                Console.WriteLine(string.Concat(i.ToString().PadLeft(4), "|", item.Id.ToString().PadLeft(4),  "|", item.DisplayAs.PadRight(50), "|", item.Asset.Id.ToString().PadRight(10), "|", item.TimeZone.ToString().PadRight(10), "|", (item.Number??"").PadRight(40), "|", item.Contract?.Id.ToString().PadRight(20), "|", (item.TaxRegion == null? "" : item.TaxRegion.Id.ToString()).PadRight(20)));
            }

            Console.WriteLine();
            return list;
        }

        public static CorrigoEntity[] RetrieveByQuery(CorrigoService service)
        {
            var list = service.RetrieveMultiple(
                new QueryExpression
                {
                    EntityType = EntityType.WorkZone,
                    PropertySet = new PropertySet
                    {
                        Properties = new string[]
                        {
                            "Id",
                            "DisplayAs",
                            "TimeZone",
                            "Contract.*",
                            "TaxRegion.*",
                            "Asset.*",
                            "Number"
                        }
                    },
                    Criteria = new FilterExpression
                    {
                        Conditions = new ConditionExpression[]
                        {
                            new ConditionExpression
                            {
                                PropertyName = "Contract.Id",
                                Operator = ConditionOperator.GreaterThan,
                                Values = new object[] {0}
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
            Console.WriteLine("WorkZones: Retrieve by query - having contract");

            if (list == null || list.Length == 0) return list;

            Console.WriteLine(string.Concat("#".PadLeft(4), "|", "Id".PadLeft(4), "|", "DisplayAs".PadRight(50), "|", "Asset.Id".PadRight(10), "|", "TimeZone".PadRight(10), "|", "Number".PadRight(40), "|", "Contract.Id".PadRight(20), "|", "TaxRegion.Id".PadRight(20)));

            int i = 0;
            foreach (WorkZone item in list)
            {
                i++;
                Console.WriteLine(string.Concat(i.ToString().PadLeft(4), "|", item.Id.ToString().PadLeft(4), "|", item.DisplayAs.PadRight(50), "|", item.Asset.Id.ToString().PadRight(10), "|", item.TimeZone.ToString().PadRight(10), "|", (item.Number ?? "").PadRight(40), "|", item.Contract?.Id.ToString().PadRight(20), "|", (item.TaxRegion == null ? "" : item.TaxRegion.Id.ToString()).PadRight(20)));
            }

            Console.WriteLine();
            return list;
        }

    }
}
