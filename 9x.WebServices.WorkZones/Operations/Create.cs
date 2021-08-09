using CorrigoServiceWebReference.CorrigoGA;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;

namespace _9x.WebServices.WorkZones.Operations
{
    internal static class Create
    {
        public static WorkZone Execute(CorrigoService service)
        {
            Console.WriteLine();
            Debug.Print("Creating WorkZone");
            Console.WriteLine("Creating WorkZone");

            //
            // Get Asset Template Id by its name.
            // Template name can be spotted by navigation Url: https://qa-am-ent-f1.corrigo-qa.com/corpnet/assets/templatemanager.aspx
            //for _company = "Integrations Tests";
            //string assetTemplateName = "Oleksii";created by UI
            string assetTemplateName = "Workzone";

            var assetTemplates = service.RetrieveMultiple(
                new QueryByProperty
                {
                    Count = 1,
                    EntityType = EntityType.AssetTemplate,
                    PropertySet = new PropertySet
                    {
                        Properties = new string[]
                        {
                            "Id",
                        }
                    },
                    Conditions = new[]
                    {
                        new PropertyValuePair {PropertyName = "DisplayAs", Value = assetTemplateName},
                    },
                });


            WorkZone toCreate = new WorkZone
            {
                DisplayAs = $"Test {DateTime.Now}",
                Number = $"Test {DateTime.Now}",
                WoNumberPrefix = Guid.NewGuid().ToString().Split('-')[0],
                TimeZone = 4, //(GMT-08:00) Pacific Time (US & Canada) 
                AutoAssignEnabled = true,
                DefaultAccess = PTEType.Unknown,
                SlotsCount = 3,
                LatestSlot = 1
                //LatestSlot should be greater than or equal to 1.|Supported values for SlotsCount are 3, 5, 7 and 10
            };


            var resultData = service.Execute(new WorkZoneCreateCommand
            {
                AssetTemplateId = assetTemplates[0].Id,
                WorkZone = toCreate,
                SkipDefaultSettings = false
            });

            if (resultData == null)
            {
                Debug.Print("Creation of new WorkZone failed");
                Console.WriteLine("Creation of new WorkZone failed");
                Console.WriteLine();
                return null;
            }


            var commandResponse = resultData as WorkZoneCommandResponse;

            int? id = commandResponse?.WorkZone?.Id;

            if (id.HasValue && resultData.ErrorInfo == null)
            {
                toCreate.Id = id.Value;
                Debug.Print($"Created new WorkZone with Id={id}");
                Console.WriteLine($"Created new WorkZone with Id={id}");
                Console.WriteLine();
                return toCreate;
            }


            Debug.Print("Creation of new WorkZone failed");
            Console.WriteLine("Creation of new WorkZone failed");
            Console.WriteLine();
            if (resultData.ErrorInfo != null && !string.IsNullOrEmpty(resultData.ErrorInfo.Description))
            {
                Debug.Print(resultData.ErrorInfo.Description);
                Console.WriteLine(resultData.ErrorInfo.Description);
            }


            return null;
        }

        public static EntitySpecifier[] CreateAssetHierarchy(CorrigoService service, int rootId)
        {
            //creating some nested entities
            var assets = new List<EntitySpecifier>();
            //creating building under work zone(see ParentId = rootId):
            var buildingModel = GetModel(service, "Building Model");
            var createBuilding = new CreateCommand
            {
                Entity = new Location { ModelId = buildingModel.Id, Name = "Building #4", TypeId = buildingModel.AssetCategoryId, ParentId = rootId }
            };
            var buildingCreateResult = service.Execute(createBuilding) as OperationCommandResponse;

            if (buildingCreateResult == null) return assets.ToArray();
            assets.Add(buildingCreateResult.EntitySpecifier);

            var buildingId = buildingCreateResult.EntitySpecifier.Id.Value;

            //creating unit under the building(see ParentId = buildingId):
            //units are the assets that could be refered by customer in work orders- during the work order creation you can select the unit child assets(+unit itself) for customer tasks
            var unitModel = GetModel(service, "Site Model");
            var createUnit = new CreateCommand
            {
                Entity = new Location { ModelId = unitModel.Id, Name = "Unit #4", TypeId = unitModel.AssetCategoryId, ParentId = buildingId }
            };
            var unitCreateResult = service.Execute(createUnit) as OperationCommandResponse;
            
            if (unitCreateResult == null) return assets.ToArray();
            assets.Add(unitCreateResult.EntitySpecifier);

            var unitId = unitCreateResult.EntitySpecifier.Id.Value;

            //creating equipment under the unit(see ParentId = unitId):
            var equipmentModel = GetModel(service, "Equipment Model");

            //Location class is used for creating assets with any model type
            //The model is associated with a set of attributes(their descriptors)
            //Asset - is a specific model instance - it can have only the attributes that are linked to asset model

            var equipmentAsset = new Location
            {
                ModelId = equipmentModel.Id,
                Name = "Equipment #2",
                TypeId = equipmentModel.AssetCategoryId,
                Attributes = new[]
                {
                    new AssetAttribute { Descriptor = GetAttribute(service, "Serial #"), Value = "123" },
                    new AssetAttribute {
                                            Descriptor = GetAttribute(service, "Salvage Value"),
                                            Value = "55",
                                            CurrencyTypeId =CurrencyType.USD
                                       }
                    //Note: If you need to update some existing asset attribute, you must retrieve the whole asset
                    //with its attributes -existing attributes have the attribute Id initialized, which is necessary for the update.
                    //Then find the attribute with the correct attibute descriptor and change its value,
                    //if it's missing - add a new attribute object with Id=0.
                    //To remove attribute, set existing attribute PerformDeletion field to true 
                },
                ParentId = unitId
            };
            var createEquipment = new CreateCommand { Entity = equipmentAsset };

            var equipmentCreateResult = service.Execute(createEquipment) as OperationCommandResponse;

            if (equipmentCreateResult != null) assets.Add(equipmentCreateResult.EntitySpecifier);
            var equipmentId = equipmentCreateResult.EntitySpecifier.Id.Value;

            return assets.ToArray();
        }

        private static AttributeDescriptor GetAttribute(CorrigoService corrigoClient, string name)
        {
            var query = new QueryByProperty
            {
                EntityType = EntityType.AttributeDescriptor,
                PropertySet = new AllProperties(),
                Conditions = new[] {
                    new PropertyValuePair { PropertyName= "Name", Value = name },
                    new PropertyValuePair { PropertyName= "IsRemoved", Value = false }}
            };
            //models define the set of tasks/actions which could be performed on specific asset. + attributes
            return corrigoClient.RetrieveMultiple(query)
                                .Cast<AttributeDescriptor>()
                                .FirstOrDefault();
        }
        private static Model GetModel(CorrigoService corrigoClient, string name)
        {
            var query = new QueryByProperty
            {
                EntityType = EntityType.Model,
                PropertySet = new AllProperties(),
                Conditions = new[] {
                    new PropertyValuePair { PropertyName= "DisplayAs", Value = name },
                    new PropertyValuePair { PropertyName= "IsRemoved", Value = false }}
            };
            //models define the set of tasks/actions which could be performed on specific asset. + attributes
            return corrigoClient.RetrieveMultiple(query).Cast<Model>().FirstOrDefault();
        }
    }
}
