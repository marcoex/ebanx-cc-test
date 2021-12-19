using System;
using System.Linq;
using System.Collections.Generic;

namespace EBanx.Cc.BusinessAccount
{
	/// <summary>
	/// Representa um administrador geral de contas.
	/// </summary>
	public static class Accounts
	{
		private static IList<Account> AccountData;
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
			#region Criar contas fake para teste
			var ac = new Account(100);
			ac.Statement.Add("Depósito", DateTime.Parse("2020-01-30"), 1000.10f);
			ac.Statement.Add("Saque", DateTime.Parse("2020-02-05"), -150.10f);
			AccountData.Add(ac);
			#endregion
		}

		/// <summary>
		/// Obter uma conta existente. Retorna nulo se não encontrado.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static Account Find(long id)
		{
			return AccountData.FirstOrDefault(x => x.Id == id);
		}

		/// <summary>
		/// Tenta executar uma operação em conta existe, ou cria uma conta nova.
		/// </summary>
		/// <param name="type"></param>
		/// <param name="id"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static bool TryOperation(EventType type, long origin, long destination, float value)
		{
			var originCC = Find(origin);
			var destinationCC = Find(destination);
			if (type != EventType.Deposit && originCC == null)  //saque/transf em conta inexistente
				return false;

			lock (__operation) {
				if (destinationCC == null)
					destinationCC = Accounts.Create(destination);

				switch (type) {
					case EventType.Deposit:
						destinationCC.Statement.Add(type.GetDescription(), DateTime.Now, value);
						break;
					case EventType.WithDraw:
						destinationCC.Statement.Add(type.GetDescription(), DateTime.Now, -value);
						break;
					case EventType.Transfer:
						originCC.Statement.Add($"{type.GetDescription()} (to: {destination})", DateTime.Now, -value);
						destinationCC.Statement.Add($"{type.GetDescription()} (from: {origin})", DateTime.Now, value);
						break;
					default:
						break;
				}
			}
			return true;
		}

		/// <summary>
		/// Verifica a existência de uma conta.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static bool Exists(long id)
		{
			return AccountData.Any(x => x.Id == id);
		}

		/// <summary>
		/// Cria uma nova conta e a retorna.
		/// </summary>
		/// <param name="id"></param>
		/// <param name="type"></param>
		/// <returns></returns>
		public static Account Create(long id)
		{
			var cc = new Account(id);
			AccountData.Add(cc);
			return cc;
		}
	}
}
