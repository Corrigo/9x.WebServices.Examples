using System.Diagnostics;
using CorrigoServiceWebReference.CorrigoGA;

namespace _9x.WebServices.WorkOrders.Operations.Notes
{
	internal static class Update
	{
		public static void Execute (CorrigoService service, int workOrderId, int noteId)
		{
			var workOrder = new WorkOrder { Id = workOrderId };
			workOrder.Notes = new[] {new WoNote
			{
				Id = noteId,
				Body = "new note body",
				NoteTypeId = WONoteType.Public,
				WorkOrderId = workOrderId
			}};

			var command = new UpdateCommand
			{
				Entity = workOrder,
				PropertySet = new PropertySet { Properties = new[] { "Notes.*" } }
			};
			var response = service.Execute(command) as OperationCommandResponse;

			//var updatedNoteId = response?.NestedEntitiesOperationResults?[0]?.EntitySpecifier?.Id ?? 0;
			Debug.Print(response?.ErrorInfo?.Description
					?? $"Successfulle created note with id {noteId} for WorkOrder {workOrderId}");
		}
	}
}
