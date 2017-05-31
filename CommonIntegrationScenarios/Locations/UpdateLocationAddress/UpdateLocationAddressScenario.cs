using CorrigoServiceWebReference.CorrigoGA;
using System;


namespace CommonIntegrationScenarios.Locations.UpdateLocationAddress
{
    internal class UpdateLocationAddressScenario
    {
        /// <summary>
        /// Update Location Address
        /// </summary>
        /// <param name="corrigoService"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        public static void UpdateLocationAddress(CorrigoService corrigoService, Location location)
        {
            if (location?.Address == null) return;

            location.Address.Street = $"Disneyland {location.Address.Street ?? ""}";

            var command = new UpdateCommand
            {
                Entity = location,
                PropertySet = new PropertySet {Properties = new[] {"Address.Street"}}
            };
            var response = corrigoService.Execute(command) as OperationCommandResponse;

            Console.WriteLine(response?.ErrorInfo?.Description ?? $"Successfully updated Location with id {location.Id}");

            //Console.ReadKey();
        }
    }
}
