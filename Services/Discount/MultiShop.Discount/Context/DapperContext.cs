﻿using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MultiShop.Discount.Entites;
using System.Data;

namespace MultiShop.Discount.Context
{
	public class DapperContext:DbContext
	{
		private readonly IConfiguration _configuration;
		private readonly string _connectionString;

		public DapperContext(IConfiguration configuration)
		{
			_configuration = configuration;
			_connectionString = _configuration.GetConnectionString("DefaultConnection");
		}
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("Server=DESKTOP-UOIJ6R2\\SQLEXPRESSS;initial Catalog=MultiShopDiscountDb;integrated Security=true");
		}
        public DbSet<Coupon> Coupons { get; set; }
		public IDbConnection CreatConnection()=>new SqlConnection(_connectionString);
    }
}
