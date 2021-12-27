using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPersonalAccount.DBContext
{
    public class UserData
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? SecondName { get; set; }
        public string? LastName { get; set; }
        public string? Phone { get; set; }
        public string GroupNumber { get; set; } = null!;
        public string ProfilePhoto { get; set; } = null!;

        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
