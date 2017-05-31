using CorrigoServiceWebReference.CorrigoGA;
using System;
using System.Diagnostics;

namespace _9x.WebServices.WorkOrders.Operations
{
	/// <summary>
	/// as it's not possible to create WorkOrder.Note via CreateCommand for entity=Note
	/// we have to use UpdateCommand for WorkOrder and add new Note in it
	/// </summary>
	internal static class CreateNote
	{
		public static int Execute (CorrigoService service, int workOrderId)// WorkOrder workOrder
		{
			var workOrder = new WorkOrder { Id = workOrderId };

			workOrder.Notes = new[]
			{
				new WoNote
				{
					Body = $"this is a note for wo {workOrder.Id}",
					NoteTypeId = WONoteType.Public,
					WorkOrderId = workOrder.Id,
					CreatedDate = DateTime.Now                  //DtCreated is a required field.
				}
			};

			var command = new UpdateCommand
			{
				Entity = workOrder,
				PropertySet = new PropertySet { Properties = new[] { "Notes.*" } }
			};
			var response = service.Execute(command) as OperationCommandResponse;

			var noteId = response?.NestedEntitiesOperationResults?[0].EntitySpecifier.Id ?? 0;
			Debug.Print(response?.ErrorInfo?.Description
					?? $"Successfulle created note with id {noteId} for WorkOrder {workOrderId}");

			return noteId;
		}
	}
}
