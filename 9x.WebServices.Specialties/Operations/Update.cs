using CorrigoServiceWebReference.CorrigoGA;
using System;

namespace _9x.WebServices.Specialties.Operations
{
    internal static class Update
    {

        public static void Execute(CorrigoService service, Specialty toUpdate)
        {
            if (toUpdate == null || service == null) return;

            var taxCodes = service.RetrieveMultiple(
                new QueryByProperty
                {
                    EntityType = EntityType.TaxCode,
                    Count = 1,
                    PropertySet = new AllProperties(),
                    Conditions = new PropertyValuePair[0]
                });

            if (taxCodes != null && taxCodes.Length == 1)
            {
                toUpdate.TaxCode = taxCodes[0] as TaxCode;
            }

            toUpdate.Instructions = "-SOS-";



            Console.WriteLine();
            Console.WriteLine($"Updating Specialty with id={toUpdate.Id}");

            var resultUpdate = service.Execute(new UpdateCommand
            {
                Entity = toUpdate,
                PropertySet = new PropertySet { Properties = new[] { "TaxCode.*", "Instructions" } }
            });

            if (resultUpdate == null)
            {
                Console.WriteLine("Update of Specialty failed");
                return;
            }

            if (resultUpdate.ErrorInfo != null && !string.IsNullOrEmpty(resultUpdate.ErrorInfo.Description))
            {
                Console.WriteLine(resultUpdate.ErrorInfo.Description);
                Console.WriteLine("Update of Specialty failed");
                return;
            }

            Console.WriteLine("Specialty is updated");

        }

        public static void Restore(CorrigoService service, Specialty toRestore)
        {
            if (toRestore == null || service == null) return;
            Console.WriteLine();
            Console.WriteLine($"Restoring Specialty with id={toRestore.Id}");

            var restoreResult = service.Execute(new RestoreCommand
            {
                EntitySpecifier = new EntitySpecifier { Id = toRestore.Id, EntityType = EntityType.Specialty }
            });

            if (restoreResult == null)
            {
                Console.WriteLine("Update of Specialty failed");
                Console.WriteLine();
                return;
            }

            if (restoreResult.ErrorInfo != null && !string.IsNullOrEmpty(restoreResult.ErrorInfo.Description))
            {
                Console.WriteLine(restoreResult.ErrorInfo.Description);
                Console.WriteLine("Restore of Specialty failed");
                Console.WriteLine();
                return;
            }

            Console.WriteLine("Specialty is restored");
            Console.WriteLine();

        }
    }
}
