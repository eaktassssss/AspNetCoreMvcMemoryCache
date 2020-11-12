using MemoryCacheWebApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemoryCacheWebApp.Context
{
    public class MemoryCacheContext:DbContext
    {
        public MemoryCacheContext(DbContextOptions<MemoryCacheContext> contextOptions):base(contextOptions)
        {
        }
        public DbSet<Products> Products { get; set; }
    }
}
