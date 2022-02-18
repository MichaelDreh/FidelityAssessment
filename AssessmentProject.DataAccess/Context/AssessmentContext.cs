using AssessmentProject.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssessmentProject.DataAccess.Context
{
    public class AssessmentContext : DbContext
    {
        public AssessmentContext(DbContextOptions<AssessmentContext> options) : base(options)
        {
        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Account>().HasIndex(a => a.CompanyName).IsUnique();
            builder.Entity<Account>().HasIndex(a => a.Website).IsUnique();
            builder.Entity<User>().HasIndex(a => a.Email).IsUnique();
        }
    }
}
