using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace KanbanDal
{
    public partial class KanbanDbModel : DbContext
    {
        public KanbanDbModel()
            : base("name=KanbanDbModelConnection")
        {
        }

        public virtual DbSet<ConfigTable> ConfigTables { get; set; }
        public virtual DbSet<FogLamp> FogLamps { get; set; }
        public virtual DbSet<FogLampOrder> FogLampOrders { get; set; }
        public virtual DbSet<TestTray> TestTrays { get; set; }
        public virtual DbSet<Workstation> Workstations { get; set; }
        public virtual DbSet<OrderLine> OrderLines { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ConfigTable>()
                .Property(e => e.SystemProperty)
                .IsUnicode(false);

            modelBuilder.Entity<ConfigTable>()
                .Property(e => e.SystemValue)
                .IsUnicode(false);

            modelBuilder.Entity<FogLamp>()
                .HasMany(e => e.OrderLines)
                .WithRequired(e => e.FogLamp)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FogLampOrder>()
                .HasMany(e => e.OrderLines)
                .WithRequired(e => e.FogLampOrder)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TestTray>()
                .HasMany(e => e.FogLamps)
                .WithRequired(e => e.TestTray)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TestTray>()
                .HasMany(e => e.OrderLines)
                .WithRequired(e => e.TestTray)
                .WillCascadeOnDelete(false);
        }
    }
}
