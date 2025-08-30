using FluentMigrator;

namespace ApiDemarco.Infrastructure.Migrations;

[Migration(2)]
public class CreateTableClientes : Migration{
    public override void Up()
    {
        Create.Table("Clientes")
            .InSchema("demarco")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity().NotNullable().WithColumnDescription("Id do cliente")
            .WithColumn("Nome").AsString(100).NotNullable().WithColumnDescription("Nome do cliente")
            .WithColumn("Email").AsString(100).NotNullable().WithColumnDescription("E-mail do cliente");
        
        Create.UniqueConstraint("UQ_Clientes_Email")
            .OnTable("Clientes").WithSchema("demarco")
            .Column("Email");
    }

    public override void Down()
    {
    }
}