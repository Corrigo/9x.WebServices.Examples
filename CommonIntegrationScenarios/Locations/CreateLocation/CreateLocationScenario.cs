using CorrigoServiceWebReference.CorrigoGA;
using System;
using System.Linq;


namespace CommonIntegrationScenarios.Locations.CreateLocation
{
    internal class CreateLocationScenario
    {
        /// <summary>
        /// Create Location
        /// </summary>
        /// <param name="corrigoService"></param>
        /// <returns></returns>
        public static Location CreateLocation(CorrigoService corrigoService)
        {
            #region Get Ids for Location required fields: Model, WorkZoneAsset Id

            //
            // Retrieve Asset Id
            //
            int workZoneAssetId = (corrigoService.RetrieveMultiple(
                new QueryByProperty
                {
                    Count = 1,
                    EntityType = EntityType.WorkZone,
                    PropertySet = new PropertySet
                    {
                        Properties = new string[]
                        {
                            "Asset.Id",
                        }
                    },
                    Orders = new[]
                    {
                        new OrderExpression
                        {
                            OrderType = OrderType.Descending,
                            PropertyName = "Id"
                        }
                    },
                    //Conditions = new PropertyValuePair[0]
                }).FirstOrDefault() as WorkZone)?.Asset.Id ?? 0;

            if (workZoneAssetId == 0) return null;

            int modelId = corrigoService.RetrieveMultiple(
                new QueryExpression
                {
                    EntityType = EntityType.Model,
                    Orders = new[]
                    {
                        new OrderExpression
                        {
                            OrderType = OrderType.Descending,
                            PropertyName = "Id"
                        }
                    },
                    Count = 1
                }).FirstOrDefault()?.Id ?? 0;


            if (modelId == 0) return null;

            #endregion

            var asset = new Location
            {
                Name = $"Test location {DateTime.Now}",
                Address = new Address2 {TypeId = StreetAddrType.Primary, Street = $"Test Street {DateTime.Now}"},
                ModelId = modelId,
                Orphan = true,
                TypeId = AssetType.Building,
                ParentId = workZoneAssetId,
                IsTemplate = false
            };
            var command = new CreateCommand {Entity = asset};
            var response = corrigoService.Execute(command) as OperationCommandResponse;

            var id = response?.EntitySpecifier.Id ?? 0;
            Console.WriteLine(response?.ErrorInfo?.Description ?? $"Successfully created Location with id {id}");

            //Console.ReadKey();

            var location = corrigoService.Retrieve(
                new EntitySpecifier
                {
                    EntityType = EntityType.Location,
                    Id = id
                },
                new PropertySet
                {
                    Properties = new string[]
                    {
                        "*",
                        "Address.*",
                    }
                }) as Location;

            return location;
        }
    }
}
