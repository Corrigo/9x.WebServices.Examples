using CorrigoServiceWebReference.CorrigoGA;
using System;
using System.Diagnostics;
using System.Security.Cryptography;

namespace _9x.WebServices.AssetTrees.Operations
{
	internal static class Create
	{

        public static AssetTree Execute(CorrigoService service)
        {
            Console.WriteLine();
            Debug.Print("Creating AssetTree");
            Console.WriteLine("Creating AssetTree");

            var toCreate = new AssetTree
            {
                //ParentId =1,
                //ChildId = 1,
                //Child = new Location { Id = 1},
                //Distance = 2
            };

            var resultData = service.Execute(new CreateCommand
            {
                Entity = toCreate
            });

            if (resultData == null)
            {
                Debug.Print("Creation of new AssetTree failed");
                Console.WriteLine("Creation of new AssetTree failed");
                Console.WriteLine();
                return null;
            }


            var commandResponse = resultData as OperationCommandResponse;

            int? id = (commandResponse != null && commandResponse.EntitySpecifier != null) ? commandResponse.EntitySpecifier.Id : null;

            if (id.HasValue && resultData.ErrorInfo == null)
            {
                toCreate.Id = id.Value;
                Debug.Print($"Created new AssetTree with Id={id.ToString()}");
                Console.WriteLine($"Created new AssetTree with Id={id.ToString()}");
                Console.WriteLine();
                return toCreate;
            }


            Debug.Print("Creation of new AssetTree failed");
            Console.WriteLine("Creation of new AssetTree failed");
            Console.WriteLine();
            if (resultData.ErrorInfo != null && !string.IsNullOrEmpty(resultData.ErrorInfo.Description))
            {
                Debug.Print(resultData.ErrorInfo.Description);
                Console.WriteLine(resultData.ErrorInfo.Description);
            }


            return null;
        }
    }
}
