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
            InvoiceScenario.UpdateInvoiceStatus(corrigoService, invoiceIDs, ApState.Exported);

            //
            // Vendor Invoice Payment
            //
            InvoiceScenario.VendorInvoicePayment(corrigoService, invoiceIDs);
        }
    }
}
