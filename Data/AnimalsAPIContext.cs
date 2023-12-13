using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AnimalsAPI.Models;

namespace AnimalsAPI.Data
{
    public class AnimalsAPIContext : DbContext
    {
        public AnimalsAPIContext (DbContextOptions<AnimalsAPIContext> options)
            : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MsSqlLocalDb;Database=AllatDb;Trusted_Connection=true");
        }

        public DbSet<AnimalsAPI.Models.Breed> Breed { get; set; } = default!;

        public DbSet<AnimalsAPI.Models.Animal>? Animal { get; set; }
    }
}
