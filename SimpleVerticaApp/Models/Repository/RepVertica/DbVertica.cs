
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Vertica.Data.VerticaClient;

namespace SimpleVerticaApp.Models.Repository.RepVertica
{
    public static class DbVertica
    {
        public static VerticaConnection ConnectDb(DbCredentials pCredentials)
        {
            var vcsb = new VerticaConnectionStringBuilder
            {
                Database = pCredentials.DataBase,
                User = pCredentials.User,
                Password = pCredentials.Password,
                Host = pCredentials.ServerIp
            };

            var vConnection = new VerticaConnection(vcsb.ConnectionString);

            vConnection.Open();
            return vConnection;

        }


        public static IEnumerable<SampleTable> SelectSampleTable(VerticaConnection pConnection)
        {
            var vResult = new List<SampleTable>();
            using (var dt = new DataTable())
            using (var comm = new VerticaCommand {
                CommandText = Queries.SelectSampleTable,
                Connection = pConnection})
            {
                dt.Load(comm.ExecuteReader());
                vResult.AddRange(Enumerable.Select(dt.AsEnumerable(), dataRow => new SampleTable(dataRow)));
            }
            return vResult;
        }


        public static int InsertSampleTable(SampleTable pData, VerticaConnection pConnection)
        {
            using (var comm = new VerticaCommand())
            {
                comm.Connection = pConnection;
                comm.CommandText = Queries.InsertSampleTable;

                comm.Parameters.AddRange(new[]
                {
                    new VerticaParameter("@pID", VerticaType.BigInt, pData.Id),
                    new VerticaParameter("@pText", VerticaType.VarChar, pData.Text)
                }
                    );
                
                return comm.ExecuteNonQuery();
            }
        }



        public static int UpdateSampleTable(SampleTable pData, VerticaConnection pConnection)
        {
            using (var comm = new VerticaCommand())
            {
                comm.Connection = pConnection;
                comm.CommandText = Queries.UpdateSampleTable;

                comm.Parameters.AddRange(new[]
                {
                    new VerticaParameter("@pID", VerticaType.Numeric, pData.Id),
                    new VerticaParameter("@pText", VerticaType.VarChar, pData.Text)
                }
                    );

                return comm.ExecuteNonQuery();
            }
        }

        public static int DeleteSampleTableById(SampleTable pData, VerticaConnection pConnection)
        {
            using (var comm = new VerticaCommand())
            {
                comm.Connection = pConnection;
                comm.CommandText = Queries.DeleteSampleTableById;

                comm.Parameters.AddRange(new[]
                {
                    new VerticaParameter("@pID", VerticaType.Numeric, pData.Id)
                    
                }
                    );

                return comm.ExecuteNonQuery();
            }
        }
    }
}
