using CorrigoServiceWebReference.CorrigoGA;
using System;
using System.Diagnostics;
using System.Security.Cryptography;

namespace _9x.WebServices.Employees.Operations
{
	public static class Create
	{

        public static Employee Execute(CorrigoService service)
        {
            Debug.Print("Creating Employee");
            Console.WriteLine("Creating Employee");

            Random rnd = new Random();
            int randNmbr = rnd.Next(0, 25);
            char[] alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

            string suffix = String.Concat("_", alpha[randNmbr], randNmbr.ToString());

            var toCreate = new Employee

            {
                FirstName = "FN"+ suffix,
                LastName = "LN" + suffix,
                DisplayAs = "FN.LN_"+ suffix+ $".ByWSDK.{DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}",
                Username = "FN.LN_" + suffix,

                Password = "PWD_" + suffix + "!Hey-Ya!",
                ActorTypeId = ActorType.Employee,
                WonServiceRadius = 1,
                Role = new Role { Id=1 },

                Number = "Number"+ suffix,
                FederalId = "FederalId" + suffix,
                ExternalId = "ExternalId" + suffix

            };


            var resultData = service.Execute(new CreateCommand
            {
                Entity = toCreate
            });

            if (resultData == null)
            {
                Debug.Print("Creation of new Employee failed");
                Console.WriteLine("Creation of new Employee failed");
                Console.WriteLine();
                return null;
            }


            var commandResponse = resultData as OperationCommandResponse;

            int? id = (commandResponse != null && commandResponse.EntitySpecifier != null) ? commandResponse.EntitySpecifier.Id : null;

            if (id.HasValue && resultData.ErrorInfo == null)
            {
                toCreate.Id = id.Value;
                Debug.Print($"Created new Employee with Id={id.ToString()}");
                Console.WriteLine($"Created new Employee with Id={id.ToString()}");
                Console.WriteLine();
                return toCreate;
            }


            Debug.Print("Creation of new Employee failed");
            Console.WriteLine("Creation of new Employee failed");
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
