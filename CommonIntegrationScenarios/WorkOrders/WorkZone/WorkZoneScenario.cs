using CorrigoServiceWebReference.CorrigoGA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonIntegrationScenarios.WorkOrders.WorkZones
{
    /// <summary>
    /// To create a Work Order, the Work Zone and/or Customer must exist.
    /// In addition, a minimum set of fields is required to create a Work Order. 
    /// This sample illustrates usage of the Retrieve/Retrieve Multiple web services operation
    /// to retrieve the available Wok Zone(s) in the system.
    /// </summary>
    internal class WorkZoneScenario
    {
        /// <summary>
        /// WZ Use Case1: Retrieving customers with work zones
        /// Operation: RetrieveMultiple (QueryExpression)
        /// Get a list of all Customers where Customer Name like “xyz%”
        /// 
        /// Fields to retrieve:
        ///   Customer:  Id, Name, Address;
        ///   Space:     Id, Name, Address;
        ///   Contact:   Id, Name, Address, Phone;
        ///   Work Zone: Id, Number, Name, IsOffline.
        /// 
        /// Sort by: Customer Id, Ascending
        /// </summary>
        /// <param name="corrigoService"></param>
        /// <param name="customerName"></param>
        public static Customer[] RetrieveMultipleCustomersByCustomerName(CorrigoService corrigoService, string customerName)
        {
            var query = new QueryExpression
            {
                EntityType = EntityType.Customer,
                PropertySet = new PropertySet
                {
                    Properties = new string[]
                        {
                            "Id",
                            "Name",
                            "Addresses.*",

                            "Spaces.Id",
                            "Spaces.Asset.Name",
                            "Spaces.Addresses.*",

                            "Contacts.Id",
                            "Contacts.DisplayAs",
                            "Contacts.ContactAddresses.Address",
                            "Contacts.Username",

                            "WorkZone.Id",
                            "WorkZone.Number",
                            "WorkZone.DisplayAs",
                            "WorkZone.IsOffline"
                        }
                },
                Criteria = new FilterExpression
                {
                    Conditions = new ConditionExpression[]
                        {
                            new ConditionExpression
                            {
                                PropertyName = "Name",
                                Operator = ConditionOperator.Like,
                                Values = new object[] {$"{customerName}"}
                            }
                        },
                },
                Orders = new[]
                    {
                        new OrderExpression
                        {
                            PropertyName = "Id",
                            OrderType = OrderType.Ascending
                        }
                    },
            };
            Customer[] results = corrigoService.RetrieveMultiple(query)
                                               .Cast<Customer>()
                                               .ToArray();


            Console.WriteLine($"Retrieve by customer name like '{customerName}%'; # of customers retrieved: " + results.Length.ToString());
            //Console.ReadKey();

            return results;
        }

        /// <summary>
        /// WZ Use Case 2: Retrieving WZ using wz Id
        /// Operation: Retrieve
        /// Gets work zone by id
        /// 
        /// Fields to retrieve:
        ///   Work Zone: Id, Number, Name, IsOffline.
        /// 
        /// Sort by: Customer Id, Ascending
        /// </summary>
        /// <param name="corrigoService"></param>
        /// <param name="customerName"></param>
        public static WorkZone RetrieveWorkZoneById(CorrigoService corrigoService, int id)
        {
            return corrigoService.Retrieve(new EntitySpecifier { EntityType = EntityType.WorkZone, Id = id },
                new PropertySet
                {
                    Properties = new[] {
                            "Id",
                            "Number",
                            "DisplayAs",
                            "IsOffline"}
                }) as WorkZone;
        }

    }
}
