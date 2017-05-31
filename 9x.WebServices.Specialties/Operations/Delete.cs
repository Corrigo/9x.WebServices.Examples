using CorrigoServiceWebReference.CorrigoGA;
using System;

namespace _9x.WebServices.Specialties.Operations
{
	internal static class Delete
	{
        public static void Execute(CorrigoService service, int id)
		{
            Console.WriteLine();
            Console.WriteLine($"Delete Specialty with id={id}");
            var resultDelete = service.Execute(new DeleteCommand
            {
                EntitySpecifier = new EntitySpecifier { Id = id, EntityType = EntityType.Specialty },
            });

            if (resultDelete == null)
            {
                Console.WriteLine("Delete is failed");
                return;
            }

            if (resultDelete.ErrorInfo != null && !string.IsNullOrEmpty(resultDelete.ErrorInfo.Description))
            {
                Console.WriteLine(resultDelete.ErrorInfo.Description);
                Console.WriteLine("Delete of Specialty failed");
                return;
            }

            Console.WriteLine("Specialty is deleted");
        }
	}
}
