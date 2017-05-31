using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorrigoServiceWebReference;
using CorrigoServiceWebReference.CorrigoGA;
using _9x.WebServices.Addresses.Operations;


namespace _9x.WebServices.Addresses
{
    public static class Address2Examples
    {

        public static void CRUDExample(CorrigoService service, bool isCreateUpdateDelete = false)
        {
            if (service == null) return;
           
            Address2 address = isCreateUpdateDelete ? Create.Execute(service) : null;

            
            if (address != null)
            {
                address = Read.Retrieve(service, address.Id);

                Update.Execute(service, address);

                Read.Retrieve(service, address.Id);

                Update.Restore(service, address); // Address2 is not restorable.

                Delete.Execute(service, address.Id); // Attention : delete will exactly delete Address2 from DB.                
            }

            Read.RetrieveMultiple(service);

            Read.RetrieveByQuery(service);

        }
    }

}
