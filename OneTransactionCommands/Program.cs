using CorrigoServiceWebReference;
using System;

namespace OneTransactionCommands
{
	class Program
	{
		private const string _url = "http://qa-am-ent-f2.corrigo-qa.com/wsdk/CorrigoService.asmx";
		private const string _company = "Integrations Tests";
		private const string _userName = "wsdk";
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
