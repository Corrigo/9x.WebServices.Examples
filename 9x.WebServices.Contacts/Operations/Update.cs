using CorrigoServiceWebReference.CorrigoGA;
using System;

namespace _9x.WebServices.Contacts.Operations
{
    internal static class Update
    {
        
        public static void Execute(CorrigoService service, Contact toUpdate)
        {
            if (toUpdate == null || service == null) return;

            toUpdate.FirstName = "Homer";

            Console.WriteLine();
            Console.WriteLine($"Updating Contact with id={toUpdate.Id.ToString()}");

            var resultUpdate = service.Execute(new UpdateCommand
            {
                Entity = toUpdate,
                PropertySet = new PropertySet { Properties = new[] { "FirstName"} }
            });

            if (resultUpdate == null)
            {
                Console.WriteLine("Update of Contact failed");
                return;
            }

            if (resultUpdate.ErrorInfo != null && !string.IsNullOrEmpty(resultUpdate.ErrorInfo.Description))
            {
                Console.WriteLine(resultUpdate.ErrorInfo.Description);
                Console.WriteLine("Update of Contact failed");
                return;
            }

            Console.WriteLine("Contact is updated");

        }

        public static void Restore(CorrigoService service, Contact toRestore)
        {
            if (toRestore == null || service == null) return;
            Console.WriteLine();
            Console.WriteLine($"Restoring Contact with id={toRestore.Id.ToString()}");

            var restoreResult = service.Execute(new RestoreCommand
            {
                EntitySpecifier = new EntitySpecifier { Id = toRestore.Id, EntityType = EntityType.Contact }
            });

            if (restoreResult == null)
            {
                Console.WriteLine("Update of Contact failed");
                return;
            }

            if (restoreResult.ErrorInfo != null && !string.IsNullOrEmpty(restoreResult.ErrorInfo.Description))
            {
                Console.WriteLine(restoreResult.ErrorInfo.Description);
                Console.WriteLine("Restore of Contact failed");
                return;
            }

            Console.WriteLine("Contact is restored");

        }
    }
}
