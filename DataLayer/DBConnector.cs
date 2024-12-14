using System.Data.SqlClient;

namespace DataLayer
{
    public static class DBConnector
    {
        public static SqlConnectionStringBuilder GetBuilder()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = @"dbsys.cs.vsb.cz\STUDENT";
            builder.UserID = "oso0008";
            builder.Password = "NymNt27iCS40JtPZ";
            builder.InitialCatalog = "oso0008";
            return builder;
        }
    }
}