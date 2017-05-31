using CorrigoServiceWebReference.CorrigoGA;

namespace _9xWebServices.Spaces.Operations
{
	internal static class Delete
	{
		public static CommandResponse Execute (CorrigoService service, int id)
		{
			var command = new DeleteCommand
			{
				EntitySpecifier = new EntitySpecifier
				{
					EntityType = EntityType.Space,
					Id = id
				}
			};

			var response = service.Execute(command);
			return response;
		}
	}
}
