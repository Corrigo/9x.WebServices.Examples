using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using CorrigoServiceWebReference;
using CorrigoServiceWebReference.CorrigoGA;
using _9x.WebServices.Tasks.Operations;


namespace _9x.WebServices.Tasks
{
    public static class TaskExamples
    {

        public static void CRUDExample(CorrigoService service, bool isCreateUpdateDelete = false)
        {
            if (service == null) return;

            Task newTask = isCreateUpdateDelete ? Create.Execute(service) : null;

            if (newTask != null)
            {
                Read.Retrieve(service, newTask.Id);

                Update.Execute(service, newTask);

                Read.Retrieve(service, newTask.Id);

                Delete.Execute(service, newTask.Id);

                Update.Restore(service, newTask);
            }

            Read.RetrieveMultiple(service);

            Read.RetrieveByQuery(service);                       
        }


    }

}
