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
        public static int? ExecuteCreateCommand(CorrigoService service, int workOrderId)
        {
            Console.WriteLine();
            Debug.Print("Creating Flags");
            Console.WriteLine("Creating CreateCommand");

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

        public static WorkOrder ExecuteWoFlagCommand(CorrigoService service, int workOrderId, int flagId)
        {
            Console.WriteLine();
            Debug.Print("Creating Flags");
            Console.WriteLine("Creating WoFlagCommand");

            var command = new WoFlagCommand { WorkOrderId = workOrderId, ActionDate = DateTime.Now, ActionReasonId = flagId, StatusId = WOStatus.New };
            var response = service.Execute(command) as WoActionResponse;

            Debug.Print(response.ErrorInfo?.Description
                ?? $"Successfully created WoFlag for Wo with id {workOrderId}");

            return response.Wo;
        }
    }
}
