using CorrigoServiceWebReference.CorrigoGA;
using System;

namespace _9x.WebServices.WoLastActions.Operations
{
	internal static class Delete
	{
        // WoLastAction is Readonly - NonCreatable, NonDeletable, NonUpdatable
        public static void Execute(CorrigoService service, int id)
		{
            Console.WriteLine();
            Console.WriteLine($"Delete WoLastAction with id={id}");
            var resultDelete = service.Execute(new DeleteCommand
            {
                EntitySpecifier = new EntitySpecifier { Id = id, EntityType = EntityType.WoLastAction },
            });

            if (resultDelete == null)
            {
                Console.WriteLine("Delete is failed");
                return;
            }

            if (resultDelete.ErrorInfo != null && !string.IsNullOrEmpty(resultDelete.ErrorInfo.Description))
            {
                Console.WriteLine(resultDelete.ErrorInfo.Description);
                Console.WriteLine("Delete of WoLastAction failed");
                return;
            }

            Console.WriteLine("WoLastAction is deleted");
        }
	}
}
