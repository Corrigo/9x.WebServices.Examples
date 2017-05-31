using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorrigoServiceWebReference;
using CorrigoServiceWebReference.CorrigoGA;
using _9x.WebServices.Organizations.Operations;


namespace _9x.WebServices.Organizations
{
    public static class OrganizationExamples
    {

        public static void CRUDExample(CorrigoService service)
        {
            if (service == null) return;


            Organization organization = Create.Execute(service); 

            var organizations = Read.RetrieveMultiple(service);

            Read.RetrieveByQuery(service);

            if (organizations != null && organizations.Length > 0)
            {

                organization = Read.Retrieve(service, organizations[organizations.Length - 1].Id);

                Update.Execute(service, organization); 

                Delete.Execute(service, organization.Id);

                Update.Restore(service, organization); // Organization is not restorable
            }



        }
    }

}
