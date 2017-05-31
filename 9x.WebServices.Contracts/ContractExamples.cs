using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorrigoServiceWebReference;
using CorrigoServiceWebReference.CorrigoGA;
using _9x.WebServices.Contracts.Operations;


namespace _9x.WebServices.Contracts
{
    public static class ContractExamples
    {
        public static void CRUDExample(CorrigoService service)
        {
            if (service == null) return;

            Contract contract = Create.Execute(service);


            var contracts = Read.RetrieveMultiple(service);

            Read.RetrieveByQuery(service);

            if (contracts != null && contracts.Length > 0)
            {
                Read.Retrieve(service, contracts[0].Id);

                Delete.Execute(service, contracts[0].Id);
                    // Contract is Readonly - NonCreatable, NonDeletable, NonUpdatable
                Update.Restore(service, (Contract) contracts[0]);
                    // Contract is Readonly - NonCreatable, NonDeletable, NonUpdatable
                Update.Execute(service, (Contract) contracts[0]);
                    // Contract is Readonly - NonCreatable, NonDeletable, NonUpdatable
            }
        }
    }

}
