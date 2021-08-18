using CorrigoServiceWebReference.CorrigoGA;
using System;

namespace _9x.WebServices.WorkZones.Operations
{
    internal static class Update
    {
        // WorkZone is Readonly - NonCreatable, NonDeletable
        public static void Execute(CorrigoService service, WorkZone toUpdate)
        {
            if (toUpdate == null || service == null) return;

            toUpdate.DisplayAs = (toUpdate.DisplayAs ?? "") + ".";
            Console.WriteLine();
            Console.WriteLine($"Updating WorkZone with id={toUpdate.Id.ToString()}");

            var resultUpdate = service.Execute(new UpdateCommand
            {
                Entity = toUpdate,
                PropertySet = new PropertySet { Properties = new[] { "DisplayAs"} }
            });

            if (resultUpdate == null)
            {
                Console.WriteLine("Update of WorkZone failed");
                return;
            }

            if (resultUpdate.ErrorInfo != null && !string.IsNullOrEmpty(resultUpdate.ErrorInfo.Description))
            {
                Console.WriteLine(resultUpdate.ErrorInfo.Description);
                Console.WriteLine("Update of WorkZone failed");
                return;
            }

            Console.WriteLine("WorkZone is updated");

        }

        public static void Restore(CorrigoService service, WorkZone toRestore)
        {
            if (toRestore == null || service == null) return;
            Console.WriteLine();
            Console.WriteLine($"Restoring WorkZone with id={toRestore.Id.ToString()}");

            var restoreResult = service.Execute(new RestoreCommand
            {
                EntitySpecifier = new EntitySpecifier { Id = toRestore.Id, EntityType = EntityType.WorkZone }
            });

            if (restoreResult == null)
            {
                Console.WriteLine("Update of WorkZone failed");
                return;
            }

            if (restoreResult.ErrorInfo != null && !string.IsNullOrEmpty(restoreResult.ErrorInfo.Description))
            {
                Console.WriteLine(restoreResult.ErrorInfo.Description);
                Console.WriteLine("Restore of WorkZone failed");
                return;
            }

            Console.WriteLine("WorkZone is restored");

        }
    }
}
