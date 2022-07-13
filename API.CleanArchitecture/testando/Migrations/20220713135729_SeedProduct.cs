using Microsoft.EntityFrameworkCore.Migrations;

namespace Clean.Architecture.Infra.Data.Migrations
{
    public partial class SeedProduct : Migration
    {
        protected override void Up(MigrationBuilder migBuilder)
        {
            migBuilder.Sql("INSERT INTO Products(Name, Description, Price, Stock, Image, CategoryId) " +
                "VALUES('Caderno espiral', '100 Folhas', 7.45, 50, 'caderno.jpg', 1) ");
            migBuilder.Sql("INSERT INTO Products(Name, Description, Price, Stock, Image, CategoryId) " +
                "VALUES('Estojo escolar', 'estojo', 6.45, 70, 'estojo.jpg', 1) ");
        }

        protected override void Down(MigrationBuilder migBuilder)
        {
            migBuilder.Sql("DELETE FROM Products");
        }
    }
}
