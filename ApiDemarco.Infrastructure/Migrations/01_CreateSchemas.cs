using FluentMigrator;

namespace ApiDemarco.Infrastructure.Migrations;

[Migration(1)]
public class CreateSchemas : Migration {
    
    private static string[] _schemas = new string[] { "demarco" };

    public override void Up()
    {
        foreach (var nome in _schemas)
        {
            if (!Schema.Schema(nome).Exists())
            {
                Create.Schema(nome);
            }
        }
    }

    public override void Down()
    {
        foreach (var nome in _schemas)
        {
            if (Schema.Schema(nome).Exists())
            {
                Execute.Sql($"DROP SCHEMA {nome} CASCADE");
            }
        }
    }
}