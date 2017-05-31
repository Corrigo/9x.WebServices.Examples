using CorrigoServiceWebReference.CorrigoGA;
using System;
using System.Diagnostics;

namespace _9x.WebServices.RepairCodes.Operations
{
	internal static class Create
	{
		public static int Execute (CorrigoService service, int repairCategoryId, string displayAs)
		{
			// if repairCategoryId is 0, Repair Category instance will be created
			var repairCode = new RepairCode
			{
				ParentId = repairCategoryId,
				DisplayAs = displayAs
			};

			var command = new CreateCommand { Entity = repairCode };
			var response = service.Execute(command) as OperationCommandResponse;

			var repairCodeId = response?.EntitySpecifier?.Id ?? 0;
			Debug.Print(response?.ErrorInfo?.Description
				?? $"Successfully created repair code {repairCodeId} for repair category with id {repairCategoryId}");

			if (response?.ErrorInfo != null)
				throw new Exception(response?.ErrorInfo?.Description);

			return repairCodeId;
		}
	}
}
