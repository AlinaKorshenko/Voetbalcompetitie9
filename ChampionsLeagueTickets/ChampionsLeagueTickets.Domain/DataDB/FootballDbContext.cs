using ChampionsLeagueTickets.Domain.EntitiesDB;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace ChampionsLeagueTickets.Domain.DataDB;

public partial class FootballDbContext : DbContext
{
    public FootballDbContext()
    {
    }

    public FootballDbContext(DbContextOptions<FootballDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Abonnementen> Abonnementens { get; set; }

    public virtual DbSet<AbonnementenPrij> AbonnementenPrijs { get; set; }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<Match> Matches { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Orderlijnen> Orderlijnens { get; set; }

    public virtual DbSet<Seizoenen> Seizoenens { get; set; }

    public virtual DbSet<Stadion> Stadions { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<TicketsPrij> TicketsPrijs { get; set; }

    public virtual DbSet<VakType> VakTypes { get; set; }

    public virtual DbSet<Zitplaatsen> Zitplaatsens { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                  .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                  .AddJsonFile("appsettings.json")
                  .Build();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Latin1_General_CI_AS");

        modelBuilder.Entity<Abonnementen>(entity =>
        {
            entity.HasKey(e => new { e.AbonnementId, e.StadionId }).HasName("PK__Abonneme__CF3D39B38C8AC8F7");

            entity.ToTable("Abonnementen");

            entity.Property(e => e.AbonnementId)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("abonnementID");
            entity.Property(e => e.StadionId)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("stadionID");
            entity.Property(e => e.EindDatum).HasColumnName("eindDatum");
            entity.Property(e => e.SeizoenId)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("seizoenID");
            entity.Property(e => e.StartDatum).HasColumnName("startDatum");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.UserId)
                .HasMaxLength(450)
                .HasColumnName("userID");
            entity.Property(e => e.ZitplaatsId)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("zitplaatsID");

            entity.HasOne(d => d.Seizoen).WithMany(p => p.Abonnementens)
                .HasForeignKey(d => d.SeizoenId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_seizoenID_Abonnementen");

            entity.HasOne(d => d.Stadion).WithMany(p => p.Abonnementens)
                .HasForeignKey(d => d.StadionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_stadionID_Abonnementen");

            entity.HasOne(d => d.User).WithMany(p => p.Abonnementens)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Abonnementen_AspNetUsers");

            entity.HasOne(d => d.Zitplaatsen).WithMany(p => p.Abonnementens)
                .HasForeignKey(d => new { d.StadionId, d.ZitplaatsId })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_zitplaatsID_Abonnementen");
        });

        modelBuilder.Entity<AbonnementenPrij>(entity =>
        {
            entity.HasKey(e => new { e.StadionId, e.SeizoenId }).HasName("PK__Abonneme__FAA00C928D8B5A8E");

            entity.Property(e => e.StadionId)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("stadionID");
            entity.Property(e => e.SeizoenId)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("seizoenID");
            entity.Property(e => e.Prijs)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("prijs");

            entity.HasOne(d => d.Seizoen).WithMany(p => p.AbonnementenPrijs)
                .HasForeignKey(d => d.SeizoenId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_seizoenIDAbonnementenPrijs");

            entity.HasOne(d => d.Stadion).WithMany(p => p.AbonnementenPrijs)
                .HasForeignKey(d => d.StadionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_stadionIDAbonnementenPrijs");
        });

        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedName] IS NOT NULL)");

            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedUserName] IS NOT NULL)");

            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRole",
                    r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("AspNetUserRoles");
                        j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                    });
        });

        modelBuilder.Entity<Match>(entity =>
        {
            entity.HasKey(e => e.MatchId).HasName("PK__Matches__02C72A2D6D040317");

            entity.Property(e => e.MatchId)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("matchID");
            entity.Property(e => e.BezoekendTeamId)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("bezoekendTeamID");
            entity.Property(e => e.DatumTijdStartMatch)
                .HasColumnType("datetime")
                .HasColumnName("datumTijdStartMatch");
            entity.Property(e => e.ThuisTeamId)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("thuisTeamID");

            entity.HasOne(d => d.BezoekendTeam).WithMany(p => p.MatchBezoekendTeams)
                .HasForeignKey(d => d.BezoekendTeamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_bezoekendTeamID");

            entity.HasOne(d => d.ThuisTeam).WithMany(p => p.MatchThuisTeams)
                .HasForeignKey(d => d.ThuisTeamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_thuisTeamID");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__0809337D408CD464");

            entity.Property(e => e.OrderId)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("orderID");
            entity.Property(e => e.DatumTijdOrder)
                .HasColumnType("datetime")
                .HasColumnName("datumTijdOrder");
            entity.Property(e => e.Status)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.UserId)
                .HasMaxLength(450)
                .HasColumnName("userID");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Orders_AspNetUsers");
        });

        modelBuilder.Entity<Orderlijnen>(entity =>
        {
            entity.HasKey(e => new { e.OrderId, e.OrderLijnNummer }).HasName("PK__Orderlij__BEA8F4E8FF672866");

            entity.ToTable("Orderlijnen");

            entity.Property(e => e.OrderId)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("orderID");
            entity.Property(e => e.OrderLijnNummer).HasColumnName("orderLijnNummer");
            entity.Property(e => e.AbonnementId)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("abonnementID");
            entity.Property(e => e.Bedrag)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("bedrag");
            entity.Property(e => e.MatchId)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("matchID");
            entity.Property(e => e.StadionId)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("stadionID");
            entity.Property(e => e.TicketId)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("ticketID");

            entity.HasOne(d => d.Order).WithMany(p => p.Orderlijnens)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_orderID_Orderlijnen");

            entity.HasOne(d => d.Abonnementen).WithMany(p => p.Orderlijnens)
                .HasForeignKey(d => new { d.AbonnementId, d.StadionId })
                .HasConstraintName("fk_abonnement_Orderlijnen");

            entity.HasOne(d => d.Ticket).WithMany(p => p.Orderlijnens)
                .HasForeignKey(d => new { d.TicketId, d.MatchId })
                .HasConstraintName("fk_ticket_Orderlijnen");
        });

        modelBuilder.Entity<Seizoenen>(entity =>
        {
            entity.HasKey(e => e.SeizoenId).HasName("PK__Seizoene__C0EC0D7B16A4F77F");

            entity.ToTable("Seizoenen");

            entity.Property(e => e.SeizoenId)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("seizoenID");
            entity.Property(e => e.EindDatum).HasColumnName("eindDatum");
            entity.Property(e => e.Naam)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("naam");
            entity.Property(e => e.StartDatum).HasColumnName("startDatum");
        });

        modelBuilder.Entity<Stadion>(entity =>
        {
            entity.HasKey(e => e.StadionId).HasName("PK__Stadions__56AECC45BAFDAEA9");

            entity.Property(e => e.StadionId)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("stadionID");
            entity.Property(e => e.Adres)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("adres");
            entity.Property(e => e.Gemeente)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("gemeente");
            entity.Property(e => e.Land)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("land");
            entity.Property(e => e.Naam)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("naam");
            entity.Property(e => e.Postcode)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("postcode");
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasKey(e => e.TeamId).HasName("PK__Teams__5ED7534A07296CA1");

            entity.Property(e => e.TeamId)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("teamID");
            entity.Property(e => e.Naam)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("naam");
            entity.Property(e => e.StadionId)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("stadionID");

            entity.HasOne(d => d.Stadion).WithMany(p => p.Teams)
                .HasForeignKey(d => d.StadionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_StadionID_Teams");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => new { e.TicketId, e.MatchId }).HasName("PK__Tickets__F31FB4D279B38C40");

            entity.Property(e => e.TicketId)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("ticketID");
            entity.Property(e => e.MatchId)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("matchID");
            entity.Property(e => e.Prijs)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("prijs");
            entity.Property(e => e.StadionId)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("stadionID");
            entity.Property(e => e.ZitplaatsId)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("zitplaatsID");

            entity.HasOne(d => d.Match).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.MatchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_matchID_Tickets");

            entity.HasOne(d => d.Stadion).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.StadionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_stadionID_Tickets");

            entity.HasOne(d => d.Zitplaatsen).WithMany(p => p.Tickets)
                .HasForeignKey(d => new { d.StadionId, d.ZitplaatsId })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_zitplaatsID_Tickets");
        });

        modelBuilder.Entity<TicketsPrij>(entity =>
        {
            entity.HasKey(e => new { e.MatchId, e.VakNummer }).HasName("PK__TicketsP__B97B8E44CB171748");

            entity.Property(e => e.MatchId)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("matchID");
            entity.Property(e => e.VakNummer)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("vakNummer");
            entity.Property(e => e.Prijs)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("prijs");

            entity.HasOne(d => d.Match).WithMany(p => p.TicketsPrijs)
                .HasForeignKey(d => d.MatchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_matchIDTicketsPrijs");

            entity.HasOne(d => d.VakNummerNavigation).WithMany(p => p.TicketsPrijs)
                .HasForeignKey(d => d.VakNummer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_vakNummerTicketsPrijs");
        });

        modelBuilder.Entity<VakType>(entity =>
        {
            entity.HasKey(e => e.VakNummer).HasName("PK__VakTypes__BBCA469A4C59D109");

            entity.Property(e => e.VakNummer)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("vakNummer");
            entity.Property(e => e.Omschrijving)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("omschrijving");
            entity.Property(e => e.Ring).HasColumnName("ring");
        });

        modelBuilder.Entity<Zitplaatsen>(entity =>
        {
            entity.HasKey(e => new { e.StadionId, e.ZitplaatsId }).HasName("PK__Zitplaat__76B42E8743521A35");

            entity.ToTable("Zitplaatsen");

            entity.Property(e => e.StadionId)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("stadionID");
            entity.Property(e => e.ZitplaatsId)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("zitplaatsID");
            entity.Property(e => e.RijNummer)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("rijNummer");
            entity.Property(e => e.StoelNummer)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("stoelNummer");
            entity.Property(e => e.VakNummer)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("vakNummer");

            entity.HasOne(d => d.Stadion).WithMany(p => p.Zitplaatsens)
                .HasForeignKey(d => d.StadionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_stadionID_Zitplaatsen");

            entity.HasOne(d => d.VakNummerNavigation).WithMany(p => p.Zitplaatsens)
                .HasForeignKey(d => d.VakNummer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_vakNummer_Zitplaatsen");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
