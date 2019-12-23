namespace teknikservis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Olustur1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Servisler", "Durum", c => c.Int(nullable: false));
            AlterColumn("dbo.Servisler", "Cihaz", c => c.Int(nullable: false));
            AlterColumn("dbo.Servisler", "Sil", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Servisler", "Sil", c => c.String(maxLength: 36, fixedLength: true, unicode: false));
            AlterColumn("dbo.Servisler", "Cihaz", c => c.String(maxLength: 36, fixedLength: true, unicode: false));
            AlterColumn("dbo.Servisler", "Durum", c => c.String(maxLength: 36, fixedLength: true, unicode: false));
        }
    }
}
