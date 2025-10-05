using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Loan_Management.Data.Migrations
{
    /// <inheritdoc />
    public partial class DbRelationsMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    LoanProductId = table.Column<Guid>(type: "TEXT", nullable: false),
                    RequestedAmount = table.Column<decimal>(type: "TEXT", nullable: false),
                    RepaymentPeriodMonths = table.Column<int>(type: "INTEGER", nullable: false),
                    Purpose = table.Column<string>(type: "TEXT", nullable: true),
                    Status = table.Column<string>(type: "TEXT", nullable: false),
                    ApplicationDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    ProcessedBy = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationModel_LoanProducts_LoanProductId",
                        column: x => x.LoanProductId,
                        principalTable: "LoanProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationModel_LoanProductId",
                table: "ApplicationModel",
                column: "LoanProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationModel");
        }
    }
}
