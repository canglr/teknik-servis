namespace teknikservis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Olustur3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ayarlar",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Site_Adi = c.String(),
                        Site_Adresi = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Ayarlar");
        }
    }
}
