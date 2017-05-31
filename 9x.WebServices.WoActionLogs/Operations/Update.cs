using CorrigoServiceWebReference.CorrigoGA;
using System;

namespace _9x.WebServices.WoActionLogs.Operations
{
    internal static class Update
    {
        // WoActionLog is Readonly - NonCreatable, NonDeletable, NonUpdatable
        public static void Execute(CorrigoService service, WoActionLog toUpdate)
        {
            if (toUpdate == null || service == null) return;

            toUpdate.Comment = (toUpdate.Comment ?? "") + ".";
            Console.WriteLine();
            Console.WriteLine($"Updating WoActionLog with id={toUpdate.Id.ToString()}");

            var resultUpdate = service.Execute(new UpdateCommand
            {
                Entity = toUpdate,
                PropertySet = new PropertySet { Properties = new[] { "Comment" } }
            });

            if (resultUpdate == null)
            {
                Console.WriteLine("Update of WoActionLog failed");
                return;
            }

            if (resultUpdate.ErrorInfo != null && !string.IsNullOrEmpty(resultUpdate.ErrorInfo.Description))
            {
                Console.WriteLine(resultUpdate.ErrorInfo.Description);
                Console.WriteLine("Update of WoActionLog failed");
                return;
            }

            Console.WriteLine("WoActionLog is updated");

        }

        public static void Restore(CorrigoService service, WoActionLog toRestore)
        {
            if (toRestore == null || service == null) return;
            Console.WriteLine();
            Console.WriteLine($"Restoring WoActionLog with id={toRestore.Id}");

            var restoreResult = service.Execute(new RestoreCommand
            {
                EntitySpecifier = new EntitySpecifier { Id = toRestore.Id, EntityType = EntityType.WoActionLog }
            });

            if (restoreResult == null)
            {
                Console.WriteLine("Update of WoActionLog failed");
                return;
            }

            if (restoreResult.ErrorInfo != null && !string.IsNullOrEmpty(restoreResult.ErrorInfo.Description))
            {
                Console.WriteLine(restoreResult.ErrorInfo.Description);
                Console.WriteLine("Restore of WoActionLog failed");
                return;
            }

            Console.WriteLine("WoActionLog is restored");

        }
    }
}
