namespace Vidly3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetBithdateOfOneCustomer : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE Customers Set Birthdate = CAST('1980-12-23' AS DATETIME) WHERE Id = 4");
        }
        
        public override void Down()
        {
        }
    }
}
