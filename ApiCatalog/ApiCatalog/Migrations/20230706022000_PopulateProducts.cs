using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiCatalog.Migrations
{
    /// <inheritdoc />
    public partial class PopulateProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("INSERT INTO Products(Name,Description,Price,ImageURL,Stock,RegisterDate,CategoryId)" +
                "Values('Coca-Cola Diet', 'Cola Soda 350ml', '5.45', 'cocacola.png', 50, now(), 1)");
            mb.Sql("INSERT INTO Products(Name,Description,Price,ImageURL,Stock,RegisterDate,CategoryId)" +
                "Values('Chocolate Cake', 'One piece of Chocolate Cake', '3.45', 'chocolate-cake.png', 10, now(), 3)");
            mb.Sql("INSERT INTO Products(Name,Description,Price,ImageURL,Stock,RegisterDate,CategoryId)" +
                "Values('Ruffles', 'Ruffles Chips', '4.50', 'ruffles.png', 200, now(), 2)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("Delete from Products");
        }
    }
}
