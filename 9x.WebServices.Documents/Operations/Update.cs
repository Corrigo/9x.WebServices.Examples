using CorrigoServiceWebReference.CorrigoGA;
using System;

namespace _9x.WebServices.Documents.Operations
{
    internal static class Update
    {

        public static void Execute(CorrigoService service, Document toUpdate)
        {
            if (toUpdate == null || service == null) return;

            toUpdate.Description = (toUpdate.Description ?? "") + ".";

            Console.WriteLine();
            Console.WriteLine($"Updating Document with id={toUpdate.Id.ToString()}");

            var resultUpdate = service.Execute(new UpdateCommand
            {
                Entity = toUpdate,
                PropertySet = new PropertySet { Properties = new[] { "Description"} }
            });

            if (resultUpdate == null)
            {
                Console.WriteLine("Update of Document failed");
                return;
            }

            if (resultUpdate.ErrorInfo != null && !string.IsNullOrEmpty(resultUpdate.ErrorInfo.Description))
            {
                Console.WriteLine(resultUpdate.ErrorInfo.Description);
                Console.WriteLine("Update of Document failed");
                return;
            }

            Console.WriteLine("Document is updated");

        }

        public static void Restore(CorrigoService service, Document toRestore)
        {
            if (toRestore == null || service == null) return;
            Console.WriteLine();
            Console.WriteLine($"Restoring Document with id={toRestore.Id.ToString()}");

            var restoreResult = service.Execute(new RestoreCommand
            {
                EntitySpecifier = new EntitySpecifier { Id = toRestore.Id, EntityType = EntityType.Document }
            });

            if (restoreResult == null)
            {
                Console.WriteLine("Update of Document failed");
                return;
            }

            if (restoreResult.ErrorInfo != null && !string.IsNullOrEmpty(restoreResult.ErrorInfo.Description))
            {
                Console.WriteLine(restoreResult.ErrorInfo.Description);
                Console.WriteLine("Restore of Document failed");
                return;
            }

            Console.WriteLine("Document is restored");

        }
    }
}
