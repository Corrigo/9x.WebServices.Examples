using CorrigoServiceWebReference.CorrigoGA;
using System;
using System.Linq;

namespace CommonIntegrationScenarios.WorkOrders.WorkOrderPriority
{
    internal class WorkOrderPriorityScenario
    {
        /// <summary>
        /// To create a Work Order, the Priority is mandatory. 
        /// To view all the available priorities in the system, 
        /// this sample illustrates usage of the RetrieveMultiple (QueryByProperty) web services operation.
        /// 
        /// Get a list of all Priorities in the system.
        /// Fields to retrieve: Id, Name, Emergency Flag
        /// Sort by: Priority Id, Ascending
        /// </summary>
        /// <param name="corrigoService"></param>
        public static WoPriority[] RetrieveMultiple(CorrigoService corrigoService)
        {
            QueryByProperty queryByProperty = new QueryByProperty();

            //Set Entity Type to Work Order Priority
            queryByProperty.EntityType = EntityType.WoPriority;

            //Define fields to retrieve via Property Set
            queryByProperty.PropertySet = new PropertySet
            {
                Properties = new[] {"Id", "DisplayAs", "IsEmergency"}
            };

            //Sort by: Priority Id, Ascending
            queryByProperty.Orders = new[]
            {
                new OrderExpression
                {
                    PropertyName = "Id",
                    OrderType = OrderType.Ascending
                }
            };

            //To return 100 objects max uncomment this
            //queryByProperty.Count = 100; 

            //Run the search
            WoPriority[] results = corrigoService.RetrieveMultiple(queryByProperty)
                                                 .Cast<WoPriority>()
                                                 .ToArray();

            Console.WriteLine("# of objects retrieved: " + results.Length.ToString());
            //Console.ReadKey();

            return results;
        }
    }
}
