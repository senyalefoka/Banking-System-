using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Project.Domain.Entities
{
    public class Account
    {
        public Guid code { get; private set; }
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public Guid PersonalId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public virtual PersonalDetail PersonalDetail { get; set; } = null;
        public virtual ICollection<Transactions> Transactions { get; private set; } = new List<Transactions>();
        public Account()
        {
            AccountNumber = string.Empty;
            CreatedAt = DateTime.UtcNow;
        }
        public Account(string accountNumber, decimal initialBalance, Guid personalId)
        {


            if (initialBalance < 0)
            {
                throw new ArgumentException("Initial balance cannot be negative.");
            }
            this.AccountNumber = accountNumber;
            this.Balance = initialBalance;
            this.PersonalId = personalId;






        }

        public void Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Deposit amount must be positive.");
            }
            Balance += amount;
        }

        public void Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Withdrawal amount must be positive.");
            }
            if (amount > Balance)
            {
                throw new InvalidOperationException("Insufficient funds for this withdrawal.");
            }
            Balance -= amount;
        }

        public void Transfer(decimal amount, Account targetAccount)
        {
            if (targetAccount == null)
            {
                throw new ArgumentNullException(nameof(targetAccount), "Target account cannot be null.");
            }
            if (amount <= 0)
            {
                throw new ArgumentException("Transfer amount must be positive.");
            }
            if (amount > Balance)
            {
                throw new InvalidOperationException("Insufficient funds for this transfer.");
            }
            this.Withdraw(amount);
            targetAccount.Deposit(amount);
        }


    }
}
