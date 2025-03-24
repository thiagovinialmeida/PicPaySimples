using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PicpaySimples.Migrations
{
    /// <inheritdoc />
    public partial class ValorTransacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Valor",
                table: "Transacao",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Valor",
                table: "Transacao");
        }
    }
}
