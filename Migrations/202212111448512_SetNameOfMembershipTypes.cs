namespace Vidly3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetNameOfMembershipTypes : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE MembershipTypes SET name = 'Pay As You Go' WHERE Id = 1");
            Sql("UPDATE MembershipTypes SET name = 'Monthly' WHERE Id = 2");
            Sql("UPDATE MembershipTypes SET name = 'Quarterly' WHERE Id = 3");
            Sql("UPDATE MembershipTypes SET name = 'Annual' WHERE Id = 4");
        }
        
        public override void Down()
        {
        }
    }
}
