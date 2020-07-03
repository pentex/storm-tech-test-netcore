using Microsoft.EntityFrameworkCore.Migrations;

namespace Todo.Data.Migrations
{
    public partial class AddRankToItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                "Rank",
                "TodoItems",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Note: This won't actually work with SQLite because the Entity Framework Core SQLite provider doesn't support dropping columns from tables
            // See this link for more info on the limitations: https://docs.microsoft.com/en-us/ef/core/providers/sqlite/limitations
            // I'm leaving this as it is for now due to time constraints
            migrationBuilder.DropColumn(
                "Rank",
                "TodoItems");
        }
    }
}
