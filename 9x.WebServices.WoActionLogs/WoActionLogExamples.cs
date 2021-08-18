using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorrigoServiceWebReference;
using CorrigoServiceWebReference.CorrigoGA;
using _9x.WebServices.WoActionLogs.Operations;


namespace _9x.WebServices.WoActionLogs
{
    public static class WoActionLogExamples
    {

        public static void CRUDExample(CorrigoService service)
        {
            if (service == null) return;

            Console.WriteLine("WoActionLog is Readonly - NonCreatable, NonDeletable, NonUpdatable");
            

            WoActionLog woal = Create.Execute(service);

            CorrigoEntity[] latestWOs = GetLatestWOs(service, 15);
            if (latestWOs == null || latestWOs.Length == 0) return;


            CorrigoEntity[] actionLogs = Read.RetrieveByQuery(service, latestWOs);

            if (actionLogs != null && actionLogs.Length > 0)
            {
                int actionLogIdWithProperty = GetWoActionLogIdWithProperty(service);

                WoActionLog actionLog = Read.Retrieve(service, (actionLogIdWithProperty > 0)? actionLogIdWithProperty : actionLogs[0].Id);
                if (actionLog == null) return;

                Delete.Execute(service, actionLog.Id); // WoActionLog is Readonly - NonCreatable, NonDeletable, NonUpdatable
                Update.Restore(service, actionLog); // WoActionLog is Readonly - NonCreatable, NonDeletable, NonUpdatable
                Update.Execute(service, actionLog); // WoActionLog is Readonly - NonCreatable, NonDeletable, NonUpdatable
            }


        }

        /// <summary>
        /// Returns specified number of latest Work Orders
        /// Ordered - latest is 1st
        /// </summary>
        /// <param name="service"></param>
        /// <param name="numberOfWOs"></param>
        /// <returns></returns>
        public static CorrigoEntity[] GetLatestWOs(CorrigoService service, int numberOfWOs)
        {
            var list = service.RetrieveMultiple(
                new QueryExpression
                {
                    EntityType = EntityType.WorkOrder,
                    PropertySet = new AllProperties(),
                    Orders = new[]
                    {
                        new OrderExpression
                        {
                            OrderType = OrderType.Descending,
                            PropertyName = "CreatedDateUtc"
                        }
                    },
                    Count = numberOfWOs
                });

            return list;
        }

        public static int GetWoActionLogIdWithProperty(CorrigoService service)
        {
            var list = service.RetrieveMultiple(
                new QueryExpression
                {
                    EntityType = EntityType.WoActionLogProp,
                    PropertySet = new AllProperties(),
                    Orders = new[]
                    {
                        new OrderExpression
                        {
                            OrderType = OrderType.Descending,
                            PropertyName = "Id"
                        }
                    },
                    Count = 1
                });

            int toReturn = (list!= null && list.Length >0)? ((WoActionLogProp)list[0]).WoActionLogId: 0;

            return toReturn;

        }
    }

}
