using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FunctionFood.Models;
using System;
using System.Collections.Generic;

namespace FunctionFood.Data;

    public partial class FunctionalFoodDatabaseContext : IdentityDbContext
    {
        public FunctionalFoodDatabaseContext()
        {
        }

        public FunctionalFoodDatabaseContext(DbContextOptions<FunctionalFoodDatabaseContext> options)
            : base(options)
        {
        }


        public virtual DbSet<Cart> Carts { get; set; }

        public virtual DbSet<CartItem> CartItems { get; set; }

        public virtual DbSet<Favorite> Favorites { get; set; }

        public virtual DbSet<Gallery> Galleries { get; set; }

        public virtual DbSet<Invoice> Invoices { get; set; }

        public virtual DbSet<InvoiceDetail> InvoiceDetails { get; set; }

        public virtual DbSet<Product> Products { get; set; }



        public virtual DbSet<ContactMessage> ContactMessages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
            => optionsBuilder.UseSqlServer("Server=LAPTOP-CL85TQ3K\\SQLEXPRESS;Database=FunctionalFoodDatabase;User Id=sa;Password=12;TrustServerCertificate=True;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<Cart>(entity =>
            {
                entity.HasKey(e => e.CartId).HasName("PK__Carts__51BCD7971BBD538F");

                entity.HasIndex(e => e.UserId, "IX_Carts_UserID");

                entity.Property(e => e.CartId)
                    .HasMaxLength(50)
                    .HasColumnName("CartID");
                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<CartItem>(entity =>
            {
                entity.HasKey(e => e.CartItemId).HasName("PK__CartItem__488B0B2A758FB707");

                entity.ToTable(tb => tb.HasTrigger("trg_CartItems_Insert"));

                entity.HasIndex(e => e.CartId, "IX_CartItems_CartID");

                entity.HasIndex(e => e.ProductId, "IX_CartItems_ProductID");

                entity.Property(e => e.CartItemId)
                    .HasMaxLength(50)
                    .HasColumnName("CartItemID");
                entity.Property(e => e.CartId)
                    .HasMaxLength(50)
                    .HasColumnName("CartID");
                entity.Property(e => e.ProductId)
                    .HasMaxLength(50)
                    .HasColumnName("ProductID");

                entity.HasOne(d => d.Cart).WithMany(p => p.CartItems)
                    .HasForeignKey(d => d.CartId)
                    .HasConstraintName("FK__CartItems__CartI__4D94879B");

                entity.HasOne(d => d.Product).WithMany(p => p.CartItems)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__CartItems__Produ__4E88ABD4");
            });

            modelBuilder.Entity<Favorite>(entity =>
            {
                entity.HasKey(e => e.FavoriteId).HasName("PK__Favorite__CE74FAF569C8B02A");

                entity.ToTable(tb => tb.HasTrigger("trg_Favorites_Insert"));

                entity.HasIndex(e => e.ProductId, "IX_Favorites_ProductID");

                entity.HasIndex(e => e.UserId, "IX_Favorites_UserID");

                entity.Property(e => e.FavoriteId)
                    .HasMaxLength(50)
                    .HasColumnName("FavoriteID");
                entity.Property(e => e.ProductId)
                    .HasMaxLength(50)
                    .HasColumnName("ProductID");
                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Product).WithMany(p => p.Favorites)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__Favorites__Produ__52593CB8");
            });

            modelBuilder.Entity<Gallery>(entity =>
            {
                entity.HasKey(e => e.GalleryId).HasName("PK__Gallerie__CF4F7B9561E67FC7");

                entity.ToTable(tb => tb.HasTrigger("trg_Galleries_Insert"));

                entity.Property(e => e.GalleryId)
                    .HasMaxLength(50)
                    .HasColumnName("GalleryID");
                entity.Property(e => e.ImageName).HasMaxLength(100);
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.HasKey(e => e.InvoiceId).HasName("PK__Invoices__D796AAD54169D70F");

                entity.ToTable(tb => tb.HasTrigger("trg_Invoices_Insert"));

                entity.HasIndex(e => e.UserId, "IX_Invoices_UserID");

                entity.Property(e => e.InvoiceId)
                    .HasMaxLength(50)
                    .HasColumnName("InvoiceID");
                entity.Property(e => e.CreatedDate)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.TotalAmount).HasColumnType("decimal(10, 2)");
                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<InvoiceDetail>(entity =>
            {
                entity.HasKey(e => e.InvoiceDetailId).HasName("PK__InvoiceD__1F1578F14DC01708");

                entity.ToTable(tb => tb.HasTrigger("trg_InvoiceDetails_Insert"));

                entity.HasIndex(e => e.InvoiceId, "IX_InvoiceDetails_InvoiceID");

                entity.HasIndex(e => e.ProductId, "IX_InvoiceDetails_ProductID");

                entity.Property(e => e.InvoiceDetailId)
                    .HasMaxLength(50)
                    .HasColumnName("InvoiceDetailID");
                entity.Property(e => e.InvoiceId)
                    .HasMaxLength(50)
                    .HasColumnName("InvoiceID");
                entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
                entity.Property(e => e.ProductId)
                    .HasMaxLength(50)
                    .HasColumnName("ProductID");

                entity.HasOne(d => d.Invoice).WithMany(p => p.InvoiceDetails)
                    .HasForeignKey(d => d.InvoiceId)
                    .HasConstraintName("FK__InvoiceDe__Invoi__59063A47");

                entity.HasOne(d => d.Product).WithMany(p => p.InvoiceDetails)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__InvoiceDe__Produ__59FA5E80");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.ProductId).HasName("PK__Products__B40CC6EDB1825F50");

                entity.ToTable(tb => tb.HasTrigger("trg_Products_Insert"));

                entity.Property(e => e.ProductId)
                    .HasMaxLength(50)
                    .HasColumnName("ProductID");

                entity.Property(e => e.Category).HasMaxLength(50);
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.Property(e => e.Image).HasMaxLength(200);
                entity.Property(e => e.ProductName).HasMaxLength(100);
                entity.Property(e => e.Trending).HasDefaultValue(false);
            });
        modelBuilder.Entity<Product>().HasData(
    new Product
    {
        ProductId = "P001",
        ProductName = "Viên uống Vitamin C 500mg",
        Category = "Vitamin, Chống oxy hóa",
        Description = "Hỗ trợ tăng sức đề kháng và chống lão hóa",
        Price = 120000,
        Image = "/img/vitamin-c.jpg",
        Trending = true
    },
    new Product
    {
        ProductId = "P002",
        ProductName = "Omega-3 Fish Oil",
        Category = "Omega, Não bộ",
        Description = "Hỗ trợ trí nhớ và tim mạch",
        Price = 150000,
        Image = "/img/omega3.jpg",
        Trending = false
    },
    new Product
    {
        ProductId = "P003",
        ProductName = "Collagen Beauty Plus",
        Category = "Collagen, Làm đẹp",
        Description = "Giúp da sáng mịn và đàn hồi tốt hơn",
        Price = 220000,
        Image = "/img/collagen.jpg",
        Trending = true
    },
    new Product
    {
        ProductId = "P004",
        ProductName = "Probiotic Digestive Plus",
        Category = "Tiêu hóa, Miễn dịch",
        Description = "Cung cấp lợi khuẩn giúp hỗ trợ tiêu hóa và tăng cường miễn dịch",
        Price = 180000,
        Image = "/img/probiotic.jpg",
        Trending = false
    },
new Product
{
    ProductId = "P005",
    ProductName = "Calcium + Vitamin D3",
    Category = "Xương khớp, Canxi",
    Description = "Giúp chắc xương, hỗ trợ hấp thu canxi hiệu quả",
    Price = 135000,
    Image = "/img/calcium.jpg",
    Trending = true
}

);




        OnModelCreatingPartial(modelBuilder);
            base.OnModelCreating(modelBuilder);

        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
