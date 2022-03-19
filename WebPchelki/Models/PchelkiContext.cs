using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebPchelki.Models.Entities;

namespace WebPchelki.Models
{
    public class PchelkiContext: DbContext
    {
        #region DbSet
        public DbSet<Client> Clients { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<CommentProduct> CommentProducts { get; set; }
        public DbSet<CommentBee> CommentBees { get; set; }
        public DbSet<CommentBeehive> CommentBeehives { get; set; }
        public DbSet<CommentEquipment> CommentEquipments { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<TypeProduct> TypeProducts { get; set; }
        public DbSet<Bee> Bees { get; set; }
        public DbSet<Beehive> Beehives { get; set; }
        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(local);Database=Pchelki;Trusted_Connection=True;");
        }

        public PchelkiContext(DbContextOptions<PchelkiContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
