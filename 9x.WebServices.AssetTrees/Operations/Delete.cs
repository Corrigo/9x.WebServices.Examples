using CorrigoServiceWebReference.CorrigoGA;
using System;

namespace _9x.WebServices.AssetTrees.Operations
{
	internal static class Delete
	{
        
        public static void Execute(CorrigoService service, int id)
		{
            Console.WriteLine();
            Console.WriteLine($"Delete AssetTree with id={id}");
            var resultDelete = service.Execute(new DeleteCommand
            {
                EntitySpecifier = new EntitySpecifier { Id = id, EntityType = EntityType.AssetTree },
            });

            if (resultDelete == null)
            {
                Console.WriteLine("Delete is failed");
                return;
            }

            if (resultDelete.ErrorInfo != null && !string.IsNullOrEmpty(resultDelete.ErrorInfo.Description))
            {
                Console.WriteLine(resultDelete.ErrorInfo.Description);
                Console.WriteLine("Delete of AssetTree failed");
                return;
            }

            Console.WriteLine("AssetTree is deleted");
        }
	}
}
