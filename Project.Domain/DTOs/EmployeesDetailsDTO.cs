using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.DTOs
{
    public class EmployeesDetailsDTO
    {
        public Guid emNo { get; set; }
        public string empName { get; set; }
        public string empSurname { get; set; }
        public string empIdNum { get; set; }


    }
}
