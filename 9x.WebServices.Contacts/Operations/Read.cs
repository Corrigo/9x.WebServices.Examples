using CorrigoServiceWebReference.CorrigoGA;
using System;

namespace _9x.WebServices.Contacts.Operations
{
	internal static class Read
	{
        public static Contact Retrieve(CorrigoService service, int id)
        {
            Console.WriteLine();
            Console.WriteLine($"Retrieve Contact with id={id}");
            CorrigoEntity result = null;

            try
            {
                result = service.Retrieve(
                    new EntitySpecifier
                    {
                        EntityType = EntityType.Contact,
                        Id = id
                    },
                    new PropertySet
                    {
                        Properties = new string[]
                        {
                            "Id",
                            "DisplayAs",
                            "FirstName",
                            "LastName",
                            "TypeId",
                            "CustomerId",
                            "CanViewAnyRequest",
                            "CanCreateRequest ",
                            "PriorityThreshold",
                            "CustomFields.*",
                            "ContactAddresses.*",
                            "GroupsBridge.*",
                            "Username",
                            "Number",
                            "MustResetPassword",
                            "NoAlertEmails",
                            "Comment",
                            "Currencies.*",
                            "UnlimitedAuthorization",
                            "UnlimitedRequest"
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

            Contact toReturn = result as Contact;

            if (toReturn == null)
            {
                Console.WriteLine("Retrieve failed");
                return null;
            }

            int padRightNumber = 35;

            Console.WriteLine(string.Concat("Contact.Id=".PadRight(padRightNumber), toReturn.Id.ToString()));
            Console.WriteLine(string.Concat("Contact.DisplayAs=".PadRight(padRightNumber), toReturn.DisplayAs ?? ""));
            Console.WriteLine(string.Concat("Contact.FirstName=".PadRight(padRightNumber), toReturn.FirstName ?? ""));
            Console.WriteLine(string.Concat("Contact.LastName=".PadRight(padRightNumber), toReturn.LastName ?? ""));
            Console.WriteLine(string.Concat("Contact.TypeId=".PadRight(padRightNumber), toReturn.TypeId.ToString()));
            Console.WriteLine(string.Concat("Contact.CustomerId=".PadRight(padRightNumber), toReturn.CustomerId.ToString()));
            Console.WriteLine(string.Concat("Contact.CanViewAnyRequest=".PadRight(padRightNumber), toReturn.CanViewAnyRequest.ToString()));
            Console.WriteLine(string.Concat("Contact.CanCreateRequest =".PadRight(padRightNumber), toReturn.CanCreateRequest.ToString()));
            Console.WriteLine(string.Concat("Contact.PriorityThreshold=".PadRight(padRightNumber), toReturn.PriorityThreshold.ToString()));

            if (toReturn.CustomFields != null && toReturn.CustomFields.Length > 0)
            {
                for (int i=0; i < toReturn.CustomFields.Length; i++)
                {
                    Console.WriteLine(string.Concat($"Contact.CustomFields[{i}].Value=".PadRight(padRightNumber), toReturn.CustomFields[i].Value ?? ""));
                }
            }

            if (toReturn.ContactAddresses != null && toReturn.ContactAddresses.Length > 0)
            {
                for (int i = 0; i < toReturn.ContactAddresses.Length; i++)
                {
                    Console.WriteLine(string.Concat($"Contact.ContactAddresses[{i}].Id=".PadRight(padRightNumber), toReturn.ContactAddresses[i].Id.ToString() ?? ""));
                }
            }

            if (toReturn.GroupsBridge != null && toReturn.GroupsBridge.Length > 0)
            {
                for (int i = 0; i < toReturn.GroupsBridge.Length; i++)
                {
                    Console.WriteLine(string.Concat($"Contact.GroupsBridge[{i}].Id=".PadRight(padRightNumber), toReturn.GroupsBridge[i].Id.ToString() ?? ""));
                }
            }

            Console.WriteLine(string.Concat("Contact.Username=".PadRight(padRightNumber), toReturn.Username ?? ""));
            Console.WriteLine(string.Concat("Contact.Number=".PadRight(padRightNumber), toReturn.Number ?? ""));
            Console.WriteLine(string.Concat("Contact.MustResetPassword=".PadRight(padRightNumber), toReturn.MustResetPassword.ToString()));
            Console.WriteLine(string.Concat("Contact.NoAlertEmails=".PadRight(padRightNumber), toReturn.NoAlertEmails.ToString()));
            Console.WriteLine(string.Concat("Contact.Comment=".PadRight(padRightNumber), toReturn.Comment ?? ""));


            if (toReturn.Currencies != null && toReturn.Currencies.Length > 0)
            {
                for (int i = 0; i < toReturn.Currencies.Length; i++)
                {
                    Console.WriteLine(string.Concat($"Contact.Currencies[{i}].AuthorizationLimit=".PadRight(padRightNumber), toReturn.Currencies[i].AuthorizationLimit?.ToString() ?? ""));
                    Console.WriteLine(string.Concat($"Contact.Currencies[{i}].NotificationThreshold=".PadRight(padRightNumber), toReturn.Currencies[i].NotificationThreshold?.ToString() ?? ""));
                    Console.WriteLine(string.Concat($"Contact.Currencies[{i}].RequestLimit=".PadRight(padRightNumber), toReturn.Currencies[i].RequestLimit?.ToString() ?? ""));
                }
            }

            Console.WriteLine(string.Concat("Contact.UnlimitedAuthorization=".PadRight(padRightNumber), toReturn.UnlimitedAuthorization.ToString()));
            Console.WriteLine(string.Concat("Contact.UnlimitedRequest=".PadRight(padRightNumber), toReturn.UnlimitedRequest.ToString()));

            Console.WriteLine();

            return toReturn;
        }

        public static CorrigoEntity[] RetrieveMultiple(CorrigoService service)
        {
            var list = service.RetrieveMultiple(
                new QueryByProperty
                {
                    EntityType = EntityType.Contact,
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
            Console.WriteLine("Contacts: Retrieve Multiple");

            if (list == null || list.Length == 0) return list;

            Console.WriteLine(string.Concat("#".PadLeft(4), "|", "Id".PadLeft(4), "|", "DisplayAs".PadRight(40), "|", "Username".PadRight(40), "|", "Number".PadRight(15)));

            int i = 0;
            foreach (Contact item in list)
            {
                i++;
                Console.WriteLine(string.Concat(i.ToString().PadLeft(4), "|", item.Id.ToString().PadLeft(4), "|", item.DisplayAs.PadRight(40), "|", item.Username.PadRight(40), "|", item.Number.PadRight(15)));
            }

            Console.WriteLine();
            return list;
        }

        public static CorrigoEntity[] RetrieveByQuery(CorrigoService service)
        {
            var list = service.RetrieveMultiple(
                new QueryExpression
                {
                    EntityType = EntityType.Contact,
                    PropertySet = new AllProperties(),
                    Criteria = new FilterExpression
                    {
                        Conditions = new ConditionExpression[]
                        {
                            new ConditionExpression
                            {
                                PropertyName = "DisplayAs",
                                Operator = ConditionOperator.Like,
                                Values = new object[] { "%(deleted)%" }
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
            Console.WriteLine("Contacts: Retrieve by query - (deleted)");

            if (list == null || list.Length == 0) return list;

            Console.WriteLine(string.Concat("#".PadLeft(4), "|", "Id".PadLeft(4), "|", "DisplayAs".PadRight(40), "|", "Username".PadRight(40), "|", "Number".PadRight(15)));

            int i = 0;
            foreach (Contact item in list)
            {
                i++;
                Console.WriteLine(string.Concat(i.ToString().PadLeft(4), "|", item.Id.ToString().PadLeft(4), "|", item.DisplayAs.PadRight(40), "|", item.Username.PadRight(40), "|", item.Number.PadRight(15)));
            }


            Console.WriteLine();
            return list;
        }

    }
}
