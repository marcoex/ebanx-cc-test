using EBanx.Cc.AccountsAdmin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ebanx.Cc.WebApi.Controllers
{
	[ApiController]
	public class AccountsController : ControllerBase
	{
		private readonly ILogger<AccountsController> _logger;

		public AccountsController(ILogger<AccountsController> logger)
		{
			_logger = logger;
		}

		/// <summary>
		/// Reset state before starting tests
		/// </summary>
		[Route("[action]")]
		[HttpPost]
		public IActionResult Reset()
		{
			Accounts.Initialize();
			return Ok();
		}

		/// <summary>
		/// Determina se a Api está totalmente operante.
		/// </summary>
		[Route("[action]")]
		[HttpGet]
		public IActionResult Health()
		{
			//hack: testes de saúde da api
			var list = new List<KeyValuePair<string, bool>>();
			list.Add(KeyValuePair.Create("Serviço administrador de contas", true));
			list.Add(KeyValuePair.Create("Serviço de persistência de dados.", false));
			return Ok(list);
		}

		/// <summary>
		/// Get balance for existing account
		/// </summary>
		[Route("[action]")]
		[HttpGet]
		public IActionResult Balance(long account_id)
		{
			var cc = Accounts.Find(account_id);
			if (cc == null)
				return NotFound(0);

			return Ok(cc.Balance);
		}

		/// <summary>
		/// Create account with initial balance
		/// </summary>
		[Route("[action]")]
		[HttpPost]
		public IActionResult Event(EventType type, long origin, long destination, float amount)
		{
			var originCC = Accounts.Find(origin);
			var destinationCC = Accounts.Find(destination);
			if (!Accounts.TryOperation(type, origin, destination, amount))
				return NotFound(0);


			switch (type) {
				case EventType.Deposit:
					return Created("", new { Destination = originCC });
				case EventType.WithDraw:
					return Created("", new { Origin = originCC });
				case EventType.Transfer:
					return Created("", new { Origin = originCC, destination = destinationCC });
			}
			return null;  //não chegará aqui
		}



	}
}
