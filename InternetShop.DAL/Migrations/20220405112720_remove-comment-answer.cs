using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternetShop.DAL.Migrations
{
    public partial class removecommentanswer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommentAnswer");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Rating",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Rating");

            migrationBuilder.CreateTable(
                name: "CommentAnswer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CommentId = table.Column<int>(type: "int", nullable: false),
                    Likes = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentAnswer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommentAnswer_Comment_CommentId",
                        column: x => x.CommentId,
                        principalTable: "Comment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommentAnswer_CommentId",
                table: "CommentAnswer",
                column: "CommentId");
        }
    }
}
