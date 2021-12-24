using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPersonalAccount.DBContext
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;

        public UserData? UserData { get; set; }
    }
}
