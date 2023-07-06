using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiCatalog.Migrations
{
    /// <inheritdoc />
    public partial class PopulateCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("INSERT INTO Categories(Name,ImageURL) Values('Drinks', 'drinks.png')");
            mb.Sql("INSERT INTO Categories(Name,ImageURL) Values('Snacks', 'snacks.png')");
            mb.Sql("INSERT INTO Categories(Name,ImageURL) Values('Deserts', 'deserts.png')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("Delete from Categories");
        }
    }
}
