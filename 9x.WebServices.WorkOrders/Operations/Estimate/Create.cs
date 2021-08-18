using CorrigoServiceWebReference.CorrigoGA;
using System.Diagnostics;

namespace _9x.WebServices.WorkOrders.Operations.Estimate
{
    internal static class Create
    {
        public static int Execute(CorrigoService service, int woid)
        {
            var specifier = new EntitySpecifier
            {
                EntityType = EntityType.WorkOrder,
                Id = woid
            };
            string[] properties = { nameof(WorkOrder.Estimate) + ".*" };
            var workOrder = service.Retrieve(specifier, new PropertySet { Properties = properties }) as WorkOrder;

            var list = service.RetrieveMultiple(
                new QueryByProperty
                {
                    EntityType = EntityType.Actor,
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

            var estimate = new WoEstimate
            {
                Id = workOrder.Estimate?.Id ?? 0,
                CurrencyTypeId = CurrencyType.USD,//required
                                                  //ConcurrencyId = workOrder.Estimate?.ConcurrencyId ?? 0,
                Amount = new MoneyValue { Value = 10.1m, CurrencyTypeId = CurrencyType.USD },
                Comment = "custom estimate 0",
                ContactName = "Con 2 FN",
                ContactId = 11,//required
                Reason = "some custom reason",
                StatusId = QuoteStatus.WaitingForApproval,              //required
                IsMultiline = false,//required
                Items = new[] {
                    new QuoteOrEstimateItem {
                        WorkOrderId = woid,
                        ParentId = 111,
                        ParentTypeId = ActorType.Employee,
                        Quantity = 2,
                        IsFlagged = false,
                        CurrencyTypeId = CurrencyType.USD
                    } }
            };

            workOrder.Estimate = estimate;

            var command = new UpdateCommand
            {
                Entity = workOrder,
                PropertySet = new PropertySet { Properties = properties }
            };

            var response = service.Execute(command) as OperationCommandResponse;
            var estimateId = response?.NestedEntitiesOperationResults?[0]?.EntitySpecifier?.Id ?? 0;

            Debug.Print(response?.ErrorInfo?.Description
                ?? $"Successfully created an Estimate with id '{estimateId}' for WO with id '{woid}'");

            return estimateId;
        }
    }
}
