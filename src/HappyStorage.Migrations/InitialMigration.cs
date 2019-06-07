using System;

namespace HappyStorage.Migrations
{
    using FluentMigrator;

    namespace TrinugApi.Migrations
    {
        /// <summary>
        /// This migration is just to prove that the migrations *CAN* work.
        /// </summary>
        [Migration(1)]
        public class MigrationInitial : Migration
        {
            public override void Up()
            {
                Create.Table("trinug_temp_table")
                    .WithColumn("distribution_id").AsCustom("serial").PrimaryKey()
                    .WithColumn("text").AsString().NotNullable();
                Execute.Sql("SELECT create_distributed_table('trinug_temp_table', 'distribution_id')");
                Delete.Table("trinug_temp_table");
            }
            public override void Down()
            {
                throw new NotSupportedException();
            }
        }
    }
}
