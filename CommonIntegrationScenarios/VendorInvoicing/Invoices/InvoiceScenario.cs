using CorrigoServiceWebReference.CorrigoGA;
using System;
using System.Linq;


namespace CommonIntegrationScenarios.VendorInvoicing.Invoices
{
    /// <summary>
    /// For vendor invoices we do not create invoices via Corrigo web services
    /// since vendors will be submitting their invoices over the Network. 
    /// The vendor invoice web services are simply used to retrieve vendor invoices
    /// to send to the Corrigo Enterprise customer’s financial system. 
    ///
    /// The Use case describes the retrieval of such approved Invoices, 
    /// sending them to the customer’s financial system and making payments on the invoices.
    /// When a payment is made, a check is cut and the check # is sent back to the provider system.
    /// </summary>
    internal class InvoiceScenario
    {
        /// <summary>
        /// Retrieve a list of approved invoices
        /// Operation: RetrieveMultiple(QueryByProperty)
        /// Retrieve all Invoices Where Invoice Status = “Authorized”
        /// Fields to retrieve:
        ///  Invoice Id,
        ///  Invoice Number,
        ///  Billing Account,
        ///  Invoice Line Items,
        /// </summary>
        /// <param name="corrigoService"></param>
        /// <returns></returns>
        public static WorkOrderCost[] RetrieveMultipleApprovedInvoices(CorrigoService corrigoService)
        {
            WorkOrderCost[] results = corrigoService.RetrieveMultiple(
                new QueryByProperty
                {
                    EntityType = EntityType.WorkOrderCost,
                    PropertySet = new PropertySet
                    {
                        Properties = new string[]
                        {
                            "Id",
                            "Number",
                            "BillingAccount.*",
                            "Items.*",
                            "Items.InvoiceItem.*"
                        }
                    },
                    Conditions = new[]
                    {
                        new PropertyValuePair {PropertyName = "ApStateId", Value = ApState.Authorized},
                    },
                    Orders = new[]
                    {
                        new OrderExpression
                        {
                            PropertyName = "Id",
                            OrderType = OrderType.Ascending
                        }
                    },
                }).Cast<WorkOrderCost>().ToArray();

            Console.WriteLine($"# of Authorized vendor invoices retrieved: " + results.Length);
            //Console.ReadKey();

            return results;
        }


        /// <summary>
        /// Update the status of specified invoices to "Exported"
        /// Operation: ExecuteMultiple(CommandRequest[])
        /// Update the retrieved list of Invoices as follows:
        ///     Set
        ///       Invoice Status = "Exported"
        ///     Where
        ///       Invoice Id IN (list of Invoice IDs)
        /// </summary>
        /// <param name="corrigoService"></param>
        /// <param name="invoiceIDs"></param>
        /// <param name="invoiceStatus"></param>
        /// <returns></returns>
        public static CommandResponse[] UpdateInvoiceStatus(CorrigoService corrigoService, 
                                                            int[] invoiceIDs,
                                                            APInvoiceStatus invoiceStatus)
        {
            CommandRequest[] commands = invoiceIDs.Select(i =>
                new ApStatusChangeCommand

                {
                    WorkOrderId = i, // Invoice Id is equal to Work Order Id.
                    VendorInvoiceStatusId = invoiceStatus
                }).ToArray();

            CommandResponse[] commandResponses = corrigoService.ExecuteMultiple(commands);

            Console.WriteLine(
                $"# of ApStatusChangeCommand executed with success/total: {commandResponses.Count(cr => cr.ErrorInfo == null)} / {commands.Length}");
            //Console.ReadKey();

            return commandResponses;
        }

        /// <summary>
        /// Vendor Invoice Payment
        /// Operation: ExecuteMultiple(CommandRequest[])
        /// Update the retrieved list of Invoices as follows:
        ///     Set
        ///       Invoice Status = "Paid"
        ///     Where
        ///       Invoice Id IN (list of Invoice IDs)
        ///     Return
        ///       List of Invoice IDs
        ///       Check Number 
        /// </summary>
        /// <param name="corrigoService"></param>
        /// <param name="invoiceIDs">Invoice id and its payment id has the same value</param>
        /// <returns></returns>
        public static WorkOrderCost[] VendorInvoicePayment(CorrigoService corrigoService, int[] invoiceIDs)
        {
            CommandRequest[] commands = invoiceIDs.Select(i =>
                new ApSubmitPaymentCommand
                {
                    WorkOrderId = i, // Invoice Id is equal to Work Order Id, Payment Id.
                    CheckNumber = "CN#" + i,
                    PaymentAmount = 100
                }).ToArray();

            CommandResponse[] commandResponses = corrigoService.ExecuteMultiple(commands);

            Console.WriteLine(
                $"# of ApSubmitPaymentCommand executed with success/total: {commandResponses.Count(cr => cr.ErrorInfo == null)} / {commands.Length}");
            //Console.ReadKey();

            WorkOrderCost[] results = corrigoService.RetrieveMultiple(
                new QueryExpression
                {
                    EntityType = EntityType.WorkOrderCost,
                    PropertySet = new PropertySet
                    {
                        Properties = new string[]
                        {
                            "Id",
                            "CheckNumber",
                            "ApStateId",
                            "PaymentAmount"
                        }
                    },
                    Criteria = new FilterExpression
                    {
                        Conditions = new ConditionExpression[]
                        {
                            new ConditionExpression
                            {
                                PropertyName = "Id",
                                Operator = ConditionOperator.In,
                                Values = invoiceIDs.Cast<object>().ToArray()
                            }
                        },
                    },
                    Orders = new[]
                    {
                        new OrderExpression
                        {
                            PropertyName = "Id",
                            OrderType = OrderType.Ascending
                        }
                    },
                }).Cast<WorkOrderCost>().ToArray();


            return results;
        }
    }
}
