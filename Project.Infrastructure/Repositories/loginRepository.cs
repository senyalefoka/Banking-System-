using Project.Domain.Entities;
using Project.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Infrastructure.Repositories
{
    public class loginRepository : ILogin
    {


        private readonly employeeDetailsContext _LoginContext;

        public loginRepository(employeeDetailsContext LoginContext) 
        { 
        
        _LoginContext= LoginContext;

        }

        public Task<IEnumerable<Login>> GetAllUserAsync()
        {
            var users = _LoginContext.Logins.AsEnumerable();

            return Task.FromResult(users);
        }
        public Task<Login> Register(string username, string password, string email)
        {
            var login = new Login
            {
                loginId = Guid.NewGuid(),
                Username = username,
                Password = password, // You should hash this!
                Email = email
            };

            // Add to context and save changes
            _LoginContext.Logins.Add(login);
            _LoginContext.SaveChanges(); // This blocks the thread!

            return Task.FromResult(login);
        }
        public Task<Login> ValidateUserAsync(string username, string password)
        {
                // Find the user in the context
            var login = _LoginContext.Logins
                .FirstOrDefault(l => l.Username == username && l.Password == password);

            // Return the found user or null
            return Task.FromResult(login);
        }
    }
}   
