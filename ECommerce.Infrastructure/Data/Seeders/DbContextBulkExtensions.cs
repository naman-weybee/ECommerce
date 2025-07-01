using Microsoft.Data.SqlClient;
using System.Data;

namespace ECommerce.Infrastructure.Data.Seeders
{
    public static class DbContextBulkExtensions
    {
        private const string connectionString = "Server=DESKTOP-B8PQOC6; Database=ECommerce; Integrated Security=True; TrustServerCertificate=True;";

        public static bool TableHasData(string tableName)
        {
            using var connection = new SqlConnection(connectionString);
            connection.Open();

            using var command = new SqlCommand($"SELECT TOP 1 1 FROM {tableName}", connection);

            if (connection.State != ConnectionState.Open)
                connection.Open();

            using var reader = command.ExecuteReader();
            return reader.HasRows;
        }

        public static void BulkInsert(DataTable table, string destinationTable)
        {
            try
            {
                using var connection = new SqlConnection(connectionString);
                connection.Open();

                using var bulk = new SqlBulkCopy(connection)
                {
                    DestinationTableName = destinationTable
                };

                foreach (DataColumn column in table.Columns)
                    bulk.ColumnMappings.Add(column.ColumnName, column.ColumnName);

                Console.WriteLine($"Started Adding Data for Table: {destinationTable}");
                bulk.WriteToServer(table);
                Console.WriteLine($"Data Added Successfully for Table: {destinationTable}\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DB ERROR: {ex}");
            }
        }
    }
}