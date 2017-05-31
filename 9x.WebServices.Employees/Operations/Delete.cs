using CorrigoServiceWebReference.CorrigoGA;
using System;

namespace _9x.WebServices.Employees.Operations
{
	internal static class Delete
	{
        public static void Execute(CorrigoService service, int id)
		{
            Console.WriteLine($"Delete Employee with id={id}");
            var resultDelete = service.Execute(new DeleteCommand
            {
                EntitySpecifier = new EntitySpecifier { Id = id, EntityType = EntityType.Employee },
            });

            if (resultDelete == null)
            {
                Console.WriteLine("Delete is failed");
                return;
            }

            if (resultDelete.ErrorInfo != null && !string.IsNullOrEmpty(resultDelete.ErrorInfo.Description))
            {
                Console.WriteLine(resultDelete.ErrorInfo.Description);
                Console.WriteLine("Delete of Employee failed");
                return;
            }

            Console.WriteLine("Employee is deleted");
        }
	}
}
