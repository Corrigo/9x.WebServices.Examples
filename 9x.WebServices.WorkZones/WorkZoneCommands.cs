using CorrigoServiceWebReference.CorrigoGA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _9x.WebServices.WorkZones
{
    public class WorkZoneCommands
    {
		public static bool? WzOnline(CorrigoService service, int wzId)
		{
			var command = new WorkZoneOnlineCommand
			{
				WorkZoneId = wzId,
				
			};

			var result = service.Execute(command) as WorkZoneCommandResponse;
			return result.WorkZone?.IsOffline;
		}

		public static bool? WzOffline(CorrigoService service, int wzId, bool deactivateCust = false)
		{
			var command = new WorkZoneOfflineCommand
			{
				WorkZoneId = wzId,
				DeactivateCustomers = deactivateCust
			};

			var result = service.Execute(command) as WorkZoneCommandResponse;

			return result.WorkZone?.IsOffline;
		}

		public static bool? WzDelete(CorrigoService service, int wzId)
		{
			var command = new WorkZoneDeleteCommand
			{
				WorkZoneId = wzId,

			};

			var result = service.Execute(command) as WorkZoneCommandResponse;
			return result.WorkZone?.IsOffline;
		}
	}
}
