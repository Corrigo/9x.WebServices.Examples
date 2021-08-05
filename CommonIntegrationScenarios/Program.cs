using CorrigoServiceWebReference;
using System;

namespace CommonIntegrationScenarios
{
    internal class Program
    {
        //private const string _url = "http://v91.qa.corrigo.com/wsdk/CorrigoService.asmx";
        //private const string _company = "Integrations 9.1 DB";
        //private const string _userName = "ewi";
        //private const string _password = "Corrigo2!";
        private const string _url = "http://qa-am-ent-f2.corrigo-qa.com/wsdk/CorrigoService.asmx";
        private const string _company = "Integrations Tests";
        private const string _userName = "wsdk";
        private const string _password = "1234";

        private static readonly CorrigoClientProvider _clientProvider = new CorrigoClientProvider();

        private static void Main(string[] args)
        {
            try
            {
                var corrigoService = _clientProvider.GetCorrigoService(_url, _company, _userName, _password);

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
