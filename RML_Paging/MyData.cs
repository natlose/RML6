using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RML_Paging
{
    class MyData : DbContext
    {
        public MyData() : base ("Server=.;Database=RMLabor6;Trusted_Connection=True;MultipleActiveResultSets=true;") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Product
            modelBuilder.Entity<Product>().HasKey(p => p.Id); //ez egyébként a default
            modelBuilder.Entity<Product>().Property(p => p.SKU)
                .IsRequired()
                .HasMaxLength(30);
            modelBuilder.Entity<Product>().Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode();
            modelBuilder.Entity<Product>().Property(p => p.Unit)
                .IsRequired()
                .HasMaxLength(5);
            modelBuilder.Entity<Product>()
                .HasMany<Stock>(p => p.Stocks)
                .WithRequired(s => s.Product)
                .HasForeignKey(s => s.FiProduct);
            #endregion

            #region Stock
            modelBuilder.Entity<Stock>().Property(s => s.Shelf)
                .IsRequired()
                .HasMaxLength(3);
            #endregion

            #region Service
            modelBuilder.Entity<Service>().Property(s => s.Nr)
                .IsRequired()
                .HasMaxLength(10);
            modelBuilder.Entity<Service>().Property(s => s.Description)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode();
            modelBuilder.Entity<Service>().Property(s => s.Unit)
                .IsRequired()
                .HasMaxLength(5);
            #endregion
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Stock> Stocks { get; set; }

        public DbSet<Service> Services { get; set; }

    }
}
