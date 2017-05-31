using CorrigoServiceWebReference.CorrigoGA;
using System;
using System.Diagnostics;
using System.Security.Cryptography;

namespace _9x.WebServices.Organizations.Operations
{
	internal static class Create
	{

        public static Organization Execute(CorrigoService service)
        {
            Console.WriteLine();
            Debug.Print("Creating Organization");
            Console.WriteLine("Creating Organization");

            var toCreate = new Organization
            {
                DisplayAs = "Fake Organization",
                Number = "Org.Fake.01",
            };

            var resultData = service.Execute(new CreateCommand
            {
                Entity = toCreate
            });

            if (resultData == null)
            {
                Debug.Print("Creation of new Organization failed");
                Console.WriteLine("Creation of new Organization failed");
                Console.WriteLine();
                return null;
            }


            var commandResponse = resultData as OperationCommandResponse;

            int? id = (commandResponse != null && commandResponse.EntitySpecifier != null) ? commandResponse.EntitySpecifier.Id : null;

            if (id.HasValue && resultData.ErrorInfo == null)
            {
                toCreate.Id = id.Value;
                Debug.Print($"Created new Organization with Id={id.ToString()}");
                Console.WriteLine($"Created new Organization with Id={id.ToString()}");
                Console.WriteLine();
                return toCreate;
            }


            Debug.Print("Creation of new Organization failed");
            Console.WriteLine("Creation of new Organization failed");
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
