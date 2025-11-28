using Microsoft.EntityFrameworkCore;
using Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Infrastructure.Repositories
{
    public class TransactionRepository: Domain.Interfaces.ITransactionRepository
    {
        private readonly employeeDetailsContext _context;

        public TransactionRepository(employeeDetailsContext context)
        {
            _context = context;
        }

        public async Task<Transactions> GetByIdAsync(Guid id)
        {
            return await _context.Transactions
                .FirstOrDefaultAsync(t => t.code == id);
        }

        public async Task<List<Transactions>> GetByAccountIdAsync(Guid accountId)
        {
            return await _context.Transactions
                .Where(t => t.AccountId == accountId)
                .OrderByDescending(t => t.TransactionDate)
                .ToListAsync();
        }

        //public async Task<Transactions> CreateAsync(Transactions transaction)
        //{
        //    // Generate new GUID if not provided
        //    if (transaction.code == Guid.Empty)
        //        transaction.code = Guid.NewGuid();

        //    transaction.TransactionDate = DateTime.UtcNow;

        //    _context.Transactions.Add(transaction);
        //    await _context.SaveChangesAsync();
        //    return transaction;
        //}

        //public async Task UpdateAsync(Transactions transaction)
        //{
        //    var existing = await _context.Transactions
        //        .FirstOrDefaultAsync(t => t.code == transaction.code);

        //    if (existing != null)
        //    {
        //        // Update properties
        //        existing.Outstanding_Amount = transaction.Outstanding_Amount;
        //        existing.TransactionType = transaction.TransactionType;
        //        existing.Descriptiion = transaction.Descriptiion;
        //        // Note: Usually you wouldn't update AccountId or TransactionDate

        //        await _context.SaveChangesAsync();
        //    }
        //}

        public async Task<bool> DeleteAsync(Guid id)
        {
            var transaction = await _context.Transactions
                .FirstOrDefaultAsync(t => t.code == id);

            if (transaction == null)
                return false;

            _context.Transactions.Remove(transaction);
            var rowsAffected = await _context.SaveChangesAsync();
            return rowsAffected > 0;
        }

        public async Task<List<Transactions>> GetAllTaransaction()
        {
            var result = await _context.Transactions.Include(t => t.Account).ThenInclude(p => p.PersonalDetail).Where(t => t.TransactionType != null).ToListAsync();
            return result;
        }

        public Task<Transactions> CreateAsync(Transactions transaction)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Transactions transaction)
        {
            throw new NotImplementedException();
        }

    }
}
