using CorrigoServiceWebReference.CorrigoGA;
using System;
using System.Diagnostics;
using System.Security.Cryptography;

namespace _9x.WebServices.Contacts.Operations
{
	internal static class Create
	{
        
        public static Contact Execute(CorrigoService service)
        {
            Console.WriteLine();
            Debug.Print("Creating Contact");
            Console.WriteLine("Creating Contact");
            
            var toCreate = new Contact
            {
                DisplayAs = "Test Contact" + $".ByWSDK.{DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}",
                LastName = "Test Simpson",
                Username = "Test Simpson",
                Number = "Test Simpson Number",
                Comment = "Test Simpson Comment"
            };

            var resultData = service.Execute(new CreateCommand
            {
                Entity = toCreate
            });

            if (resultData == null)
            {
                Debug.Print("Creation of new Contact failed");
                Console.WriteLine("Creation of new Contact failed");
                Console.WriteLine();
                return null;
            }


            var commandResponse = resultData as OperationCommandResponse;

            int? id = (commandResponse != null && commandResponse.EntitySpecifier != null) ? commandResponse.EntitySpecifier.Id : null;

            if (id.HasValue && resultData.ErrorInfo == null)
            {
                toCreate.Id = id.Value;
                Debug.Print($"Created new Contact with Id={id.ToString()}");
                Console.WriteLine($"Created new Contact with Id={id.ToString()}");
                Console.WriteLine();
                return toCreate;
            }


            Debug.Print("Creation of new Contact failed");
            Console.WriteLine("Creation of new Contact failed");
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
