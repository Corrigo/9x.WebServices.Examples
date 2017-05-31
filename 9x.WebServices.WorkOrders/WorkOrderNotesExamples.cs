using CorrigoServiceWebReference.CorrigoGA;

namespace _9x.WebServices.WorkOrders
{
	public static class WorkOrderNotesExamples
	{
		public static int CreateNote (CorrigoService service, int workOrderId)
			=> Operations.CreateNote.Execute(service, workOrderId);
		public static void UpdateNote (CorrigoService service, int workOrderId, int noteId)
			=> Operations.Notes.Update.Execute(service, workOrderId, noteId);
		public static void ReadNote (CorrigoService service, int noteId)
			=> Operations.Notes.Read.Retrieve(service, noteId);
		public static void DeleteNote (CorrigoService service, int workOrderId, int noteId)
			=> Operations.Notes.Delete.Execute(service, workOrderId, noteId);
	}
}
