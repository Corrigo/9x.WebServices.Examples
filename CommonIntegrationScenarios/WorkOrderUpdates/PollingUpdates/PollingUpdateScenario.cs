using CorrigoServiceWebReference.CorrigoGA;
using System;
using System.Linq;


namespace CommonIntegrationScenarios.WorkOrderUpdates.PollingUpdates
{

    class PollingUpdateScenario
    {
        static PropertySet _propertySet = new PropertySet
        {
            Properties = new string[]
                            {
                                "Id",
                                "Number",
                                "TypeCategory",
                                "StatusId",
                                "Customer.Name",
                                "ContactName",
                                "ContactAddress.Address",
                                "CreatedDateUtc",
                                "DueDateUtc",
                                "Flag.*"
                            }
        };


        /// <summary>
        /// Poll Use Case 1: “New” Work Orders
        /// Operation: RetrieveMultiple(QueryExpression)
        /// Retrieve a list of Work Orders as follows:
        ///     Where 
        ///       Work Order Status = "New"
        ///       Work Order Scheduled Start Date = Some Date
        ///       AND On Site By Date = Some Date
        /// </summary>
        /// <param name="corrigoService"></param>
        /// <returns></returns>
        public static WorkOrder[] RetrieveMultipleNewWorkOrders(CorrigoService corrigoService)
        {
            DateTime dtScheduledStart = DateTime.Today;
            DateTime dtUtcOnSiteBy = DateTime.Today;

            var query = new QueryExpression
            {
                EntityType = EntityType.WorkOrder,
                PropertySet = _propertySet,
                Criteria = new FilterExpression
                {
                    Conditions = new ConditionExpression[]
                         {
                            new ConditionExpression
                            {
                                PropertyName = "StatusId",
                                Operator = ConditionOperator.Equal,
                                Values = new object[] { WorkOrderStatus.New }
                            },
                            new ConditionExpression
                            {
                                PropertyName = "DtScheduledStart",
                                Operator = ConditionOperator.Equal,
                                Values = new object[] { dtScheduledStart }
                            },
                            new ConditionExpression
                            {
                                PropertyName = "DtUtcOnSiteBy",
                                Operator = ConditionOperator.Equal,
                                Values = new object[] { dtUtcOnSiteBy }
                            }
                         },
                    FilterOperator = LogicalOperator.And
                },
                Orders = new[]
                     {
                        new OrderExpression
                        {
                            PropertyName = "Id",
                            OrderType = OrderType.Ascending
                        }
                     },
            };
            WorkOrder[] results = corrigoService.RetrieveMultiple(query)
                                                .Cast<WorkOrder>()
                                                .ToArray();

            Console.WriteLine($"Retrieve New Work Orders where Scheduled Start Date = '{dtScheduledStart}' and On Site By Date = '{dtUtcOnSiteBy}' ; # of Work Orders retrieved: " + results.Length);
            //Console.ReadKey();

            return results;
        }

        /// <summary>
        /// Poll Use Case 2: Emergency Work Orders
        /// Operation: RetrieveMultiple(QueryByProperty)
        /// Retrieve a list of Work Orders whose Priority = "Emergency"
        /// </summary>
        /// <param name="corrigoService"></param>
        /// <returns></returns>
        public static WorkOrder[] RetrieveMultipleEmergencyWorkOrders(CorrigoService corrigoService)
        {
            var query = new QueryByProperty
            {
                EntityType = EntityType.WorkOrder,
                PropertySet = _propertySet,
                Conditions = new[]
                    {
                        new PropertyValuePair {PropertyName = "Priority.IsEmergency", Value = true},
                    },
                Orders = new[]
                     {
                        new OrderExpression
                        {
                            PropertyName = "Id",
                            OrderType = OrderType.Ascending
                        }
                     },
            };
            WorkOrder[] results = corrigoService.RetrieveMultiple(query)
                                                .Cast<WorkOrder>()
                                                .ToArray();

            Console.WriteLine($"# of Emergency Work Orders retrieved: " + results.Length);
            //Console.ReadKey();

            return results;
        }
    }
}
