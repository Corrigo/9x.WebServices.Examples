using CorrigoServiceWebReference.CorrigoGA;
using System;

namespace _9x.WebServices.AssetTrees.Operations
{
	internal static class Read
	{
        public static AssetTree Retrieve(CorrigoService service, int childId, int parentId)
        {
            Console.WriteLine();
            Console.WriteLine($"Retrieve AssetTree with ChildId={childId} ParentId={parentId}");
            CorrigoEntity result = null;

            try
            {
                result = service.Retrieve(
                    new AssetTreeEntitySpecifier
                    {
                        ChildId = childId,
                        ParentId = parentId
                    },
                    //new PropertySet
                    //{
                    //    Properties = new string[]
                    //    {
                    //        "Id",
                    //        "ParentId",
                    //        "ChildId",
                    //        "Child.*",
                    //        //"Child.Address.*",
                    //        "Distance"
                    //    }
                    //}
                    new AllProperties()
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

            AssetTree toReturn = result as AssetTree;

            if (toReturn == null)
            {
                Console.WriteLine("Retrieve failed");
                return null;
            }

            int padRightNumber = 55;

            Console.WriteLine(string.Concat("AssetTree.Id=".PadRight(padRightNumber), toReturn.Id.ToString()));
            Console.WriteLine(string.Concat("AssetTree.ParentId=".PadRight(padRightNumber), toReturn.ParentId.ToString()));

            if (toReturn.Child != null)
            {
                Console.WriteLine(string.Concat("AssetTree.Child.Name=".PadRight(padRightNumber), toReturn.Child.Name??""));

                if (toReturn.Child.Address != null)
                {
                    Console.WriteLine(string.Concat("AssetTree.Child.Address.Country=".PadRight(padRightNumber), toReturn.Child.Address.Country ?? ""));
                    Console.WriteLine(string.Concat("AssetTree.Child.Address.City=".PadRight(padRightNumber), toReturn.Child.Address.City ?? ""));
                    Console.WriteLine(string.Concat("AssetTree.Child.Address.Street=".PadRight(padRightNumber), toReturn.Child.Address.Street ?? ""));
                }
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
                    EntityType = EntityType.AssetTree,
                    //PropertySet = new AllProperties(),
                    PropertySet = new PropertySet
                    {
                        Properties = new string[]
                        {
                            "Id",
                            "ParentId",
                            "ChildId",
                            "Child.*",
                            "Child.Address.*",
                            "Distance"
                        }
                    },
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
            Console.WriteLine($"AssetTrees: Retrieve Multiple Top {countNumber} asset trees");

            if (list == null || list.Length == 0) return list;

            Console.WriteLine(string.Concat("#".PadLeft(4), "|", "Id".PadLeft(4), "|", "ParentId".PadRight(10), "|", "ChildId".PadRight(10), "|", "Child.Name".PadRight(40), "|", "Child.Address.City".PadRight(40)));

            int i = 0;
            foreach (AssetTree item in list)
            {
                i++;
                Console.WriteLine(string.Concat(i.ToString().PadLeft(4), "|", item.Id.ToString().PadLeft(4), "|", item.ParentId.ToString().PadRight(10), "|", item.ChildId.ToString().PadRight(10), "|", item.Child.Name.PadRight(40), "|", item.Child.Address?.City??"".PadRight(40)));
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
                    EntityType = EntityType.AssetTree,
                    PropertySet = new PropertySet
                    {
                        Properties = new string[]
                        {
                            "Id",
                            "ParentId",
                            "ChildId",
                            "Child.*",
                            "Child.Address.*",
                            "Distance"
                        }
                    },
                    Criteria = new FilterExpression
                    {
                        Conditions = new ConditionExpression[]
                        {
                            new ConditionExpression
                            {
                                PropertyName = "Child.Address.City",
                                Operator = ConditionOperator.NotNull,
                                //Values = new object[] {"CA"}
                            },
                            new ConditionExpression
                            {
                                PropertyName = "Child.Address.City",
                                Operator = ConditionOperator.NotIn,
                                Values = new object[] {"", " ", "  ", "   " }
                            }
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
            Console.WriteLine($"AssetTrees: Retrieve by query - Top {countNumber} Child assets which reside in cities");

            if (list == null || list.Length == 0) return list;

            Console.WriteLine(string.Concat("#".PadLeft(4), "|", "Id".PadLeft(4), "|", "ParentId".PadRight(10), "|", "ChildId".PadRight(10), "|", "Child.Name".PadRight(40), "|", "Child.Address.City".PadRight(40)));

            int i = 0;
            foreach (AssetTree item in list)
            {
                i++;
                Console.WriteLine(string.Concat(i.ToString().PadLeft(4), "|", item.Id.ToString().PadLeft(4), "|", item.ParentId.ToString().PadRight(10), "|", item.ChildId.ToString().PadRight(10), "|", item.Child.Name.PadRight(40), "|", item.Child.Address?.City ?? "".PadRight(40)));
            }


            Console.WriteLine();
            return list;
        }

    }
}
