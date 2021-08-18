using CorrigoServiceWebReference.CorrigoGA;
using System;

namespace _9x.WebServices.Documents.Operations
{
	internal static class Read
	{
        public static Document Retrieve(CorrigoService service, int id)
        {
            Console.WriteLine();
            Console.WriteLine($"Retrieve Document with id={id}");
            CorrigoEntity result = null;

            try
            {
                result = service.Retrieve(
                    new EntitySpecifier
                    {
                        EntityType = EntityType.Document,
                        Id = id
                    },
                    new PropertySet
                    {
                        Properties = new string[]
                        {
                            "Id",
                            "ActorId",
                            "ActorTypeId",
                            "Description",
                            "Title",
                            "DocType.*",
                            "EndDate",
                            "StartDate",
                            "UpdatedDate",
                            "ExtensionId",                            
                            "MimeType",
                            "UpdatedBy.*",
                            "WonId",
                            "WonMemberId",
                            "Blob.*",
                            "DocUrl",
                            "IsPublic",
                            "IsShared",
                            "StorageTypeId"
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

            Document toReturn = result as Document;

            if (toReturn == null)
            {
                Console.WriteLine("Retrieve failed");
                return null;
            }

            int padRightNumber = 35;

            Console.WriteLine(string.Concat("Document.Id=".PadRight(padRightNumber), toReturn.Id.ToString()));
            Console.WriteLine(string.Concat("Document.ActorId=".PadRight(padRightNumber), toReturn.ActorId.ToString()));
            Console.WriteLine(string.Concat("Document.ActorTypeId=".PadRight(padRightNumber), toReturn.ActorTypeId.ToString()));
            Console.WriteLine(string.Concat("Document.Description=".PadRight(padRightNumber), toReturn.Description ?? ""));
            Console.WriteLine(string.Concat("Document.Title=".PadRight(padRightNumber), toReturn.Title ?? ""));

            if (toReturn.DocType != null)
            {
                Console.WriteLine(string.Concat("Document.DocType.DisplayAs=".PadRight(padRightNumber), toReturn.DocType.DisplayAs ?? ""));
                Console.WriteLine(string.Concat("Document.DocType.Description=".PadRight(padRightNumber), toReturn.DocType.Description ?? ""));
            }

            Console.WriteLine(string.Concat("Document.EndDate=".PadRight(padRightNumber), toReturn.EndDate?.ToString() ?? ""));
            Console.WriteLine(string.Concat("Document.StartDate=".PadRight(padRightNumber), toReturn.StartDate.ToString()));
            Console.WriteLine(string.Concat("Document.UpdatedDate=".PadRight(padRightNumber), toReturn.UpdatedDate.ToString()));
            Console.WriteLine(string.Concat("Document.ExtensionId=".PadRight(padRightNumber), toReturn.ExtensionId.ToString()));
            Console.WriteLine(string.Concat("Document.StorageTypeId=".PadRight(padRightNumber), toReturn.StorageTypeId.ToString()));
            Console.WriteLine(string.Concat("Document.MimeType=".PadRight(padRightNumber), toReturn.MimeType.ToString()));


            if (toReturn.UpdatedBy != null)
            {
                Console.WriteLine(string.Concat("Document.UpdatedBy.DisplayAs=".PadRight(padRightNumber), toReturn.UpdatedBy.DisplayAs ?? ""));
                Console.WriteLine(string.Concat("Document.UpdatedBy.Id=".PadRight(padRightNumber), toReturn.UpdatedBy.Id.ToString()));
            }

            Console.WriteLine(string.Concat("Document.WonId=".PadRight(padRightNumber), toReturn.WonId.ToString()));
            Console.WriteLine(string.Concat("Document.WonMemberId=".PadRight(padRightNumber), toReturn.WonMemberId.ToString()));
           

            if (toReturn.Blob != null)
            {
                Console.WriteLine(string.Concat("Document.Blob.Id=".PadRight(padRightNumber), toReturn.Blob.Id.ToString()));
                Console.WriteLine(string.Concat("Document.Blob.FileName=".PadRight(padRightNumber), toReturn.Blob.FileName ?? ""));                
            }

            Console.WriteLine(string.Concat("Document.DocUrl=".PadRight(padRightNumber), toReturn.DocUrl));
            Console.WriteLine(string.Concat("Document.IsPublic=".PadRight(padRightNumber), toReturn.IsPublic));
            Console.WriteLine(string.Concat("Document.IsShared=".PadRight(padRightNumber), toReturn.IsShared));
            Console.WriteLine(string.Concat("Document.StorageTypeId=".PadRight(padRightNumber), toReturn.StorageTypeId));

            Console.WriteLine();

            return toReturn;
        }

        public static CorrigoEntity[] RetrieveMultiple(CorrigoService service, int count = 10)
        {
            var list = service.RetrieveMultiple(
                new QueryByProperty
                {
                    EntityType = EntityType.Document,
                    //PropertySet = new AllProperties(),
                    PropertySet = new PropertySet
                    {
                        Properties = new string[]
                        {
                            "Id",
                            "ActorId",
                            "ActorTypeId",
                            "Description",
                            "Title",
                            "DocType.*",
                            "EndDate",
                            "StartDate",
                            "UpdatedDate",
                            "ExtensionId",
                            "StorageTypeId",
                            "MimeType",
                            "UpdatedBy.*",
                            "WonId",
                            "WonMemberId",
                            "Blob.*"
                        }
                    },
                    Count = count,
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
            Console.WriteLine("Documents: Retrieve Multiple");

            if (list == null || list.Length == 0) return list;

            Console.WriteLine(string.Concat("#".PadLeft(4), "|", "Id".PadLeft(4), "|", "ActorId".PadLeft(7), "|", "Title".PadRight(40), "|", "Description".PadRight(55), "|", "DocType.DisplayAs".PadRight(17), "|", "MimeType".PadRight(45), "|", "ActorTypeId".PadRight(15), "|", "File Name".PadRight(35)));

            int i = 0;
            foreach (Document item in list)
            {
                i++;
                Console.WriteLine(string.Concat(i.ToString().PadLeft(4), "|", item.Id.ToString().PadLeft(4), "|", item.ActorId.ToString().PadLeft(7), "|", item.Title.PadRight(40), "|", item.Description.PadRight(55), "|", item.DocType.DisplayAs.PadRight(17), "|", item.MimeType.PadRight(45), "|", item.ActorTypeId.ToString().PadRight(15), "|", item.Blob?.FileName ?? ""));
            }

            Console.WriteLine();
            return list;
        }

        public static CorrigoEntity[] RetrieveByQuery(CorrigoService service)
        {
            var list = service.RetrieveMultiple(
                new QueryExpression
                {
                    EntityType = EntityType.Document,
                    //PropertySet = new AllProperties(),
                    PropertySet = new PropertySet
                    {
                        Properties = new string[]
                        {
                            "Id",
                            "ActorId",
                            "ActorTypeId",
                            "Description",
                            "Title",
                            "DocType.*",
                            "EndDate",
                            "StartDate",
                            "UpdatedDate",
                            "ExtensionId",
                            "StorageTypeId",
                            "MimeType",
                            "UpdatedBy.*",
                            "WonId",
                            "WonMemberId",
                            "Blob.*"
                        }
                    },
                    Criteria = new FilterExpression
                    {
                        Conditions = new ConditionExpression[]
                        {
                            new ConditionExpression
                            {
                                PropertyName = "StorageTypeId",
                                Operator = ConditionOperator.Equal,
                                Values = new object[] { DocumentStorageType.URL}
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
            Console.WriteLine("Documents: Retrieve by query - Links");

            if (list == null || list.Length == 0) return list;

            Console.WriteLine(string.Concat("#".PadLeft(4), "|", "Id".PadLeft(4), "|", "ActorId".PadLeft(7), "|", "Title".PadRight(30), "|", "Description".PadRight(30), "|", "DocType.DisplayAs".PadRight(17), "|", "MimeType".PadRight(25), "|", "ActorTypeId".PadRight(15), "|", "File Name".PadRight(35)));

            int i = 0;
            foreach (Document item in list)
            {
                i++;
                Console.WriteLine(string.Concat(i.ToString().PadLeft(4), "|", item.Id.ToString().PadLeft(4), "|", item.ActorId.ToString().PadLeft(7), "|", item.Title.PadRight(30), "|", item.Description.PadRight(30), "|", item.DocType.DisplayAs.PadRight(17), "|", item.MimeType.PadRight(25), "|", item.ActorTypeId.ToString().PadRight(15), "|", item.Blob?.FileName ?? ""));
            }

            Console.WriteLine();
            return list;
        }

    }
}
