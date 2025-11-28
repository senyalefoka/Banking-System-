using Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Interfaces
{
    public interface IAccount
    {

        // Get account by its primary key
        Task<Account> GetByIdAsync(Guid id);
        Task<IEnumerable<Account>> GetAllAsync();



        // Get account by account number
        Task<Account> GetByAccountNumberAsync(string accountNumber);

        // Get ALL accounts for a person
        Task<List<Account>> GetByPersonalIdAsync(Guid personalId);

        // Create new account
        Task<Account> CreateAsync(Account account);

        // Update existing account
        Task<bool> UpdateAsync(Account account);

        // Delete account
        Task DeleteAsync(Guid id);
    }

}
