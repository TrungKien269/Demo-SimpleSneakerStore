using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DemoSneakerStore.Models
{
    public partial class SneakerStoreContext : DbContext
    {
        public SneakerStoreContext(DbContextOptions<SneakerStoreContext> options): base(options) { }

        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<AccountType> AccountType { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Color> Color { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<ExportBill> ExportBill { get; set; }
        public virtual DbSet<Image> Image { get; set; }
        public virtual DbSet<ImportBill> ImportBill { get; set; }
        public virtual DbSet<Maker> Maker { get; set; }
        public virtual DbSet<Origin> Origin { get; set; }
        public virtual DbSet<Price> Price { get; set; }
        public virtual DbSet<Sell> Sell { get; set; }
        public virtual DbSet<Shoes> Shoes { get; set; }
        public virtual DbSet<ShoesColor> ShoesColor { get; set; }
        public virtual DbSet<ShoesSize> ShoesSize { get; set; }
        public virtual DbSet<Size> Size { get; set; }
        public virtual DbSet<Supplier> Supplier { get; set; }
        public virtual DbSet<WareHouse> WareHouse { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Server=localhost;Database=QLSneakerStore;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.Property(e => e.UserName)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Password).IsUnicode(false);

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.TypeNavigation)
                    .WithMany(p => p.Account)
                    .HasForeignKey(d => d.Type)
                    .HasConstraintName("FK_AccType");
            });

            modelBuilder.Entity<AccountType>(entity =>
            {
                entity.Property(e => e.TypeId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<Color>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Username).IsUnicode(false);

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.Customer)
                    .HasForeignKey(d => d.Username)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CusAcc");
            });

            modelBuilder.Entity<ExportBill>(entity =>
            {
                entity.HasKey(e => new { e.ExSerial, e.ShoesId });

                entity.HasOne(d => d.ExSerialNavigation)
                    .WithMany(p => p.ExportBill)
                    .HasForeignKey(d => d.ExSerial)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExportSerial");

                entity.HasOne(d => d.Shoes)
                    .WithMany(p => p.ExportBill)
                    .HasForeignKey(d => d.ShoesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExportShoes");
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.HasKey(e => new { e.ImageId, e.ShoesId });

                entity.HasOne(d => d.Shoes)
                    .WithMany(p => p.Image)
                    .HasForeignKey(d => d.ShoesId)
                    .HasConstraintName("FK_Image");
            });

            modelBuilder.Entity<ImportBill>(entity =>
            {
                entity.HasKey(e => new { e.ImSerial, e.ShoesId });

                entity.HasOne(d => d.ImSerialNavigation)
                    .WithMany(p => p.ImportBill)
                    .HasForeignKey(d => d.ImSerial)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ImportSerial");

                entity.HasOne(d => d.Shoes)
                    .WithMany(p => p.ImportBill)
                    .HasForeignKey(d => d.ShoesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ImportShoes");
            });

            modelBuilder.Entity<Maker>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<Origin>(entity =>
            {
                entity.Property(e => e.CountryId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Price>(entity =>
            {
                entity.HasKey(e => new { e.ShoesId, e.ShoesPrice });

                entity.HasOne(d => d.Shoes)
                    .WithMany(p => p.Price)
                    .HasForeignKey(d => d.ShoesId)
                    .HasConstraintName("FK_Price");
            });

            modelBuilder.Entity<Sell>(entity =>
            {
                entity.Property(e => e.UserName).IsUnicode(false);

                entity.HasOne(d => d.UserNameNavigation)
                    .WithMany(p => p.Sell)
                    .HasForeignKey(d => d.UserName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User");
            });

            modelBuilder.Entity<Shoes>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Cate)
                    .WithMany(p => p.Shoes)
                    .HasForeignKey(d => d.CateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cate");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Shoes)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Origin");

                entity.HasOne(d => d.Maker)
                    .WithMany(p => p.Shoes)
                    .HasForeignKey(d => d.MakerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Maker");
            });

            modelBuilder.Entity<ShoesColor>(entity =>
            {
                entity.HasKey(e => new { e.ShoesId, e.ColorId });

                entity.HasOne(d => d.Color)
                    .WithMany(p => p.ShoesColor)
                    .HasForeignKey(d => d.ColorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SColorShoes");

                entity.HasOne(d => d.Shoes)
                    .WithMany(p => p.ShoesColor)
                    .HasForeignKey(d => d.ShoesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShoesColor");
            });

            modelBuilder.Entity<ShoesSize>(entity =>
            {
                entity.HasKey(e => new { e.ShoesId, e.Vnsize });

                entity.HasOne(d => d.Shoes)
                    .WithMany(p => p.ShoesSize)
                    .HasForeignKey(d => d.ShoesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShoesSize");

                entity.HasOne(d => d.VnsizeNavigation)
                    .WithMany(p => p.ShoesSize)
                    .HasForeignKey(d => d.Vnsize)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SizeShoes");
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<WareHouse>(entity =>
            {
                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.WareHouse)
                    .HasForeignKey(d => d.SupplierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Supply");
            });
        }
    }
}
