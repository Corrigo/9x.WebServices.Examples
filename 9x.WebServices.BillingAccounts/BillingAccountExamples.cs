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
            
            Console.WriteLine("BillingAccount is Readonly - ?");


            BillingAccount ba = Create.Execute(service);//Can create

            
            var billingAccounts = Read.RetrieveMultiple(service);

            Read.RetrieveByQuery(service);

            if (billingAccounts != null && billingAccounts.Length > 0)
            {
                Read.Retrieve(service, billingAccounts[0].Id);

                Delete.Execute(service, billingAccounts[0].Id); // Doesn't affect and no error response
                Update.Restore(service, (BillingAccount) billingAccounts[0]); // Doesn't affect and no error response
                Update.Execute(service, (BillingAccount)billingAccounts[0]); // Doesn't affect and no error response
            }
        }
    }

}
