//using System;
//using System.Net.Http;
//using System.Threading.Tasks;
//using System.Data.SqlClient;
//using System.Text.Json;
//using ConsoleApp2;
//using ConsoleApp2.Utilities.Exceptions;

//class Program
//{
//    static async Task Main()
//    {
//        Console.Write("Enter the ID: ");
//        int id = int.Parse(Console.ReadLine());

//        try
//        {
//            var apiObject = await GetFromApi(id);
//            AddToDatabase(apiObject);
//            Console.WriteLine("Object added to the database successfully.");
//        }
//        catch (ObjectAlreadyExistsException ex)
//        {
//            Console.WriteLine(ex.Message);
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine("An error occurred: " + ex.Message);
//        }
//    }

//    static void AddToDatabase(ApiObject apiObject)
//    {
//        string connectionString = @"Server=MSI\\SQLEXPRESS;Database=UsersDB;Trusted_Connection=true;";

//        if (CheckIfObjectExists(apiObject.Id, connectionString))
//        {
//            throw new ObjectAlreadyExistsException("Object already exists in the database.");
//        }

//        InsertIntoDatabase(apiObject, connectionString);
//    }

//    static bool CheckIfObjectExists(int id, string connectionString)
//    {
//        using (SqlConnection connection = new SqlConnection(connectionString))
//        {
//            connection.Open();

//            using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Shop Users Id = @Id", connection))
//            {
//                command.Parameters.AddWithValue("@Id", id);

//                int count = (int)command.ExecuteScalar();

//                return count > 0;
//            }
//        }
//    }

//    static void InsertIntoDatabase(ApiObject apiObject, string connectionString)
//    {
//        using (SqlConnection connection = new SqlConnection(connectionString))
//        {
//            connection.Open();

//            using (SqlCommand command = new SqlCommand("INSERT INTO Users (Id, Title, Body) VALUES (@Id, @Title, @Body)", connection))
//            {
//                command.Parameters.AddWithValue("@Id", apiObject.Id);
//                command.Parameters.AddWithValue("@Title", apiObject.Title);
//                command.Parameters.AddWithValue("@Body", apiObject.Body);

//                command.ExecuteNonQuery();
//            }
//        }
//    }
//}




