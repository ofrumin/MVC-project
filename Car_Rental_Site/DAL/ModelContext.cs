using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;



namespace DAL
{
    public class ModelContext : DbContext
    {
        public ModelContext()
            : base("name=ModelContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ModelContext,DAL.Migrations.Configuration>("ModelContext"));
        }

        public virtual DbSet<Branch> Branches { get; set; }
        public virtual DbSet<LeasedVehicle> LeasedVehicles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<VehiclesFleet> VehiclesFleets { get; set; }
        public virtual DbSet<VehiclesType> VehiclesTypes { get; set; }
        public virtual DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
        }
    }
}
