using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver.Core.Configuration;
using ShoppingCart.Data.Context;
using ShoppingCart.Data.Repositories;
using ShoppingCart.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace ShoppingCart.IOC
{
    public class DependancyContainer
    {
        public static void RegisterServices(IServiceCollection services, String connectionString)
        {

            services.AddDbContext<ShoppingCartDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped<IProductsRepository, ProductsRepository>();
        }
    }
}