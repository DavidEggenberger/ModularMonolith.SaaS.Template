using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modules.Subscriptions.Features.Infrastructure.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class StripeSubscriptionPortalId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StripePortalSubscriptionId",
                schema: "Subscriptions",
                table: "StripeSubscriptions",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StripePortalSubscriptionId",
                schema: "Subscriptions",
                table: "StripeSubscriptions");
        }
    }
}
