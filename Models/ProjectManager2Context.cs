using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace APIConfig.Models;

public partial class ProjectManager2Context : DbContext
{
    public ProjectManager2Context()
    {
    }

    public ProjectManager2Context(DbContextOptions<ProjectManager2Context> options)
        : base(options)
    {
    }

    public virtual DbSet<RdActualPlan> RdActualPlans { get; set; }

    public virtual DbSet<RdActualPlanOption> RdActualPlanOptions { get; set; }

    public virtual DbSet<RdApprove> RdApproves { get; set; }

    public virtual DbSet<RdBrand> RdBrands { get; set; }

    public virtual DbSet<RdCapStandard> RdCapStandards { get; set; }

    public virtual DbSet<RdCapType> RdCapTypes { get; set; }

    public virtual DbSet<RdDraftman> RdDraftmen { get; set; }

    public virtual DbSet<RdHeadProject> RdHeadProjects { get; set; }

    public virtual DbSet<RdHistoryEdit> RdHistoryEdits { get; set; }

    public virtual DbSet<RdListReport> RdListReports { get; set; }

    public virtual DbSet<RdMonth> RdMonths { get; set; }

    public virtual DbSet<RdSerie> RdSeries { get; set; }

    public virtual DbSet<RdTeam> RdTeams { get; set; }

    public virtual DbSet<RdType> RdTypes { get; set; }
    public virtual DbSet<BlockedIps> BlockedIps { get; set; }
    

