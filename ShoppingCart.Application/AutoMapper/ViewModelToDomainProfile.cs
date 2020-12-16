using AutoMapper;
using ShoppingCart.Application.ViewModels;
using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Application.AutoMapper
{
    public class ViewModelToDomainProfile : Profile
    {
        public ViewModelToDomainProfile()
        {
            // We are adding the for Member in order not to map the category when adding a product
            CreateMap<ProductViewModel, Product>().ForMember(x=>x.Category,opt => opt.Ignore());
            //ONLY ADD THE OTHERS IF WE WANT TO ADD CATEGORIES OR MEMBERS
            //CreateMap<CategoryViewModel, Category>();
        }
    }
}
