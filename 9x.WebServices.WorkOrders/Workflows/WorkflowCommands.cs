using CorrigoServiceWebReference.CorrigoGA;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace _9x.WebServices.WorkOrders.Workflows
{
	internal static class WorkflowCommands
	{
		private static void ExecuteAndPrintResponse (this CorrigoService service,
													BaseWoActionCommand command,
													[CallerMemberName] string callerName = "Execute Command")
		{
			if (service == null) throw new ArgumentNullException(nameof(service), "Service should not be null");

			var response = (WoActionResponse) service.Execute(command);
			Debug.Print(response?.ErrorInfo?.Description
					?? $"{callerName} successfully! Current status is '{response?.Wo.StatusId ?? WorkOrderStatus.Unknown}'");
		}

		public static void WoAssignCommand_Execute (CorrigoService service, int workOrderId)
		{
			var command = new WoAssignCommand
			{
				WorkOrderId = workOrderId,
				Comment = "test assignment",
				Mode = WoChangeAssigmentMode.Primary,
				Assignees = new int[] { 3 }                // is taken from Eployee db table
			};

			service.ExecuteAndPrintResponse(command);
		}

		public static void WoPickUpCommand_Execute (CorrigoService service, int workOrderId)
		{
			var command = new WoPickUpCommand
			{
				Comment = "running pick up command",
				WorkOrderId = workOrderId,
			};

			service.ExecuteAndPrintResponse(command);
		}

		public static void WoStartCommand_Execute (CorrigoService service, int workOrderId)
		{
			var command = new WoStartCommand
			{
				Comment = "running start command",
				WorkOrderId = workOrderId,
			};

			service.ExecuteAndPrintResponse(command);
		}

		public static void WoPauseCommand_Execute (CorrigoService service, int workOrderId)
		{
			var command = new WoPauseCommand
			{
				Comment = "running pause command",
				WorkOrderId = workOrderId,
			};

			service.ExecuteAndPrintResponse(command);
		}

		public static void WoReopenCommand_Execute (CorrigoService service, int workOrderId)
		{
			var command = new WoReopenCommand
			{
				Comment = "running reopen command",
				WorkOrderId = workOrderId,
			};
			service.ExecuteAndPrintResponse(command);
		}

		public static void WoOnHoldCommand_Execute (CorrigoService service, int workOrderId, int actionReasonId)
		{
			var command = new WoOnHoldCommand
			{
				Comment = "running on hold command",
				WorkOrderId = workOrderId,
				ActionReasonId = actionReasonId,        //is taken from database table [WOActionReasonLookup]
			};
			service.ExecuteAndPrintResponse(command);
		}

		public static void WoCancelCommand_Execute (CorrigoService service, int workOrderId, int actionReasonId)
		{
			var command = new WoCancelCommand
			{
				Comment = "running cancel command",
				WorkOrderId = workOrderId,
				ActionReasonId = actionReasonId,        //is taken from database table [WOActionReasonLookup]
			};
			service.ExecuteAndPrintResponse(command);
		}

		public static void WoCompleteCommand_Execute (CorrigoService service, int workOrderId, int repairCodeId)
		{
			var command = new WoCompleteCommand
			{
				Comment = "running complete command",
				WorkOrderId = workOrderId,
				RepairCodeId = repairCodeId,
			};
			service.ExecuteAndPrintResponse(command);
		}
	}
}
