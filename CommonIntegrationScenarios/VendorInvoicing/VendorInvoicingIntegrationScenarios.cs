using CommonIntegrationScenarios.VendorInvoicing.Invoices;
using CorrigoServiceWebReference.CorrigoGA;
using System.Linq;


namespace CommonIntegrationScenarios.VendorInvoicing
{
    internal class VendorInvoicingIntegrationScenarios
    {
        public static void Execute(CorrigoService corrigoService)
        {
            //
            // Retrieve a list of approved invoices
            //
            var approvedInvoices = InvoiceScenario.RetrieveMultipleApprovedInvoices(corrigoService);

            //
            // Update the status of specified invoices to "Exported"
            // 
            int[] invoiceIDs = approvedInvoices.Select(i => i.Id).Take(2).ToArray();

            PropertySet _propertySet = new PropertySet
            {
                Properties = new string[]
                            {
                                "Id",
                                "DisplayAs",
                                "Description",
                                "ForecastGroupId",
                                "ApStateId",//
                                "ReadyForExport",
                                "SetOnExport",
                                "ChildWoDefault"
                            }
            };
            var query = new QueryByProperty
            {
                EntityType = EntityType.ApInvoiceStatus,
                PropertySet = _propertySet,//new AllProperties(),
                Conditions = new[]
                    {
                        new PropertyValuePair {PropertyName = "ApInvoiceStatus", Value = ApState.Exported},
                    },
            };
            var invoiceStatuses = corrigoService.RetrieveMultiple(query).Cast<ApInvoiceStatus>().ToArray();

            InvoiceScenario.UpdateInvoiceStatus(corrigoService, invoiceIDs, invoiceStatuses[0].ApStateId);

            //
            // Vendor Invoice Payment
            //
            InvoiceScenario.VendorInvoicePayment(corrigoService, invoiceIDs);
        }
    }
}
