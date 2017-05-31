using CorrigoServiceWebReference.CorrigoGA;
using System;

namespace _9x.WebServices.WoPriorities.Operations
{
	internal static class Update
	{
		public static void Execute(CorrigoService service, WoPriority toUpdate)
        {
            if (toUpdate == null || service == null) return;

            toUpdate.DisplayAs = "-";     //Label
            toUpdate.DueInMinutes = 3;      //Complete mins.
            toUpdate.IsEmergency = true;  //Escalate
            toUpdate.RespondInMinutes = 5;  //Responce mins.

            Console.WriteLine($"Update WoPriority with id={toUpdate.Id.ToString()}");

            var resultUpdate = service.Execute(new UpdateCommand
            {
                Entity = toUpdate,
                PropertySet = new PropertySet { Properties = new[] { "DisplayAs", "IsEmergency", "RespondInMinutes", "DueInMinutes" } }
            });

            if (resultUpdate == null)
            {
                Console.WriteLine("Update of WoPriority failed");
                return;
            }

            if (resultUpdate.ErrorInfo != null && !string.IsNullOrEmpty(resultUpdate.ErrorInfo.Description))
            {
                Console.WriteLine(resultUpdate.ErrorInfo.Description);
                Console.WriteLine("Update of WoPriority failed");
                return;
            }

            Console.WriteLine("WoPriority is updated");

        }
    }
}
