using CorrigoServiceWebReference;
using System;

namespace OneTransactionCommands
{
	class Program
	{
		private const string _url = "http://v90ga.qa.corrigo.com/wsdk/CorrigoService.asmx";
		private const string _company = "OVCP_Test";
		private const string _userName = "FeederQa";
		private const string _password = "1234";

		private static readonly CorrigoClientProvider _clientProvider = new CorrigoClientProvider();

		static void Main(string[] args)
		{
			try
			{
				var service = _clientProvider.GetCorrigoService(_url, _company, _userName, _password);

				UnitSpaceContactWorkOrder.Create(service, 34, "Unit1");
			}
			catch (Exception exc)
			{
				Console.WriteLine("Program has been executed with error " +
								  $"\n{exc.Message}\n" +
								  $"Stack Trace:\n{exc.StackTrace}");
			}
		}
	}
}
