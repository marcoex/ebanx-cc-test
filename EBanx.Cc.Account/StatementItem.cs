using System;

namespace EBanx.Cc.BusinessAccount
{
	public class StatementItem
	{
		/// <summary>
		/// Descrição do evento extrato.
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Data/Hora do evento.
		/// </summary>
		public DateTime DateTime { get; set; }

		/// <summary>
		/// Valor envolvido.
		/// </summary>
		public float Value { get; set; }
	}
}