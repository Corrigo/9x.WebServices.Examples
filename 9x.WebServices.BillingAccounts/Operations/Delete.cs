using CorrigoServiceWebReference.CorrigoGA;
using System;

namespace _9x.WebServices.BillingAccounts.Operations
{
	internal static class Delete
	{
        // BillingAccount is Readonly - NonCreatable, NonDeletable, NonUpdatable
        public static void Execute(CorrigoService service, int id)
		{
            Console.WriteLine();
            Console.WriteLine($"Delete BillingAccount with id={id}");
            var resultDelete = service.Execute(new DeleteCommand
            {
                EntitySpecifier = new EntitySpecifier { Id = id, EntityType = EntityType.BillingAccount },
            });

            if (resultDelete == null)
            {
                Console.WriteLine("Delete is failed");
                return;
            }

            if (resultDelete.ErrorInfo != null && !string.IsNullOrEmpty(resultDelete.ErrorInfo.Description))
            {
                Console.WriteLine(resultDelete.ErrorInfo.Description);
                Console.WriteLine("Delete of BillingAccount failed");
                return;
            }

            Console.WriteLine("BillingAccount is deleted");
        }
	}
}
