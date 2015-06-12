using AP.PD.Domain;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace AP.PD.Web.ResourceServer.API.Repository
{
    public class ResourceContext : DbContext
    {
        public ResourceContext()
            : base("ResourceContext")
        {
        }

        public DbSet<ParcelDeliveryDomain> ParcelDeliveries { get; set; }

        public DbSet<ParcelOrderDomain> ParcelOrders { get; set; }

        public DbSet<UserDomain> Users { get; set; }

        public DbSet<CategoryDomain> Categories { get; set; }

        public DbSet<RoleDomain> Roles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();


            modelBuilder.Entity<RoleDomain>().ToTable("Role");
            modelBuilder.Entity<RoleDomain>().Property(t => t.Id).HasColumnName("Id");
            modelBuilder.Entity<RoleDomain>().Property(t => t.Name).HasColumnName("Name");
            modelBuilder.Entity<RoleDomain>().Property(t => t.Description).HasColumnName("Description");

            modelBuilder.Entity<UserDomain>().ToTable("User");
            modelBuilder.Entity<UserDomain>().Property(t => t.Id).HasColumnName("Id");
            modelBuilder.Entity<UserDomain>().Property(t => t.LoginId).HasColumnName("LoginId");
            modelBuilder.Entity<UserDomain>().Property(t => t.Password).HasColumnName("Password");
            modelBuilder.Entity<UserDomain>().Property(t => t.RoleId).HasColumnName("RoleId");

            modelBuilder.Entity<CategoryDomain>().ToTable("Category");
            modelBuilder.Entity<CategoryDomain>().Property(t => t.Id).HasColumnName("Id");
            modelBuilder.Entity<CategoryDomain>().Property(t => t.Name).HasColumnName("Name");
            modelBuilder.Entity<CategoryDomain>().Property(t => t.Description).HasColumnName("Description");

            modelBuilder.Entity<ParcelDeliveryDomain>().ToTable("ParcelDelivery");
            modelBuilder.Entity<ParcelDeliveryDomain>().Property(t => t.Id).HasColumnName("Id");
            modelBuilder.Entity<ParcelDeliveryDomain>().Property(t => t.DeliveredDate).HasColumnName("DeliveredDate");
            modelBuilder.Entity<ParcelDeliveryDomain>().Property(t => t.DeliveredQuantity).HasColumnName("DeliveredQuantity");
            modelBuilder.Entity<ParcelDeliveryDomain>().HasRequired(t => t.RepUser);
            modelBuilder.Entity<ParcelDeliveryDomain>().Property(t => t.RepUserId).HasColumnName("RepId");
            modelBuilder.Entity<ParcelDeliveryDomain>().HasRequired(t => t.Category);
            modelBuilder.Entity<ParcelDeliveryDomain>().Property(t => t.CategoryId).HasColumnName("CategoryId");

            modelBuilder.Entity<ParcelOrderDomain>().ToTable("ParcelOrder");
            modelBuilder.Entity<ParcelOrderDomain>().Property(t => t.Id).HasColumnName("Id");
            modelBuilder.Entity<ParcelOrderDomain>().Property(t => t.RepId).HasColumnName("RepId");
            modelBuilder.Entity<ParcelOrderDomain>().Property(t => t.OrderDate).HasColumnName("OrderDate");
            modelBuilder.Entity<ParcelOrderDomain>().Property(t => t.TotalQuantity).HasColumnName("TotalQuantity");
        }
    }
}