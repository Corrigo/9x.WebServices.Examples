using CorrigoServiceWebReference.CorrigoGA;
using System;
using System.Diagnostics;
using System.Security.Cryptography;

namespace _9x.WebServices.Specialties.Operations
{
	internal static class Create
	{

        public static Specialty Execute(CorrigoService service)
        {
            Console.WriteLine();
            Debug.Print("Creating Specialty");
            Console.WriteLine("Creating Specialty");

            var toCreate = new Specialty
            {
                DisplayAs = "Test" + $".ByWSDK.{DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}",
            };

            var resultData = service.Execute(new CreateCommand
            {
                Entity = toCreate
            });

            if (resultData == null)
            {
                Debug.Print("Creation of new Specialty failed");
                Console.WriteLine("Creation of new Specialty failed");
                Console.WriteLine();
                return null;
            }


            var commandResponse = resultData as OperationCommandResponse;

            int? id = (commandResponse != null && commandResponse.EntitySpecifier != null) ? commandResponse.EntitySpecifier.Id : null;

            if (id.HasValue && resultData.ErrorInfo == null)
            {
                toCreate.Id = id.Value;
                Debug.Print($"Created new Specialty with Id={id}");
                Console.WriteLine($"Created new Specialty with Id={id}");
                Console.WriteLine();
                return toCreate;
            }


            Debug.Print("Creation of new Specialty failed");
            Console.WriteLine("Creation of new Specialty failed");
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
