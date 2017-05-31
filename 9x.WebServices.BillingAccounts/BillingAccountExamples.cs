using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorrigoServiceWebReference;
using CorrigoServiceWebReference.CorrigoGA;
using _9x.WebServices.BillingAccounts.Operations;


namespace _9x.WebServices.BillingAccounts
{
    public static class BillingAccountExamples
    {

        public static void CRUDExample(CorrigoService service)
        {
            if (service == null) return;
            
            Console.WriteLine("BillingAccount is Readonly - NonCreatable, NonDeletable, NonUpdatable");


            BillingAccount ba = Create.Execute(service);

            
            var billingAccounts = Read.RetrieveMultiple(service);

            Read.RetrieveByQuery(service);

            if (billingAccounts != null && billingAccounts.Length > 0)
            {
                Read.Retrieve(service, billingAccounts[0].Id);

                Delete.Execute(service, billingAccounts[0].Id); // BillingAccount is Readonly - NonCreatable, NonDeletable, NonUpdatable
                Update.Restore(service, (BillingAccount) billingAccounts[0]); // BillingAccount is Readonly - NonCreatable, NonDeletable, NonUpdatable
                Update.Execute(service, (BillingAccount)billingAccounts[0]); // BillingAccount is Readonly - NonCreatable, NonDeletable, NonUpdatable
            }
        }
    }

}
