using CorrigoServiceWebReference.CorrigoGA;
using System;
using System.Diagnostics;
using System.Linq;

namespace _9x.WebServices.RepairCodes.Operations
{
	internal static class Update
	{
		public static int UpdateDisplayAs (CorrigoService service, int repairCodeId)
		{
			var repairCode = Read.Retrieve(service, repairCodeId);
			repairCode.DisplayAs = new string(repairCode.DisplayAs.Reverse().ToArray());
			string[] properties = { "DisplayAs" };
			var command = new UpdateCommand
			{
				Entity = repairCode,
				PropertySet = new PropertySet { Properties = properties }
			};
			var response = service.Execute(command) as OperationCommandResponse;

			Debug.Print(response?.ErrorInfo?.Description
				?? $"Successfully updated repair code {repairCodeId}");

			if (response?.ErrorInfo != null)
				throw new Exception(response?.ErrorInfo?.Description);

			return repairCodeId;
		}

		public static int UpdateParentId (CorrigoService service, int repairCodeId)
		{
			var repairCode = Read.Retrieve(service, repairCodeId);
			repairCode.ParentId = repairCode.ParentId == 0 ? 100 : 0;
			string[] properties = { "ParentId" };
			var command = new UpdateCommand
			{
				Entity = repairCode,
				PropertySet = new PropertySet { Properties = properties }
			};
			var response = service.Execute(command) as OperationCommandResponse;

			Debug.Print(response?.ErrorInfo?.Description
				?? $"Successfully updated repair code {repairCodeId}");

			if (response?.ErrorInfo != null)
				throw new Exception(response?.ErrorInfo?.Description);

			return repairCodeId;
		}

		public static void AddChildRepairCodes (CorrigoService service, int repairCategoryId, params int[] repairCodeIds)
		{
			var repairCategory = Read.Retrieve(service, repairCategoryId);
			if (repairCategory.ParentId != 0)
				throw new Exception("Example intended to work with RepairCategory only! Provide valid Id.");

			repairCategory.Codes = repairCodeIds.Select(id =>
			{
				try { return Read.Retrieve(service, id); }
				catch { return null; }
			}).Where(rc => rc != null).ToArray();
			string[] properties = { "Codes.*" };
			var command = new UpdateCommand
			{
				Entity = repairCategory,
				PropertySet = new PropertySet { Properties = properties }
			};
			var response = service.Execute(command) as OperationCommandResponse;

			Debug.Print(response?.ErrorInfo?.Description
				?? $"Successfully updated repair code {repairCategoryId}");

			if (response?.ErrorInfo != null)
				throw new Exception(response?.ErrorInfo?.Description);
		}

		public static void RemoveChildRepairCodes (CorrigoService service, int repairCategoryId, params int[] repairCodeIds)
		{
			var repairCategory = Read.Retrieve(service, repairCategoryId);
			if (repairCategory.ParentId != 0)
				throw new Exception("Example intended to work with RepairCategory only! Provide valid Id.");

			repairCategory.Codes = repairCategory.Codes.Where(rc => !repairCodeIds.Contains(rc.Id)).ToArray();
			string[] properties = { "Codes.*" };
			var command = new UpdateCommand
			{
				Entity = repairCategory,
				PropertySet = new PropertySet { Properties = properties }
			};
			var response = service.Execute(command) as OperationCommandResponse;

			Debug.Print(response?.ErrorInfo?.Description
				?? $"Successfully updated repair code {repairCategoryId}");

			if (response?.ErrorInfo != null)
				throw new Exception(response?.ErrorInfo?.Description);
		}
	}
}
