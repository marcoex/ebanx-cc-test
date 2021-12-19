using System;
using System.Collections.Generic;

namespace EBanx.Cc.AccountsAdmin
{
	/// <summary>
	/// Representa uma conta específica.
	/// </summary>
	public class Account
	{

		/// <summary>
		/// Identificador da conta.
		/// </summary>
		public long Id { get; private set; }

		/// <summary>
		/// Saldo total.
		/// </summary>
		public float Balance
		{
			get {
				return Statement.Sum;
			}
		}

		/// <summary>
		/// Extrato
		/// </summary>
		internal Statement Statement { get; } = new Statement();

		/// <summary>
		/// Cria uma instância de Conta (Account).
		/// </summary>
		/// <param name="id">Identificador da conta</param>
		public Account(long id)
		{
			this.Id = id;
		}

	}
}

