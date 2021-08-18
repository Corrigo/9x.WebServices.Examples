using _9x.WebServices.Contracts;
using _9x.WebServices.Customers;
using _9x.WebServices.Locations;
using _9x.WebServices.RepairCodes;
using _9x.WebServices.WoActionLogs;
using _9x.WebServices.WorkOrders;
using _9x.WebServices.WorkOrders.Operations.Items;
using _9x.WebServices.WorkOrders.Workflows;
using _9xWebServices.Spaces;
using CorrigoServiceWebReference;
using CorrigoServiceWebReference.CorrigoGA;
using OneTransactionCommands;
using System;
using System.Diagnostics;
using System.Linq;

namespace _9x.WebServices.Examples
{
	class Program
	{
        static readonly CorrigoClientProvider clientProvider = new CorrigoClientProvider();
		static void Main(string[] args)
		{
			try
			{
                var service = clientProvider.GetCorrigoService(Credentials.Url, Credentials.Company, Credentials.UserName, Credentials.Password);

                #region different test cases - to execute - uncomment
                //var id = CustomerExamples.CreateRetreive(service);
                //var id = 12;
                //CustomerExamples.RetreiveUpdate(service, id);
                //CustomerExamples.Delete(service, id);

                //var createResponse = SpaceExamples.CreateSpaceByUnitId(service, 73);
                //var space = SpaceExamples.ReadSpace(service, createResponse.NewSpace?.Id ?? 45);
                //var updateByIdResponse = SpaceExamples.UpdateSpaceById(service, space.Id);
                //var updateResponse = SpaceExamples.UpdateSpaceByInstance(service, space);                
                //var deleteResponse = SpaceExamples.DeleteSpace(service, updateResponse?.EntitySpecifier?.Id ?? 46);

                //WoPriorities.WoPriorityExamples.CRUDExample(service);
                //Employees.EmployeeExamples.CRUDExample(service);
                //BillingAccounts.BillingAccountExamples.CRUDExample(service);
                //_9x.WebServices.WoActionLogs.WoActionLogExamples.CRUDExample(service);
                //WoLastActions.WoLastActionExamples.CRUDExample(service);
                //Tasks.TaskExamples.CRUDExample(service);
                //Specialties.SpecialtyExamples.CRUDExample(service);
                //_9x.WebServices.WorkZones.WorkZoneExamples.CRUDExample(service);
                //WorkZones.WorkZoneExamples.DeleteWz(service, 28);
                //WorkZones.WorkZoneExamples.Offline(service, 28);
                //WorkZones.WorkZoneExamples.Online(service, 28);
                //ContractExamples.CRUDExample(service);
                //_9x.WebServices.Addresses.Address2Examples.CRUDExample(service);
                //Contacts.ContactExamples.CRUDExample(service);
                //Documents.DocumentExamples.CRUDExample(service);
                //AssetTrees.AssetTreeExamples.CRUDExample(service);
                //Organizations.OrganizationExamples.CRUDExample(service);

                //CustomField2s.CustomField2Examples.CRUDExample(service);
                //WorkOrderOperationExamples.CreateWorkOrderAndUpdateCustomFields(service);

                //var readWO = WorkOrderOperationExamples.RetrieveWorkOrder(service, WoActionLogExamples.GetLatestWOs(service,1)[0].Id);
                //var woCancellationResponse = WorkOrderOperationExamples.DeleteWorkOrder(service, readWO.Id);

                //int workOrderId = WoActionLogExamples.GetLatestWOs(service, 2)[1].Id;
                //WorkOrderWorkflowExamples.Assign(service, workOrderId);
                //WorkOrderWorkflowExamples.PickUp(service, workOrderId);
                //WorkOrderWorkflowExamples.Start(service, workOrderId);
                //WorkOrderWorkflowExamples.Pause(service, workOrderId);
                //WorkOrderWorkflowExamples.Reopen(service, workOrderId);
                //WorkOrderWorkflowExamples.OnHold(service, workOrderId);
                //WorkOrderWorkflowExamples.Cancel(service, workOrderId);
                //WorkOrderWorkflowExamples.Complete(service, workOrderId);

                //WorkOrderWorkflowExamples.NewToPause(service, workOrderId);
                //WorkOrderWorkflowExamples.NewToOnHoldThroughPause(service, workOrderId);
                //WorkOrderWorkflowExamples.NewToCompleteThroughCancel(service, workOrderId);

                //var noteId = WorkOrderNotesExamples.CreateNote(service, workOrderId);
                //WorkOrderNotesExamples.UpdateNote(service, workOrderId, noteId);
                //WorkOrderNotesExamples.ReadNote(service, noteId);
                //WorkOrderNotesExamples.DeleteNote(service, workOrderId, noteId);

                //var costId = WorkOrderCostsExamples.CreateCosts(service);
                //var woCost = WorkOrderCostsExamples.ReadCosts(service, WoActionLogExamples.GetLatestWOs(service, 1)[0].Id);

                //var repairCategoryId = RepairCodeExamples.CreateRepairCategory(service);
                //var repairCodeId = RepairCodeExamples.CreateRepairCode(service, repairCategoryId);
                //RepairCodeExamples.UpdateDisplayAs(service, repairCategoryId);
                //RepairCodeExamples.UpdateParentId(service, repairCategoryId);	//is not possible
                //RepairCodeExamples.AddChildRepairCodes(service);                // is not possible
                //method doesn't return error but doesn't change anything
                // RepairCodeExamples.RemoveChildRepairCodes(service);
                //var repairCategory = RepairCodeExamples.RetrieveRepairCode(service, repairCategoryId);
                //var repairCode = RepairCodeExamples.RetrieveRepairCode(service, repairCodeId);
                //RepairCodeExamples.DeleteRepairCode(service, repairCodeId);

                int estimateWorkOrderId = 578;
                var estimateId = WorkOrderEstimateExamples.CreateEstimate(service, estimateWorkOrderId);
                var all = WorkOrderEstimateExamples.ReadEstimates(service);
                //var estimate = WorkOrderEstimateExamples.ReadEstimate(service, estimateId);
                //WorkOrderEstimateExamples.UpdateStatusEstimate(service, estimateWorkOrderId);
                WorkOrderEstimateExamples.DeleteEstimate(service, estimateWorkOrderId);

                //UnitSpaceContactWorkOrder.Create(service, 12, "QSR");//"QSR"=>see by navigation path Assets>TemplateManager> Type="Unit"

                //var assignmentId = WorkOrderAssignmentExamples.CreateAssignment(service, WoActionLogExamples.GetLatestWOs(service, 1)[0].Id);
                //var assignments = WorkOrderAssignmentExamples.ReadAssignment(service, assignmentId);
                ////var wos = WoActionLogExamples.GetLatestWOs(service, 3);
                //WorkOrderAssignmentExamples.UpdateAssignment(service, assignmentId);
                //WorkOrderAssignmentExamples.DeleteAssignment(service, assignmentId);

                //var flagId = WorkOrderFlagsExamples.CreateFlag(service, WoActionLogExamples.GetLatestWOs(service, 1)[0].Id);
                //var wos = WorkOrderOperationExamples.RetrieveAllWorkOrders(service);
                //var wo = WorkOrderOperationExamples.RetrieveWorkOrder(service, 583);
                //var flags = WorkOrderFlagsExamples.ReadFlags(service);
                //wo = WorkOrderFlagsExamples.CreateFlagWo(service, 583, 3332);
                //wo = WorkOrderOperationExamples.RetrieveWorkOrder(service, 583);
                //var woset = WorkOrderFlagsExamples.SetupFlags(service, 583, new[] { 3368, 3366 });
                //var woclear = WorkOrderFlagsExamples.ClearFlags(service, 583, new[] { 3368, 3336, 1798 });
                //var flags1 = WorkOrderFlagsExamples.ReadFlags(service);

                //WorkOrderActionReasonLookupExamples.ReadActionReasonLookup(service, WoActionLogExamples.GetLatestWOs(service, 1)[0].Id);

                //var locationId = LocationExamples.CreateLocation(service);
                //LocationExamples.ReadLocation(service, locationId);
                //LocationExamples.UpdateLocation(service, locationId);
                //LocationExamples.DeleteLocation(service, locationId);
                #endregion different test cases - to execute - uncomment

                //int itemWorkOrderId = WoActionLogExamples.GetLatestWOs(service, 1)[0].Id;
                //var item = WorkOrderItemExamples.CreateItem(service, 583);
                //var items = WorkOrderItemExamples.ReadItems(service);
                //WorkOrderItemExamples.UpdateItem(service, 583);
                //WorkOrderItemExamples.DeleteItem(service, 583);

                //WorkOrderOperationExamples.CreateCustomFieldForWorkOrder(service, itemWorkOrderId);
                //WoScheduleExample.InitializeSchedule(service);
                //WoAutoAssignExample.Execute(service, itemWorkOrderId);
            }
            catch (Exception exc)
			{
				Debug.Print(exc.Message);
				Debug.Print("\n");
				Debug.Print(exc.StackTrace);
			}
		}
	}
}
