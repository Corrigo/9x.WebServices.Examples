using CorrigoServiceWebReference.CorrigoGA;
using System;
using System.Diagnostics;
using System.Security.Cryptography;

namespace _9x.WebServices.WoLastActions.Operations
{
	internal static class Create
	{
        // WoLastAction is Readonly - NonCreatable, NonDeletable, NonUpdatable
        public static WoLastAction Execute(CorrigoService service)
        {
            Console.WriteLine();
            Debug.Print("Creating WoLastAction");
            Console.WriteLine("Creating WoLastAction");

            var toCreate = new WoLastAction
            {
               WorkOrderId = 1,
               EmergencyReason = new WoActionReasonLookup()
            };

            var resultData = service.Execute(new CreateCommand
            {
                Entity = toCreate
            });

            if (resultData == null)
            {
                Debug.Print("Creation of new WoLastAction failed");
                Console.WriteLine("Creation of new WoLastAction failed");
                Console.WriteLine();
                return null;
            }


            var commandResponse = resultData as OperationCommandResponse;

            int? id = (commandResponse != null && commandResponse.EntitySpecifier != null) ? commandResponse.EntitySpecifier.Id : null;

            if (id.HasValue && resultData.ErrorInfo == null)
            {
                toCreate.Id = id.Value;
                Debug.Print($"Created new WoLastAction with Id={id.ToString()}");
                Console.WriteLine($"Created new WoLastAction with Id={id.ToString()}");
                Console.WriteLine();
                return toCreate;
            }


            Debug.Print("Creation of new WoLastAction failed");
            Console.WriteLine("Creation of new WoLastAction failed");
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
