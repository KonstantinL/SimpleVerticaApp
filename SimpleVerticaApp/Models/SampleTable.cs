using System.Data;

namespace SimpleVerticaApp.Models
{
    public class SampleTable
    {
        public long Id { get; set; }
        public string Text { get; set; }

        public SampleTable()
        {
        }

        public SampleTable(DataRow pRow)
        {
            Id = pRow.Field<long>("ID");
            Text = pRow.Field<string>("Text");
        }
    }
}
