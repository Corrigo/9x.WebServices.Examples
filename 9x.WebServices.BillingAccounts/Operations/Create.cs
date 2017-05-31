using CorrigoServiceWebReference.CorrigoGA;
using System;
using System.Diagnostics;
using System.Security.Cryptography;

namespace _9x.WebServices.BillingAccounts.Operations
{
	internal static class Create
	{
        // BillingAccount is Readonly - NonCreatable, NonDeletable, NonUpdatable
        public static BillingAccount Execute(CorrigoService service)
        {
            Console.WriteLine();
            Debug.Print("Creating BillingAccount");
            Console.WriteLine("Creating BillingAccount");

            var toCreate = new BillingAccount
            {

                DisplayAs = "Test Name" + $".ByWSDK.{DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}",
                Number = "Test BA Number"

            };

            var resultData = service.Execute(new CreateCommand
            {
                Entity = toCreate
            });

            if (resultData == null)
            {
                Debug.Print("Creation of new BillingAccount failed");
                Console.WriteLine("Creation of new BillingAccount failed");
                Console.WriteLine();
                return null;
            }


            var commandResponse = resultData as OperationCommandResponse;

            int? id = (commandResponse != null && commandResponse.EntitySpecifier != null) ? commandResponse.EntitySpecifier.Id : null;

            if (id.HasValue && resultData.ErrorInfo == null)
            {
                toCreate.Id = id.Value;
                Debug.Print($"Created new BillingAccount with Id={id.ToString()}");
                Console.WriteLine($"Created new BillingAccount with Id={id.ToString()}");
                Console.WriteLine();
                return toCreate;
            }


            Debug.Print("Creation of new BillingAccount failed");
            Console.WriteLine("Creation of new BillingAccount failed");
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
