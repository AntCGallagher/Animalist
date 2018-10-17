using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Animalist.Models
{
    public class AnimalContext : DbContext
    {
        public AnimalContext(DbContextOptions<AnimalContext> options)
            : base(options)
        { }

        public DbSet<Animal> animaldb { get; set; }
    }

    public class Animal {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}