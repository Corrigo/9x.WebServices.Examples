using CorrigoServiceWebReference.CorrigoGA;

namespace _9x.WebServices.WorkOrders.Operations.Notes
{
	internal static class Read
	{
		public static WoNote Retrieve (CorrigoService service, int noteId)
		{
			var entitySpecifier = new EntitySpecifier { EntityType = EntityType.WoNote, Id = noteId };

			var propertySet = new PropertySet
			{
				Properties = new[]
				{
					nameof(WoNote.Id),
					nameof(WoNote.Body),
					nameof(WoNote.CreatedDate),
					//nameof(WoNote.PerformDeletion),
					nameof(WoNote.NoteTypeId),
					nameof(WoNote.WorkOrderId),
					nameof(WoNote.CreatedBy) + ".*"
				}
			};

			var note = service.Retrieve(entitySpecifier, propertySet) as WoNote;
			return note;
		}
	}
}
