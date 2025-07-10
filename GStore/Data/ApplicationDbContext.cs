using GStore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Hosting;
using System.Xml;

namespace GStore.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Level1Set> Level1Sets { get; set; }

        public DbSet<Level2Set> Level2Sets { get; set; }

        public DbSet<ColorSet> ColorSets { get; set; }

        public DbSet<SizeSet> SizeSets { get; set; }

        public DbSet<Shirt> Shirts { get; set; }

        public DbSet<ShirtSizeSet> ShirtSizeSets { get; set; }

        public DbSet<ShirtColorSet> ShirtColorSets { get; set; }

        public DbSet<ShirtAvailability> ShirtAvailabilitys { get; set; }

        public DbSet<CartItem> ShoppingCartItems { get; set; }  //

        public DbSet<ProductExtraImage> ProductExtraImages { get; set; }

        public DbSet<spShirtPreviewModel> spShirtPreview { get; set; }

        public DbSet<spShirtShortWithCategoryName> spShirtPrevWithCatName { get; set; }

        public DbSet<spShirtShortWithCategoryNameById> spShirtPrevWithCatNameById { get; set; }

        public DbSet<spShirtFullShopPreviewModel> spShirtFullShopPreview { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Shirt>()
                .HasMany(e => e.SizeSets)
                .WithMany(e => e.Products)
                .UsingEntity<ShirtSizeSet>();

            modelBuilder.Entity<ShirtSizeSet>()
                .HasKey(p => new { p.ProductId, p.SizeSetId });

            modelBuilder.Entity<Shirt>()
                .HasMany(e => e.ColorSets)
                .WithMany(e => e.Products)
                .UsingEntity<ShirtColorSet>();

            modelBuilder.Entity<ShirtColorSet>()
                .HasKey(c => new { c.ProductId, c.ColorSetId });

            modelBuilder.Entity<ShirtAvailability>()
    .HasKey(s => new { s.ProductId, s.SizeSetId, s.ColorSetId });

            modelBuilder.Entity<ColorSet>().Property<bool>("IsActive").HasDefaultValue(true);

            modelBuilder.Entity<SizeSet>().Property<bool>("IsActive").HasDefaultValue(true);

            modelBuilder.Entity<Shirt>().Property<bool>("IsActive").HasDefaultValue(false);

            modelBuilder.Entity<Shirt>().Property<bool>("IsAvailable").HasDefaultValue(true);

            modelBuilder.Entity<Level1Set>().Property<bool>("IsActive").HasDefaultValue(true);

            modelBuilder.Entity<Level2Set>().Property<bool>("IsActive").HasDefaultValue(true);

            //migrationBuilder.RenameTable("Old", null, "New");

            modelBuilder.Entity<Shirt>().ToTable("Shirts");

            modelBuilder.Entity<ShirtSizeSet>().ToTable("ShirtsSizeSets");

            modelBuilder.Entity<ShirtColorSet>().ToTable("ShirtsColorSets");

            //modelBuilder.Entity<vwShirtShort>()
            //.HasNoKey()
            //.ToView(nameof(VW_ShirtSqlShortView));

            modelBuilder.Entity<spShirtPreviewModel>(entity =>
            {
                entity.HasNoKey().ToTable(nameof(spShirtPreviewModel), t => t.ExcludeFromMigrations());
                entity.HasNoKey().ToView(null);
            });

            modelBuilder.Entity<spShirtShortWithCategoryName>(entity =>
            {
                entity.HasNoKey().ToTable(nameof(spShirtShortWithCategoryName), t => t.ExcludeFromMigrations());
                entity.HasNoKey().ToView(null);
            });

            modelBuilder.Entity<spShirtShortWithCategoryNameById>(entity =>
            {
                entity.HasNoKey().ToTable(nameof(spShirtShortWithCategoryNameById), t => t.ExcludeFromMigrations());
                entity.HasNoKey().ToView(null);
            });
            
            modelBuilder.Entity<spColorsForImageUploads>(entity =>
            {
                entity.HasNoKey().ToTable(nameof(spColorsForImageUploads), t => t.ExcludeFromMigrations());
                entity.HasNoKey().ToView(null);
            });

            modelBuilder.Entity<spShirtFullShopPreviewModel>(entity =>
            {
                entity.HasNoKey().ToTable(nameof(spShirtFullShopPreviewModel), t => t.ExcludeFromMigrations());
                entity.HasNoKey().ToView(null);
            });

            base.OnModelCreating(modelBuilder);

        }
    }
}