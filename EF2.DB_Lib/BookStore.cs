using System.IO;
using EF2.Model;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace EF2.DB_Lib
{
    public partial class BookStore : DbContext
    {
        public BookStore()
        {
        }

        public BookStore(DbContextOptions<BookStore> options)
            : base(options)
        {
        }

        public virtual DbSet<Author> TabAuthors { get; set; }
        public virtual DbSet<Book> TabBooks { get; set; }
        public virtual DbSet<Edition> TabEditions { get; set; }
        public virtual DbSet<Genre> TabGenres { get; set; }
        public virtual DbSet<BookPrice> TabPrices { get; set; }
        public virtual DbSet<PublishingHouse> TabPublishingHouses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL(GetConnectionString("connection_to_db.txt"));
            }
        }
        
        private static string GetConnectionString(string path)
        {
            return File.ReadAllText(path);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>(entity =>
            {
                entity.ToTable("tab_authors");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("first_name");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(75)
                    .HasColumnName("last_name");
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("tab_books");

                entity.HasIndex(e => e.IdAuthor, "id_author");

                entity.HasIndex(e => e.IdGenre, "id_genre");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.IdAuthor)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_author");

                entity.Property(e => e.IdGenre)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_genre");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("title");

                entity.Property(e => e.YearOfCreation)
                    .HasColumnType("year(4)")
                    .HasColumnName("year_of_creation");

                entity.HasOne(d => d.IdAuthorNavigation)
                    .WithMany(p => p.TabBooks)
                    .HasForeignKey(d => d.IdAuthor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tab_books_ibfk_1");

                entity.HasOne(d => d.IdGenreNavigation)
                    .WithMany(p => p.TabBooks)
                    .HasForeignKey(d => d.IdGenre)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tab_books_ibfk_2");
            });

            modelBuilder.Entity<Edition>(entity =>
            {
                entity.ToTable("tab_editions");

                entity.HasIndex(e => e.IdBook, "id_book");

                entity.HasIndex(e => e.IdPublishingHouse, "id_publishing_house");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.IdBook)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_book");

                entity.Property(e => e.IdPublishingHouse)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_publishing_house");

                entity.Property(e => e.NumberOfPages)
                    .HasColumnType("int(11)")
                    .HasColumnName("number_of_pages");

                entity.Property(e => e.YearOfPublishing)
                    .HasColumnType("year(4)")
                    .HasColumnName("year_of_publishing");

                entity.HasOne(d => d.IdBookNavigation)
                    .WithMany(p => p.TabEditions)
                    .HasForeignKey(d => d.IdBook)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tab_editions_ibfk_1");

                entity.HasOne(d => d.IdPublishingHouseNavigation)
                    .WithMany(p => p.TabEditions)
                    .HasForeignKey(d => d.IdPublishingHouse)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tab_editions_ibfk_2");
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.ToTable("tab_genres");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<BookPrice>(entity =>
            {
                entity.ToTable("tab_prices");

                entity.HasIndex(e => e.IdEdition, "id_edition");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Cost).HasColumnName("cost");

                entity.Property(e => e.IdEdition)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_edition");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.HasOne(d => d.IdEditionNavigation)
                    .WithMany(p => p.TabPrices)
                    .HasForeignKey(d => d.IdEdition)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tab_prices_ibfk_1");
            });

            modelBuilder.Entity<PublishingHouse>(entity =>
            {
                entity.ToTable("tab_publishing_houses");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
