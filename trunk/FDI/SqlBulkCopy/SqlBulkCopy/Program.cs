using System;
using System.Data;
using System.Data.SqlClient;

namespace SqlBulkCopy
{
    class Program
    {
        static void Main(string[] args)
        {
            var time= new DateTime();
            string connectionString = GetConnectionString();
            // Open a connection to the AdventureWorks database. 
            using (SqlConnection connection =
                       new SqlConnection(connectionString))
            {
                connection.Open();

                // Perform an initial count on the destination table.
                SqlCommand commandRowCount = new SqlCommand(
                    "SELECT COUNT(*) FROM " +
                    "dbo.BulkCopyDemoMatchingColumns;",
                    connection);
                long countStart = System.Convert.ToInt32(
                    commandRowCount.ExecuteScalar());
                Console.WriteLine("Starting row count = {0}", countStart);

                // Create a table with some rows. 
                DataTable newProducts = MakeTable();

                // Create the SqlBulkCopy object.  
                // Note that the column positions in the source DataTable  
                // match the column positions in the destination table so  
                // there is no need to map columns.  
                using (var bulkCopy = new System.Data.SqlClient.SqlBulkCopy(connection))
                {
                    bulkCopy.DestinationTableName =
                        "dbo.BulkCopyDemoMatchingColumns";

                    try
                    {
                        // Write from the source to the destination.
                        var b  = DateTime.Now.Second;
                        bulkCopy.WriteToServer(newProducts);
                        var a = DateTime.Now.Second;
                        Console.WriteLine("bat dau:"+b);
                        Console.WriteLine("Tổng thời gian Lưu:"+a);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }

                // Perform a final count on the destination  
                // table to see how many rows were added. 
                long countEnd = System.Convert.ToInt32(
                    commandRowCount.ExecuteScalar());
                Console.WriteLine("Ending row count = {0}", countEnd);
                Console.WriteLine("{0} rows were added.", countEnd - countStart);
                Console.WriteLine("Press Enter to finish.");
                Console.ReadLine();
            }
        }

        private static DataTable MakeTable()
        {
            DataTable newProducts = new DataTable("NewProducts");

            //DataColumn productID = new DataColumn();
            //productID.DataType = System.Type.GetType("System.Int32");
            //productID.ColumnName = "ProductID";
            //productID.AutoIncrement = true;
            //newProducts.Columns.Add(productID);

            newProducts.Columns.Add("ProductID", typeof(int));
            newProducts.Columns.Add("Name", typeof(string));
            newProducts.Columns.Add("ProductNumber", typeof(string));

            //DataColumn productName = new DataColumn();
            //productName.DataType = System.Type.GetType("System.String");
            //productName.ColumnName = "Name";
            //newProducts.Columns.Add(productName);

            //DataColumn productNumber = new DataColumn();
            //productNumber.DataType = System.Type.GetType("System.String");
            //productNumber.ColumnName = "ProductNumber";
            //newProducts.Columns.Add(productNumber);

            // Create an array for DataColumn objects.
            //DataColumn[] keys = new DataColumn[1];
            //keys[0] = productID;
            //newProducts.PrimaryKey = keys;

            // Add some new rows to the collection. 
                   
            for (int i = 0; i < 100; i++)
            {
                var row = newProducts.NewRow();
                row["Name"] = "CC-101-ST"+i;
                row["ProductNumber"] = "Cyclocomputer - Stainless" +i;
                newProducts.Rows.Add(row);
                newProducts.AcceptChanges();
            }
           

            // Return the new DataTable.  
            return newProducts;
        }
        private static DataTable MakeTable1()
        {
            var newProducts = new DataTable("NewProducts");

            newProducts.Columns.Add("MaSV", typeof(int));
            newProducts.Columns.Add("ProductID", typeof(int));
            for (var i = 0; i < 90; i++)
            {
                var row = newProducts.NewRow();
                row["MaSV"] = 3758;
                row["ProductID"] = i+1;
                newProducts.Rows.Add(row);
                newProducts.AcceptChanges();
            }
            return newProducts;
        }
        private static string GetConnectionString()
        // To avoid storing the connection string in your code,  
        // you can retrieve it from a configuration file. 
        {
            return @"Data Source=QUANGKHANH-PC\SQLEXPRESS; " +
                @" Integrated Security=true;" +
                @"Initial Catalog=ToeicTestManager;";
            //string conString = @"Data Source = QUANGKHANH-PC\SQLEXPRESS;Initial Catalog = ToiecTestManager;Integrated Security=SSPI";
        }

    }
}
