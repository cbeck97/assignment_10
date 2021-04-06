using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Assignment_10.Models
{
    public partial class BowlingLeagueContext : DbContext
    {
        public BowlingLeagueContext()
        {
        }

        public BowlingLeagueContext(DbContextOptions<BowlingLeagueContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bowler> Bowlers { get; set; }
        public virtual DbSet<BowlerScore> BowlerScores { get; set; }
        public virtual DbSet<MatchGame> MatchGames { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<Tournament> Tournaments { get; set; }
        public virtual DbSet<TourneyMatch> TourneyMatches { get; set; }
        public virtual DbSet<ZtblBowlerRating> ZtblBowlerRatings { get; set; }
        public virtual DbSet<ZtblSkipLabel> ZtblSkipLabels { get; set; }
        public virtual DbSet<ZtblWeek> ZtblWeeks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlite("Data Source = BowlingLeague.sqlite");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bowler>(entity =>
            {
                entity.HasIndex(e => e.BowlerLastName, "BowlerLastName");

                entity.HasIndex(e => e.TeamId, "BowlersTeamID");

                entity.Property(e => e.BowlerId)
                    .HasColumnType("int")
                    .ValueGeneratedNever()
                    .HasColumnName("BowlerID");

                entity.Property(e => e.BowlerAddress).HasColumnType("nvarchar (50)");

                entity.Property(e => e.BowlerCity).HasColumnType("nvarchar (50)");

                entity.Property(e => e.BowlerFirstName).HasColumnType("nvarchar (50)");

                entity.Property(e => e.BowlerLastName).HasColumnType("nvarchar (50)");

                entity.Property(e => e.BowlerMiddleInit).HasColumnType("nvarchar (1)");

                entity.Property(e => e.BowlerPhoneNumber).HasColumnType("nvarchar (14)");

                entity.Property(e => e.BowlerState).HasColumnType("nvarchar (2)");

                entity.Property(e => e.BowlerZip).HasColumnType("nvarchar (10)");

                entity.Property(e => e.TeamId)
                    .HasColumnType("int")
                    .HasColumnName("TeamID");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.Bowlers)
                    .HasForeignKey(d => d.TeamId);
            });

            modelBuilder.Entity<BowlerScore>(entity =>
            {
                entity.HasKey(e => new { e.MatchId, e.GameNumber, e.BowlerId });

                entity.ToTable("Bowler_Scores");

                entity.HasIndex(e => e.BowlerId, "BowlerID");

                entity.HasIndex(e => new { e.MatchId, e.GameNumber }, "MatchGamesBowlerScores");

                entity.Property(e => e.MatchId)
                    .HasColumnType("int")
                    .HasColumnName("MatchID");

                entity.Property(e => e.GameNumber).HasColumnType("smallint");

                entity.Property(e => e.BowlerId)
                    .HasColumnType("int")
                    .HasColumnName("BowlerID");

                entity.Property(e => e.HandiCapScore)
                    .HasColumnType("smallint")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.RawScore)
                    .HasColumnType("smallint")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.WonGame)
                    .IsRequired()
                    .HasColumnType("bit")
                    .HasDefaultValueSql("0");

                entity.HasOne(d => d.Bowler)
                    .WithMany(p => p.BowlerScores)
                    .HasForeignKey(d => d.BowlerId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<MatchGame>(entity =>
            {
                entity.HasKey(e => new { e.MatchId, e.GameNumber });

                entity.ToTable("Match_Games");

                entity.HasIndex(e => e.WinningTeamId, "Team1ID");

                entity.HasIndex(e => e.MatchId, "TourneyMatchesMatchGames");

                entity.Property(e => e.MatchId)
                    .HasColumnType("int")
                    .HasColumnName("MatchID");

                entity.Property(e => e.GameNumber).HasColumnType("smallint");

                entity.Property(e => e.WinningTeamId)
                    .HasColumnType("int")
                    .HasColumnName("WinningTeamID")
                    .HasDefaultValueSql("0");

                entity.HasOne(d => d.Match)
                    .WithMany(p => p.MatchGames)
                    .HasForeignKey(d => d.MatchId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.HasIndex(e => e.TeamId, "TeamID")
                    .IsUnique();

                entity.Property(e => e.TeamId)
                    .HasColumnType("int")
                    .ValueGeneratedNever()
                    .HasColumnName("TeamID");

                entity.Property(e => e.CaptainId)
                    .HasColumnType("int")
                    .HasColumnName("CaptainID");

                entity.Property(e => e.TeamName)
                    .IsRequired()
                    .HasColumnType("nvarchar (50)");
            });

            modelBuilder.Entity<Tournament>(entity =>
            {
                entity.HasKey(e => e.TourneyId);

                entity.Property(e => e.TourneyId)
                    .HasColumnType("int")
                    .ValueGeneratedNever()
                    .HasColumnName("TourneyID");

                entity.Property(e => e.TourneyDate).HasColumnType("date");

                entity.Property(e => e.TourneyLocation).HasColumnType("nvarchar (50)");
            });

            modelBuilder.Entity<TourneyMatch>(entity =>
            {
                entity.HasKey(e => e.MatchId);

                entity.ToTable("Tourney_Matches");

                entity.HasIndex(e => e.OddLaneTeamId, "TourneyMatchesOdd");

                entity.HasIndex(e => e.TourneyId, "TourneyMatchesTourneyID");

                entity.HasIndex(e => e.EvenLaneTeamId, "Tourney_MatchesEven");

                entity.Property(e => e.MatchId)
                    .HasColumnType("int")
                    .ValueGeneratedNever()
                    .HasColumnName("MatchID");

                entity.Property(e => e.EvenLaneTeamId)
                    .HasColumnType("int")
                    .HasColumnName("EvenLaneTeamID")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Lanes).HasColumnType("nvarchar (5)");

                entity.Property(e => e.OddLaneTeamId)
                    .HasColumnType("int")
                    .HasColumnName("OddLaneTeamID")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.TourneyId)
                    .HasColumnType("int")
                    .HasColumnName("TourneyID")
                    .HasDefaultValueSql("0");

                entity.HasOne(d => d.EvenLaneTeam)
                    .WithMany(p => p.TourneyMatchEvenLaneTeams)
                    .HasForeignKey(d => d.EvenLaneTeamId);

                entity.HasOne(d => d.OddLaneTeam)
                    .WithMany(p => p.TourneyMatchOddLaneTeams)
                    .HasForeignKey(d => d.OddLaneTeamId);

                entity.HasOne(d => d.Tourney)
                    .WithMany(p => p.TourneyMatches)
                    .HasForeignKey(d => d.TourneyId);
            });

            modelBuilder.Entity<ZtblBowlerRating>(entity =>
            {
                entity.HasKey(e => e.BowlerRating);

                entity.ToTable("ztblBowlerRatings");

                entity.Property(e => e.BowlerRating).HasColumnType("nvarchar (15)");

                entity.Property(e => e.BowlerHighAvg).HasColumnType("smallint");

                entity.Property(e => e.BowlerLowAvg).HasColumnType("smallint");
            });

            modelBuilder.Entity<ZtblSkipLabel>(entity =>
            {
                entity.HasKey(e => e.LabelCount);

                entity.ToTable("ztblSkipLabels");

                entity.Property(e => e.LabelCount)
                    .HasColumnType("int")
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<ZtblWeek>(entity =>
            {
                entity.HasKey(e => e.WeekStart);

                entity.ToTable("ztblWeeks");

                entity.Property(e => e.WeekStart).HasColumnType("date");

                entity.Property(e => e.WeekEnd).HasColumnType("date");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
