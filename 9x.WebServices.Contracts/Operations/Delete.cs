using CorrigoServiceWebReference.CorrigoGA;
using System;

namespace _9x.WebServices.Contracts.Operations
{
	internal static class Delete
	{
        // Contract is Readonly - NonCreatable, NonDeletable, NonUpdatable
        public static void Execute(CorrigoService service, int id)
		{
            Console.WriteLine();
            Console.WriteLine($"Delete Contract with id={id}");
            var resultDelete = service.Execute(new DeleteCommand
            {
                EntitySpecifier = new EntitySpecifier { Id = id, EntityType = EntityType.Contract },
            });

            if (resultDelete == null)
            {
                Console.WriteLine("Delete is failed");
                return;
            }

            if (resultDelete.ErrorInfo != null && !string.IsNullOrEmpty(resultDelete.ErrorInfo.Description))
            {
                Console.WriteLine(resultDelete.ErrorInfo.Description);
                Console.WriteLine("Delete of Contract failed");
                return;
            }

            Console.WriteLine("Contract is deleted");
        }
	}
}
