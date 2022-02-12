using StudentPersonalAccount.IBCrypt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPersonalAccount.Classes
{
    internal class EncryptionFactory
    {
        public static IEncryption Create()
        {
            return new BCryption();
        }
    }
}
