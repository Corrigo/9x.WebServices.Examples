using CorrigoServiceWebReference.CorrigoGA;
using System;

namespace _9x.WebServices.AssetTrees.Operations
{
    internal static class Update
    {

        public static void Execute(CorrigoService service, AssetTree toUpdate)
        {
            if (toUpdate == null || service == null) return;

            toUpdate.Distance = 1;

            Console.WriteLine();
            Console.WriteLine($"Updating AssetTree with id={toUpdate.Id.ToString()}");

            var resultUpdate = service.Execute(new UpdateCommand
            {
                Entity = toUpdate,
                PropertySet = new PropertySet { Properties = new[] {"Distance"} }
            });

            if (resultUpdate == null)
            {
                Console.WriteLine("Update of AssetTree failed");
                return;
            }

            if (resultUpdate.ErrorInfo != null && !string.IsNullOrEmpty(resultUpdate.ErrorInfo.Description))
            {
                Console.WriteLine(resultUpdate.ErrorInfo.Description);
                Console.WriteLine("Update of AssetTree failed");
                return;
            }

            Console.WriteLine("AssetTree is updated");
        }

        public static void Restore(CorrigoService service, AssetTree toRestore)
        {
            if (toRestore == null || service == null) return;
            Console.WriteLine();
            Console.WriteLine($"Restoring AssetTree with id={toRestore.Id.ToString()}");

            var restoreResult = service.Execute(new RestoreCommand
            {
                EntitySpecifier = new EntitySpecifier { Id = toRestore.Id, EntityType = EntityType.AssetTree }
            });

            if (restoreResult == null)
            {
                Console.WriteLine("Update of AssetTree failed");
                return;
            }

            if (restoreResult.ErrorInfo != null && !string.IsNullOrEmpty(restoreResult.ErrorInfo.Description))
            {
                Console.WriteLine(restoreResult.ErrorInfo.Description);
                Console.WriteLine("Restore of AssetTree failed");
                return;
            }

            Console.WriteLine("AssetTree is restored");

        }
    }
}
