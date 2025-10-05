using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Loan_Management.Models;
// namespace Loan_Management.Data;


public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<LoanProductsRegister> LoanProducts { get; set; }

    public DbSet<LoanApplicationModel> ApplicationModel { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //One loan product has many LoanApplications
        modelBuilder.Entity<LoanApplicationModel>()
        .HasOne(a => a.LoanProduct)
        .WithMany(p => p.ApplicationsModel)
        .HasForeignKey(a => a.LoanProductId)
        .OnDelete(DeleteBehavior.Cascade); //If a LoanProduct is deleted, its applications go too
    }
}
