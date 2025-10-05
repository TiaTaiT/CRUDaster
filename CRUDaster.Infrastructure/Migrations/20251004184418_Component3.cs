using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRUDaster.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Component3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ComponentProtocol",
                table: "ComponentProtocol");

            migrationBuilder.DropIndex(
                name: "IX_ComponentProtocol_ProtocolId",
                table: "ComponentProtocol");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ComponentProtocol",
                table: "ComponentProtocol",
                columns: new[] { "ProtocolId", "ComponentId" });

            migrationBuilder.CreateIndex(
                name: "IX_ComponentProtocol_ComponentId",
                table: "ComponentProtocol",
                column: "ComponentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ComponentProtocol",
                table: "ComponentProtocol");

            migrationBuilder.DropIndex(
                name: "IX_ComponentProtocol_ComponentId",
                table: "ComponentProtocol");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ComponentProtocol",
                table: "ComponentProtocol",
                columns: new[] { "ComponentId", "ProtocolId" });

            migrationBuilder.CreateIndex(
                name: "IX_ComponentProtocol_ProtocolId",
                table: "ComponentProtocol",
                column: "ProtocolId");
        }
    }
}
