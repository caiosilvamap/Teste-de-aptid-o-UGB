using Microsoft.EntityFrameworkCore;
using SolicitacaoDeMateriais.Models;
using System.Reflection.Metadata;

namespace SolicitacaoDeMateriais.Infra.Data
{
    public partial class DataContext : DbContext
    {
        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer("Data Source=PC; User Id=sa; Password=2246;Initial Catalog=SolicitacaoDeMateriaisDB; TrustServerCertificate=True;", options => options.EnableRetryOnFailure()).UseLazyLoadingProxies().EnableDetailedErrors();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("User_Pk");

                entity.ToTable("User");

                entity.Property(e => e.DepartmentId).HasColumnName("DepartmentId");

                entity.Property(e => e.Name)
                        .HasMaxLength(200)
                        .IsUnicode(false);

                entity.Property(e => e.Active);

                entity.HasOne(d => d.DepartmentObj).WithMany(p => p.Users)
                        .HasForeignKey(d => d.DepartmentId)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("DepartmentId_Fk");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("Department_Pk");

                entity.ToTable("Department");

                entity.Property(e => e.Name)
                        .HasMaxLength(200)
                        .IsUnicode(false);

                entity.Property(e => e.Active);

            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("Product_Pk");

                entity.ToTable("Product");

                entity.Property(e => e.SupplierId).HasColumnName("SupplierId");

                entity.Property(e => e.Name)
                        .HasMaxLength(200)
                        .IsUnicode(false);

                entity.Property(e => e.EANcode)
                .HasMaxLength(500)
                .IsUnicode(false);

                entity.HasOne(d => d.SupplierObj).WithMany(p => p.Products)
                        .HasForeignKey(d => d.SupplierId)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Supplier_Product_Fk");
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("Supplier_Pk");

                entity.ToTable("Supplier");

                entity.Property(e => e.Name)
                        .HasMaxLength(200)
                        .IsUnicode(false);

                entity.Property(e => e.Address)
                .HasMaxLength(200)
                .IsUnicode(false);

                entity.Property(e => e.Email)
               .HasMaxLength(200)
               .IsUnicode(false);

                entity.Property(e => e.CNPJ)
               .HasMaxLength(200)
               .IsUnicode(false);

                entity.Property(e => e.StateRegistration)
               .HasMaxLength(200)
               .IsUnicode(false);

                entity.Property(e => e.CountyRegistration)
               .HasMaxLength(200)
               .IsUnicode(false);

            });


            modelBuilder.Entity<Service>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("Service_Pk");

                entity.ToTable("Service");

                entity.Property(e => e.SupplierId).HasColumnName("SupplierId");

                entity.Property(e => e.Name)
                        .HasMaxLength(200)
                        .IsUnicode(false);

                entity.Property(e => e.Description)
                        .HasMaxLength(200)
                        .IsUnicode(false);

                entity.Property(e => e.Deadline);
                 
                entity.Property(e => e.Description)
                .HasMaxLength(500)
                .IsUnicode(false);

                entity.HasOne(d => d.SupplierObj).WithMany(p => p.Services)
                        .HasForeignKey(d => d.SupplierId)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Supplier_Fk");
            });

            modelBuilder.Entity<RequestProductService>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("Request_Product_Service_Pk");

                entity.ToTable("Request_Product_Service");

                entity.Property(e => e.ProductId).HasColumnName("ProductId");

                entity.Property(e => e.UserId).HasColumnName("UserId");

                entity.Property(e => e.ServiceId).HasColumnName("ServiceId");

                entity.Property(e => e.DepartmentId).HasColumnName("DepartmentId");

                entity.Property(e => e.RequestCode)
                        .HasMaxLength(200)
                        .IsUnicode(false);

                entity.Property(e => e.Observation)
                .HasMaxLength(500)
                .IsUnicode(false);

                entity.HasOne(d => d.ServiceObj).WithMany(p => p.Requests)
                        .HasForeignKey(d => d.ServiceId)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Service_Fk");

                entity.HasOne(d => d.UserObj).WithMany(p => p.Requests)
                         .HasForeignKey(d => d.UserId)
                         .OnDelete(DeleteBehavior.ClientSetNull)
                         .HasConstraintName("User_Fk");

                entity.HasOne(d => d.ProductObj).WithMany(p => p.Requests)
                        .HasForeignKey(d => d.ProductId)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Product_Fk");

                entity.HasOne(d => d.DepartmentObj).WithMany(p => p.Requests)
                       .HasForeignKey(d => d.DepartmentId)
                       .OnDelete(DeleteBehavior.ClientSetNull)
                       .HasConstraintName("Department_Fk");
            });

            modelBuilder.Entity<StockExit>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("StockExit_Pk");

                entity.ToTable("StockExit");

                entity.Property(e => e.ProductId).HasColumnName("ProductId");

                entity.Property(e => e.UserId).HasColumnName("UserId");

                entity.Property(e => e.DepartmentId).HasColumnName("DepartmentId");

                entity.HasOne(d => d.UserObj).WithMany(p => p.StockExits)
                         .HasForeignKey(d => d.UserId)
                         .OnDelete(DeleteBehavior.ClientSetNull)
                         .HasConstraintName("User_StockExity_Fk");

                entity.HasOne(d => d.ProductObj).WithMany(p => p.StockExits)
                        .HasForeignKey(d => d.ProductId)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Product_StockExity_Fk");

                entity.HasOne(d => d.DepartmentObj).WithMany(p => p.StockExits)
                       .HasForeignKey(d => d.DepartmentId)
                       .OnDelete(DeleteBehavior.ClientSetNull)
                       .HasConstraintName("Department_StockExity_Fk");
            });

            modelBuilder.Entity<StockEntry>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("StockEntry_Pk");

                entity.ToTable("StockEntry");

                entity.Property(e => e.ProductId).HasColumnName("ProductId");

                entity.HasOne(d => d.ProductObj).WithMany(p => p.StockEntrys)
                        .HasForeignKey(d => d.ProductId)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Product_StockEntry_Fk");
            });

            modelBuilder.Entity<Stock>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("Stock_Pk");

                entity.ToTable("Stock");

                entity.Property(e => e.ProductId).HasColumnName("ProductId");

                entity.HasOne(d => d.ProductObj).WithMany(p => p.Stocks)
                        .HasForeignKey(d => d.ProductId)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Product_Stock_Fk");

            });

            OnModelCreatingPartial(modelBuilder);
        }


        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
