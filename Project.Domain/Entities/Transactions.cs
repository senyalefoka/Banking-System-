using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Entities
{
    public class Transactions
    {

        public Guid code { get; set; }
        public Guid AccountId { get; set; }
        public decimal Outstanding_Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TransactionType { get; set; } = string.Empty;
        public string Descriptiion { get; set; } = string.Empty;
        public virtual Account Account { get; set; } = null;

        // ADD THIS: Parameterless constructor for Entity Framework
        public Transactions()
        {
            // Initialize with default values
            code = Guid.NewGuid();
            TransactionDate = DateTime.Now;
            TransactionType = string.Empty;
            Descriptiion = string.Empty;
        }

        public Transactions(Guid accountId, decimal outstanding_Amount, DateTime transactionDate, string transactionType)
        {
            // FIXED: Correct validation logic
            if (outstanding_Amount < 0)  // Changed from == 0 to < 0
            {
                throw new ArgumentException("Outstanding amount cannot be negative.");
            }

            if (string.IsNullOrWhiteSpace(transactionType))
            {
                throw new ArgumentException("Transaction type cannot be empty.");
            }

            if (transactionDate > DateTime.Now)
            {
                throw new ArgumentException("Transaction date cannot be in the future.");
            }

            // FIXED: Set ALL properties in constructor
            this.code = Guid.NewGuid();
            this.AccountId = accountId;
            this.Outstanding_Amount = outstanding_Amount;
            this.TransactionDate = transactionDate;
            this.TransactionType = transactionType;
            this.Descriptiion = string.Empty; // Initialize description
        }

        public void SetTransactionDate(DateTime transactionDate)
        {
            if (transactionDate > DateTime.Now)
            {
                throw new ArgumentException("Transaction date cannot be in the future.");
            }
            this.TransactionDate = transactionDate;
        }

        public void SetTransactionType(string transactionType)
        {
            if (string.IsNullOrWhiteSpace(transactionType))
            {
                throw new ArgumentException("Transaction type cannot be empty.");
            }
            this.TransactionType = transactionType;
        }

        // ADD: Method to set description
        public void SetDescription(string description)
        {
            this.Descriptiion = description ?? string.Empty;
        }

    }
}
