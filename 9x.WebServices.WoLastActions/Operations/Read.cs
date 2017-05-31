using CorrigoServiceWebReference.CorrigoGA;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _9x.WebServices.WoLastActions.Operations
{
	internal static class Read
	{

        public static WoLastAction Retrieve(CorrigoService service, int id)
        {
            Console.WriteLine();
            Console.WriteLine($"Retrieve WoLastAction with id={id}");
            CorrigoEntity result = null;

            try
            {
                result = service.Retrieve(
                    new EntitySpecifier
                    {
                        EntityType = EntityType.WoLastAction,
                        Id = id
                    },
                    new PropertySet
                    {
                        Properties = new string[]
                        {
                            "*",
                            "LastAction.*","LastAction.Actor.*",
                            "EmergencyReason.*",
                            "Reason.*",
                            "Invoice.*",
                            "BilledTotal.*",
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

            WoLastAction toReturn = result as WoLastAction;

            if (toReturn == null)
            {
                Console.WriteLine("Retrieve failed");
                return null;
            }

            int padRightNumber = 45;

            Console.WriteLine(string.Concat("WoLastAction.Id=".PadRight(padRightNumber), toReturn.Id.ToString()));
            Console.WriteLine(string.Concat("WoLastAction.WorkOrderId=".PadRight(padRightNumber), toReturn.WorkOrderId.ToString()));

            var wo = service.Retrieve(new EntitySpecifier { EntityType = EntityType.WorkOrder, Id = toReturn.WorkOrderId }, new AllProperties()) as WorkOrder;
            if (wo != null) Console.WriteLine(string.Concat("WorkOrder.Number= ".PadRight(padRightNumber), $"'{wo.Number}'"));
            
            if (toReturn.EmergencyReason != null)
            {
                Console.WriteLine(string.Concat("WoLastAction.EmergencyReason.Id=".PadRight(padRightNumber), toReturn.EmergencyReason.Id.ToString()));
                Console.WriteLine(string.Concat("WoLastAction.EmergencyReason.DisplayAs=".PadRight(padRightNumber), toReturn.EmergencyReason.DisplayAs ?? ""));
                Console.WriteLine(string.Concat("WoLastAction.EmergencyReason.ReasonId=".PadRight(padRightNumber), toReturn.EmergencyReason.ReasonId.ToString()));
            }

            if (toReturn.Reason != null)
            {
                Console.WriteLine(string.Concat("WoLastAction.Reason.Id=".PadRight(padRightNumber), toReturn.Reason.Id.ToString()));
                Console.WriteLine(string.Concat("WoLastAction.Reason.DisplayAs=".PadRight(padRightNumber), toReturn.Reason.DisplayAs ?? ""));
                Console.WriteLine(string.Concat("WoLastAction.Reason.ReasonId=".PadRight(padRightNumber), toReturn.Reason.ReasonId.ToString()));
            }

            Console.WriteLine(string.Concat("WoLastAction.BillStatus=".PadRight(padRightNumber), toReturn.BillStatus.ToString()));

            if (toReturn.Invoice != null)
            {
                Console.WriteLine(string.Concat("WoLastAction.Invoice.Id=".PadRight(padRightNumber), toReturn.Invoice.Id.ToString()));
                Console.WriteLine(string.Concat("WoLastAction.Invoice.Comments=".PadRight(padRightNumber), toReturn.Invoice.Comments ?? ""));
                Console.WriteLine(string.Concat("WoLastAction.Invoice.Number=".PadRight(padRightNumber), toReturn.Invoice.Number ?? ""));
            }


            Console.WriteLine(string.Concat("WoLastAction.BilledTotal.Value= ".PadRight(padRightNumber), toReturn.BilledTotal?.Value.ToString() ?? ""));
            Console.WriteLine(string.Concat("WoLastAction.Xnumber= ".PadRight(padRightNumber), toReturn.XNumber ?? ""));


            Console.WriteLine();

            return toReturn;
        }

        public static CorrigoEntity[] RetrieveByQuery(CorrigoService service, int countNumber = 15)
        {
            Console.WriteLine();

            var list = service.RetrieveMultiple(
                new QueryExpression
                {
                    EntityType = EntityType.WoLastAction,
                    PropertySet = new PropertySet
                    {
                        Properties = new string[]
                        {
                            "Id",
                            "WorkOrderId",
                            "LastAction.*","LastAction.Actor.*",
                            "EmergencyReason.*",
                            "Reason.*",
                            "BillStatus",
                            "Invoice.*",
                            "BilledTotal",
                            "Xnumber",
                        }
                    },
                    //PropertySet = new AllProperties(),
                    //Criteria = new FilterExpression
                    //{
                    //    Conditions = new[]
                    //    {
                    //        new ConditionExpression
                    //        {
                    //            Operator = ConditionOperator.NotNull,
                    //            PropertyName = "Invoice.Id"
                    //        }
                    //    }
                    //},
                    Orders = new[]
                    {
                        new OrderExpression
                        {
                            OrderType = OrderType.Descending,
                            PropertyName = "Id"
                        }
                    },
                    Count = countNumber
                });
            Console.WriteLine();
            Console.WriteLine($"WoLastActions: Retrieve by query latest {countNumber} Work Orders Last Actions");

            if (list == null || list.Length == 0) return list;

            Console.WriteLine(string.Concat("#".PadLeft(4), "|", "Id".PadLeft(4), "|", "WorkOrderId".PadRight(11)
                , "|",
                "WO Number".PadRight(15), "|",
                "LastAction.Id".PadRight(15),"|",
                "LastAction.ActionDate".PadRight(21), "|",
                "LastAction.TypeId".PadRight(17), "|",
                "LastAction.Actor.DisplayAs".PadRight(26), "|",
                "LastAction.Comment".PadRight(40)
                ));

            int i = 0;
            foreach (WoLastAction item in list)
            {
                i++;
                var wo =
                    (WorkOrder)
                        service.Retrieve(
                            new EntitySpecifier {EntityType = EntityType.WorkOrder, Id = item.WorkOrderId},
                            new PropertySet { Properties = new string[] { "Number" } });

                Console.WriteLine(string.Concat(i.ToString().PadLeft(4), "|", item.Id.ToString().PadLeft(4), "|", item.WorkOrderId.ToString().PadRight(11)
                    , "|",
                    wo.Number.PadRight(15), "|",
                    item.LastAction.Id.ToString().PadRight(15), "|",
                    item.LastAction.ActionDate.ToString().PadRight(21), "|",
                    item.LastAction.TypeId.ToString().PadRight(17), "|",
                    item.LastAction.Actor.DisplayAs.PadRight(26), "|",
                    item.LastAction.Comment.PadRight(40)
                    ));
            }


            Console.WriteLine();
            return list;
        }

    }
}
