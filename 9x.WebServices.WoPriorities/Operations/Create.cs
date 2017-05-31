using CorrigoServiceWebReference.CorrigoGA;
using System;
using System.Diagnostics;

namespace _9x.WebServices.WoPriorities.Operations
{
	internal static class Create
	{
        //
        // Warning : for the moment create WoPriority does not work properly - it just creates an empty WoPriority.
        //
        public static WoPriority Execute(CorrigoService service)
		{
            Debug.Print("Creating WoPriority");
            Console.WriteLine("Creating WoPriority");

            var toCreate = new WoPriority

            {
                DisplayAs = $"Priority.ByWSDK.{DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}", //Label
                IsEmergency = true,  //Escalate
                RespondInMinutes = 11, 
                DueInMinutes = 22,     
                AcknowledgeInMinutes= 15
            };
            
            var resultData = service.Execute(new CreateCommand
            {
                Entity = toCreate
            });

            if (resultData == null)
            {
                Debug.Print("Creation of new WoPriority failed");
                Console.WriteLine("Creation of new WoPriority failed");
                Console.WriteLine();
                return null;
            }


            var commandResponse = resultData as OperationCommandResponse;

            int? id = (commandResponse != null && commandResponse.EntitySpecifier != null) ? commandResponse.EntitySpecifier.Id : null;

            if (id.HasValue && resultData.ErrorInfo == null)
            {
                toCreate.Id = id.Value;
                Debug.Print($"Created new WoPriority with Id={id.ToString()}");
                Console.WriteLine($"Created new WoPriority with Id={id.ToString()}");
                Console.WriteLine();
                return toCreate;
            }


            Debug.Print("Creation of new WoPriority failed");
            Console.WriteLine("Creation of new WoPriority failed");
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
