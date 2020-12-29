using AutoMapper;
using ShoppingCart.Application.ViewModels;
using ShoppingCart.Domain.Interfaces;
using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Application.Services
{
    public class MembersService : IMembersService
    {
        private IMembersRepository _membersRepo;
        private IMapper _mapper;
        public MembersService(IMembersRepository memberRepo, IMapper mapper)
        {
            _membersRepo = memberRepo;
            _mapper = mapper;
        }
        public void AddMember(MemberViewModel m)
        {
            _membersRepo.AddMember(_mapper.Map<Member>(m));
        }

        public MemberViewModel GetMember(string name)
        {
            return _mapper.Map<MemberViewModel>(_membersRepo.GetMember(name));
        }
    }
}
