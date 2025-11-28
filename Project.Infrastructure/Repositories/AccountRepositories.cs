using Microsoft.Identity.Client;
using Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace Project.Infrastructure.Repositories
{
    public class AccountRepositories : Domain.Interfaces.IAccount
    {
        private readonly employeeDetailsContext _context;

        // Implement IAccount interface members
     

        public AccountRepositories(employeeDetailsContext context)
        {
            _context = context;
        }

        public Task<Account> CreateAsync(Account account)
        {
            var createdAccount = _context.Accounts.Add(account);
            _context.SaveChangesAsync();
            return Task.FromResult(createdAccount.Entity);
        }

        //public Task<Account> CreateAsync(Account account)
        //{
        //    throw new NotImplementedException();
        //}

        public Task DeleteAsync(Guid id)
        {
            var account = _context.Accounts.FirstOrDefault(a => a.code == id);
            if (account != null)
                _context.Accounts.Remove(account);
            return _context.SaveChangesAsync();

        }

        public async Task<IEnumerable<Account>> GetAllAsync()
        {
            return await _context.Accounts.ToListAsync();
        }


        public async Task<Account> GetByAccountNumberAsync(string accountNumber)
        {
            var account = await _context.Accounts
                .FirstOrDefaultAsync(a => a.AccountNumber == accountNumber);
            return account;

        }

        public async Task<Account> GetByIdAsync(Guid id)
        {
            var account = await _context.Accounts
                  .FirstOrDefaultAsync(a => a.code == id);
            return account;
        }

        public Task<List<Account>> GetByPersonalIdAsync(Guid personalId)
        {
            var accounts = _context.Accounts
                .Where(a => a.PersonalId == personalId)
                .ToListAsync();
            return accounts;
        }

        public Task<bool> UpdateAsync(Account account)
        {
            throw new NotImplementedException();
        }

    }
}
