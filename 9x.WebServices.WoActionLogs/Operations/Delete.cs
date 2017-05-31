using CorrigoServiceWebReference.CorrigoGA;
using System;

namespace _9x.WebServices.WoActionLogs.Operations
{
	internal static class Delete
	{
        // WoActionLog is Readonly - NonCreatable, NonDeletable, NonUpdatable
        public static void Execute(CorrigoService service, int id)
		{
            Console.WriteLine();
            Console.WriteLine($"Delete WoActionLog with id={id}");
            var resultDelete = service.Execute(new DeleteCommand
            {
                EntitySpecifier = new EntitySpecifier { Id = id, EntityType = EntityType.WoActionLog },
            });

            if (resultDelete == null)
            {
                Console.WriteLine("Delete is failed");
                return;
            }

            if (resultDelete.ErrorInfo != null && !string.IsNullOrEmpty(resultDelete.ErrorInfo.Description))
            {
                Console.WriteLine(resultDelete.ErrorInfo.Description);
                Console.WriteLine("Delete of WoActionLog failed");
                return;
            }

            Console.WriteLine("WoActionLog is deleted");
        }
	}
}
