using System;
using Microsoft.Data.SqlClient;

namespace OnlineStoreApp_HomeWork
{
    internal class DataBaseManager
    {
        private readonly string connectionString = "Server=localhost;Database=OnlineStore;Trusted_Connection=True;Encrypt=False;";


        public bool TestConnection()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    Console.WriteLine("Coonecting to server succesfully");
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка подключения: {ex.Message}");
                return false;
            }
        }
        public void AddProduct(string name, string description, decimal price, int stock)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Products (Name, Description, Price, Stock) VALUES (@name, @description, @price, @stock)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@description", description);
                    command.Parameters.AddWithValue("@price", price);
                    command.Parameters.AddWithValue("@stock", stock);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void AddCategory (string categoryName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Categories (CategoryName) VALUES (@CategoryName)";
                if (connection.State != System.Data.ConnectionState.Open)
                {
                    connection.Open();
                }
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CategoryName", categoryName);

                    try
                    {
                        command.ExecuteNonQuery();
                        Console.WriteLine("Category added successfully.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                    finally
                    {
                        
                        connection.Close();
                    }
                }
            }
        }

        public void LinkProductToCategory(int productId, int categoryId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO ProductCategories (ProductId, CategoryId) VALUES (@productId, @categoryId)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@productId", productId);
                    command.Parameters.AddWithValue("@categoryId", categoryId);
                    connection.Open();
                    command.ExecuteNonQuery();
                }

            }


        }
        public void GetAllProducts()
        {
            using(SqlConnection connection = new SqlConnection (connectionString))
            {
                string query = "SELECT * FROM Products";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        Console.WriteLine("\nProduct List:");
                        while (reader.Read())
                        {
                            Console.WriteLine($"ID: {reader["ProductID"]}, Name: {reader["Name"]}, Description: {reader["Description"]}, Price: {reader["Price"]}, Stock: {reader["Stock"]}");
                        }
                        Console.WriteLine();
                    }
                            
                        
                }
            }

        }


    }
}
