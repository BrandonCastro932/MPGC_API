using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace MPGC_API.Models
{
    public partial class MPGCContext : DbContext
    {
        public MPGCContext()
        {
        }

        public MPGCContext(DbContextOptions<MPGCContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<GameMovie> GameMovies { get; set; }
        public virtual DbSet<GamePlatform> GamePlatforms { get; set; }
        public virtual DbSet<GameScreenshot> GameScreenshots { get; set; }
        public virtual DbSet<GameState> GameStates { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Platform> Platforms { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserGame> UserGames { get; set; }
        public virtual DbSet<UserStatus> UserStatuses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("SERVER=.;DATABASE=MPGC;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<Game>(entity =>
            {
                entity.HasKey(e => e.Idgame)
                    .HasName("PK__Game__D0B765D568439989");

                entity.ToTable("Game");

                entity.Property(e => e.Idgame)
                    .ValueGeneratedNever()
                    .HasColumnName("IDGame");

                entity.Property(e => e.BackgroundImage)
                    .IsRequired()
                    .HasMaxLength(800)
                    .IsUnicode(false)
                    .HasColumnName("Background_image");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(8000)
                    .IsUnicode(false);

                entity.Property(e => e.Idgenre).HasColumnName("IDGenre");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Playtime).HasColumnType("decimal(19, 1)");

                entity.Property(e => e.Rating).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.Released).HasColumnType("date");

                entity.Property(e => e.UrlMusicTheme)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdgenreNavigation)
                    .WithMany(p => p.Games)
                    .HasForeignKey(d => d.Idgenre)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKGame392858");
            });

            modelBuilder.Entity<GameMovie>(entity =>
            {
                entity.HasKey(e => e.IdgameMovie);

                entity.HasIndex(e => e.IdgameMovie, "IX_GameMovies")
                    .IsUnique();

                entity.Property(e => e.IdgameMovie).HasColumnName("IDGameMovie");

                entity.Property(e => e.Idgame).HasColumnName("IDGame");

                entity.Property(e => e.Urlembed)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("URLembed");
            });

            modelBuilder.Entity<GamePlatform>(entity =>
            {
                entity.HasKey(e => e.IdgamePlatform)
                    .HasName("PK__GamePlat__21E0B0DDD4DC1FBA");

                entity.ToTable("GamePlatform");

                entity.Property(e => e.IdgamePlatform).HasColumnName("IDGamePlatform");

                entity.Property(e => e.GameIdgame).HasColumnName("GameIDGame");

                entity.Property(e => e.PlatformsIdplatform).HasColumnName("PlatformsIDPlatform");

                entity.HasOne(d => d.GameIdgameNavigation)
                    .WithMany(p => p.GamePlatforms)
                    .HasForeignKey(d => d.GameIdgame)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKGamePlatfo268730");

                entity.HasOne(d => d.PlatformsIdplatformNavigation)
                    .WithMany(p => p.GamePlatforms)
                    .HasForeignKey(d => d.PlatformsIdplatform)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKGamePlatfo596607");
            });

            modelBuilder.Entity<GameScreenshot>(entity =>
            {
                entity.HasKey(e => e.Idscreenshot)
                    .HasName("PK__GameScre__58A43F3002F03EB9");

                entity.Property(e => e.Idscreenshot).HasColumnName("IDScreenshot");

                entity.Property(e => e.Idgame).HasColumnName("IDGame");

                entity.Property(e => e.Sslink)
                    .IsRequired()
                    .HasMaxLength(800)
                    .IsUnicode(false)
                    .HasColumnName("SSLink");

                entity.HasOne(d => d.IdgameNavigation)
                    .WithMany(p => p.GameScreenshots)
                    .HasForeignKey(d => d.Idgame)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKGameScreen747857");
            });

            modelBuilder.Entity<GameState>(entity =>
            {
                entity.HasKey(e => e.IdgameState)
                    .HasName("PK__GameStat__9525A553EBC7C3C0");

                entity.ToTable("GameState");

                entity.Property(e => e.IdgameState)
                    .ValueGeneratedNever()
                    .HasColumnName("IDGameState");

                entity.Property(e => e.StateName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.HasKey(e => e.Idgenre)
                    .HasName("PK__Genre__23FDA2F0D27245A2");

                entity.ToTable("Genre");

                entity.HasIndex(e => e.NameGenre, "UQ__Genre__ED75A170CBB74C8D")
                    .IsUnique();

                entity.Property(e => e.Idgenre)
                    .ValueGeneratedNever()
                    .HasColumnName("IDGenre");

                entity.Property(e => e.NameGenre)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Platform>(entity =>
            {
                entity.HasKey(e => e.Idplatform)
                    .HasName("PK__Platform__5C439843FC4D30E5");

                entity.HasIndex(e => e.Platform1, "UQ__Platform__130CCEA24A888C08")
                    .IsUnique();

                entity.Property(e => e.Idplatform)
                    .ValueGeneratedNever()
                    .HasColumnName("IDPlatform");

                entity.Property(e => e.Logo)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Platform1)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("Platform");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Iduser)
                    .HasName("PK__User__EAE6D9DFD403405F");

                entity.ToTable("User");

                entity.HasIndex(e => e.Username, "UQ__User__536C85E4815AAEEA")
                    .IsUnique();

                entity.HasIndex(e => e.Phone, "UQ__User__5C7E359EB93204C9")
                    .IsUnique();

                entity.HasIndex(e => e.Email, "UQ__User__A9D10534A147EA74")
                    .IsUnique();

                entity.Property(e => e.Iduser)
                    .ValueGeneratedNever()
                    .HasColumnName("IDUser");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.IduserStatus).HasColumnName("IDUserStatus");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(800)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.IduserStatusNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.IduserStatus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKUser4105");
            });

            modelBuilder.Entity<UserGame>(entity =>
            {
                entity.HasKey(e => e.IduserGame)
                    .HasName("PK__UserGame__37C637DF5531CABF");

                entity.ToTable("UserGame");

                entity.Property(e => e.IduserGame)
                    .ValueGeneratedNever()
                    .HasColumnName("IDUserGame");

                entity.Property(e => e.Idgame).HasColumnName("IDGame");

                entity.Property(e => e.IdgameState).HasColumnName("IDGameState");

                entity.Property(e => e.Iduser).HasColumnName("IDUser");

                entity.HasOne(d => d.IdgameNavigation)
                    .WithMany(p => p.UserGames)
                    .HasForeignKey(d => d.Idgame)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKUserGame108176");

                entity.HasOne(d => d.IdgameStateNavigation)
                    .WithMany(p => p.UserGames)
                    .HasForeignKey(d => d.IdgameState)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKUserGame995523");

                entity.HasOne(d => d.IduserNavigation)
                    .WithMany(p => p.UserGames)
                    .HasForeignKey(d => d.Iduser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKUserGame805763");
            });

            modelBuilder.Entity<UserStatus>(entity =>
            {
                entity.HasKey(e => e.IduserStatus)
                    .HasName("PK__UserStat__BDCCE0F9AF94F177");

                entity.ToTable("UserStatus");

                entity.HasIndex(e => e.Status, "UQ__UserStat__3A15923FB51DE870")
                    .IsUnique();

                entity.Property(e => e.IduserStatus)
                    .ValueGeneratedNever()
                    .HasColumnName("IDUserStatus");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
