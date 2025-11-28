//using Microsoft.EntityFrameworkCore;
//using Project.Domain;
////using Project.Domain.Entities;
//using Project.Domain.Interfaces;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Project.Infrastructure.Repositories
//{
//    public class EmployeeRepository : IEmployee
//    {
//        private readonly employeeDetailsContext _employeeDetailsContext;


//        public EmployeeRepository( employeeDetailsContext detailsContext)
//        { 
//        _employeeDetailsContext= detailsContext;
//        }

//        public bool Equals(IEmployee? other)
//        {
//            throw new NotImplementedException();
//        }

//        public async Task<IEnumerable<EmployeeDetails>> GetAllEmployessAsync()
//        {
//            var employees = await _employeeDetailsContext.empoyeeDetails.ToListAsync();


//            return employees;




//        }
//    }
//}
