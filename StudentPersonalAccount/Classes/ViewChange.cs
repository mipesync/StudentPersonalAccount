using StudentPersonalAccount.View;
using StudentPersonalAccount.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPersonalAccount.Classes
{
    internal class ViewChange
    {
        public ViewChange(object viewObject)
        {
            viewObject = new AuthenticationView();
        }
    }
}
