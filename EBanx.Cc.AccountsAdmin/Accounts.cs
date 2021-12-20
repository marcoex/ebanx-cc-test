using System;
using System.Linq;
using System.Collections.Generic;

namespace EBanx.Cc.AccountsAdmin
{
	/// <summary>
	/// Representa um administrador geral de contas.
	/// </summary>
	public static class Accounts
	{
		private static List<Account> AccountData;
		private static object __operation = new object();

		/// <summary>
		/// Primeiro carregamento.
		/// </summary>
		static Accounts()
		{
			Initialize();
		}

		public static void Initialize()
		{
			AccountData = new List<Account>();
		}

		/// <summary>
		/// Obter uma conta existente. Retorna nulo se não encontrado.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static Account Find(string id)
		{
			return AccountData.FirstOrDefault(x => x.Id == id);
		}

		/// <summary>
		/// Executar uma operação de saque.
		/// </summary>
		/// <param name="id">Identificador da conta.</param>
		/// <param name="amount">Valor monetário.</param>
		/// <returns></returns>
		public static Account WithDraw(string id, float amount)
		{
			var cc = Find(id);
			if (cc == null)
				return null;


			lock (__operation) {
				cc.Statement.Add(EventType.WithDraw.GetDescription(), DateTime.Now, -amount);
			}
			return cc;
		}

		/// <summary>
		/// Executar uma operação de depósito.
		/// </summary>
		/// <param name="id">Identificador da conta.</param>
		/// <param name="amount">Valor monetário.</param>
		public static Account Deposit(string id, float amount)
		{
			var cc = Find(id) ?? Create(id);
			lock (__operation) {
				cc.Statement.Add(EventType.Deposit.GetDescription(), DateTime.Now, amount);
			}
			return cc;
		}

		/// <summary>
		/// Executar uma operação de transferência.
		/// </summary>
		/// <param name="origin">Identificador da conta origem.</param>
		/// <param name="destination">Identificador da conta destino.</param>
		/// <param name="amount">Valor monetário.</param>
		public static Tuple<Account, Account> Transfer(string origin, string destination, float amount)
		{
			var originCC = Find(origin);
			if (originCC == null)
				return Tuple.Create(default(Account), default(Account));

			var destinationCC = Find(destination) ?? Create(destination);
			lock (__operation) {
				originCC.Statement.Add($"{EventType.WithDraw.GetDescription()} (to: {destination})", DateTime.Now, -amount);
				destinationCC.Statement.Add($"{EventType.Deposit} (from: {origin})", DateTime.Now, amount);
			}
			return Tuple.Create(originCC, destinationCC);
		}

		/// <summary>
		/// Verifica a existência de uma conta.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static bool Exists(string id)
		{
			return AccountData.Any(x => x.Id == id);
		}

		/// <summary>
		/// Cria uma nova conta e a retorna.
		/// </summary>
		/// <param name="id"></param>
		/// <param name="type"></param>
		/// <returns></returns>
		public static Account Create(string id)
		{
			var cc = new Account(id);
			AccountData.Add(cc);
			return cc;
		}
	}
}
