using CorrigoServiceWebReference.CorrigoGA;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _9x.WebServices.WorkOrders.Operations.Flags
{
    internal static class Update
    {
        public static WorkOrder SetFlags(CorrigoService service, int workOrderId, int[] flagIds)
        {
            Console.WriteLine();
            Debug.Print("Setting Flags");
            Console.WriteLine("Setting Flags");

            var command = new WoSetFlagsCommand { 
                FlagsToSet = flagIds, 
                WorkOrderId = workOrderId, 
                ActionDate = DateTime.Now, 
                ActionReasonId = 3334,  //is taken from database table [WOActionReasonLookup]
                ConcurrencyId = 1,
                Comment = "flag setup",
                StatusId = WOStatus.InProgress
            };
            var response = service.Execute(command) as WoActionResponse;

            Debug.Print(response.ErrorInfo?.Description
                ?? $"Successfully setup WoFlag for Wo with id {workOrderId}");

            return response.Wo;
        }
        public static WorkOrder ClearFlags(CorrigoService service, int workOrderId, int[] flagIds)
        {
            Console.WriteLine();
            Debug.Print("Setting Flags");
            Console.WriteLine("Setting Flags");

            var command = new WoClearFlagsCommand
            {
                FlagsToClear = flagIds,
                WorkOrderId = workOrderId,
                ActionDate = DateTime.Now,
                ActionReasonId = 1796,
                ConcurrencyId = 1,
                Comment = "flag setup",
                StatusId = WOStatus.Cancelled
            };
            var response = service.Execute(command) as WoActionResponse;

            Debug.Print(response.ErrorInfo?.Description
                ?? $"Successfully cleared WoFlag for Wo with id {workOrderId}");

            return response.Wo;
        }

    }
}
