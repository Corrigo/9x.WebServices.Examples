using CorrigoServiceWebReference.CorrigoGA;
using System;

namespace _9x.WebServices.Contracts.Operations
{
    internal static class Update
    {
        // Contract is Readonly - NonCreatable, NonDeletable, NonUpdatable
        public static void Execute(CorrigoService service, Contract toUpdate)
        {
            if (toUpdate == null || service == null) return;

            toUpdate.DisplayAs = (toUpdate.DisplayAs ?? "") + ".";
            Console.WriteLine();
            Console.WriteLine($"Updating Contract with id={toUpdate.Id.ToString()}");

            var resultUpdate = service.Execute(new UpdateCommand
            {
                Entity = toUpdate,
                PropertySet = new PropertySet { Properties = new[] { "DisplayAs"} }
            });

            if (resultUpdate == null)
            {
                Console.WriteLine("Update of Contract failed");
                return;
            }

            if (resultUpdate.ErrorInfo != null && !string.IsNullOrEmpty(resultUpdate.ErrorInfo.Description))
            {
                Console.WriteLine(resultUpdate.ErrorInfo.Description);
                Console.WriteLine("Update of Contract failed");
                return;
            }

            Console.WriteLine("Contract is updated");

        }

        public static void Restore(CorrigoService service, Contract toRestore)
        {
            if (toRestore == null || service == null) return;
            Console.WriteLine();
            Console.WriteLine($"Restoring Contract with id={toRestore.Id.ToString()}");

            var restoreResult = service.Execute(new RestoreCommand
            {
                EntitySpecifier = new EntitySpecifier { Id = toRestore.Id, EntityType = EntityType.Contract }
            });

            if (restoreResult == null)
            {
                Console.WriteLine("Update of Contract failed");
                return;
            }

            if (restoreResult.ErrorInfo != null && !string.IsNullOrEmpty(restoreResult.ErrorInfo.Description))
            {
                Console.WriteLine(restoreResult.ErrorInfo.Description);
                Console.WriteLine("Restore of Contract failed");
                return;
            }

            Console.WriteLine("Contract is restored");

        }
    }
}
