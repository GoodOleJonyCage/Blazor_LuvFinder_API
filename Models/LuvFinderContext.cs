using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LuvFinder_API.Models;

public partial class LuvFinderContext : DbContext
{
    public LuvFinderContext()
    {
    }

    public LuvFinderContext(DbContextOptions<LuvFinderContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Answer> Answers { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Profile> Profiles { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<QuestionType> QuestionTypes { get; set; }

    public virtual DbSet<Region> Regions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserBlog> UserBlogs { get; set; }

    public virtual DbSet<UserBlogComment> UserBlogComments { get; set; }

    public virtual DbSet<UserGender> UserGenders { get; set; }

    public virtual DbSet<UserInfo> UserInfos { get; set; }

    public virtual DbSet<UserLike> UserLikes { get; set; }

    public virtual DbSet<UserMaritalStatus> UserMaritalStatuses { get; set; }

    public virtual DbSet<UserMessage> UserMessages { get; set; }

    public virtual DbSet<UserProfile> UserProfiles { get; set; }

    public virtual DbSet<UserProfilePic> UserProfilePics { get; set; }

    public virtual DbSet<UserProfilePicsDb> UserProfilePicsDbs { get; set; }

    public virtual DbSet<UserViewing> UserViewings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAPTOP-6CKU0B7N\\SQLEXPRESS;Database=LuvFinder;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Answer>(entity =>
        {
            entity.ToTable("Answer");

            entity.HasIndex(e => e.QuestionId, "IDX_Answer_QuestionID");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.QuestionId).HasColumnName("QuestionID");
            entity.Property(e => e.Text)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.HasOne(d => d.Question).WithMany(p => p.Answers)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Answer_Question");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cities__3214EC074A6A2D20");

            entity.HasIndex(e => e.Name, "IDX_Cities_Name");

            entity.HasIndex(e => e.RegionId, "IDX_Cities_RegionId");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Region).WithMany(p => p.Cities)
                .HasForeignKey(d => d.RegionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cities_Regions");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Countrie__3214EC07E9E4D47E");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.Language)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Profile>(entity =>
        {
            entity.ToTable("Profile");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.QuestionId).HasColumnName("QuestionID");

            entity.HasOne(d => d.Question).WithMany(p => p.Profiles)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Profile_Question");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.ToTable("Question");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ShortDesc)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Text)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.HasOne(d => d.QuestionTypeNavigation).WithMany(p => p.Questions)
                .HasForeignKey(d => d.QuestionType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Question_QuestionType");
        });

        modelBuilder.Entity<QuestionType>(entity =>
        {
            entity.ToTable("QuestionType");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Region>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Regions__3214EC0711BA03CA");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Country).WithMany(p => p.Regions)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Regions_Countries");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Date)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Password)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(500)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UserBlog>(entity =>
        {
            entity.ToTable("UserBlog");

            entity.HasIndex(e => e.UserId, "IDX_UserBlog_UserID");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Body).IsUnicode(false);
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Title)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");
        });

        modelBuilder.Entity<UserBlogComment>(entity =>
        {
            entity.ToTable("UserBlogComment");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.BlogId).HasColumnName("BlogID");
            entity.Property(e => e.Comment).IsUnicode(false);
            entity.Property(e => e.Date)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Blog).WithMany(p => p.UserBlogComments)
                .HasForeignKey(d => d.BlogId)
                .HasConstraintName("FK_UserBlogComment_UserBlog");

            entity.HasOne(d => d.ReplyToNavigation).WithMany(p => p.InverseReplyToNavigation)
                .HasForeignKey(d => d.ReplyTo)
                .HasConstraintName("FK_UserBlogComment_UserBlogComment");

            entity.HasOne(d => d.User).WithMany(p => p.UserBlogComments)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_UserBlogComment_User");
        });

        modelBuilder.Entity<UserGender>(entity =>
        {
            entity.ToTable("UserGender");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Gender)
                .HasMaxLength(500)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UserInfo>(entity =>
        {
            entity.ToTable("UserInfo");

            entity.HasIndex(e => e.UserId, "IDX_UserInfo");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CityId).HasColumnName("CityID");
            entity.Property(e => e.CountryId).HasColumnName("CountryID");
            entity.Property(e => e.Date)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Dob)
                .HasColumnType("datetime")
                .HasColumnName("DOB");
            entity.Property(e => e.FirstName)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.GenderId).HasColumnName("GenderID");
            entity.Property(e => e.LastName)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.MaritalStatusId).HasColumnName("MaritalStatusID");
            entity.Property(e => e.RegionId).HasColumnName("RegionID");
            entity.Property(e => e.SeekingGenderId).HasColumnName("SeekingGenderID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.City).WithMany(p => p.UserInfos)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserInfo_Cities");

            entity.HasOne(d => d.Country).WithMany(p => p.UserInfos)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserInfo_Countries");

            entity.HasOne(d => d.Gender).WithMany(p => p.UserInfoGenders)
                .HasForeignKey(d => d.GenderId)
                .HasConstraintName("FK_UserInfo_UserGender");

            entity.HasOne(d => d.MaritalStatus).WithMany(p => p.UserInfos)
                .HasForeignKey(d => d.MaritalStatusId)
                .HasConstraintName("FK_UserInfo_UserMaritalStatus");

            entity.HasOne(d => d.Region).WithMany(p => p.UserInfos)
                .HasForeignKey(d => d.RegionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserInfo_Regions");

            entity.HasOne(d => d.SeekingGender).WithMany(p => p.UserInfoSeekingGenders)
                .HasForeignKey(d => d.SeekingGenderId)
                .HasConstraintName("FK_UserInfo_UserGender1");

            entity.HasOne(d => d.User).WithMany(p => p.UserInfos)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_UserInfo_User");
        });

        modelBuilder.Entity<UserLike>(entity =>
        {
            entity.HasIndex(e => e.FromId, "IDX_UserLikes_FromID");

            entity.HasIndex(e => e.ToId, "IDX_UserLikes_ToId");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Date)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FromId).HasColumnName("FromID");
            entity.Property(e => e.LikeAccepted).HasDefaultValueSql("((0))");
            entity.Property(e => e.LikeAcceptedDate).HasColumnType("datetime");
            entity.Property(e => e.ToId).HasColumnName("ToID");

            entity.HasOne(d => d.From).WithMany(p => p.UserLikeFroms)
                .HasForeignKey(d => d.FromId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserLikes_User");

            entity.HasOne(d => d.To).WithMany(p => p.UserLikeTos)
                .HasForeignKey(d => d.ToId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserLikes_User1");
        });

        modelBuilder.Entity<UserMaritalStatus>(entity =>
        {
            entity.ToTable("UserMaritalStatus");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Status)
                .HasMaxLength(500)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UserMessage>(entity =>
        {
            entity.HasIndex(e => e.FromId, "IDX_UserMessages_FromID");

            entity.HasIndex(e => e.ToId, "IDX_UserMessages_ToId");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Date)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FromId).HasColumnName("FromID");
            entity.Property(e => e.Message)
                .HasMaxLength(2000)
                .IsUnicode(false);
            entity.Property(e => e.MessageRead).HasDefaultValueSql("((0))");
            entity.Property(e => e.MessageReadDate).HasColumnType("datetime");
            entity.Property(e => e.ToId).HasColumnName("ToID");

            entity.HasOne(d => d.From).WithMany(p => p.UserMessageFroms)
                .HasForeignKey(d => d.FromId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserMessages_User");

            entity.HasOne(d => d.To).WithMany(p => p.UserMessageTos)
                .HasForeignKey(d => d.ToId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserMessages_User1");
        });

        modelBuilder.Entity<UserProfile>(entity =>
        {
            entity.ToTable("UserProfile");

            entity.HasIndex(e => e.UserId, "IDX_UserProfile_UserID");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AnswerId).HasColumnName("AnswerID");
            entity.Property(e => e.AnswerText).HasColumnType("text");
            entity.Property(e => e.Date)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.QuestionId).HasColumnName("QuestionID");
            entity.Property(e => e.Selected).HasDefaultValueSql("((0))");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Answer).WithMany(p => p.UserProfiles)
                .HasForeignKey(d => d.AnswerId)
                .HasConstraintName("FK_UserProfile_Answer");

            entity.HasOne(d => d.Question).WithMany(p => p.UserProfiles)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserProfile_Question");

            entity.HasOne(d => d.User).WithMany(p => p.UserProfiles)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserProfile_User");
        });

        modelBuilder.Entity<UserProfilePic>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IDX_UserID");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.UserProfilePics)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_UserProfilePics_User");
        });

        modelBuilder.Entity<UserProfilePicsDb>(entity =>
        {
            entity.ToTable("UserProfilePicsDB");

            entity.HasIndex(e => e.UserId, "IDX_UserID");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.UserProfilePicsDbs)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_UserProfilePicsDB_User");
        });

        modelBuilder.Entity<UserViewing>(entity =>
        {
            entity.HasIndex(e => e.UserViewingId, "IDX_UserViewings_UserViewingID");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Date)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserViewedId).HasColumnName("UserViewedID");
            entity.Property(e => e.UserViewingId).HasColumnName("UserViewingID");

            entity.HasOne(d => d.UserViewed).WithMany(p => p.UserViewingUserVieweds)
                .HasForeignKey(d => d.UserViewedId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserViewings_User4");

            entity.HasOne(d => d.UserViewingNavigation).WithMany(p => p.UserViewingUserViewingNavigations)
                .HasForeignKey(d => d.UserViewingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserViewings_User3");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
