using CorrigoServiceWebReference.CorrigoGA;
using System;

namespace _9x.WebServices.WoLastActions.Operations
{
    internal static class Update
    {
        // WoLastAction is Readonly - NonCreatable, NonDeletable, NonUpdatable
        public static void Execute(CorrigoService service, WoLastAction toUpdate)
        {
            if (toUpdate == null || service == null) return;

            toUpdate.BilledTotal = new MoneyValue {Value = 0, CurrencyTypeId = CurrencyType.USD};
            Console.WriteLine();
            Console.WriteLine($"Updating WoLastAction with id={toUpdate.Id}");

            var resultUpdate = service.Execute(new UpdateCommand
            {
                Entity = toUpdate,
                PropertySet = new PropertySet { Properties = new[] { "BilledTotal" } }
            });

            if (resultUpdate == null)
            {
                Console.WriteLine("Update of WoLastAction failed");
                return;
            }

            if (resultUpdate.ErrorInfo != null && !string.IsNullOrEmpty(resultUpdate.ErrorInfo.Description))
            {
                Console.WriteLine(resultUpdate.ErrorInfo.Description);
                Console.WriteLine("Update of WoLastAction failed");
                return;
            }

            Console.WriteLine("WoLastAction is updated");

        }

        public static void Restore(CorrigoService service, WoLastAction toRestore)
        {
            if (toRestore == null || service == null) return;
            Console.WriteLine();
            Console.WriteLine($"Restoring WoLastAction with id={toRestore.Id}");

            var restoreResult = service.Execute(new RestoreCommand
            {
                EntitySpecifier = new EntitySpecifier { Id = toRestore.Id, EntityType = EntityType.WoLastAction }
            });

            if (restoreResult == null)
            {
                Console.WriteLine("Update of WoLastAction failed");
                return;
            }

            if (restoreResult.ErrorInfo != null && !string.IsNullOrEmpty(restoreResult.ErrorInfo.Description))
            {
                Console.WriteLine(restoreResult.ErrorInfo.Description);
                Console.WriteLine("Restore of WoLastAction failed");
                return;
            }

            Console.WriteLine("WoLastAction is restored");

        }
    }
}
