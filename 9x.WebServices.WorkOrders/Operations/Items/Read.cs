using CorrigoServiceWebReference.CorrigoGA;
using System.Diagnostics;

namespace _9x.WebServices.WorkOrders.Operations.Items
{
    internal static class Read
	{
		public static CorrigoEntity[] RetrieveAll(CorrigoService service)
		{
			var list = service.RetrieveMultiple(
				new QueryByProperty
				{
					EntityType = EntityType.WoItem,
					PropertySet = new AllProperties(),
					Conditions = new PropertyValuePair[0],
					Orders = new[]
					{
						new OrderExpression
						{
							OrderType = OrderType.Descending,
							PropertyName = "Id"
						}
					},
				});


			Debug.Print(list == null ? "Failure!" : "Success!");
			return list;
		}
	}
}
