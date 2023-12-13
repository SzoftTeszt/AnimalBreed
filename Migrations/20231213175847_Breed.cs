using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimalsAPI.Migrations
{
    public partial class Breed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "BreedId",
                table: "Animal",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Animal_BreedId",
                table: "Animal",
                column: "BreedId");

            migrationBuilder.AddForeignKey(
                name: "FK_Animal_Breed_BreedId",
                table: "Animal",
                column: "BreedId",
                principalTable: "Breed",
                principalColumn: "BreedId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animal_Breed_BreedId",
                table: "Animal");

            migrationBuilder.DropIndex(
                name: "IX_Animal_BreedId",
                table: "Animal");

            migrationBuilder.AlterColumn<string>(
                name: "BreedId",
                table: "Animal",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
