using CorrigoServiceWebReference.CorrigoGA;
using System;

namespace _9x.WebServices.Tasks.Operations
{
    internal static class Update
    {

        public static void Execute(CorrigoService service, Task toUpdate)
        {
            if (toUpdate == null || service == null) return;
            
            toUpdate.Instructions = (toUpdate.Instructions ?? "") + ".";
            Console.WriteLine();
            Console.WriteLine($"Updating Task with id={toUpdate.Id.ToString()}");

            var resultUpdate = service.Execute(new UpdateCommand
            {
                Entity = toUpdate,
                PropertySet = new PropertySet { Properties = new[] { "Instructions" } }
            });

            if (resultUpdate == null)
            {
                Console.WriteLine("Update of Task failed");
                return;
            }

            if (resultUpdate.ErrorInfo != null && !string.IsNullOrEmpty(resultUpdate.ErrorInfo.Description))
            {
                Console.WriteLine(resultUpdate.ErrorInfo.Description);
                Console.WriteLine("Update of Task failed");
                return;
            }

            Console.WriteLine("Task is updated");

        }

        public static void Restore(CorrigoService service, Task toRestore)
        {
            if (toRestore == null || service == null) return;
            Console.WriteLine();
            Console.WriteLine($"Restoring Task with id={toRestore.Id}");

            var restoreResult = service.Execute(new RestoreCommand
            {
                EntitySpecifier = new EntitySpecifier { Id = toRestore.Id, EntityType = EntityType.Task }
            });

            if (restoreResult == null)
            {
                Console.WriteLine("Restore of Task failed");
                return;
            }

            if (restoreResult.ErrorInfo != null && !string.IsNullOrEmpty(restoreResult.ErrorInfo.Description))
            {
                Console.WriteLine(restoreResult.ErrorInfo.Description);
                Console.WriteLine("Restore of Task failed");
                return;
            }

            Console.WriteLine("Task is restored");

        }
    }
}
