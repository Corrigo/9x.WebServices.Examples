using CorrigoServiceWebReference.CorrigoGA;
using System;

namespace _9x.WebServices.BillingAccounts.Operations
{
    internal static class Update
    {
        // BillingAccount is Readonly - NonCreatable, NonDeletable, NonUpdatable
        public static void Execute(CorrigoService service, BillingAccount toUpdate)
        {
            if (toUpdate == null || service == null) return;

            toUpdate.DisplayAs = (toUpdate.DisplayAs ?? "") + ".";
            Console.WriteLine();
            Console.WriteLine($"Updating BillingAccount with id={toUpdate.Id.ToString()}");

            var resultUpdate = service.Execute(new UpdateCommand
            {
                Entity = toUpdate,
                PropertySet = new PropertySet { Properties = new[] { "DisplayAs"} }
            });

            if (resultUpdate == null)
            {
                Console.WriteLine("Update of BillingAccount failed");
                return;
            }

            if (resultUpdate.ErrorInfo != null && !string.IsNullOrEmpty(resultUpdate.ErrorInfo.Description))
            {
                Console.WriteLine(resultUpdate.ErrorInfo.Description);
                Console.WriteLine("Update of BillingAccount failed");
                return;
            }

            Console.WriteLine("BillingAccount is updated");

        }

        public static void Restore(CorrigoService service, BillingAccount toRestore)
        {
            if (toRestore == null || service == null) return;
            Console.WriteLine();
            Console.WriteLine($"Restoring BillingAccount with id={toRestore.Id.ToString()}");

            var restoreResult = service.Execute(new RestoreCommand
            {
                EntitySpecifier = new EntitySpecifier { Id = toRestore.Id, EntityType = EntityType.BillingAccount }
            });

            if (restoreResult == null)
            {
                Console.WriteLine("Update of BillingAccount failed");
                return;
            }

            if (restoreResult.ErrorInfo != null && !string.IsNullOrEmpty(restoreResult.ErrorInfo.Description))
            {
                Console.WriteLine(restoreResult.ErrorInfo.Description);
                Console.WriteLine("Restore of BillingAccount failed");
                return;
            }

            Console.WriteLine("BillingAccount is restored");

        }
    }
}
