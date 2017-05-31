using CorrigoServiceWebReference.CorrigoGA;
using System;

namespace _9x.WebServices.Employees.Operations
{
	internal static class Read
	{
        public static Employee Retrieve(CorrigoService service, int id)
        {
            Console.WriteLine($"Retrieve Employee with id={id.ToString()}");
            CorrigoEntity result = null;

            try
            {
                result = service.Retrieve(
                    new EntitySpecifier
                    {
                        EntityType = EntityType.Employee,
                        Id = id
                    },
                    new PropertySet
                    {
                        Properties = new string[]
                        {
                            "FirstName",
                            "LastName",
                            "DisplayAs",
                            "Role.*",
                            "AccessToAllWorkZones",
                            "LanguageId",
                            "ActorTypeId",
                            "Username",
                            "DtPwdChange",
                            "ProviderInvitedOn",
                            "Instructions",
                            "WonMemberId",
                            "WonLocationId",
                            "WonServiceRadius",
                            "IsElectronicPayment",
                            "ProviderStatusId",
                            "LabelId",
                            "FreeTextAllowed",
                            "RadiusUnit",
                            "Password",
                            "Number",
                            "JobTitle",
                            "FederalId",
                            "ExternalId",
                            "ForcePasswordReset",
                            "DefaultPriceList.*",
                            "PriceLists.*",
                            "CustomFields.*", "CustomFields.Descriptor.*",
                            "Organization.*",
                            "ContactAddresses.*",
                            "Address.*",
                            "Teams.*",
                            "WorkZones.*",
                            "Portfolios.*",
                            "CustomerGroups.*",
                            "Specialties.*",
                            "PayRates.*",
                            "StockLocations.*",
                            "Services.*",
                            "AlertSubscriptions.*",                            
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

            Employee toReturn = result as Employee;

            if (toReturn == null)
            {
                Console.WriteLine("Retrieve failed");
                return null;
            }

            int padRightNumber = 45;

            Console.WriteLine(string.Concat("Employee.Id=".PadRight(padRightNumber), toReturn.Id.ToString()));
            Console.WriteLine(string.Concat("Employee.FirstName=".PadRight(padRightNumber), toReturn.FirstName ?? ""));
            Console.WriteLine(string.Concat("Employee.LastName=".PadRight(padRightNumber), toReturn.LastName ?? ""));
            Console.WriteLine(string.Concat("Employee.DisplayAs=".PadRight(padRightNumber), toReturn.DisplayAs ?? ""));

            if (toReturn.Role != null)
            {
                Console.WriteLine(string.Concat("Employee.Role.Id=".PadRight(padRightNumber), toReturn.Role.Id.ToString()));
                Console.WriteLine(string.Concat("Employee.Role.DisplayAs=".PadRight(padRightNumber), toReturn.Role.DisplayAs ?? ""));
            }

            Console.WriteLine(string.Concat("Employee.AccessToAllWorkZones=".PadRight(padRightNumber), toReturn.AccessToAllWorkZones));
            Console.WriteLine(string.Concat("Employee.LanguageId=".PadRight(padRightNumber), toReturn.LanguageId));
            Console.WriteLine(string.Concat("Employee.ActorTypeId=".PadRight(padRightNumber), toReturn.ActorTypeId));

            Console.WriteLine(string.Concat("Employee.Username=".PadRight(padRightNumber), toReturn.Username ?? ""));

            Console.WriteLine(string.Concat("Employee.DtPwdChange=".PadRight(padRightNumber), toReturn.DtPwdChange));
            Console.WriteLine(string.Concat("Employee.ProviderInvitedOn=".PadRight(padRightNumber), !toReturn.ProviderInvitedOn.HasValue? "" : toReturn.ProviderInvitedOn.Value.ToString()));

            Console.WriteLine(string.Concat("Employee.DisplayAs=".PadRight(padRightNumber), toReturn.DisplayAs ?? ""));
            Console.WriteLine(string.Concat("Employee.Instructions=".PadRight(padRightNumber), toReturn.Instructions ?? ""));
            Console.WriteLine(string.Concat("Employee.WonMemberId=".PadRight(padRightNumber), toReturn.WonMemberId));
            Console.WriteLine(string.Concat("Employee.WonLocationId=".PadRight(padRightNumber), toReturn.WonLocationId));
            Console.WriteLine(string.Concat("Employee.WonServiceRadius=".PadRight(padRightNumber), toReturn.WonServiceRadius));
            Console.WriteLine(string.Concat("Employee.IsElectronicPayment=".PadRight(padRightNumber), toReturn.IsElectronicPayment));
            Console.WriteLine(string.Concat("Employee.ProviderStatusId=".PadRight(padRightNumber), toReturn.ProviderStatusId));
            Console.WriteLine(string.Concat("Employee.LabelId=".PadRight(padRightNumber), toReturn.LabelId));
            Console.WriteLine(string.Concat("Employee.FreeTextAllowed=".PadRight(padRightNumber), toReturn.FreeTextAllowed));
            Console.WriteLine(string.Concat("Employee.RadiusUnit=".PadRight(padRightNumber), toReturn.RadiusUnit));
            Console.WriteLine(string.Concat("Employee.Password=".PadRight(padRightNumber), toReturn.Password ?? ""));
            Console.WriteLine(string.Concat("Employee.Number=".PadRight(padRightNumber), toReturn.Number ?? ""));
            Console.WriteLine(string.Concat("Employee.JobTitle=".PadRight(padRightNumber), toReturn.JobTitle ?? ""));
            Console.WriteLine(string.Concat("Employee.FederalId=".PadRight(padRightNumber), toReturn.FederalId ?? ""));
            Console.WriteLine(string.Concat("Employee.ExternalId=".PadRight(padRightNumber), toReturn.ExternalId ?? ""));
            Console.WriteLine(string.Concat("Employee.ForcePasswordReset=".PadRight(padRightNumber), toReturn.ForcePasswordReset));

            if (toReturn.DefaultPriceList != null)
            {
                Console.WriteLine(string.Concat("Employee.DefaultPriceList.Id=".PadRight(padRightNumber), toReturn.DefaultPriceList.Id));
                Console.WriteLine(string.Concat("Employee.DefaultPriceList.DisplayAs=".PadRight(padRightNumber), toReturn.DefaultPriceList.DisplayAs ?? ""));
            }

            int i = 0;
            if (toReturn.PriceLists != null && toReturn.PriceLists.Length > 0)
            {
                foreach (var price in toReturn.PriceLists)
                {
                    Console.WriteLine(string.Concat($"Employee.PriceLists[{i}].Id=".PadRight(padRightNumber), price.Id));
                    i++;
                }
            }

            if (toReturn.CustomFields != null && toReturn.CustomFields.Length > 0)
            {
                i = 0;
                foreach (var customField in toReturn.CustomFields)
                {
                    Console.WriteLine(string.Concat($"Employee.CustomFields[{i}].Id=".PadRight(padRightNumber), customField.Id));
                    if (customField.Descriptor != null) Console.WriteLine(string.Concat($"Employee.CustomFields[{i}].Descriptor.Name=".PadRight(padRightNumber), customField.Descriptor.Name ?? ""));
                    Console.WriteLine(string.Concat($"Employee.CustomFields[{i}].Value=".PadRight(padRightNumber), customField.Value ?? ""));
                    i++;
                }
            }

            if (toReturn.Organization != null)
            {
                Console.WriteLine(string.Concat("Employee.Organization.Id=".PadRight(padRightNumber), toReturn.Organization.Id));
                Console.WriteLine(string.Concat("Employee.Organization.DisplayAs=".PadRight(padRightNumber), toReturn.Organization.DisplayAs));
            }

            if (toReturn.ContactAddresses != null && toReturn.ContactAddresses.Length > 0)
            {
                i = 0;
                foreach (var address in toReturn.ContactAddresses)
                {
                    Console.WriteLine(string.Concat($"Employee.ContactAddresses[{i}].Id=".PadRight(padRightNumber), address.Id));
                    i++;
                }
            }
            
            if (toReturn.Address != null)
            {
                Console.WriteLine(string.Concat("Employee.Address.Id=".PadRight(padRightNumber), toReturn.Address.Id));
                Console.WriteLine(string.Concat("Employee.Address.City=".PadRight(padRightNumber), toReturn.Address.City ?? ""));
                Console.WriteLine(string.Concat("Employee.Address.Street=".PadRight(padRightNumber), toReturn.Address.Street ?? ""));
            }

            if (toReturn.Teams != null && toReturn.Teams.Length > 0)
            {
                i = 0;
                foreach (var team in toReturn.Teams)
                {
                    Console.WriteLine(string.Concat($"Employee.Teams[{i}].TeamId=".PadRight(padRightNumber), team.TeamId));
                    i++;
                }
            }

            if (toReturn.WorkZones != null && toReturn.WorkZones.Length > 0)
            {
                i = 0;
                foreach (var wz in toReturn.WorkZones)
                {
                    Console.WriteLine(string.Concat($"Employee.WorkZones[{i}].WorkZoneId=".PadRight(padRightNumber), wz.WorkZoneId));
                    i++;
                }
            }

            if (toReturn.Portfolios != null && toReturn.Portfolios.Length > 0)
            {
                i = 0;
                foreach (var portfolio in toReturn.Portfolios)
                {
                    Console.WriteLine(string.Concat($"Employee.Portfolios[{i}].PortfolioId=".PadRight(padRightNumber), portfolio.PortfolioId));
                    i++;
                }
            }

            if (toReturn.CustomerGroups != null && toReturn.CustomerGroups.Length > 0)
            {
                i = 0;
                foreach (var cg in toReturn.CustomerGroups)
                {
                    Console.WriteLine(string.Concat($"Employee.CustomerGroups[{i}].CustomerGroupId=".PadRight(padRightNumber), cg.CustomerGroupId));
                    i++;
                }
            }

            if (toReturn.Specialties != null && toReturn.Specialties.Length > 0)
            {
                i = 0;
                foreach (var sp in toReturn.Specialties)
                {
                    Console.WriteLine(string.Concat($"Employee.Specialties[{i}].SpecialtyId=".PadRight(padRightNumber), sp.SpecialtyId));
                    i++;
                }
            }

            if (toReturn.PayRates != null && toReturn.PayRates.Length > 0)
            {
                i = 0;
                foreach (var pr in toReturn.PayRates)
                {
                    Console.WriteLine(string.Concat($"Employee.PayRates[{i}].LaborCodeId=".PadRight(padRightNumber), pr.LaborCodeId));
                    i++;
                }
            }

            if (toReturn.StockLocations != null && toReturn.StockLocations.Length > 0)
            {
                i = 0;
                foreach (var sl in toReturn.StockLocations)
                {
                    Console.WriteLine(string.Concat($"Employee.StockLocations[{i}].StockLocationId=".PadRight(padRightNumber), sl.StockLocationId));                    
                    i++;
                }
            }

            if (toReturn.Services != null && toReturn.Services.Length > 0)
            {
                i = 0;
                foreach (var srv in toReturn.Services)
                {
                    Console.WriteLine(string.Concat($"Employee.Services[{i}].ServiceId=".PadRight(padRightNumber), srv.ServiceId));
                    i++;
                }
            }

            if (toReturn.AlertSubscriptions != null && toReturn.AlertSubscriptions.Length > 0)
            {
                i = 0;
                foreach (var alertSubcription in toReturn.AlertSubscriptions)
                {
                    Console.WriteLine(string.Concat($"Employee.AlertSubscriptions[{i}].AlertTypeId=".PadRight(padRightNumber), alertSubcription.AlertTypeId));
                    i++;
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
                       EntityType = EntityType.Employee,
                       PropertySet = new AllProperties(),
                       Conditions = new PropertyValuePair[0]
                   });
            Console.WriteLine("Employees: Retrieve Multiple");

            if (list == null || list.Length == 0) return list;

            Console.WriteLine(string.Concat("#".PadLeft(4), "|", "Id".PadLeft(4), "|", "DisplayAs".PadRight(50), "|", "Username".PadRight(25), "|", "JobTitle".PadRight(15), "|", "Number".PadRight(10)));

            int i = 0;
            foreach (Employee item in list)
            {
                i++;
                Console.WriteLine(string.Concat(i.ToString().PadLeft(4), "|", item.Id.ToString().PadLeft(4), "|", item.DisplayAs.PadRight(50), "|", item.Username.PadLeft(25), "|", item.JobTitle.PadLeft(15), "|", item.Number.ToString().PadLeft(10)));
            }

            Console.WriteLine();
            return list;
        }

        public static CorrigoEntity[] RetrieveByQuery(CorrigoService service)
        {
            var list = service.RetrieveMultiple(
                new QueryExpression
                {
                    EntityType = EntityType.Employee,
                    PropertySet = new AllProperties(),
                    Criteria = new FilterExpression
                    {
                        Conditions = new ConditionExpression[]
                        {
                            new ConditionExpression
                            {
                                PropertyName = "IsRemoved",
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
            Console.WriteLine("Employees: Retrieve by query - Removed");

            if (list == null || list.Length == 0) return list;

            Console.WriteLine(string.Concat("#".PadLeft(4), "|", "Id".PadLeft(4), "|", "DisplayAs".PadRight(50), "|", "Username".PadRight(25), "|", "JobTitle".PadRight(15), "|", "Number".PadRight(10)));

            int i = 0;
            foreach (Employee item in list)
            {
                i++; 
                Console.WriteLine(string.Concat(i.ToString().PadLeft(4), "|", item.Id.ToString().PadLeft(4), "|", item.DisplayAs.PadRight(50), "|", item.Username.PadLeft(25), "|", item.JobTitle.PadLeft(15), "|", item.Number.ToString().PadLeft(10)));
            }

            Console.WriteLine();
            return list;
        }

    }
}
