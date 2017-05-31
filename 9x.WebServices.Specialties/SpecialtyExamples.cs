using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using CorrigoServiceWebReference;
using CorrigoServiceWebReference.CorrigoGA;
using _9x.WebServices.Specialties.Operations;


namespace _9x.WebServices.Specialties
{
    public static class SpecialtyExamples
    {

        public static void CRUDExample(CorrigoService service, bool isCreateUpdateDelete = false)
        {
            if (service == null) return;

            Specialty specialty = isCreateUpdateDelete ? Create.Execute(service) : null;

            if (specialty != null)
            {
                Read.Retrieve(service, specialty.Id);

                Update.Execute(service, specialty);

                Read.Retrieve(service, specialty.Id);

                Delete.Execute(service, specialty.Id);

                Update.Restore(service, specialty);
            }

            Read.RetrieveMultiple(service);

            Read.RetrieveByQuery(service);
        }


    }
}
