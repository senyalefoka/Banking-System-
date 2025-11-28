using Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Interfaces
{
        public interface ILogin
    {
        Task<Login> ValidateUserAsync(string username, string password);
        Task<Login>Register(string username, string password, string email);
        public Task<IEnumerable<Login>> GetAllUserAsync();
    }
}
