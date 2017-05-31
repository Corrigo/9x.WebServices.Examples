using CorrigoServiceWebReference.CorrigoGA;
using System;

namespace _9x.WebServices.Documents.Operations
{
	internal static class Delete
	{
        
        public static void Execute(CorrigoService service, int id)
		{
            Console.WriteLine();
            Console.WriteLine($"Delete Document with id={id}");
            var resultDelete = service.Execute(new DeleteCommand
            {
                EntitySpecifier = new EntitySpecifier { Id = id, EntityType = EntityType.Document },
            });

            if (resultDelete == null)
            {
                Console.WriteLine("Delete is failed");
                return;
            }

            if (resultDelete.ErrorInfo != null && !string.IsNullOrEmpty(resultDelete.ErrorInfo.Description))
            {
                Console.WriteLine(resultDelete.ErrorInfo.Description);
                Console.WriteLine("Delete of Document failed");
                return;
            }

            Console.WriteLine("Document is deleted");
        }
	}
}
