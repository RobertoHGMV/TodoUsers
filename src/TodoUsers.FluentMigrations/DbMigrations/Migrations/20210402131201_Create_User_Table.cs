using FluentMigrator;

namespace TodoUsers.FluentMigrations.DbMigrations.Migrations
{
    [Migration(20210402131201)]
    public class _20210402131201_Create_User_Table : Migration
    {
        public override void Down() { }

        public override void Up()
        {
            Create.Table("User")
                .WithColumn("Id").AsString().PrimaryKey()
                .WithColumn("FirstName").AsString(60).Nullable()
                .WithColumn("LastName").AsString(60).Nullable()
                .WithColumn("UserName").AsString(20).Nullable()
                .WithColumn("Password").AsString(32).Nullable()
                .WithColumn("Address").AsString(160).Nullable();
        }
    }
}
