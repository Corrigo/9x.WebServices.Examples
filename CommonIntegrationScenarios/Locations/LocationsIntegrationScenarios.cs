using CommonIntegrationScenarios.Locations.CreateLocation;
using CommonIntegrationScenarios.Locations.UpdateLocationAddress;
using CorrigoServiceWebReference.CorrigoGA;


namespace CommonIntegrationScenarios.Locations
{
    internal class LocationsIntegrationScenarios
    {
        public static void Execute(CorrigoService corrigoService)
        {
            //
            // Create Location
            //
            Location location = CreateLocationScenario.CreateLocation(corrigoService);

            //
            // Update Location Address
            //
            UpdateLocationAddressScenario.UpdateLocationAddress(corrigoService, location);
        }
    }
}
