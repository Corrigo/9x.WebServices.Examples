using _9x.WebServices.RepairCodes.Operations;
using CorrigoServiceWebReference.CorrigoGA;

namespace _9x.WebServices.RepairCodes
{
	public static class RepairCodeExamples
	{
		public static int CreateRepairCategory (CorrigoService service)
			=> Create.Execute(service, 0, "Repair Category Y");
		public static int CreateRepairCode (CorrigoService service, int repairCategoryId)
			=> Create.Execute(service, repairCategoryId, "Repair Code Z");
		public static int UpdateDisplayAs (CorrigoService service, int repairCodeId)
			=> Update.UpdateDisplayAs(service, repairCodeId);
		public static int UpdateParentId (CorrigoService service, int repairCodeId)
			=> Update.UpdateParentId(service, repairCodeId);

		public static void RemoveChildRepairCodes (CorrigoService service)
			=> Update.RemoveChildRepairCodes(service, 7, 10);
		public static void AddChildRepairCodes (CorrigoService service)
			=> Update.AddChildRepairCodes(service, 7, 9, 10);
		public static RepairCode RetrieveRepairCode (CorrigoService service, int repairCodeId)
			=> Read.Retrieve(service, repairCodeId);
		public static void DeleteRepairCode (CorrigoService service, int repairCodeId)
			=> Delete.Execute(service, repairCodeId);
	}
}
