using CorrigoServiceWebReference.CorrigoGA;
using System;

namespace _9x.WebServices.Addresses.Operations
{
    internal static class Update
    {
        
        public static void Execute(CorrigoService service, Address2 toUpdate)
        {
            if (toUpdate == null || service == null) return;

            toUpdate.Street = (toUpdate.Street ?? "") + ".";
            toUpdate.Street2 = "Test street2.";
            toUpdate.City = "Test city by wsdk";
            Console.WriteLine();
            Console.WriteLine($"Updating Address2 with id={toUpdate.Id.ToString()}");

            var resultUpdate = service.Execute(new UpdateCommand
            {
                Entity = toUpdate,
                PropertySet = new PropertySet { Properties = new[] { "Street", "Street2", "City" } }
            });

            if (resultUpdate == null)
            {
                Console.WriteLine("Update of Address2 failed");
                return;
            }

            if (resultUpdate.ErrorInfo != null && !string.IsNullOrEmpty(resultUpdate.ErrorInfo.Description))
            {
                Console.WriteLine(resultUpdate.ErrorInfo.Description);
                Console.WriteLine("Update of Address2 failed");
                return;
            }

            Console.WriteLine("Address2 is updated");

        }

        public static void Restore(CorrigoService service, Address2 toRestore)
        {
            if (toRestore == null || service == null) return;
            Console.WriteLine();
            Console.WriteLine($"Restoring Address2 with id={toRestore.Id.ToString()}");

            var restoreResult = service.Execute(new RestoreCommand
            {
                EntitySpecifier = new EntitySpecifier { Id = toRestore.Id, EntityType = EntityType.Address2 }
            });

            if (restoreResult == null)
            {
                Console.WriteLine("Update of Address2 failed");
                return;
            }

            if (restoreResult.ErrorInfo != null && !string.IsNullOrEmpty(restoreResult.ErrorInfo.Description))
            {
                Console.WriteLine(restoreResult.ErrorInfo.Description);
                Console.WriteLine("Restore of Address2 failed");
                return;
            }

            Console.WriteLine("Address2 is restored");

        }
    }
}
