using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinancasApp.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CATEGORIA",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NOME = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CATEGORIA", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "USUARIO",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NOME = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EMAIL = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SENHA = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USUARIO", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CONTA",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NOME = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DESCRICAO = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    DATA = table.Column<DateTime>(type: "date", nullable: false),
                    VALOR = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TIPO = table.Column<int>(type: "int", nullable: false),
                    CATEGORIAID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    USUARIOID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CONTA", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CONTA_CATEGORIA_CATEGORIAID",
                        column: x => x.CATEGORIAID,
                        principalTable: "CATEGORIA",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CONTA_USUARIO_USUARIOID",
                        column: x => x.USUARIOID,
                        principalTable: "USUARIO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CONTA_CATEGORIAID",
                table: "CONTA",
                column: "CATEGORIAID");

            migrationBuilder.CreateIndex(
                name: "IX_CONTA_USUARIOID",
                table: "CONTA",
                column: "USUARIOID");

            migrationBuilder.CreateIndex(
                name: "IX_USUARIO_EMAIL",
                table: "USUARIO",
                column: "EMAIL",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CONTA");

            migrationBuilder.DropTable(
                name: "CATEGORIA");

            migrationBuilder.DropTable(
                name: "USUARIO");
        }
    }
}
