using _9xWebServices.Spaces.Operations;
using CorrigoServiceWebReference.CorrigoGA;

namespace _9xWebServices.Spaces
{
	public static class SpaceExamples
	{
		public static SpaceCreateCommandResponse CreateSpaceByUnitId (CorrigoService service, int? unitId)
		{
			return Create.ExecuteByUnitId(service, unitId);
		}
		public static SpaceCreateCommandResponse CreateSpaceWithNewUnit (CorrigoService service)
		{
			return Create.ExecuteWithNewUnit(service);
		}

		public static Space ReadSpace (CorrigoService service, int id)
		{
			return Read.Execute(service, id);
		}

		public static OperationCommandResponse UpdateSpaceById (CorrigoService service, int id)
		{
			return Update.Execute(service, id);
		}
		public static OperationCommandResponse UpdateSpaceByInstance (CorrigoService service, Space space)
		{
			return Update.Execute(service, space);
		}

		public static CommandResponse DeleteSpace (CorrigoService service, int id)
		{
			return Delete.Execute(service, id);
		}
	}
}
