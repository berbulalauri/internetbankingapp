using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BBS.DAL.Repositories;
using BBS.Interfaces.Services;
using BBS.Models.Models;
using BBS.Web.Areas.Administration.Constants;
using BBS.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace BBS.Web.Areas.Administration.Controllers
{
    [Area(AdministratorArea.Name)]
    public class CardRequestController : Controller
    {
        private readonly ICardRequestService _cardRequestService;
        private readonly ICardRequestHistoryService _cardRequestHistoryService;
        private readonly ICityRepository _cityRepository;
        private readonly ICardService _cardService;
        private readonly IBankAccountService _bankAccountService;

        public CardRequestController(ICardRequestService cardRequestService, 
                                     ICardRequestHistoryService cardRequestHistoryService, 
                                     ICityRepository cityRepository,
                                     ICardService cardService,
                                     IBankAccountService bankAccountService)
        {
            _bankAccountService = bankAccountService;
            _cardService = cardService;
            _cityRepository = cityRepository;
            _cardRequestHistoryService = cardRequestHistoryService;
            _cardRequestService = cardRequestService; 
        }
        public async Task<IActionResult> Index()
        {
            var cardRequests = (await _cardRequestService.GetAllIncludes()).ToList();

            return View(cardRequests);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateRequest(int id)
        {
            var updateRequestViewModel = new UpdateRequestViewModel();
            updateRequestViewModel.CardRequest = (await _cardRequestService.GetByIdIncludes(id));
            updateRequestViewModel.City = await _cityRepository.GetByIdAsync(updateRequestViewModel.CardRequest.CityId);
            updateRequestViewModel.CardRequestHistories = await _cardRequestHistoryService.GetAllByRequestIdIncludes(id);
            return View(updateRequestViewModel);
        }
        public async Task<IActionResult> Accept(int id)
        {
            var cardRequest = await _cardRequestService.GetById(id);
            cardRequest.CardRequestStatus = CardRequestStatus.Accepted;
            await _cardRequestService.Update(cardRequest);
            await _cardRequestHistoryService.MakeHistory(cardRequest.UserId);
            var accaunt = await _bankAccountService.GetUserAccounts(cardRequest.UserId);
            await _cardService.GenerateCard(accaunt.Where(x=>x.UserId == cardRequest.UserId).FirstOrDefault(), BBS.Core.Helpers.CardHelper.CardTypes.typeDeluxe);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Decline(int id)
        {
            var cardRequest = await _cardRequestService.GetById(id);
            cardRequest.CardRequestStatus = BBS.Models.Models.CardRequestStatus.Declined;
            await _cardRequestService.Update(cardRequest);
            await _cardRequestHistoryService.MakeHistory(cardRequest.UserId);
            return RedirectToAction(nameof(Index));
        }


    }
}