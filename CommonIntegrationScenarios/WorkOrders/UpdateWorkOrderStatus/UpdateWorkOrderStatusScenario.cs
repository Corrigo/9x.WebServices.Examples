using CorrigoServiceWebReference.CorrigoGA;
using System;
using System.Linq;

namespace CommonIntegrationScenarios.WorkOrders.UpdateWorkOrderStatus
{
    class UpdateWorkOrderStatusScenario
    {
        /// <summary>
        /// Use Case1: Place Work Orders "On Hold"
        /// Operation: ExecuteMultiple(QueryByProperty)
        /// Update a list of Work Orders as follows:
        ///   Set
        ///       Work Order Status = "On Hold"
        ///       Action Reason Id = Id
        ///  Where
        ///       Work Order Status not in ("Completed", "Open: Paused", "On Hold")
        ///       AND Work Order Create Date >= Some Date
        /// </summary>
        /// <param name="corrigoService"></param>
        public static void ExecuteMultipleWoOnHoldCommand(CorrigoService corrigoService)
        {
            var workOrders = corrigoService.RetrieveMultiple(
                new QueryExpression
                {
                    EntityType = EntityType.WorkOrder,
                    PropertySet = new PropertySet
                    {
                        Properties = new[] { "Id"}
                    },
                    Criteria = new FilterExpression
                    {
                        Conditions = new ConditionExpression[]
                        {
                            new ConditionExpression
                            {
                                PropertyName = "StatusId",
                                Operator = ConditionOperator.NotIn,
                                Values = new object[] { WorkOrderStatus.Completed, WorkOrderStatus.Paused, WorkOrderStatus.OnHold}
                            },
                            new ConditionExpression
                            {
                                PropertyName = "CreatedDateUtc",
                                Operator = ConditionOperator.GreaterOrEqual,
                                Values = new object[] {DateTime.Now.AddDays(-1)}
                            }
                        },
                        FilterOperator = LogicalOperator.And
                    },
                    Orders = new[]
                    {
                        new OrderExpression
                        {
                            OrderType = OrderType.Ascending,
                            PropertyName = "Id"
                        }
                    },

                });


            int otherReasonLookupId = corrigoService.RetrieveMultiple(
                new QueryByProperty
                {
                    EntityType = EntityType.WoActionReasonLookup,
                    PropertySet = new PropertySet
                    {
                        Properties = new[] {"Id", "ReasonId"}
                    },
                    Conditions = new[]
                    {
                        new PropertyValuePair {PropertyName = "ActionId", Value = WOActionType.OnHold}
                    }
                }).FirstOrDefault(ar =>
                    ((WoActionReasonLookup) ar).ReasonId == 4)?.Id ?? 0;
                        //4 "Other reason" Id. One of pre-defined reasons ids, expectation is user/contact provides a more specific description in comments 
                        //See reasons in Admin & Settings->Work Order and workflow->action reasons
                        //Get reason ids is possible only from settings html or from db, there are predefined reasons

                        //for OnHold:
                        //ReasonID	Name
                        //   3       Request Needs Customer Approval
                        //   4       Other
                        //   5       Estimate Needs Customer Approval
                        //   6       Waiting for Estimate
                        //   9       Dependency
                        //   23      Deferred

                        //for Cancel:
                        //ReasonID    Name
                        //     2     Customer Cancel Request
                        //     4     Other
                        //     7     All Work Items Removed from Work Order

                        //for Flagging
                        //ReasonID	Name
                        //   6	    Other
                        //   8	    Estimate Rejected
                        //   11	    Rejected by {!{Provider}!}



            CommandRequest[] commands = workOrders.Select(wo =>
                new WoOnHoldCommand
                {
                    WorkOrderId = wo.Id,
                    ActionReasonId = otherReasonLookupId

                }).ToArray();

            CommandResponse[] commandResponses = corrigoService.ExecuteMultiple(commands);

            Console.WriteLine($"# of WoOnHoldCommand executed with success/total: {commandResponses.Count(cr => cr.ErrorInfo == null)} / {commands.Length}");
            //Console.ReadKey();

        }
    }
}
