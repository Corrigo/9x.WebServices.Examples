using CorrigoServiceWebReference;
using System;

namespace OneTransactionCommands
{
	class Program
	{
		private static readonly CorrigoClientProvider _clientProvider = new CorrigoClientProvider();

		static void Main(string[] args)
		{
			try
			{
				var service = _clientProvider.GetCorrigoService(Credentials.Url, Credentials.Company, Credentials.UserName, Credentials.Password);

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
