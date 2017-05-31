using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorrigoServiceWebReference;
using CorrigoServiceWebReference.CorrigoGA;
using _9x.WebServices.Contacts.Operations;


namespace _9x.WebServices.Contacts
{
    public static class ContactExamples
    {

        public static void CRUDExample(CorrigoService service, bool isCreateUpdateDelete = false)
        {
            if (service == null) return;
            
            Contact contact = isCreateUpdateDelete ? Create.Execute(service) : null;

            if (contact != null)
            {
                contact = Read.Retrieve(service, contact.Id);

                Update.Execute(service, contact);

                Read.Retrieve(service, contact.Id);
                
                Delete.Execute(service, contact.Id);

                Update.Restore(service, contact);
            }

            Read.RetrieveMultiple(service);

            Read.RetrieveByQuery(service);

        }
    }

}
