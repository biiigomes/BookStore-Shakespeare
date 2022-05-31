using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArch.Infra.Data.Migrations
{
    public partial class SeedingProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, Stock, Image, CategoryId)" + "VALUES('Caderno espiral', 'Caderno espiral 100 folhas',  7.45,50, 'caderno1.jpg', 1)");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, Stock, Image, CategoryId)" + "VALUES('Caneta azul', 'Caneta azul BIC',  1.00,50, 'caneta1.jpg', 1)");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, Stock, Image, CategoryId)" + "VALUES('Borracha branca', 'Borracha branca média',  3.50,00, 'borracha1.jpg', 1)");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, Stock, Image, CategoryId)" + "VALUES('Calculadora cientifica', 'Calculadora cientifica grande',  20.30,00, 'calculdora1.jpg', 2)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Products");
        }
    }
}
