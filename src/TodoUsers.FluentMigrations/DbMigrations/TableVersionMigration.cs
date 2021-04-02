using FluentMigrator.Runner.VersionTableInfo;

namespace TodoUsers.FluentMigrations.DbMigrations
{
    [VersionTableMetaData]
    public class TableVersionMigration : IVersionTableMetaData
    {
        public object ApplicationContext { get; set; }

        public bool OwnsSchema => true;

        public string SchemaName => "";

        public string TableName => "TDU_Version";

        public string ColumnName => "Version";

        public string DescriptionColumnName => "Description";

        public string UniqueIndexName => "TDU_IDX_Version";

        public string AppliedOnColumnName => "AppliedOn";
    }
}
