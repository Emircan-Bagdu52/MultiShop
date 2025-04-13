﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Application.Services
{
    public static class ServicesRegistirarion
    {
        public static void AddAplicationService(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddMediatR(cfg =>cfg.RegisterServicesFromAssembly(typeof(ServicesRegistirarion).Assembly));
		}
	}
}
