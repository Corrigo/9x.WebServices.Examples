using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorrigoServiceWebReference;
using CorrigoServiceWebReference.CorrigoGA;
using _9x.WebServices.WoLastActions.Operations;


namespace _9x.WebServices.WoLastActions
{
    public static class WoLastActionExamples
    {
        public static void CRUDExample(CorrigoService service)
        {
            if (service == null) return;

            Console.WriteLine("WoLastAction is Readonly - NonCreatable, NonDeletable, NonUpdatable");

            WoLastAction wola = Create.Execute(service);

            CorrigoEntity[] lastActions = Read.RetrieveByQuery(service);

            if (lastActions != null && lastActions.Length > 0)
            {
                WoLastAction lastAction = Read.Retrieve(service, lastActions[0].Id);

                // WoLastAction is Readonly - NonCreatable, NonDeletable, NonUpdatable
                Delete.Execute(service, lastActions[0].Id);

                // WoLastAction is Readonly - NonCreatable, NonDeletable, NonUpdatable
                Update.Restore(service, lastAction);

                // WoLastAction is Readonly - NonCreatable, NonDeletable, NonUpdatable
                Update.Execute(service, lastAction);
            }
        }


    }

}
