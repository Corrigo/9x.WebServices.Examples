using CorrigoServiceWebReference.CorrigoGA;
using System;
using System.Linq;

namespace _9x.WebServices.WoActionLogs.Operations
{
	internal static class Read
	{

        public static WoActionLog Retrieve(CorrigoService service, int id)
        {
            Console.WriteLine();
            Console.WriteLine($"Retrieve WoActionLog with id={id}");
            CorrigoEntity result = null;

            try
            {
                result = service.Retrieve(
                    new EntitySpecifier
                    {
                        EntityType = EntityType.WoActionLog,
                        Id = id
                    },
                    new PropertySet
                    {
                        Properties = new string[]
                        {
                            "Id",
                            "WorkOrderId",
                            "TypeId",
                            "Actor.*",
                            "ActionDate",
                            "Comment",
                            "ActionReasonId",
                            "UiTypeId",
                            "TimeZone",
                            "SystemDateUtc",
                            "ObjectId",
                            "Properties.*"
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

            WoActionLog toReturn = result as WoActionLog;

            if (toReturn == null)
            {
                Console.WriteLine("Retrieve failed");
                return null;
            }

            int padRightNumber = 45;

            Console.WriteLine(string.Concat("WoActionLog.Id=".PadRight(padRightNumber), toReturn.Id.ToString()));
            Console.WriteLine(string.Concat("WoActionLog.WorkOrderId=".PadRight(padRightNumber), toReturn.WorkOrderId.ToString()));

            var wo = service.Retrieve(new EntitySpecifier { EntityType = EntityType.WorkOrder, Id = toReturn.WorkOrderId }, new AllProperties()) as WorkOrder;
            if (wo != null) Console.WriteLine(string.Concat("WorkOrder.Number= ".PadRight(padRightNumber), $"'{wo.Number}'"));

            Console.WriteLine(string.Concat("WoActionLog.TypeId=".PadRight(padRightNumber), toReturn.TypeId.ToString()));

            if (toReturn.Actor != null)
            {
                Console.WriteLine(string.Concat("WoActionLog.Actor.Id=".PadRight(padRightNumber), toReturn.Actor.Id.ToString()));
                Console.WriteLine(string.Concat("WoActionLog.Actor.DisplayAs=".PadRight(padRightNumber), toReturn.Actor.DisplayAs ?? ""));
                Console.WriteLine(string.Concat("WoActionLog.Actor.TypeId=".PadRight(padRightNumber), toReturn.Actor.TypeId.ToString()));
            }

            Console.WriteLine(string.Concat("WoActionLog.ActionDate=".PadRight(padRightNumber), toReturn.ActionDate.ToString()));
            Console.WriteLine(string.Concat("WoActionLog.Comment=".PadRight(padRightNumber), toReturn.Comment ?? ""));
            Console.WriteLine(string.Concat("WoActionLog.ActionReasonId=".PadRight(padRightNumber), toReturn.ActionReasonId.ToString()));
            Console.WriteLine(string.Concat("WoActionLog.UiTypeId=".PadRight(padRightNumber), toReturn.UiTypeId.ToString()));
            Console.WriteLine(string.Concat("WoActionLog.TimeZone=".PadRight(padRightNumber), toReturn.TimeZone.ToString()));
            Console.WriteLine(string.Concat("WoActionLog.SystemDateUtc=".PadRight(padRightNumber), toReturn.SystemDateUtc.ToString()));
            Console.WriteLine(string.Concat("WoActionLog.ObjectId=".PadRight(padRightNumber), toReturn.ObjectId.ToString()));

            int i = 0;
            if (toReturn.Properties != null && toReturn.Properties.Length > 0)
            {
                foreach (var property in toReturn.Properties)
                {
                    Console.WriteLine(string.Concat($"WoActionLog.Properties[{i}].Id=".PadRight(padRightNumber), property.Id));
                    Console.WriteLine(string.Concat($"WoActionLog.Properties[{i}].TypeId=".PadRight(padRightNumber), property.TypeId));
                    Console.WriteLine(string.Concat($"WoActionLog.Properties[{i}].ValueStr=".PadRight(padRightNumber), property.ValueStr ?? ""));
                    Console.WriteLine(string.Concat($"WoActionLog.Properties[{i}].ValueInt=".PadRight(padRightNumber), property.ValueInt));
                    i++;
                }
            }

            Console.WriteLine();

            return toReturn;
        }

        public static CorrigoEntity[] RetrieveByQuery(CorrigoService service, CorrigoEntity[] workOrders)
        {
            if (workOrders == null || workOrders.Length == 0) return null;

            Console.WriteLine();
            Console.WriteLine("Retrieving Action Logs for Work Orders:");
            foreach (WorkOrder wo in workOrders )
            {
                Console.WriteLine($"WorkOrder.Id={wo.Id} WorkOrder.Number='{wo.Number}' WorkOrder.ContactName='{wo.ContactName}'");
            }

            object[] woIds = workOrders.Select(wo => wo.Id.ToString()).ToArray();

            var list = service.RetrieveMultiple(
                new QueryExpression
                {
                    EntityType = EntityType.WoActionLog,
                    PropertySet = new PropertySet
                    {
                        Properties = new string[]
                        {
                            "Id",
                            "WorkOrderId",
                            "TypeId",
                            "Actor.*",
                            "ActionDate",
                            "Comment",
                            "ActionReasonId",
                            "UiTypeId",
                            "TimeZone",
                            "SystemDateUtc",
                            "ObjectId",
                            "Properties.*"
                        }
                    },
                    //PropertySet = new AllProperties(),

                    Criteria = new FilterExpression
                    {
                        Conditions = new ConditionExpression[]
                        {
                            new ConditionExpression
                            {
                                PropertyName = "WorkOrderId",
                                Operator = ConditionOperator.In,
                                Values = woIds
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
            Console.WriteLine($"WoActionLogs: Retrieve by query - {workOrders.Length} latest Work Orders");

            if (list == null || list.Length == 0) return list;

            Console.WriteLine(string.Concat("#".PadLeft(4), "|", "Id".PadLeft(4), "|", "WorkOrderId".PadRight(11), "|",
                "WO Number".PadRight(15), "|",
                "TypeId".PadRight(11), "|", "Actor".PadRight(12), "|", "ActionDate".PadRight(22), "|", "Comment".PadRight(40)));

            int i = 0;
            foreach (WoActionLog item in list)
            {
                i++;
                Console.WriteLine(string.Concat(i.ToString().PadLeft(4), "|", item.Id.ToString().PadLeft(4), "|", item.WorkOrderId.ToString().PadRight(11), "|",
                    (workOrders.Where(wo => wo.Id == item.WorkOrderId).Select(wo => ((WorkOrder)wo).Number).ToArray())[0].PadRight(15), "|",
                    item.TypeId.ToString().PadRight(11), "|", item.Actor.DisplayAs.PadRight(12), "|", item.ActionDate.ToString().PadRight(22), "|", item.Comment.PadRight(40)));
            }


            Console.WriteLine();
            return list;
        }

    }
}