    public virtual DbSet<UserManager> UserManagers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        => optionsBuilder.UseSqlServer("Server=172.20.10.47;Database=Project_Manager_2;User Id=sa;Password=P@ssw0rd;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RdActualPlan>(entity =>
        {
            entity.HasKey(e => e.Article);

            entity.ToTable("RD_Actual_Plan");

            entity.Property(e => e.Article).ValueGeneratedNever();
            entity.Property(e => e.CapHour).HasColumnName("Cap_Hour");
            entity.Property(e => e.CapName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Cap_Name");
            entity.Property(e => e.CapQty).HasColumnName("Cap_QTY");
            entity.Property(e => e.CapType)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Cap_Type");
            entity.Property(e => e.CodeProject).HasColumnName("Code_Project");
            entity.Property(e => e.DraftmanCode).HasColumnName("Draftman_Code");
            entity.Property(e => e.DraftmanName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Draftman_Name");
            entity.Property(e => e.InitialsBrand)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Initials_Brand");
            entity.Property(e => e.MonthCount).HasColumnName("Month_Count");
            entity.Property(e => e.OptionsSelect)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ProjectName)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("Project_Name");
            entity.Property(e => e.Remark).IsUnicode(false);
            entity.Property(e => e.TeamCode).HasColumnName("Team_Code");
            entity.Property(e => e.TeamName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Team_Name");
            entity.Property(e => e.TypeNames)
                .IsUnicode(false)
                .HasColumnName("Type_Names");
        });

        modelBuilder.Entity<RdActualPlanOption>(entity =>
        {
            entity.HasKey(e => e.Article);

            entity.ToTable("RD_Actual_Plan_Options");

            entity.Property(e => e.Article).ValueGeneratedNever();
            entity.Property(e => e.CapCopy).HasColumnName("Cap_Copy");
            entity.Property(e => e.CapName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Cap_Name");
            entity.Property(e => e.CapStandard).HasColumnName("Cap_Standard");
            entity.Property(e => e.CodeProject).HasColumnName("Code_Project");
            entity.Property(e => e.DateTimeRec)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DraftmanCode).HasColumnName("Draftman_Code");
            entity.Property(e => e.DraftmanName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Draftman_Name");
            entity.Property(e => e.InitialsBrand)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Initials_Brand");
            entity.Property(e => e.OptionsSelect)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ProjectName)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("Project_Name");
        });

        modelBuilder.Entity<RdApprove>(entity =>
        {
            entity.HasKey(e => e.NameApprove);

            entity.ToTable("RD_Approve");

            entity.Property(e => e.NameApprove)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Name_Approve");
        });

        modelBuilder.Entity<RdBrand>(entity =>
        {

            entity.HasKey(e => e.BrandNumber);
            entity.ToTable("RD_Brand");
            entity.Property(e => e.BrandNumber)
                .HasColumnName("Brand_Number"); // Adjust if needed
            entity.Property(e => e.InitialsBrand)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Initials_Brand");

            entity.Property(e => e.NameBrand)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Name_Brand");
        });

        modelBuilder.Entity<RdCapStandard>(entity =>
        {
            entity.HasKey(e => e.CapId);

            entity.ToTable("RD_Cap_Standard");

            entity.Property(e => e.CapId)
                .ValueGeneratedNever()
                .HasColumnName("Cap_Id");
            entity.Property(e => e.CapCode)
                .IsUnicode(false)
                .HasColumnName("Cap_Code");
            entity.Property(e => e.CapCopy).HasColumnName("Cap_Copy");
            entity.Property(e => e.CapName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Cap_Name");
            entity.Property(e => e.CapStandard).HasColumnName("Cap_Standard");
            entity.Property(e => e.GroupCap)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Group_Cap");
        });

        modelBuilder.Entity<RdCapType>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("RD_Cap_Type");

            entity.Property(e => e.CapId).HasColumnName("Cap_Id");
            entity.Property(e => e.CapType)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("Cap_Type");
        });

        modelBuilder.Entity<RdDraftman>(entity =>
        {
            entity.HasKey(e => e.DraftmanCode);

            entity.ToTable("RD_Draftman");
            
            entity.Property(e => e.DraftmanCode)
                .ValueGeneratedNever()
                .HasColumnName("Draftman_Code");
            entity.Property(e => e.DraftmanImage).HasColumnName("Draftman_Image");
            entity.Property(e => e.DraftmanName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Draftman_Name");
            entity.Property(e => e.IdDraftMan)
                .IsUnicode(false)
                .HasColumnName("ID_DraftMan");
            entity.Property(e => e.TeamCode).HasColumnName("Team_Code");
        });

        modelBuilder.Entity<RdHeadProject>(entity =>
        {
            entity.HasKey(e => e.CodeProject).HasName("PK_RD_ProjectHEAD_1");

            entity.ToTable("RD_HeadProject");

            entity.Property(e => e.CodeProject)
                .ValueGeneratedNever()
                .HasColumnName("Code_Project");
            entity.Property(e => e.DateFinal).HasColumnName("Date_Final");
            entity.Property(e => e.DateGet).HasColumnName("Date_Get");
            entity.Property(e => e.Deskip).IsUnicode(false);
            entity.Property(e => e.InitialsBrand)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Initials_Brand");
            entity.Property(e => e.ProjectName)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("Project_Name");
            entity.Property(e => e.ProjectOwner)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Project_Owner");
        });

        modelBuilder.Entity<RdHistoryEdit>(entity =>
        {
            entity.HasKey(e => e.CountHis);

            entity.ToTable("RD_History_Edit");

            entity.Property(e => e.CapHour).HasColumnName("Cap_Hour");
            entity.Property(e => e.CapName)
                .IsUnicode(false)
                .HasColumnName("Cap_Name");
            entity.Property(e => e.CapQty).HasColumnName("Cap_QTY");
            entity.Property(e => e.CapType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Cap_Type");
            entity.Property(e => e.CodeProject).HasColumnName("Code_Project");
            entity.Property(e => e.DraftmanCode).HasColumnName("Draftman_Code");
            entity.Property(e => e.DraftmanName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Draftman_Name");
            entity.Property(e => e.DtimeSave)
                .HasColumnType("datetime")
                .HasColumnName("DTimeSave");
            entity.Property(e => e.InitialsBrand)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Initials_Brand");
            entity.Property(e => e.NameApprove)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("Name_Approve");
            entity.Property(e => e.OptionsSelect)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Ot).HasColumnName("OT");
            entity.Property(e => e.ProjectName)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("Project_Name");
            entity.Property(e => e.Remark).IsUnicode(false);
            entity.Property(e => e.TypeNames)
                .IsUnicode(false)
                .HasColumnName("Type_Names");
        });

        modelBuilder.Entity<RdListReport>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("RD_List_Report");

            entity.Property(e => e.DateCurrent).HasColumnName("Date_Current");
            entity.Property(e => e.NameReports)
                .IsUnicode(false)
                .HasColumnName("Name_Reports");
            entity.Property(e => e.SsrsReports)
                .IsUnicode(false)
                .HasColumnName("SSRS_Reports");
        });

        modelBuilder.Entity<RdMonth>(entity =>
        {
            entity.HasKey(e => e.MonthCount).HasName("PK_Month_Cap");

            entity.ToTable("RD_Month");

            entity.Property(e => e.MonthCount)
                .ValueGeneratedNever()
                .HasColumnName("Month_Count");
            entity.Property(e => e.CapInMonth).HasColumnName("Cap_InMonth");
            entity.Property(e => e.MonthName)
                .IsUnicode(false)
                .HasColumnName("Month_Name");
        });

        modelBuilder.Entity<RdSerie>(entity =>
        {
            entity.HasKey(e => e.NameSerie);

            entity.ToTable("RD_Serie");

            entity.Property(e => e.NameSerie)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Name_Serie");
        });

        modelBuilder.Entity<RdTeam>(entity =>
        {
            entity.HasKey(e => e.TeamCode);

            entity.ToTable("RD_Team");

            entity.Property(e => e.TeamCode)
                .ValueGeneratedNever()
                .HasColumnName("Team_Code");
            entity.Property(e => e.BossId).HasColumnName("Boss_ID");
            entity.Property(e => e.BossName)
                .IsUnicode(false)
                .HasColumnName("Boss_Name");
            entity.Property(e => e.TeamName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Team_Name");
        });

        modelBuilder.Entity<BlockedIps>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("BlockedIps");

            entity.Property(e => e.Id)
                .HasColumnName("Id");
            entity.Property(e => e.IpAddress)
                .IsRequired()
                .HasMaxLength(45)
                .HasColumnName("IpAddress");
            entity.Property(e => e.BlockedAt)
                .HasColumnName("BlockedAt");
            entity.Property(e => e.Reason)
                .HasMaxLength(255)
                .HasColumnName("Reason");
        });

