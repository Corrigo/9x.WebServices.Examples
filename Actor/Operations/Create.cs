using CorrigoServiceWebReference.CorrigoGA;
using System.Diagnostics;

namespace _9x.WebServices.Actors.Operations
{
	internal static class Create
	{
		/// <summary> Create operation is not supported by Actor entity. </summary>
		public static int Execute(CorrigoService service, ActorType actorType)
		{
			var actor = new Actor
			{
				DisplayAs = "test actor",
				TypeId = actorType
			};
			var command = new CreateCommand { Entity = actor };
			var response = service.Execute(command) as OperationCommandResponse;

			int id = response.EntitySpecifier?.Id ?? 0;
			Debug.Print(response.ErrorInfo?.Description
						?? $"Successfully created actor with type {actorType} with id {id}");

			return id;
		}
	}
}