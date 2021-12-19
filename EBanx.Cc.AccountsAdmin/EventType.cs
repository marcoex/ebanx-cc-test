using System;
using System.ComponentModel;
using System.Linq;

namespace EBanx.Cc.AccountsAdmin
{
	public enum EventType : byte
	{
		[Description("Depósito")]
		Deposit,

		[Description("Saque")]
		WithDraw,

		[Description("Transferência")]
		Transfer,
	}

	public static class EventTypeExtensions
	{
		public static string GetDescription(this EventType value)
		{
			var fieldInfo = value.GetType().GetField(value.ToString());
			var desc = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as DescriptionAttribute;
			return desc?.Description ?? value.ToString();
		}

	}
}