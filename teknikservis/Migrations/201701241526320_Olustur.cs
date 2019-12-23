namespace teknikservis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Olustur : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Grup",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Grup_Ad = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Kullanici",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Ad = c.String(maxLength: 50),
                        Soyad = c.String(maxLength: 50),
                        Mail = c.String(nullable: false),
                        Kullanici_Adi = c.String(nullable: false),
                        Sifre = c.String(nullable: false),
                        Durum = c.Int(nullable: false),
                        Kayit_Tarihi = c.DateTime(nullable: false),
                        Guncellenme = c.DateTime(nullable: false),
                        Grup_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Grup", t => t.Grup_ID)
                .Index(t => t.Grup_ID);
            
            CreateTable(
                "dbo.Oturumlar",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Kimlik = c.Guid(nullable: false),
                        Ip = c.String(maxLength: 15),
                        Durum = c.Int(nullable: false),
                        Giris_Tarihi = c.DateTime(nullable: false),
                        Kullanici_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Kullanici", t => t.Kullanici_ID)
                .Index(t => t.Kullanici_ID);
            
            CreateTable(
                "dbo.Servisler",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Musteri = c.String(),
                        Tel = c.String(),
                        Durum = c.String(maxLength: 36, fixedLength: true, unicode: false),
                        Cihaz = c.String(maxLength: 36, fixedLength: true, unicode: false),
                        Model = c.String(),
                        Adres = c.String(),
                        Aciklama = c.String(),
                        Olusturulma_Tarihi = c.DateTime(nullable: false),
                        Sil = c.String(maxLength: 36, fixedLength: true, unicode: false),
                        Fiyat = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Tahsilat = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Yapilan_İslem = c.String(),
                        Guncellenme_Tarihi = c.DateTime(nullable: false),
                        Son_İslem_Yapan = c.Int(nullable: false),
                        Kullanici_ID = c.Int(),
                        Kullanici_ID1 = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Kullanici", t => t.Kullanici_ID1)
                .Index(t => t.Kullanici_ID1);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Servisler", "Kullanici_ID1", "dbo.Kullanici");
            DropForeignKey("dbo.Kullanici", "Grup_ID", "dbo.Grup");
            DropForeignKey("dbo.Oturumlar", "Kullanici_ID", "dbo.Kullanici");
            DropIndex("dbo.Servisler", new[] { "Kullanici_ID1" });
            DropIndex("dbo.Oturumlar", new[] { "Kullanici_ID" });
            DropIndex("dbo.Kullanici", new[] { "Grup_ID" });
            DropTable("dbo.Servisler");
            DropTable("dbo.Oturumlar");
            DropTable("dbo.Kullanici");
            DropTable("dbo.Grup");
        }
    }
}
