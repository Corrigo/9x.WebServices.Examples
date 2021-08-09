using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorrigoServiceWebReference;
using CorrigoServiceWebReference.CorrigoGA;
using _9x.WebServices.WorkZones.Operations;


namespace _9x.WebServices.WorkZones
{
    public static class WorkZoneExamples
    {

        public static void CRUDExample(CorrigoService service)
        {
            if (service == null) return;

            WorkZone workZone = Create.Execute(service);

            if (workZone != null && workZone.Id > 0)
            {
                workZone = Read.Retrieve(service, workZone.Id);

                Delete.Execute(service, workZone.Id); // WorkZone is NonDeletable
                Update.Restore(service, workZone); // WorkZone is not restorable

                Update.Execute(service, workZone);//updatable

                //after we've got/created the root, we can gradually add asset branhes and sub-trees
                Create.CreateAssetHierarchy(service, workZone.Asset.Id);
            }


            var workZones = Read.RetrieveMultiple(service);

            Read.RetrieveByQuery(service);

        }
    }

}
