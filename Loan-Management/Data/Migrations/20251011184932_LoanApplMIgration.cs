using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Loan_Management.Data.Migrations
{
    /// <inheritdoc />
    public partial class LoanApplMIgration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DateOfBirth",
                table: "ApplicationModel",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "ApplicationModel",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "ApplicationModel",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HomeTown",
                table: "ApplicationModel",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "ApplicationModel",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "NetIncome",
                table: "ApplicationModel",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Occupation",
                table: "ApplicationModel",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "ApplicationModel",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "ApplicationModel");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "ApplicationModel");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "ApplicationModel");

            migrationBuilder.DropColumn(
                name: "HomeTown",
                table: "ApplicationModel");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "ApplicationModel");

            migrationBuilder.DropColumn(
                name: "NetIncome",
                table: "ApplicationModel");

            migrationBuilder.DropColumn(
                name: "Occupation",
                table: "ApplicationModel");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "ApplicationModel");
        }
    }
}
