using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syste
namespace МКР
{
    
    class Program
        {
            private static void ExecuteSqlTransaction(string connectionString)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                connection.Open();
                    SqlCommand command = connection.CreateCommand();
                    SqlTransaction transaction;
                    transaction = connection.BeginTransaction("SampleTransaction");
                    command.Connection = connection;
                    command.Transaction = transaction;

                    try
                    {
                        command.CommandText =
                            "Insert into Region (RegionID, RegionDescription) VALUES (100, 'Description')";
                        command.ExecuteNonQuery();
                        command.CommandText =
                            "Insert into Region (RegionID, RegionDescription) VALUES (101, 'Description')";
                        command.ExecuteNonQuery();
                        transaction.Commit();
                        Console.WriteLine("Both records are written to database.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
                        Console.WriteLine("  Message: {0}", ex.Message);

                        
                        try
                        {
                            transaction.Rollback();
                        }
                        catch (Exception ex2)
                        {
                            
                            Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                            Console.WriteLine("  Message: {0}", ex2.Message);
                        }
                    }
                }
            }
        }
    }
}
