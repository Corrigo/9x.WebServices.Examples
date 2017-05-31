using CorrigoServiceWebReference.CorrigoGA;
using System.Diagnostics;
using static _9x.WebServices.WorkOrders.Workflows.WorkflowCommands;

namespace _9x.WebServices.WorkOrders
{
	/// <summary>
	/// there are 2 types of methods in the class:
	/// 1 - simple dereferencing to one command execution - be aware of initial WO state - some commands cannot be successfully executed when WO is in some state.
	/// 2 - a sequence of commands - such method transmits WO from state A to state B through a set of intermediate states
	/// </summary>
	/// <remarks>
	/// all commands are taken from WorkflowCommands class
	/// </remarks>
	public static class WorkOrderWorkflowExamples
	{
		public static void Assign (CorrigoService service, int id)
		{
			WoAssignCommand_Execute(service, id);
		}
		public static void PickUp (CorrigoService service, int id)
		{
			WoPickUpCommand_Execute(service, id);
		}
		public static void Start (CorrigoService service, int id)
		{
			WoStartCommand_Execute(service, id);
		}
		public static void Pause (CorrigoService service, int id)
		{
			WoPauseCommand_Execute(service, id);
		}
		public static void Reopen (CorrigoService service, int id)
		{
			WoReopenCommand_Execute(service, id);
		}
		public static void OnHold (CorrigoService service, int id, int actionReasonId = 1286)
		{
			WoOnHoldCommand_Execute(service, id, actionReasonId);
		}
		public static void Cancel (CorrigoService service, int id, int actionReasonId = 1794)
		{
			WoCancelCommand_Execute(service, id, actionReasonId);
		}
		public static void Complete (CorrigoService service, int id, int repairCodeId = 6)
		{
			WoCompleteCommand_Execute(service, id, repairCodeId);
		}

		public static void NewToPause (CorrigoService service, int id)
		{
			Debug.Print($"Beginning of {nameof(NewToPause)} method execution");

			WoAssignCommand_Execute(service, id);
			WoPickUpCommand_Execute(service, id);
			WoStartCommand_Execute(service, id);
			WoPauseCommand_Execute(service, id);

			Debug.Print($"Ending of {nameof(NewToPause)} method execution");
		}
		public static void NewToOnHoldThroughPause (CorrigoService service, int id)
		{
			Debug.Print($"Beginning of {nameof(NewToOnHoldThroughPause)} method execution");

			WoAssignCommand_Execute(service, id);
			WoPickUpCommand_Execute(service, id);
			WoStartCommand_Execute(service, id);
			WoPauseCommand_Execute(service, id);
			WoReopenCommand_Execute(service, id);
			WoPickUpCommand_Execute(service, id);
			WoStartCommand_Execute(service, id);
			WoOnHoldCommand_Execute(service, id, 1286);

			Debug.Print($"Ending of {nameof(NewToOnHoldThroughPause)} method execution");
		}
		public static void NewToCompleteThroughCancel (CorrigoService service, int id)
		{
			Debug.Print($"Beginning of {nameof(NewToCompleteThroughCancel)} method execution");

			WoAssignCommand_Execute(service, id);
			WoPickUpCommand_Execute(service, id);
			WoStartCommand_Execute(service, id);
			WoCancelCommand_Execute(service, id, 1794);
			WoReopenCommand_Execute(service, id);
			WoPickUpCommand_Execute(service, id);
			WoStartCommand_Execute(service, id);
			WoCompleteCommand_Execute(service, id, 6);

			Debug.Print($"Ending of {nameof(NewToCompleteThroughCancel)} method execution");
		}
	}
}
