using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RandomProfileAPI.Data.RandomProfileMigrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProfileImages",
                columns: table => new
                {
                    ProfileImageID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    ProfileImageBytes = table.Column<byte[]>(type: "BLOB", nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileImages", x => x.ProfileImageID);
                });

            migrationBuilder.CreateTable(
                name: "Profiles",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    MiddleName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    Bio = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    NickName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    DOB = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ProfileImageID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Profiles_ProfileImages_ProfileImageID",
                        column: x => x.ProfileImageID,
                        principalTable: "ProfileImages",
                        principalColumn: "ProfileImageID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_ProfileImageID",
                table: "Profiles",
                column: "ProfileImageID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Profiles");

            migrationBuilder.DropTable(
                name: "ProfileImages");
        }
    }
}
