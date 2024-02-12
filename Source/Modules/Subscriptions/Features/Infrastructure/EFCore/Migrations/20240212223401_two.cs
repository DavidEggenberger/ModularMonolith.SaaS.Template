using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modules.Subscriptions.Features.Infrastructure.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class two : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Subscriptions",
                table: "StripeSubscriptions");

            migrationBuilder.DropColumn(
                name: "IsSoftDeleted",
                schema: "Subscriptions",
                table: "StripeSubscriptions");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Subscriptions",
                table: "StripeCustomers");

            migrationBuilder.DropColumn(
                name: "IsSoftDeleted",
                schema: "Subscriptions",
                table: "StripeCustomers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Subscriptions",
                table: "StripeSubscriptions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSoftDeleted",
                schema: "Subscriptions",
                table: "StripeSubscriptions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Subscriptions",
                table: "StripeCustomers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSoftDeleted",
                schema: "Subscriptions",
                table: "StripeCustomers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
