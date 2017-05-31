using CorrigoServiceWebReference.CorrigoGA;
using System;

namespace _9x.WebServices.Addresses.Operations
{
	internal static class Delete
	{
        
        public static void Execute(CorrigoService service, int id)
		{
            Console.WriteLine();
            Console.WriteLine($"Delete Address2 with id={id}");
            var resultDelete = service.Execute(new DeleteCommand
            {
                EntitySpecifier = new EntitySpecifier { Id = id, EntityType = EntityType.Address2 },
            });

            if (resultDelete == null)
            {
                Console.WriteLine("Delete is failed");
                return;
            }

            if (resultDelete.ErrorInfo != null && !string.IsNullOrEmpty(resultDelete.ErrorInfo.Description))
            {
                Console.WriteLine(resultDelete.ErrorInfo.Description);
                Console.WriteLine("Delete of Address2 failed");
                return;
            }

            Console.WriteLine("Address2 is deleted");
        }
	}
}
