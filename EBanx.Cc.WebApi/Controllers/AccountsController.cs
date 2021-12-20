using Ebanx.Cc.WebApi.ViewModels;
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
		public IActionResult Balance(string account_id)
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
		public IActionResult Event([FromBody] EventViewModel model)
		{
			var originCC = model.Origin != null ? Accounts.Find(model.Origin) : default(Account);
			var destinationCC = model.Destination != null ? Accounts.Find(model.Destination) : default(Account);
			model.Type = model.Type.ToLower();

			if (model.Type != "deposit" && originCC == null)
				return NotFound(0);

			var result = new Dictionary<string, Account>();
			switch (model.Type) {
				case "deposit":
					result["destination"] = Accounts.Deposit(model.Destination, model.Amount);
					break;
				case "withdraw":
					result["origin"] = Accounts.WithDraw(model.Origin, model.Amount);
					break;
				case "transfer":
					var ccs = Accounts.Transfer(model.Origin, model.Destination, model.Amount);
					result["origin"] = ccs.Item1;
					result["destination"] = ccs.Item2;
					break;
				default:
					return BadRequest();
			}
			return Created("", result);
		}



	}
}
