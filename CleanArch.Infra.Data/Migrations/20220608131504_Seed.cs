using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArch.Infra.Data.Migrations
{
    public partial class Seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, Stock, Image, CategoryId)" + "VALUES('Dia Zero', 'Não temos nomes, temos números. Não vivemos, apenas sobrevivemos',  40.00, 50, 'diazero.jpg', 1)");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, Stock, Image, CategoryId)" + "VALUES('Reinado das Trevas: Rainha Perversa', 'Quando o mundo se tornar puro caos e a última planta mágica falecer. Eu irei retonar. Eu irei matar você.',  40.00, 50, 'rainhaperversa.jpg', 1)");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, Stock, Image, CategoryId)" + "VALUES('Jade Suaveolens e os Descendentes de Tupã', 'Siga suas flores, Jade. Cumpra o seu destino',  20.50, 20, 'jade1.jpg', 1)");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, Stock, Image, CategoryId)" + "VALUES('Hiraeth: Bruxas da Noite', 'Nunca se esqueça de nada. Sempre se lembre de tudo. As cicatrizes do passado te fortalecem e te forjam como uma espada para enfrentar seu futuro',  50.00, 40, 'hiraeth1.jpg', 2)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Products");
        }
    }
}
