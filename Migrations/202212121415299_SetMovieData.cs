namespace Vidly3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetMovieData : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Movies (Name, ReleaseDate, DateAdded, NumberInStock, GenreId) VALUES ('Troy', '5/4/1996', '5/4/1996', 5, 1)");
        }
        
        public override void Down()
        {
        }
    }
}
