using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BBS.Interfaces.Services
{
    public interface ISecurityService
    {
        Task<bool> Login(string email, string password);
        Task<bool> LoginWithPhone(string PhoneNumber, string password);

        Task Logout();
    }
}
