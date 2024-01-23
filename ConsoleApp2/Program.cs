using System;
using System.Data.SqlClient;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ConsoleApp2;

class Program
{
    static async Task Main()
    {
        Console.Write("Enter the ID to add to the database: ");
        int idToAdd = int.Parse(Console.ReadLine());

        ApiObject apiObject = await GetObjectFromApi(idToAdd);
        AddObjectToDatabase(apiObject, @"Server=MSI\SQLEXPRESS;Database=UsersDB;Trusted_Connection=true");


        Console.WriteLine("Press any key to exit.");
        Console.ReadKey();
    }


    private static async Task<ApiObject> GetObjectFromApi(int id)
    {
        using (HttpClient client = new HttpClient())
        {
            HttpResponseMessage response = await client.GetAsync($"https://jsonplaceholder.typicode.com/posts/{id}");
            response.EnsureSuccessStatusCode();

            string jsonString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ApiObject>(jsonString);
        }
    }

    private static void AddObjectToDatabase(ApiObject apiObject, string connectionString)
    {
        if (apiObject != null)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("INSERT INTO Users (Id, UserId, Title, Body) VALUES (@Id, @UserId, @Title, @Body)", connection))
                {
                    command.Parameters.AddWithValue("@Id", apiObject.Id);
                    command.Parameters.AddWithValue("@UserId", apiObject.UserId);
                    command.Parameters.AddWithValue("@Title", apiObject.Title);
                    command.Parameters.AddWithValue("@Body", apiObject.Body);

                    command.ExecuteNonQuery();
                }
            }

            Console.WriteLine("Object added to the database successfully.");
        }
        else
        {
            Console.WriteLine("Object is null. Insertion into the database is not performed.");
        }
    }
}




