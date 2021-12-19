using EBanx.Cc.BusinessAccount;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ebanx.Cc.WebApi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class AccountController : ControllerBase
	{
		private readonly ILogger<AccountController> _logger;

		public AccountController(ILogger<AccountController> logger)
		{
			_logger = logger;
		}

		/// <summary>
		/// Reset state before starting tests
		/// </summary>
		[HttpPost]
		public void Reset()
		{
			Accounts.Initialize();
		}

		/// <summary>
		/// Get balance for existing account
		/// </summary>
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
