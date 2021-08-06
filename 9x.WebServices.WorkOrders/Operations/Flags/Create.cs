using CorrigoServiceWebReference.CorrigoGA;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _9x.WebServices.WorkOrders.Operations.Flags
{
    internal static class Create
    {
        public static int? Execute(CorrigoService service, int workOrderId)
        {
            Console.WriteLine();
            Debug.Print("Creating Flags");
            Console.WriteLine("Creating Flags");

            var toCreate = new WoFlag
            {
                WoId = workOrderId,
                FlagId = 1,
                UtcStamp = DateTime.Now,
                Comment = "creation"
            };

            var command = new CreateCommand { Entity = toCreate };
            var response = service.Execute(command) as OperationCommandResponse;

            Debug.Print(response.ErrorInfo?.Description
                ?? $"Successfully created WoFlag for Wo with id {workOrderId}");

            return response.EntitySpecifier?.Id;
        }

    }
}
