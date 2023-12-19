using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jwt.Api.Data.Migrations
{
    public partial class InitialDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FullName = table.Column<string>(type: "TEXT", nullable: false),
                    Age = table.Column<int>(type: "INTEGER", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Phone = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleTitle = table.Column<string>(type: "TEXT", nullable: false),
                    RoleInformation = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleId = table.Column<int>(type: "INTEGER", nullable: true),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    Token = table.Column<string>(type: "TEXT", nullable: true),
                    TokenExpiredDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Age", "Email", "FullName", "Phone" },
                values: new object[] { 1, 23, "ut.nec@icloud.edu", "Quinn Baldwin", "(687) 487-7412" });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Age", "Email", "FullName", "Phone" },
                values: new object[] { 2, 50, "cras.pellentesque@outlook.couk", "Shoshana Campbell", "(868) 245-2812" });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Age", "Email", "FullName", "Phone" },
                values: new object[] { 3, 39, "consequat.dolor.vitae@outlook.org", "India Johnson", "(394) 327-4827" });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Age", "Email", "FullName", "Phone" },
                values: new object[] { 4, 27, "arcu.iaculis.enim@protonmail.net", "Odysseus Watkins", "(884) 344-2238" });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Age", "Email", "FullName", "Phone" },
                values: new object[] { 5, 32, "nulla@yahoo.org", "Germane Lynch", "(527) 785-7348" });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Age", "Email", "FullName", "Phone" },
                values: new object[] { 6, 22, "risus.donec@aol.edu", "Myra Lott", "(681) 436-3166" });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Age", "Email", "FullName", "Phone" },
                values: new object[] { 7, 31, "malesuada.fames@hotmail.net", "Cade Chase", "(406) 236-6085" });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Age", "Email", "FullName", "Phone" },
                values: new object[] { 8, 49, "euismod.ac@protonmail.org", "Todd Kennedy", "(445) 870-7432" });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Age", "Email", "FullName", "Phone" },
                values: new object[] { 9, 33, "id.mollis.nec@hotmail.com", "Lunea Pennington", "(847) 885-3753" });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Age", "Email", "FullName", "Phone" },
                values: new object[] { 10, 42, "lectus.ante@icloud.com", "Azalia Ellis", "(618) 483-2702" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "RoleInformation", "RoleTitle" },
                values: new object[] { 1, "This role has all processes.", "SuperAdmin" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "RoleInformation", "RoleTitle" },
                values: new object[] { 2, "This role has only get and create processes.", "Admin" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "RoleInformation", "RoleTitle" },
                values: new object[] { 3, "This role has only get process.", "Standard" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Password", "RoleId", "Token", "TokenExpiredDate", "Username" },
                values: new object[] { 1, "john.doe@mail.com", "123456", 1, null, null, "john_doe" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Password", "RoleId", "Token", "TokenExpiredDate", "Username" },
                values: new object[] { 2, "joel.foster@mail.com", "123456", 2, null, null, "joel_foster" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Password", "RoleId", "Token", "TokenExpiredDate", "Username" },
                values: new object[] { 3, "Elis.Howard@mail.com", "123456", 3, null, null, "elis_howard" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Password", "RoleId", "Token", "TokenExpiredDate", "Username" },
                values: new object[] { 4, "lucas.mccarthy@mail.com", "123456", 3, null, null, "lucas_mccarthy" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Password", "RoleId", "Token", "TokenExpiredDate", "Username" },
                values: new object[] { 5, "andrew.stone@mail.com", "123456", 3, null, null, "andrew_stone" });

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
