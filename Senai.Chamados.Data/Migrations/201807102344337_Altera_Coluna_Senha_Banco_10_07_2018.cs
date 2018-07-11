namespace Senai.Chamados.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Altera_Coluna_Senha_Banco_10_07_2018 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Usuarios", "Senha", c => c.String(nullable: false, maxLength: 150));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Usuarios", "Senha", c => c.String(nullable: false, maxLength: 8));
        }
    }
}
