using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SubwayEntrance.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace SubwayEntrance.Data
{
    public class SubwayContext : DbContext
    {
        public SubwayContext(DbContextOptions<SubwayContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<UserWithSubway>()
            .HasOne(sc => sc.Authenticate)
            .WithMany(s => s.UserWithSubways)
            .HasForeignKey(sc => sc.userId);


            modelBuilder.Entity<UserWithSubway>()
            .HasOne(sc => sc.SubwayUser)
            .WithMany(s => s.UserWithSubways)
            .HasForeignKey(sc => sc.SubwayUserId);

            

        }
        public DbSet<SubwayUser>  SubwayUsers { get; set; }
        public DbSet<User>  Users { get; set; }

        public DbSet<UserWithSubway>  UserWithSubways { get; set; }


       
    }
    
    
}
