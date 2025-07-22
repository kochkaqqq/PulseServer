using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pulse.DataBase.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDocument : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Requests_RequestId",
                table: "Documents");

            migrationBuilder.DropIndex(
                name: "IX_Documents_RequestId",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "RequestId",
                table: "Documents");

            migrationBuilder.AddColumn<int>(
                name: "DocumentId",
                table: "Requests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Hash",
                table: "Documents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "DocumentRequest",
                columns: table => new
                {
                    FilesDocumentId = table.Column<int>(type: "int", nullable: false),
                    RequestsRequestId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentRequest", x => new { x.FilesDocumentId, x.RequestsRequestId });
                    table.ForeignKey(
                        name: "FK_DocumentRequest_Documents_FilesDocumentId",
                        column: x => x.FilesDocumentId,
                        principalTable: "Documents",
                        principalColumn: "DocumentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocumentRequest_Requests_RequestsRequestId",
                        column: x => x.RequestsRequestId,
                        principalTable: "Requests",
                        principalColumn: "RequestId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Requests_DocumentId",
                table: "Requests",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentRequest_RequestsRequestId",
                table: "DocumentRequest",
                column: "RequestsRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Documents_DocumentId",
                table: "Requests",
                column: "DocumentId",
                principalTable: "Documents",
                principalColumn: "DocumentId",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Documents_DocumentId",
                table: "Requests");

            migrationBuilder.DropTable(
                name: "DocumentRequest");

            migrationBuilder.DropIndex(
                name: "IX_Requests_DocumentId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "DocumentId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "Hash",
                table: "Documents");

            migrationBuilder.AddColumn<int>(
                name: "RequestId",
                table: "Documents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Documents_RequestId",
                table: "Documents",
                column: "RequestId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Requests_RequestId",
                table: "Documents",
                column: "RequestId",
                principalTable: "Requests",
                principalColumn: "RequestId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
