using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Loan_Management.Data.Migrations
{
    /// <inheritdoc />
    public partial class LoanApplicationMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProcessingFee",
                table: "LoanProducts");

            migrationBuilder.AddColumn<string>(
                name: "CollateralDetails",
                table: "ApplicationModel",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "MaturityDate",
                table: "ApplicationModel",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<decimal>(
                name: "ProcessingFee",
                table: "ApplicationModel",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "RequiresCollateral",
                table: "ApplicationModel",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CollateralDetails",
                table: "ApplicationModel");

            migrationBuilder.DropColumn(
                name: "MaturityDate",
                table: "ApplicationModel");

            migrationBuilder.DropColumn(
                name: "ProcessingFee",
                table: "ApplicationModel");

            migrationBuilder.DropColumn(
                name: "RequiresCollateral",
                table: "ApplicationModel");

            migrationBuilder.AddColumn<decimal>(
                name: "ProcessingFee",
                table: "LoanProducts",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
