using BBS.Models.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BBS.Interfaces.Services
{
    public interface IPaymentService
    {
        Task<IEnumerable<Service>> GetServicesById(int id);
        int GetSenderAccountId(int cardId);
        int? GetReceiverAccountId(int serviceId);
        string GetReceiverNameService(int serviceId);
        Task AddTransactionToHistory(Transactions transactions);
        Task TransferMoneyToService(int senderAccountId, int receiverAccountId, decimal amount, int serviceId);
        Task PaymentForService(Transactions transactions);
        Task GeneralTransfer(Transactions transaction, ClaimsPrincipal currentUser);
    }
}
