namespace SimpleVerticaApp.Models.Repository.RepVertica
{
    public static class Queries
    {
        public const string SelectSampleTable = @"
select ID, Text from testSchema.SampleTable;";

        public const string InsertSampleTable = @"
insert into testSchema.SampleTable (ID, Text)
values(@pID,@pText);";

        public const string UpdateSampleTable = @"
update testSchema.SampleTable
set Text = @pText
where ID = @pID;";

        public const string DeleteSampleTableById = @"
Delete from testSchema.SampleTable
where ID = @pID";
    }
}
