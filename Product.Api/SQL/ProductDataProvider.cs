﻿using System.Data.SqlClient;
using Product.Api.Models;

namespace Product.Api.SQL
{
    public class ProductDataProvider : IProductDataProvider
    {

        private readonly string _connectionString;
        public ProductDataProvider(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ProductDto GetById(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            var query = "SELECT * FROM Product WHERE Id=@Id";
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("id", id);

            using var reader = command.ExecuteReader();

            if (!reader.Read())
                throw new Exception("No product found");

            return new ProductDto
            {
                Id = int.Parse(reader["ID"].ToString()),
                Name = reader["Name"].ToString(),
                Description = reader["Description"].ToString(),
                Quantity = int.Parse(reader["Quantity"].ToString()),
                Price = decimal.Parse(reader["Price"].ToString())
            };
        }

        public void Add(ProductDto product)
        {

            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            var query = @"insert into Product ([Name],[Price],[Quantity],[Description])
                        values(@Name, @Price, @Quantity, @Description)";

            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("Name", product.Name);
            command.Parameters.AddWithValue("Price", product.Price);
            command.Parameters.AddWithValue("Quantity", product.Quantity);
            command.Parameters.AddWithValue("Description", product.Description);

            command.ExecuteNonQuery();
        }

        public void Edit(ProductDto product)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            var query = @"UPDATE Product
                            SET Name = @Name,
                                Price = @Price,
                                Quantity = @Quantity,
                                Description = @Description
                            WHERE Id = @Id";
            using var command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("Id", product.Id);
            command.Parameters.AddWithValue("Name", product.Name);
            command.Parameters.AddWithValue("Price", product.Price);
            command.Parameters.AddWithValue("Quantity", product.Quantity);
            command.Parameters.AddWithValue("Description", product.Description);

            command.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            var query = @"DELETE FROM Product
                            WHERE Id = @Id;";

            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("Id", id);

            command.ExecuteNonQuery();
        }

        public IEnumerable<ProductDto> GetMany(int limit)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            var query = "SELECT TOP (@limit) [Id], [Name], [Price], [Quantity], [Description] FROM Product; ";
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("limit", limit);

            using var reader = command.ExecuteReader();

            if (!reader.HasRows)
                throw new Exception("No product found");

            while (reader.Read())
            {
                yield return new ProductDto
                {
                    Id = int.Parse(reader["ID"].ToString()),
                    Name = reader["Name"].ToString(),
                    Description = reader["Description"].ToString(),
                    Quantity = int.Parse(reader["Quantity"].ToString()),
                    Price = decimal.Parse(reader["Price"].ToString())
                };
            }
        }

        public IEnumerable<ProductDto> GetAll()
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            var query = "SELECT [Id], [Name], [Price], [Quantity], [Description] FROM Product;";

            using var command = new SqlCommand(query, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                yield return new ProductDto
                {
                    Id = int.Parse(reader["ID"].ToString()),
                    Name = reader["Name"].ToString(),
                    Description = reader["Description"].ToString(),
                    Quantity = int.Parse(reader["Quantity"].ToString()),
                    Price = decimal.Parse(reader["Price"].ToString())
                };
            }
        }
    }
}
