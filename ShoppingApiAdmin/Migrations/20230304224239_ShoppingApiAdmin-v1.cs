using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingApiAdmin.Migrations
{
    public partial class ShoppingApiAdminv1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdminShoppingLists",
                columns: table => new
                {
                    AdminShoppingListID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShoppingListID = table.Column<int>(type: "int", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShoppingListName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes1 = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminShoppingLists", x => x.AdminShoppingListID)
                        .Annotation("SqlServer:Clustered", true);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminShoppingLists");
        }
    }
}
