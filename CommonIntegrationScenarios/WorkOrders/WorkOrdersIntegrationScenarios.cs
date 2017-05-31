using CommonIntegrationScenarios.WorkOrders.CreateWorkOrder;
using CommonIntegrationScenarios.WorkOrders.UpdateWorkOrderStatus;
using CommonIntegrationScenarios.WorkOrders.WorkOrderPriority;
using CommonIntegrationScenarios.WorkOrders.WorkZones;
using CorrigoServiceWebReference.CorrigoGA;
using System.Linq;


namespace CommonIntegrationScenarios.WorkOrders
{
    internal class WorkOrdersIntegrationScenarios
    {
        public static void Execute(CorrigoService corrigoService)
        {
            #region Work Order Priority Integration Scenarios

            //
            // Get a list of all Priorities in the system.
            //
            var priorities = WorkOrderPriorityScenario.RetrieveMultiple(corrigoService);

            #endregion

            #region Work Zone Integration Scenarios

            //
            // WZ Use Case1: Retrieving WZ via Customers
            // Get a list of all Customers where Customer Name like “xyz%”
            //
            var resultsByCustomerName = WorkZoneScenario.RetrieveMultipleCustomersByCustomerName(corrigoService, "John%");

            if (resultsByCustomerName == null || !resultsByCustomerName.Any())
                return;
            //
            // WZ Use Case2: Retrieve by Work Zone Id
            //
            var workZoneId = resultsByCustomerName.FirstOrDefault().WorkZone.Id;
            var workZone = WorkZoneScenario.RetrieveWorkZoneById(corrigoService, workZoneId);
           
            #endregion

            #region Create Work Order Integration Scenarios

            //
            // WO Use Case1: Create WO for Customer
            //
            Customer customer = resultsByCustomerName.FirstOrDefault();
            var woByCustomer = CreateWorkOrderScenario.CreateWorkOrderForCustomer(corrigoService, customer);

            //
            // WO Use Case2: Create WO for Work Zone
            //
            
           var woByWorkZone = CreateWorkOrderScenario.CreateWorkOrderForWorkZone(corrigoService, workZone);

            #endregion

            #region Update Work Order Status

            //
            // Use Case1: Place Work Orders "On Hold"
            //
            UpdateWorkOrderStatusScenario.ExecuteMultipleWoOnHoldCommand(corrigoService);

            #endregion
        }
    }
}
