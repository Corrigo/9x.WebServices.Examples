using CorrigoServiceWebReference.CorrigoGA;
using System;

namespace _9x.WebServices.WorkZones.Operations
{
	internal static class Delete
	{
        // WorkZone is Readonly - NonCreatable, NonDeletable, NonUpdatable
        public static void Execute(CorrigoService service, int id)
		{
            Console.WriteLine();
            Console.WriteLine($"Delete WorkZone with id={id}");
            var resultDelete = service.Execute(new DeleteCommand
            {
                EntitySpecifier = new EntitySpecifier { Id = id, EntityType = EntityType.WorkZone },
            });

            if (resultDelete == null)
            {
                Console.WriteLine("Delete is failed");
                return;
            }

            if (resultDelete.ErrorInfo != null && !string.IsNullOrEmpty(resultDelete.ErrorInfo.Description))
            {
                Console.WriteLine(resultDelete.ErrorInfo.Description);
                Console.WriteLine("Delete of WorkZone failed");
                return;
            }

            Console.WriteLine("WorkZone is deleted");
        }
	}
}
