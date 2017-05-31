using CommonIntegrationScenarios.Locations;
using CommonIntegrationScenarios.VendorInvoicing;
using CommonIntegrationScenarios.WorkOrders;
using CommonIntegrationScenarios.WorkOrderUpdates;
using CorrigoServiceWebReference.CorrigoGA;


namespace CommonIntegrationScenarios
{
    public static class CommonIntegrationScenarios
    {
        public static void Execute(CorrigoService corrigoService)
        {
            //
            // Uncomment corresponding integration scenario(s) for execution.
            //

            //WorkOrdersIntegrationScenarios.Execute(corrigoService);

            //WorkOrderUpdatesIntegrationScenarios.Execute(corrigoService);

            //VendorInvoicingIntegrationScenarios.Execute(corrigoService);

            //LocationsIntegrationScenarios.Execute(corrigoService);
        }
    }
}
