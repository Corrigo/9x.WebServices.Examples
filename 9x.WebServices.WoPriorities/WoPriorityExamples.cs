using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorrigoServiceWebReference;
using CorrigoServiceWebReference.CorrigoGA;
using _9x.WebServices.WoPriorities.Operations;


namespace _9x.WebServices.WoPriorities
{
    public static class WoPriorityExamples
    {
        //
        // Warning 07-Sep-2016 : for the moment create/update WoPriority does not work properly - it just creates an empty WoPriority.
        //

        public static CorrigoEntity[] RetrieveMultiple(CorrigoService service)
        {
            return Read.RetrieveMultiple(service);
        }

        public static CorrigoEntity[] RetrieveByQuery(CorrigoService service)
        {
            return Read.RetrieveByQuery(service);
        }

        public static void CRUDExample(CorrigoService service, bool isCreateUpdateDelete = false)
        {
            if (service == null) return;

            WoPriority prio = isCreateUpdateDelete? Create.Execute(service) : null;

            if (prio != null)
            {
                Read.Retrieve(service, prio.Id);

                Update.Execute(service, prio);

                Read.Retrieve(service, prio.Id);

                Delete.Execute(service, prio.Id);
            }

            Read.RetrieveMultiple(service);

            Read.RetrieveByQuery(service);
        }
    }

}
