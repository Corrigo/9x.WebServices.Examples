using CorrigoServiceWebReference.CorrigoGA;
using System.Diagnostics;

namespace _9x.WebServices.Actors.Operations
{
	internal static class Read
	{
		public static Actor Retreive(CorrigoService service, int actorId = 11)
		{
			var actorSpecifier = new ActorEntitySpecifier
			{
				EntityType = EntityType.Actor,
				Id = actorId,
				TypeId = ActorType.Employee
			};
			string[] properties = { nameof(Actor.TypeId), nameof(Actor.DisplayAs), nameof(Actor.Id) };
			var actor = service.Retrieve(actorSpecifier, new PropertySet { Properties = properties }) as Actor;

			Debug.Print($"Retrieved actor of type {actor.TypeId} with description {actor.DisplayAs}");

			return actor;
		}
	}
}