        modelBuilder.Entity<RdType>(entity =>
        {
            entity.HasKey(e => e.TypeNames);

            entity.ToTable("RD_Type");

            entity.Property(e => e.TypeNames)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Type_Names");
        });

        modelBuilder.Entity<UserManager>(entity =>
        {
            entity.HasKey(e => e.Uid);
            entity.ToTable("UserManager");

            entity.Property(e => e.Uid)
                .IsRequired() 
                .HasColumnName("Uid"); 

            entity.Property(e => e.Email)
                .IsUnicode(false)
                .HasColumnName("Email");

            // Configure the Username property
            entity.Property(e => e.Username)
                .IsUnicode(false)
                .HasColumnName("Username");

            // Configure the Password property
            entity.Property(e => e.Password)
                .IsUnicode(false) 
                .HasColumnName("Password");


            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("FirstName"); 


            entity.Property(e => e.LastName)
                .HasMaxLength(50) 
                .HasColumnName("LastName"); 

            entity.Property(e => e.PasswordHash)
                .IsUnicode(false)
                .HasColumnName("PasswordHash");

            entity.Property(e => e.MidleName)
                .HasMaxLength(50)
                .HasColumnName("MidleName");

            entity.Property(e => e.ipAddress)
                .IsUnicode(false)
                .HasColumnName("IPAddress");
        });


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
