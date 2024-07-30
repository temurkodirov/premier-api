using FSSEstate.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace FSSEstate.Repository.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }
    public DbSet<AccountEntity> Accounts { get; set; }
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<ConfirmationEmailEntity> ConfirmationEmails { get; set;}
    public DbSet<ProjectEntity> Projects { get; set; }
    public DbSet<AgentEntity> Agents { get; set; }
    public DbSet<CategoryEntity> Categories { get; set; }
    public DbSet<ProjectPhotosEntity> ProjectPhotos { get; set; }
    public DbSet<AffairEntity> Affairs { get; set; }
    public DbSet<FavouriteProjectEntity> FavouriteProjects { get; set; }
    public DbSet<ReviewEntity> Reviews { get; set; }
    public DbSet<InformationEntity> Informations { get; set; }
    public DbSet<InformationPhotosEntity> InformationPhotos { get; set; }
    public DbSet<AgentAffairEntity> AgentAffairs { get; set; }
    public DbSet<xProduct> xProducts { get; set; }
    public DbSet<xProductImage> xProductImages { get; set; }
    public DbSet<xProductCharacteristics> xProductCharacteristics { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<ReviewEntity>()
            .HasIndex(r => new { r.AccountId, r.ProjectId } )
            .IsUnique();

        modelBuilder.Entity<FavouriteProjectEntity>()
            .HasIndex(f => new { f.AccountId, f.ProjectId })
            .IsUnique();
        
    }
}
