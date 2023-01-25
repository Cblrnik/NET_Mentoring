using ADO.NET_Fundamentals.Enums;
using ADO.NET_Fundamentals.Interfaces;
using ADO.NET_Fundamentals.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ADO.NET_Fundamentals.DAO
{
    public class ProductDao : EssenceDao<Product>
    {
        private readonly List<Product> _products;
        private static readonly ProductDao _instance;

        private ProductDao()
        {
            _products = new List<Product>();
            GetAllProductsCommand();
        }

        public static ProductDao GetInstance()
        {
            return _instance ?? new ProductDao();
        }

        public override void Create(Product entity)
        {
            CreateProductCommand(entity);
            _products.Add(entity);
        }

        public override void Delete(Product entity)
        {
            DeleteProductCommand(entity.Id);
            _products.Remove(entity);
        }

        public override Product Read(int id)
        {
            return _products.FirstOrDefault(product => product.Id == id);
        }

        public override List<Product> ReadAll()
        {
            return new List<Product>(_products);
        }

        public override Product Update(Product entity)
        {
            var index = _products.FindIndex(product => product.Id == entity.Id);

            if (index != -1)
            {
                UpdateProductCommand(entity);
                _products[index] = entity;
                return entity;
            }

            return null;
        }

        private void CreateProductCommand(Product entity)
        {
            using var connection = new SqlConnection(connectionString);
            connection.Open();
            var command = new SqlCommand("INSERT [Product](Name, Description, Height, Length, Weight, Width) OUTPUT Inserted.Id " +
                "VALUES (@name, @description, @height, @length, @weight, @width);", connection);
            command.Parameters.AddWithValue("@name", entity.Name);
            command.Parameters.AddWithValue("@description", entity.Description);
            command.Parameters.AddWithValue("@height", entity.Height);
            command.Parameters.AddWithValue("@length", entity.Length);
            command.Parameters.AddWithValue("@weight", entity.Weight);
            command.Parameters.AddWithValue("@width", entity.Width);

            var reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    entity.Id = (int)reader.GetValue(0);
                }
            }
        }

        private void UpdateProductCommand(Product entity)
        {
            using var connection = new SqlConnection(connectionString);
            connection.Open();

            var commandText = $"UPDATE [Product] SET Name=@name, Description=@description, " +
                $"Height=@height, Length=@length, Weight=@weight, Width=@width WHERE Id = @id;";

            var command = new SqlCommand(commandText, connection);

            command.Parameters.AddWithValue("@name", entity.Name);
            command.Parameters.AddWithValue("@description", entity.Description);
            command.Parameters.AddWithValue("@height", entity.Height);
            command.Parameters.AddWithValue("@length", entity.Length);
            command.Parameters.AddWithValue("@weight", entity.Weight);
            command.Parameters.AddWithValue("@width", entity.Width);
            command.Parameters.AddWithValue("@id", entity.Id);

            command.ExecuteNonQuery();
        }

        private void GetAllProductsCommand()
        {
            using var connection = new SqlConnection(connectionString);
            connection.Open();
            var command = new SqlCommand("SELECT * FROM [Product];", connection);
            var reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                try
                {
                    while (reader.Read())
                    {

                        var order = new Product
                        {
                            Id = (int)reader["Id"],
                            Description = (string)reader["Description"],
                            Height = (int)reader["Height"],
                            Name = (string)reader["Name"],
                            Length = (int)reader["Length"],
                            Weight = (int)reader["Weight"],
                            Width = (int)reader["Width"]
                        };

                        _products.Add(order);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        private void DeleteProductCommand(int id)
        {
            using var connection = new SqlConnection(connectionString);
            connection.Open();
            var command = new SqlCommand($"DELETE FROM [Product] WHERE Id = {id};", connection);
            command.ExecuteNonQuery();
        }
    }
}
