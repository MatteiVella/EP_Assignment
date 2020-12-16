﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using ShoppingCart.Application.Interfaces;
using ShoppingCart.Application.ViewModels;
using ShoppingCart.Domain.Interfaces;
using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Application.Services
{
    public class ProductsService : IProductsService
    {
        private IProductsRepository _productRepo;
        private IMapper _mapper;

        public ProductsService(IProductsRepository products, IMapper mapper)
        {
            _productRepo = products;
            _mapper = mapper;
        }

        public IQueryable<ProductViewModel> GetProducts()
        {
            return _productRepo.GetProducts().ProjectTo<ProductViewModel>(_mapper.ConfigurationProvider);

            /*var list = from p in _productRepo.GetProducts()
                       select new ProductViewModel()
                       {
                           Id = p.Id,
                           Name = p.Name,
                           Description = p.Description,
                           ImageUrl = p.ImageUrl,
                           Price = p.Price,

                       };
            return list;*/
        }

        public ProductViewModel GetProduct(Guid id)
        {
            return _mapper.Map<ProductViewModel>(_productRepo.GetProduct(id));
        }

        public void AddProduct(ProductViewModel data)
        {
           _productRepo.AddProduct(_mapper.Map<Product>(data));
        }

        public void DeleteProduct(Guid id)
        {
            if (_productRepo.GetProduct(id) != null)
                _productRepo.DeleteProduct(id);
        }
    }
}
