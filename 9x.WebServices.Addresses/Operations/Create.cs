using CorrigoServiceWebReference.CorrigoGA;
using System;
using System.Diagnostics;
using System.Security.Cryptography;

namespace _9x.WebServices.Addresses.Operations
{
	internal static class Create
	{
        
        public static Address2 Execute(CorrigoService service)
        {
            Console.WriteLine();
            Debug.Print("Creating Address2");
            Console.WriteLine("Creating Address2");

            // Create new Employee
            Employee newEmployee = _9x.WebServices.Employees.Operations.Create.Execute(service);
            
            var toCreate = new Address2
            {
                ActorTypeId = ActorType.Employee,
                TypeId = StreetAddrType.Primary,
                ActorId = newEmployee.Id,
                Street = "Test Street" + $".ByWSDK.{DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}",
            };

            var resultData = service.Execute(new CreateCommand
            {
                Entity = toCreate
            });

            if (resultData == null)
            {
                Debug.Print("Creation of new Address2 failed");
                Console.WriteLine("Creation of new Address2 failed");
                Console.WriteLine();
                return null;
            }


            var commandResponse = resultData as OperationCommandResponse;

            int? id = (commandResponse != null && commandResponse.EntitySpecifier != null) ? commandResponse.EntitySpecifier.Id : null;

            if (id.HasValue && resultData.ErrorInfo == null)
            {
                toCreate.Id = id.Value;
                Debug.Print($"Created new Address2 with Id={id.ToString()}");
                Console.WriteLine($"Created new Address2 with Id={id.ToString()}");
                Console.WriteLine();
                return toCreate;
            }


            Debug.Print("Creation of new Address2 failed");
            Console.WriteLine("Creation of new Address2 failed");
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
