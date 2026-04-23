using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIStreamingDeAudio.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FaixasAudio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Titulo = table.Column<string>(type: "TEXT", nullable: false),
                    NomeArtista = table.Column<string>(type: "TEXT", nullable: false),
                    NomeAlbum = table.Column<string>(type: "TEXT", nullable: false),
                    GeneroMusical = table.Column<string>(type: "TEXT", nullable: false),
                    DuracaoEmSegundos = table.Column<int>(type: "INTEGER", nullable: false),
                    CaminhoDoArquivo = table.Column<string>(type: "TEXT", nullable: false),
                    FormatoArquivo = table.Column<string>(type: "TEXT", nullable: false),
                    TaxaDeBits = table.Column<int>(type: "INTEGER", nullable: false),
                    FrequenciaAmostragem = table.Column<int>(type: "INTEGER", nullable: false),
                    TamanhoArquivoBytes = table.Column<long>(type: "INTEGER", nullable: false),
                    DataDeLancamento = table.Column<DateTime>(type: "TEXT", nullable: false),
                    NomeCompositor = table.Column<string>(type: "TEXT", nullable: false),
                    NomeGravadora = table.Column<string>(type: "TEXT", nullable: false),
                    TotalReproducoes = table.Column<long>(type: "INTEGER", nullable: false),
                    ContemConteudoExplicito = table.Column<bool>(type: "INTEGER", nullable: false),
                    DataDeUpload = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FaixasAudio", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FaixasAudio");
        }
    }
}
