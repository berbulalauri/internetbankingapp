using BBS.Interfaces;
using BBS.Interfaces.Services;
using BBS.Models.Constants;
using BBS.Models.CustomExceptions;
using BBS.Models.Models;
using BBS.Models.ApiModels;
using BBS.Web.Constants;
using BBS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BBS.Web.Controllers
{

    [Authorize]
    public class PaymentController : Controller
    {
        private const string INTERNET_PAYMENT_SERVICE_NAME = "InternetServicePayment";
        private const string TV_PAYMENT_SERVICE_NAME = "TvPayment";
        private const string BUS_TICKET_SERVICE_NAME = "Bus Ticket";

        private readonly ICardService _cardService;
        private readonly ITransactionsService _transactionsService;
        private readonly IServiceService _serviceService;
        private readonly UserManager<User> _userManager;
        private readonly IPaymentService _paymentService;
        private readonly ITripService _tripService;
        private readonly ITicketService _ticketService;
        private readonly IBusDestinationService _busDestinationService;

        private IBankAccountService _accountService;
        private IBankServService _servicesRepo;
        private ILogger _logger;

        public PaymentController(ICardService cardService, ITransactionsService transactionsService,
            UserManager<User> userManager, IServiceService serviceService, ILogger logger,
            IBankAccountService accounts, IBankServService service, IPaymentService paymentService, ITripService tripService,
            ITicketService ticketService, IBusDestinationService busDestinationService)
        {
            _cardService = cardService;
            _transactionsService = transactionsService;
            _userManager = userManager;
            _accountService = accounts;
            _serviceService = serviceService;
            _servicesRepo = service;
            _logger = logger;
            _paymentService = paymentService;
            _tripService = tripService;
            _ticketService = ticketService;
            _busDestinationService = busDestinationService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            User user = await _userManager.GetUserAsync(User);
            return View(await _cardService.GetCardsAsync(user.Id));
        }

        [HttpPost]
        public async Task<IActionResult> Index(TransferViewModel transfer)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Transactions transaction = new Transactions
                    {
                        PersonalAccountId = "",
                        SenderCardId = transfer.Id,
                        Description = BankStringConstants.GeneralTransferServiceDescription,
                        ReceiverAccountId = (await _cardService.GetCardAsync(transfer.Number)).Account.Id,
                        Amount = transfer.Amount,
                        Date = DateTime.Now,
                        ServiceId = (await _servicesRepo.GetServiceByNameAsync(BankStringConstants.GeneralTransferServiceName)).Id
                    };

                    await _paymentService.GeneralTransfer(transaction, User);
                    TempData["succsess"] = ErrorMessages.TransferSuccess;
                }
                catch (CardOwnerException e)
                {
                    TempData["cardOwnerException"] = e.Message;
                }
                catch (TransactionException e)
                {
                    TempData["status"] = e.Message;
                }
                catch (NullReferenceException)
                {
                    TempData["invalidCardNumber"] = ErrorMessages.InvalidCardNumber;
                }
                catch (InvalidOperationException)
                {
                    TempData["invalidAmountValue"] = ErrorMessages.InvalidAmountValue;
                }
                catch (Exception)
                {
                    TempData["status"] = ErrorMessages.TransferError;
                }
            }

            User user = await _userManager.GetUserAsync(User);

            return View(await _cardService.GetCardsAsync(user.Id));
        }

        public IActionResult ChooseBusSit()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RefillPhone(int cardId, int mobile, decimal amount)
        {
            if(!Regex.IsMatch(mobile.ToString(), RegexConstants.RegexForPhoneNumber))
            {
                TempData["status"] = ErrorMessages.InvalidNumber;
                return RedirectToAction(nameof(Index));
            }

            Transactions transaction = new Transactions
            {
                PersonalAccountId = "",
                Description = BankStringConstants.PhoneRefilServiceDescription + mobile,
                Amount = amount,
                Date = DateTime.Now,
                SenderCardId = cardId,
                ReceiverAccount = await _accountService.GetByNameAsync(BankStringConstants.PhoneRefillAcc),
                Service = await _servicesRepo.GetServiceByNameAsync(BankStringConstants.PhoneRefilServiceName)
            };

            try
            {
                await _transactionsService.ServicePayment(transaction, User);
                TempData["succsess"] = ErrorMessages.TransferSuccess;
            }
            catch (TransactionException e)
            {
                TempData["status"] = e.Message;
            }
            catch (InvalidOperationException)
            {
                TempData["status"] = ErrorMessages.InvalidAmountValue;
            }
            catch
            {
                TempData["status"] = ErrorMessages.TransferError;
            }

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> ElectronicTicketPayment(int id, string errorMessage = null)
        {
            ViewBag.serviceTypeId = id;
            ViewBag.serviceName = TV_PAYMENT_SERVICE_NAME;
            User user = await _userManager.GetUserAsync(User);
            ViewBag.viewAllService = new SelectList(await _paymentService.GetServicesById(id), "Id", "Name");
            ViewBag.CardList = new SelectList(await _cardService.GetCardsForPaymentAsync(user.Id), "Id", "Name");
            if (!string.IsNullOrEmpty(errorMessage))

            {
                ModelState.AddModelError(string.Empty, errorMessage);
            }

            return View();
        }

        public async Task<IActionResult> InternetService(int id)
        {
            ViewData["viewAllService"] = new SelectList(await _paymentService.GetServicesById(id), "Id", "Name");
            return View();
        }

        public async Task<IActionResult> InternetServicePayment(int id, Service service, string errorMessage = null)
        {
            ViewBag.serviceTypeId = service.Id;
            ViewBag.serviceName = INTERNET_PAYMENT_SERVICE_NAME;
            User user = await _userManager.GetUserAsync(User);
            ViewData["ServiceProviderName"] = _paymentService.GetReceiverNameService(service.Id);
            ViewData["CardList"] = new SelectList(await _cardService.GetCardsForPaymentAsync(user.Id), "Id", "Name");
            ViewData["ReceiverAccountId"] = _paymentService.GetReceiverAccountId(service.Id);
            ViewData["ServiceId"] = service.Id;
            if (!string.IsNullOrEmpty(errorMessage))
            {
                ViewData["ServiceProviderName"] = _paymentService.GetReceiverNameService(id);
                ViewData["ReceiverAccountId"] = _paymentService.GetReceiverAccountId(id);
                ViewData["ServiceId"] = id;
                ModelState.AddModelError(string.Empty, errorMessage);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CompleteTransaction(Transactions transactions, string serviceName, int serviceTypeId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _paymentService.PaymentForService(transactions);
                }
            }
            catch (BBS.Models.CustomExceptions.NotEnoughMoneyException ex)
            {
                return RedirectToAction(serviceName, "Payment", new { ErrorMessage = ex.Message, id = serviceTypeId });
            }
            User user = await _userManager.GetUserAsync(User);
            ViewBag.CardList = new SelectList(await _cardService.GetCardsForPaymentAsync(user.Id), "Id", "Name");

            ViewData["transactioType"] = _paymentService.GetReceiverNameService(transactions.ServiceId);
            ViewData["UserEmail"] = user.Email;
            ViewData["UserName"] = $"{user.FirstName} {user.LatsName}";
            ViewData["Mobile"] = user.Phone;
            ViewData["Amount"] = transactions.Amount;
            ViewData["RecipientAccountId"] = transactions.PersonalAccountId;
            Card card = await _cardService.GetCardAsync(transactions.SenderCardId);
            ViewData["CurrentPayingCartNumber"] = card.Number;

            return View();
        }

        public async Task<IActionResult> TvPayment(int id, string errorMessage = null)
        {

            ViewBag.serviceTypeId = id;
            ViewBag.serviceName = TV_PAYMENT_SERVICE_NAME;
            User user = await _userManager.GetUserAsync(User);
            ViewBag.viewAllService = new SelectList(await _paymentService.GetServicesById(id), "Id", "Name");
            ViewBag.CardList = new SelectList(await _cardService.GetCardsForPaymentAsync(user.Id), "Id", "Name");
            if (!string.IsNullOrEmpty(errorMessage))

            {
                ModelState.AddModelError(string.Empty, errorMessage);
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> BookTicket()
        {
            try
            {
                ViewData["destinations"] = await _busDestinationService.GetDestinationsAsync();
                return View();
            }
            catch (Exception)
            {
                return Redirect($"/Error/{500}");
            }

        }

        [HttpPost]
        public async Task<IActionResult> BookTicket(Trip model)
        {
            if (ModelState.IsValid)
            {
                if (model.DestinationFromId == model.DestinationToId)
                {
                    ModelState.AddModelError("DestinationToId", ModelErrors.DestinationError);
                    ViewData["destinations"] = await _busDestinationService.GetDestinationsAsync();
                    return View();
                }
            }
            try
            {
                IEnumerable<Trip> trips = await _tripService.FilterTrips(model.DestinationFromId, model.DestinationToId, model.Departure, model.FreeSeatCount);
                if (trips.ToList().Count == 0)
                {
                    ViewData["destinations"] = await _busDestinationService.GetDestinationsAsync();
                    ViewData["message"] = Alerts.NO_MATCHING_TRIPS;
                    return View(nameof(BookTicket));
                }
                else
                {
                    model.DestinationFrom = trips.FirstOrDefault().DestinationFrom;
                    model.DestinationTo = trips.FirstOrDefault().DestinationTo;
                    ViewData["trips"] = trips;
                    return View("SelectTrip", model);
                }
            }
            catch (Exception)
            {
                return Redirect($"/Error/{500}");
            }

        }


        [HttpGet]
        public async Task<IActionResult> ChooseBusSeat(int id)
        {
            Trip trip = await _tripService.GetTripById(id);
            if (ModelState.IsValid)
            {
                TripTicketViewModel tripTicketViewModel = new TripTicketViewModel()
                {
                    DestinationFrom = trip.DestinationFrom,
                    DestinationTo = trip.DestinationTo,
                    Departure = trip.Departure,
                    TotalMinutes = trip.TotalMinutes,
                    TicketPrice = trip.TicketPrice,
                    TotalSeats = trip.TotalSeats,
                    FreeSeats = trip.FreeSeats,
                    TripId = id
                };
                return View(tripTicketViewModel);
            }
            
            return View();
        }

        [HttpPost]
        public IActionResult ChooseBusSeat(TripTicketViewModel tripTicketViewModel)
        {
            return RedirectToAction("ConfirmBusTicket", tripTicketViewModel);
        }


        [HttpGet]
        public async Task<IActionResult> ConfirmBusTicket(TripTicketViewModel tripTicketViewModel)
        {
            User user = await _userManager.GetUserAsync(User);
            ViewData["CardList"] = new SelectList(await _cardService.GetCardsForPaymentAsync(user.Id), "Id", "Name");
            ViewBag.serviceTypeId = BusService.serviceCategoryId;
            ViewBag.ServiceId = BusService.serviceId;
            ViewBag.serviceName = BUS_TICKET_SERVICE_NAME;
            ViewData["ReceiverAccountId"] = _paymentService.GetReceiverAccountId(ViewBag.serviceTypeId);
            ViewData["ServiceProviderName"] = _paymentService.GetReceiverNameService(ViewBag.serviceTypeId);
            List<int> seatNumbers = tripTicketViewModel.SeatNumbers.Split(',').Select(int.Parse).ToList();
            ViewData["Seat"] = seatNumbers;
            ViewData["SeatNumbers"] = seatNumbers.Count;
            return View(tripTicketViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmBusTicket(TripTicketViewModel tripTicketViewModel, Trip trip, Ticket ticket, Transactions transactions)
        {
            User user = await _userManager.GetUserAsync(User);
            ViewData["CardList"] = new SelectList(await _cardService.GetCardsForPaymentAsync(user.Id), "Id", "Name");
            if (ModelState.IsValid)
            {
                tripTicketViewModel = new TripTicketViewModel()
                {
                    DestinationFrom = trip.DestinationFrom,
                    DestinationTo = trip.DestinationTo,
                    Departure = trip.Departure,
                    TotalMinutes = trip.TotalMinutes,
                    TicketPrice = trip.TicketPrice,
                    TotalSeats = trip.TotalSeats,
                    FreeSeats = trip.FreeSeats,
                    PassangerName = ticket.PassangerName,
                    TripId = ticket.TripId,
                    PurchaseDate = ticket.PurchaseDate = DateTime.Now,
                    SeatNumber = ticket.SeatNumber,
                    ServiceId = transactions.ServiceId,
                    ReceiverAccountId = transactions.ReceiverAccountId,
                    Amount = transactions.Amount,
                    PersonalAccountId = transactions.PersonalAccountId,
                    Date = transactions.Date = DateTime.Now
                };

                await _ticketService.Create(ticket);
                await _paymentService.PaymentForService(transactions);

                return RedirectToAction("Index", "Cards");
            }
            
            return View();
        }


        public IActionResult VerifyAmountValueForElectronicTickets(double amount = 0)
        {
            if (amount < 0.01)
            {
                return Json($"Ammount must be greater then 0.01");
            }

            return Json(true);
        }

        public IActionResult VerifyAmountValue(decimal amount = 0)
        {
            if (amount < 3)
            {
                return Json($"Ammount must be greater then 3");
            }

            return Json(true);
        }
    }
}
