using Aspcore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aspcore.Migrations
{
    public class DatnekContext : DbContext
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public DatnekContext(DbContextOptions<DatnekContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }


        /// <summary>
        /// 
        /// </summary>
        public virtual DbSet<Language> Languages { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual DbSet<User> Users { get; set; }
    }
}
