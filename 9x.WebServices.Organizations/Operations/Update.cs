using CorrigoServiceWebReference.CorrigoGA;
using System;

namespace _9x.WebServices.Organizations.Operations
{
    internal static class Update
    {

        public static void Execute(CorrigoService service, Organization toUpdate)
        {
            if (toUpdate == null || service == null) return;

            toUpdate.DisplayAs += ".";

            Console.WriteLine();
            Console.WriteLine($"Updating Organization with id={toUpdate.Id}");

            var resultUpdate = service.Execute(new UpdateCommand
            {
                Entity = toUpdate,
                PropertySet = new PropertySet { Properties = new[] { "DisplayAs" } }
            });

            if (resultUpdate == null)
            {
                Console.WriteLine("Update of Organization failed");
                return;
            }

            if (resultUpdate.ErrorInfo != null && !string.IsNullOrEmpty(resultUpdate.ErrorInfo.Description))
            {
                Console.WriteLine(resultUpdate.ErrorInfo.Description);
                Console.WriteLine("Update of Organization failed");
                return;
            }

            Console.WriteLine("Organization is updated");
        }

        public static void Restore(CorrigoService service, Organization toRestore)
        {
            if (toRestore == null || service == null) return;
            Console.WriteLine();
            Console.WriteLine($"Restoring Organization with id={toRestore.Id}");

            var restoreResult = service.Execute(new RestoreCommand
            {
                EntitySpecifier = new EntitySpecifier { Id = toRestore.Id, EntityType = EntityType.Organization }
            });

            if (restoreResult == null)
            {
                Console.WriteLine("Update of Organization failed");
                return;
            }

            if (restoreResult.ErrorInfo != null && !string.IsNullOrEmpty(restoreResult.ErrorInfo.Description))
            {
                Console.WriteLine(restoreResult.ErrorInfo.Description);
                Console.WriteLine("Restore of Organization failed");
                return;
            }

            Console.WriteLine("Organization is restored");

        }
    }
}
