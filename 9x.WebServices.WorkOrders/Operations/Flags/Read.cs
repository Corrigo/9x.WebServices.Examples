using CorrigoServiceWebReference.CorrigoGA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _9x.WebServices.WorkOrders.Operations.Flags
{
	internal static class Read
	{
		public static CorrigoEntity[] RetrieveMultiple(CorrigoService service, int countNumber = 200)
		{
			string[] properties = { "WoId", "FlagId", "UtcStamp", "Comment" };
            var list = service.RetrieveMultiple(
                new QueryByProperty
                {
                    Count = countNumber,
                    EntityType = EntityType.WoFlag,
                    PropertySet = new PropertySet
                    {
                        Properties = properties
                    },
                    Conditions = new PropertyValuePair[0],
                    Orders = new[]
                    {
                        new OrderExpression
                        {
                            OrderType = OrderType.Descending,
                            PropertyName = "UtcStamp"
                        }
                    },
                });

			return list;
		}
	}
}
