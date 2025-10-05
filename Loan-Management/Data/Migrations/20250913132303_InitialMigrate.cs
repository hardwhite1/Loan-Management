using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Loan_Management.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigrate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LoanProducts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Code = table.Column<string>(type: "TEXT", nullable: true),
                    PrincipalAmountMin = table.Column<decimal>(type: "TEXT", nullable: false),
                    PrincipalAmountMax = table.Column<decimal>(type: "TEXT", nullable: false),
                    InterestRate = table.Column<double>(type: "REAL", nullable: false),
                    InterestRateType = table.Column<string>(type: "TEXT", nullable: true),
                    ProcessingFee = table.Column<decimal>(type: "TEXT", nullable: false),
                    LatePaymentPenalty = table.Column<decimal>(type: "TEXT", nullable: false),
                    RepaymentFrequency = table.Column<string>(type: "TEXT", nullable: true),
                    GracePeriodMonths = table.Column<int>(type: "INTEGER", nullable: false),
                    InstallmentType = table.Column<string>(type: "TEXT", nullable: true),
                    RequiresCollateral = table.Column<bool>(type: "INTEGER", nullable: false),
                    CollateralType = table.Column<string>(type: "TEXT", nullable: true),
                    EligibilityCriteria = table.Column<string>(type: "TEXT", nullable: true),
                    Active = table.Column<bool>(type: "INTEGER", nullable: false),
                    MaxConcurrentLoans = table.Column<int>(type: "INTEGER", nullable: false),
                    PrepayMentAllowed = table.Column<bool>(type: "INTEGER", nullable: false),
                    PrepayMentPenalty = table.Column<decimal>(type: "TEXT", nullable: true),
                    Currency = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    LastUpdated = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanProducts", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoanProducts");
        }
    }
}
