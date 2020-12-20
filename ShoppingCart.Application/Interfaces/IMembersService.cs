using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Application.ViewModels
{
    public interface IMembersService
    {
        void AddMember(MemberViewModel m);
        MemberViewModel GetMember(string name);
    }
}
