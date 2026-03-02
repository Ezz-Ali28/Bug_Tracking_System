using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bug_Tracking_System.Migrations
{
    /// <inheritdoc />
    public partial class MakeProjectDescriptionNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bugs_Projects_ProjectId",
                table: "Bug");

            migrationBuilder.DropForeignKey(
                name: "FK_Bugs_Users_AssignedToId",
                table: "Bug");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Bugs_BugId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_UserId",
                table: "Comment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Projects",
                table: "Project");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comments",
                table: "Comment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bugs",
                table: "Bug");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "Project",
                newName: "Project");

            migrationBuilder.RenameTable(
                name: "Comment",
                newName: "Comment");

            migrationBuilder.RenameTable(
                name: "Bug",
                newName: "Bug");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_UserId",
                table: "Comment",
                newName: "IX_Comment_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_BugId",
                table: "Comment",
                newName: "IX_Comment_BugId");

            migrationBuilder.RenameIndex(
                name: "IX_Bugs_ProjectId",
                table: "Bug",
                newName: "IX_Bug_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Bugs_AssignedToId",
                table: "Bug",
                newName: "IX_Bug_AssignedToId");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Project",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Project",
                table: "Project",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comment",
                table: "Comment",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bug",
                table: "Bug",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bug_Project_ProjectId",
                table: "Bug",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bug_User_AssignedToId",
                table: "Bug",
                column: "AssignedToId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Bug_BugId",
                table: "Comment",
                column: "BugId",
                principalTable: "Bug",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_User_UserId",
                table: "Comment",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bug_Project_ProjectId",
                table: "Bug");

            migrationBuilder.DropForeignKey(
                name: "FK_Bug_User_AssignedToId",
                table: "Bug");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Bug_BugId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_User_UserId",
                table: "Comment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Project",
                table: "Project");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comment",
                table: "Comment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bug",
                table: "Bug");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "Project",
                newName: "Project");

            migrationBuilder.RenameTable(
                name: "Comment",
                newName: "Comment");

            migrationBuilder.RenameTable(
                name: "Bug",
                newName: "Bug");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_UserId",
                table: "Comment",
                newName: "IX_Comments_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_BugId",
                table: "Comment",
                newName: "IX_Comments_BugId");

            migrationBuilder.RenameIndex(
                name: "IX_Bug_ProjectId",
                table: "Bug",
                newName: "IX_Bugs_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Bug_AssignedToId",
                table: "Bug",
                newName: "IX_Bugs_AssignedToId");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Project",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "User",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Projects",
                table: "Project",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comments",
                table: "Comment",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bugs",
                table: "Bug",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bugs_Projects_ProjectId",
                table: "Bug",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bugs_Users_AssignedToId",
                table: "Bug",
                column: "AssignedToId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Bugs_BugId",
                table: "Comment",
                column: "BugId",
                principalTable: "Bug",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_UserId",
                table: "Comment",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
