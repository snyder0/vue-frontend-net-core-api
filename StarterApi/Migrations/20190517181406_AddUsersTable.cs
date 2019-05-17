using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StarterApi.Migrations
{
    public partial class AddUsersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(maxLength: 25, nullable: true),
                    LastName = table.Column<string>(maxLength: 25, nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Role = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    PasswordSalt = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "PasswordHash", "PasswordSalt", "Role" },
                values: new object[] { 1, "admin@admin.com", "Seeded-Admin-FirstName", "Seeded-Admin-LastName", new byte[] { 158, 157, 204, 220, 125, 242, 48, 1, 22, 210, 215, 225, 173, 230, 150, 192, 190, 48, 24, 210 }, new byte[] { 236, 230, 49, 209, 220, 73, 231, 173, 162, 222, 190, 88, 86, 93, 187, 100 }, "Admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
