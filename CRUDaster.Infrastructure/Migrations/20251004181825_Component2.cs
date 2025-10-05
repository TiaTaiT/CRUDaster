using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRUDaster.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Component2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Brand_BrandId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Category_CategoryId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Model_ModelId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Pim_PimId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Status_StatusId",
                table: "Product");

            migrationBuilder.DropTable(
                name: "ProductProtocol");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Product",
                table: "Product");

            migrationBuilder.RenameTable(
                name: "Product",
                newName: "Component");

            migrationBuilder.RenameIndex(
                name: "IX_Product_StatusId",
                table: "Component",
                newName: "IX_Component_StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_PimId",
                table: "Component",
                newName: "IX_Component_PimId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_ModelId",
                table: "Component",
                newName: "IX_Component_ModelId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_CategoryId",
                table: "Component",
                newName: "IX_Component_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_BrandId",
                table: "Component",
                newName: "IX_Component_BrandId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Component",
                table: "Component",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ComponentProtocol",
                columns: table => new
                {
                    ComponentId = table.Column<int>(type: "integer", nullable: false),
                    ProtocolId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentProtocol", x => new { x.ComponentId, x.ProtocolId });
                    table.ForeignKey(
                        name: "FK_ComponentProtocol_Component_ComponentId",
                        column: x => x.ComponentId,
                        principalTable: "Component",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComponentProtocol_Protocol_ProtocolId",
                        column: x => x.ProtocolId,
                        principalTable: "Protocol",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComponentProtocol_ProtocolId",
                table: "ComponentProtocol",
                column: "ProtocolId");

            migrationBuilder.AddForeignKey(
                name: "FK_Component_Brand_BrandId",
                table: "Component",
                column: "BrandId",
                principalTable: "Brand",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Component_Category_CategoryId",
                table: "Component",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Component_Model_ModelId",
                table: "Component",
                column: "ModelId",
                principalTable: "Model",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Component_Pim_PimId",
                table: "Component",
                column: "PimId",
                principalTable: "Pim",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Component_Status_StatusId",
                table: "Component",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Component_Brand_BrandId",
                table: "Component");

            migrationBuilder.DropForeignKey(
                name: "FK_Component_Category_CategoryId",
                table: "Component");

            migrationBuilder.DropForeignKey(
                name: "FK_Component_Model_ModelId",
                table: "Component");

            migrationBuilder.DropForeignKey(
                name: "FK_Component_Pim_PimId",
                table: "Component");

            migrationBuilder.DropForeignKey(
                name: "FK_Component_Status_StatusId",
                table: "Component");

            migrationBuilder.DropTable(
                name: "ComponentProtocol");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Component",
                table: "Component");

            migrationBuilder.RenameTable(
                name: "Component",
                newName: "Product");

            migrationBuilder.RenameIndex(
                name: "IX_Component_StatusId",
                table: "Product",
                newName: "IX_Product_StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_Component_PimId",
                table: "Product",
                newName: "IX_Product_PimId");

            migrationBuilder.RenameIndex(
                name: "IX_Component_ModelId",
                table: "Product",
                newName: "IX_Product_ModelId");

            migrationBuilder.RenameIndex(
                name: "IX_Component_CategoryId",
                table: "Product",
                newName: "IX_Product_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Component_BrandId",
                table: "Product",
                newName: "IX_Product_BrandId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Product",
                table: "Product",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ProductProtocol",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    ProtocolId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductProtocol", x => new { x.ProductId, x.ProtocolId });
                    table.ForeignKey(
                        name: "FK_ProductProtocol_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductProtocol_Protocol_ProtocolId",
                        column: x => x.ProtocolId,
                        principalTable: "Protocol",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductProtocol_ProtocolId",
                table: "ProductProtocol",
                column: "ProtocolId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Brand_BrandId",
                table: "Product",
                column: "BrandId",
                principalTable: "Brand",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Category_CategoryId",
                table: "Product",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Model_ModelId",
                table: "Product",
                column: "ModelId",
                principalTable: "Model",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Pim_PimId",
                table: "Product",
                column: "PimId",
                principalTable: "Pim",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Status_StatusId",
                table: "Product",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
