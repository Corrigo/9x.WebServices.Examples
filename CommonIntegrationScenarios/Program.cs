using CorrigoServiceWebReference;
using System;

namespace CommonIntegrationScenarios
{
    internal class Program
    {
        private static readonly CorrigoClientProvider _clientProvider = new CorrigoClientProvider();

        private static void Main(string[] args)
        {
            try
            {
                var corrigoService = _clientProvider.GetCorrigoService(Credentials.Url, Credentials.Company, Credentials.UserName, Credentials.Password);

                CommonIntegrationScenarios.Execute(corrigoService);
            }
            catch (Exception exc)
            {
                Console.WriteLine("Program has been executed with error " +
                                  $"\n{exc.Message}\n" +
                                  $"Stack Trace:\n{exc.StackTrace}");
            }

            Console.ReadKey();
        }
    }
}
