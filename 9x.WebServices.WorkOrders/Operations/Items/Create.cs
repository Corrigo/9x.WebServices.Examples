using System;
using System.Collections.Generic;
using CorrigoServiceWebReference.CorrigoGA;
using System.Diagnostics;
using System.Linq;

namespace _9x.WebServices.WorkOrders.Operations.Items
{
	internal static class Create
	{
		public static EntitySpecifier[] Execute(CorrigoService service, int woid)
		{
			var wo = WorkOrderItemExamples.GetOrder(service, woid);

		    int startIndex = wo.Items.Max(i => i.SortOrderIdx);

            List<WoItem> items = new List<WoItem>(wo.Items);


            items.Add(
                new WoItem
		        {
		            WorkOrderId = woid,
		            SortOrderIdx = startIndex + 1,
		            //Disposition = new Disposition(),
		            AssetLocation = "location 1",
		            Asset = new Location {Id = wo.Items[0].Asset.Id},
		            Task = new Task {Id = wo.Items[0].Task.Id},
		            Comment = "comment 1"
		        });

            items.Add(
                new WoItem
                {
                    WorkOrderId = woid,
                    SortOrderIdx = startIndex + 2,
                    //Disposition = new Disposition
                    //{
                    //    DisplayAs = "disposition 2",
                    //    IsCompleted = false,
                    //    IsCancelled = false,
                    //    OnCreate = false,
                    //    OnComplete = true,
                    //    OnCancel = false
                    //},
                    AssetLocation = "location 2",
                    Asset = new Location { Id = wo.Items[0].Asset.Id },
                    Task = new Task { Id = wo.Items[0].Task.Id },
                    Comment = "comment 2"
                });


            wo.Items = items.ToArray();

            var command = new UpdateCommand
			{
				Entity = wo,
				PropertySet = new PropertySet { Properties = new[] { nameof(WorkOrder.Items) + ".*"} }
			};

			var response = service.Execute(command) as OperationCommandResponse;
			Debug.Print(response.ErrorInfo?.Description ?? "Successfully created items for a work order");

			return response.NestedEntitiesOperationResults.Select(r => r.EntitySpecifier).ToArray();
		}
	}


}
