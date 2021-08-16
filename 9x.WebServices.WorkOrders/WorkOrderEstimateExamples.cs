using _9x.WebServices.WorkOrders.Operations.Estimate;
using CorrigoServiceWebReference.CorrigoGA;

namespace _9x.WebServices.WorkOrders
{
	public static class WorkOrderEstimateExamples
	{
		public static int CreateEstimate (CorrigoService service, int woid)
			=> Create.Execute(service, woid);
		public static WoEstimate ReadEstimate (CorrigoService service, int estimateId)
			=> Read.Retrieve(service, estimateId);
		public static CorrigoEntity[] ReadEstimates(CorrigoService service)
			=> Read.RetrieveAll(service); 
		public static void DeleteEstimate (CorrigoService service, int woid)
			=> Delete.Execute(service, woid);

		public static void UpdateStatusEstimate (CorrigoService service, int woid)
		{
			//a list of all possible statuses
			//Unknown,
			//NotSubmitted,
			//Requested,
			//WaitingForApproval,
			//Approved,
			//Rejected,
			//InProposal,
			Update.Execute(service, woid, QuoteStatus.InProposal);
			Update.Execute(service, woid, QuoteStatus.WaitingForApproval);
			Update.Execute(service, woid, QuoteStatus.Rejected);
		}
	}
}
