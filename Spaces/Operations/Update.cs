using CorrigoServiceWebReference.CorrigoGA;

namespace _9xWebServices.Spaces.Operations
{
	internal static class Update
	{
		public static OperationCommandResponse Execute (CorrigoService service, int id)
		{
			var command = new UpdateCommand
			{
				Entity = new Space
				{
					Id = id,
					CustomerId = 34,

				},     //customer is required, unit is required
				PropertySet = new AllProperties()
			};

			var response = service.Execute(command);
			return response as OperationCommandResponse;
		}
		public static OperationCommandResponse Execute (CorrigoService service, Space space)
		{
			var command = new UpdateCommand
			{
				Entity = space,
				PropertySet = new AllProperties()
			};

			var response = service.Execute(command);
			return response as OperationCommandResponse;
		}
	}
}
