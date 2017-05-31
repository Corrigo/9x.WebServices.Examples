using CommonIntegrationScenarios.WorkOrderUpdates.PollingUpdates;
using CorrigoServiceWebReference.CorrigoGA;


namespace CommonIntegrationScenarios.WorkOrderUpdates
{
    class WorkOrderUpdatesIntegrationScenarios
    {
        public static void Execute(CorrigoService corrigoService)
        {
            #region Polling for Work Order Updates

            //
            // Poll Use Case 1: "New" Work Orders
            //
            var newWworkOrders = PollingUpdateScenario.RetrieveMultipleNewWorkOrders(corrigoService);


            //
            // Poll Use Case 2: Emergency Work Orders
            //
            var emergencyWworkOrders = PollingUpdateScenario.RetrieveMultipleEmergencyWorkOrders(corrigoService);

            #endregion

        }

    }
}
