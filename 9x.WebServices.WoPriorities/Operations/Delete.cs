using CorrigoServiceWebReference.CorrigoGA;
using System;

namespace _9x.WebServices.WoPriorities.Operations
{
	internal static class Delete
	{
        public static void Execute(CorrigoService service, int id)
		{
            Console.WriteLine($"Delete WoPriority with id={id}");
            var resultDelete = service.Execute(new DeleteCommand
            {
                EntitySpecifier = new EntitySpecifier { Id = id, EntityType = EntityType.WoPriority },
            });

            if (resultDelete == null)
            {
                Console.WriteLine("Delete failed");
                return;
            }

            if (resultDelete.ErrorInfo != null && !string.IsNullOrEmpty(resultDelete.ErrorInfo.Description))
            {
                Console.WriteLine(resultDelete.ErrorInfo.Description);
                Console.WriteLine("Delete of WoPriority failed");
                return;
            }

            Console.WriteLine("WoPriority deleted");
        }
	}
}
