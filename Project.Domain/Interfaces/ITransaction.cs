using Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Interfaces
{
    public interface ITransactionRepository
    {
        // Get a single transaction by its ID
        // Get a single transaction by its ID (nullable since it might not exist)
        Task<Transactions> GetByIdAsync(Guid id);

        // Get ALL transactions for an account (returns a list)
        Task<List<Transactions>> GetByAccountIdAsync(Guid accountId);

        // Create a new transaction and return the created entity
        Task<Transactions> CreateAsync(Transactions transaction);

        // Update an existing transaction


        // Delete a transaction and return success status
        Task<bool> DeleteAsync(Guid id);

        Task UpdateAsync(Transactions transaction);

        Task<List<Transactions>> GetAllTaransaction();
    }
}
