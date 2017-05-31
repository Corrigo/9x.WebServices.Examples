using CommonIntegrationScenarios.WorkOrders.WorkZones;
using CorrigoServiceWebReference.CorrigoGA;
using System;
using System.Linq;


namespace CommonIntegrationScenarios.WorkOrders.CreateWorkOrder
{
    internal class CreateWorkOrderScenario
    {
        /// <summary>
        /// WO Use Case1: Create WO for Customer
        /// Operation: Execute
        /// Command: Create Work Order
        /// Specify required fields
        /// 
        /// Additional fields to specify:
        ///    Customer Id,
        ///    Contact: Name, Address, Phone
        ///    Comment = "Sample Work Order generated via Web Services for Customer"
        ///    Compute Schedule = true
        ///    Compute Assignment = true
        /// </summary>
        /// <param name="corrigoService"></param>
        /// <param name="customer"></param>/// 
        public static WorkOrder CreateWorkOrderForCustomer(CorrigoService corrigoService, Customer customer)
        {
            if (customer == null) return null;

            //
            // Get Ids for Asset, Task, Subtype which will be used for composition of required WO properties
            // from the existing Work Order.
            //
            var requiredFields = GetWorkOrderWithRequiredFields(corrigoService);

            // Specify required fields:
            WorkOrder newWorkOrder = new WorkOrder
            {
                Items = new[]
                {
                    new WoItem
                    {
                        Asset = new Location {Id = requiredFields.Items[0].Asset.Id},
                        Task = new Task {Id = requiredFields.Items[0].Task.Id}
                    }
                },
                Customer = new Customer {Id = customer.Id},
                SubType = new WorkOrderType { Id = 4 },//we need Request when it's simple reactive WO with a custumer
                /*
                 * ID	SubType
                    1	Basic
                    2	PMRM
                    3	Turn
                    4	Request
                    5	Project
                 */
                ContactAddress = new ContactInfo
                {
                    Address = "1(877) 267-7440, 8245 SW Tualatin Sherwood Road Tualatin, OR 97062",
                    AddrTypeId = ContactAddrType.Contact
                },
                TypeCategory = WOType.Request,
            };

            // Additional fields to specify:
            newWorkOrder.ContactName = "Contact Name";

            var command = new WoCreateCommand
            {
                WorkOrder = newWorkOrder,
                Comment = "Sample Work Order generated via Web Services for Customer",
                ComputeAssignment = true,
                ComputeSchedule = true,
            };


            var response = corrigoService.Execute(command) as WoActionResponse;
            if (response.ErrorInfo != null)
            {
                Console.WriteLine(response.ErrorInfo.Description);
            }
            else if (response?.Wo != null)
            {
                Console.WriteLine($"Created new Work Order with Id={response.Wo.Id}");
            }

            //Console.ReadKey();

            return response?.Wo;
        }


        /// <summary>
        /// WO Use Case2: Create WO for Work Zone
        /// Operation: Execute
        /// Command: Create Work Order
        /// Specify required fields
        /// 
        /// Additional fields to specify:
        ///    Work Zone Id,
        ///    Contact: Name, Address, Phone
        ///    Comment = "Sample Work Order generated via Web Services for Work Zone"
        ///    Compute Schedule = true
        ///    Compute Assignment = true
        /// </summary>
        /// <param name="corrigoService"></param>
        /// <param name="workZone"></param>/// 
        public static WorkOrder CreateWorkOrderForWorkZone(CorrigoService corrigoService, WorkZone workZone)
        {
            if (workZone == null) return null;
            
            //
            // Get Ids for Asset, Task, Subtype which will be used for composition of required WO properties
            // from the existing Work Order.
            //
            var requiredFields = GetWorkOrderWithRequiredFields(corrigoService);

            // Specify required fields:
            WorkOrder newWorkOrder = new WorkOrder
            {
                Items = new[]
                {
                    new WoItem
                    {
                        Asset = new Location {Id = requiredFields.Items[0].Asset.Id},
                        Task = new Task {Id = requiredFields.Items[0].Task.Id}
                    }
                },                
                SubType = new WorkOrderType {Id = 1},//we need basic when WO is without custumer
                /*
                 * ID	SubType
                    1	Basic
                    2	PMRM
                    3	Turn
                    4	Request
                    5	Project
                 */
                ContactAddress = new ContactInfo
                {
                    Address = "1(877) 267-7440, 8245 SW Tualatin Sherwood Road Tualatin, OR 97062",
                    AddrTypeId = ContactAddrType.Contact
                },
                TypeCategory = WOType.Basic,//Required for work orders without customers
            };


            newWorkOrder.WorkZone = new WorkZone {Id = workZone.Id};

            // Additional fields to specify:
            var command = new WoCreateCommand
            {
                WorkOrder = newWorkOrder,
                Comment = "Sample Work Order generated via Web Services for Work Zone",
                ComputeAssignment = true,
                ComputeSchedule = true,
            };


            var response = corrigoService.Execute(command) as WoActionResponse;
            if (response.ErrorInfo != null)
            {
                Console.WriteLine(response.ErrorInfo.Description);
            }
            else if (response?.Wo != null)
            {
                Console.WriteLine($"Created new Work Order with Id={response.Wo.Id}");
            }

            //Console.ReadKey();
            //work order is returned in the response with its new Id.
            return response?.Wo;
        }

        public static WorkOrder GetWorkOrderWithRequiredFields(CorrigoService corrigoService)
        {
            PropertySet propertySet = new PropertySet
            {
                Properties =
                    new[]
                    {
                        "Id", "ContactAddress.*", "Customer.Id", "SubType.Id", "TypeCategory", "Items.Asset.Id",
                        "Items.Task.Id"
                    }
            };

            WorkOrder[] latestWOs = GetLatestWOs(corrigoService, 1, propertySet);

            return latestWOs.FirstOrDefault();
        }

        // Returns specified number of latest Work Orders
        public static WorkOrder[] GetLatestWOs(CorrigoService corrigoService, int numberOfWOs,
            PropertySetBase propertySet = null)
        {
            var list = corrigoService.RetrieveMultiple(
                new QueryExpression
                {
                    EntityType = EntityType.WorkOrder,
                    PropertySet = (propertySet ?? new AllProperties()),
                    //Order to get latest
                    Orders = new[]
                    {
                        new OrderExpression
                        {
                            OrderType = OrderType.Descending,
                            PropertyName = "CreatedDateUtc"
                        }
                    },
                    Count = numberOfWOs
                });

            return list.Cast<WorkOrder>().ToArray();
        }
    }
}
