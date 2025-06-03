using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SM.FMA.Data.Entities;

namespace SM.FMA.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
    : IdentityDbContext<ApplicationUser>(options)
{
    public ApplicationDbContext() 
    : this(new DbContextOptions<ApplicationDbContext>())
    {
        
    }
    public DbSet<Certificate> Certificates { get; set; }
    public DbSet<FacultyMember> FacultyMembers { get; set; }
    public DbSet<FacultyPosition> FacultyPositions { get; set; }
    public DbSet<FacultyRank> FacultyRanks { get; set; }
    public DbSet<Publication> Publications { get; set; }
}
