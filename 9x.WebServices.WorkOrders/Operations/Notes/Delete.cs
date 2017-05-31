using CorrigoServiceWebReference.CorrigoGA;
using System;
using System.Diagnostics;
using System.Linq;
using WoRead = _9x.WebServices.WorkOrders.Operations.Read;

namespace _9x.WebServices.WorkOrders.Operations.Notes
{
	internal static class Delete
	{
		public static void Execute (CorrigoService service, int workOrderId, int noteId)
		{
			//make sure Read.Execute has "Notes.*" in property set of retrieve command 
			var workOrder = WoRead.Execute(service, workOrderId);
			var aNote = workOrder.Notes.FirstOrDefault(n => n.Id == noteId);
			if (aNote == null)
				throw new IndexOutOfRangeException($"Threre is no notes with id '{noteId}' for work order with id '{workOrderId}'.");

			aNote.PerformDeletion = true;

			var command = new UpdateCommand
			{
				Entity = workOrder,
				PropertySet = new PropertySet { Properties = new[] { "Notes.*" } }
			};
			var response = service.Execute(command) as OperationCommandResponse;

			Debug.Print(response?.ErrorInfo?.Description
				?? $"Successfully updated work order with id '{workOrderId}'");

			var updatedWorkOrder = WoRead.Execute(service, workOrderId);
			var deletedNote = updatedWorkOrder.Notes.FirstOrDefault(n => n.Id == noteId);

			Debug.Print(deletedNote == null
				? $"Successfully deleted note with id '{noteId}' for work order with id '{workOrderId}'"
				: $"Cannot delete note with id '{noteId}' for work order with id '{workOrderId}'");
		}
	}
}
