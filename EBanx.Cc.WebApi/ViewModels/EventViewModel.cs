using EBanx.Cc.AccountsAdmin;

namespace Ebanx.Cc.WebApi.ViewModels
{
	public class EventViewModel
	{
		public string Type { get; set; }

		public string Origin { get; set; }

		public string Destination { get; set; }

		public float Amount { get; set; }

	}
}