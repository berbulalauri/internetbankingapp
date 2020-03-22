using BBS.DAL.Repositories;
using BBS.Interfaces;
using BBS.Interfaces.Services;
using BBS.Models.Models;
using BBS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace BBS.Web.Controllers
{
	[Authorize]
	public class CardsController : Controller
	{
		private readonly ICardService cardService;
		private readonly ICardRequestService _cardRequestService;
		private readonly ICardRequestHistoryService _cardRequestHistoryService;
		private readonly ITransactionsService transactionsService;
		private readonly UserManager<User> userManager;
		private readonly ILogger _logger;
		private readonly ICityRepository _cityRepository;

		public CardsController(ICardService card, ITransactionsService transactions, UserManager<User> user, ILogger logger, ICityRepository cityRepository, ICardRequestService cardRequestService, ICardRequestHistoryService cardRequestHistoryService)
		{
			_cardRequestHistoryService = cardRequestHistoryService;
			_cardRequestService = cardRequestService;
			_cityRepository = cityRepository;
			cardService = card;
			transactionsService = transactions;
			userManager = user;
			_logger = logger;
		}

		[Authorize]
		public async Task<IActionResult> Index(string order = "Id", bool ascend = true)
		{
			User user = await userManager.GetUserAsync(User);
			List<Card> cards = await cardService.GetCardsAsync(user.Id);
			List<Transactions> transactions = await transactionsService.TransactionsByUserId(user.Id);
			var result = await _cardRequestService.GetByUserId(user.Id);

			if (result != null)
			{
				ViewData["cardRequest"] = result;
				ViewBag.ShowInfo = true;
			}
			else { 
				ViewBag.ShowInfo = false; 
			}

			PropertyInfo property = typeof(Transactions).GetProperty(order);

			if (property != null)
			{
				if (ascend)
				{
					ViewData["transact"] = transactions.OrderBy(x => property.GetValue(x)).ToList();
				}
				else
				{
					ViewData["transact"] = transactions.OrderByDescending(x => property.GetValue(x)).ToList();
				}
			}
			else
			{
				ViewData["transact"] = transactions.OrderBy(x => x.Id).ToList();
			}

			ViewData["orderInfo"] = new Tuple<string, bool>(order, ascend);

			ViewData["userId"] = user.Id;
			return View(cards);
		}
		[HttpGet]
		public async Task<IActionResult> RequestSalaryCard()
		{
			ViewData["Cities"] = await _cityRepository.GetCitiesWithTrackingAsync();
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> RequestSalaryCard(CardRequestViewModel cardRequestViewModel)
		{
			cardRequestViewModel.City = await _cityRepository.GetByIdAsync(cardRequestViewModel.City.Id);
			CardRequest cardRequest = cardRequestViewModel;

			if (ModelState.IsValid)
			{
				cardRequest.User = await userManager.GetUserAsync(User);
				cardRequest.UserId = cardRequest.User.Id;
				cardRequest.IsActive = true;

			    await _cardRequestService.Add(cardRequest);

				await _cardRequestHistoryService.MakeHistory(cardRequest.UserId);

				return RedirectToAction("Index", "Cards");
			}
			else
			{
				ViewData["Cities"] = await _cityRepository.GetCitiesWithTrackingAsync();
				return View();
			}
		}
	}
}
