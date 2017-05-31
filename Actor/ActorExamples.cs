using _9x.WebServices.Actors.Operations;
using CorrigoServiceWebReference.CorrigoGA;

namespace _9x.WebServices.Actors
{
	public static class ActorExamples
	{
		public static int CreateCompanyProp(CorrigoService service)
			=> Create.Execute(service, ActorType.CompanyProp);
		public static int CreateAsset(CorrigoService service)
			=> Create.Execute(service, ActorType.Asset);
		public static int CreateLeaseUser(CorrigoService service)
			=> Create.Execute(service, ActorType.LeaseUser);

		public static Actor ReadActor(CorrigoService service, int actorId)
			=> Read.Retreive(service, actorId);

	}
}
