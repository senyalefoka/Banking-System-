using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Entities
{
    public  class Login
    {
        public Guid loginId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }




    }
}
