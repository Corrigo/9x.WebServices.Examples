using CorrigoServiceWebReference.CorrigoGA;
using System;
using System.Net;

namespace CorrigoServiceWebReference
{
	public class CorrigoClientProvider
	{

		public CorrigoService GetCorrigoService (string anyKnownServiceUrl, string companyName, string userName, string password)
		{
		    var service = new CorrigoService {Url = anyKnownServiceUrl};

		    SetCorrectWebServiceUrlForCompany(service, companyName);
			LogInToCompany(service, companyName, userName, password);
			return service;
		}

		private void SetCorrectWebServiceUrlForCompany (CorrigoService service, string companyName)
		{
			var response = service.GetCompanyWsdkUrl(companyName, Protocols.HTTP) as GetCompanyWsdkUrlResult;
			if (response == null || response.ErrorInfo != null)
				throw new Exception(response?.ErrorInfo.Description ?? "No response at all!");

			//without cookies - log in information won't be stored on a client
			// each command will be recognized as from unauthorized source
			service.CookieContainer = new CookieContainer();
			service.Url = response.Url;
		}

		private void LogInToCompany (CorrigoService service, string companyName, string userName, string password)
		{
			var loginStatus = service.LogInStatus() as LoginResponse;
			var needLogIn = !loginStatus?.Success ?? true;
			if (needLogIn)
			{
				var logIn = service.LogInCompany(userName, password, companyName) as LoginResponse;
				if (!logIn?.Success ?? true)
					throw new Exception($"Cannot log into company {companyName} as {userName} with {password}."
										+ $" Using URL: {service.Url}");
			}
		}

	}
}
