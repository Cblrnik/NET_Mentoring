using ADO.NET_Fundamentals.Enums;
using ADO.NET_Fundamentals.Interfaces;
using ADO.NET_Fundamentals.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ADO.NET_Fundamentals.DAO
{
    public class OrderDao : EssenceDao<Order>
    {
        private List<Order> _orders;
        private static readonly OrderDao _instance;

        private OrderDao()
        {
            _orders = new List<Order>();
            GetAllOrdersCommand();
        }

        public static OrderDao GetInstance()
        {
            return _instance ?? new OrderDao();
        }

        public IEnumerable<Order> FetchOrdersByParameter(int? month = null, Status? status = null, int? year = null, int? productId = null)
        {
            using var db = new SqlConnection(connectionString);
            db.Open();
            var command = new SqlCommand("spFetchOrders", db)
            {
                CommandType = CommandType.StoredProcedure
            };
            if (month.HasValue)
                command.Parameters.AddWithValue("@Month", month.Value); 
            if (status.HasValue)
                command.Parameters.AddWithValue("@Status", (int)status.Value);
            if (year.HasValue)
                command.Parameters.AddWithValue("@Year", year.Value);
            if (productId.HasValue)
                command.Parameters.AddWithValue("@ProductId", productId.Value);

            var reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                var filteredOrders = new List<Order>();
                try
                {
                    while (reader.Read())
                    {

                        var order = new Order
                        {
                            Id = (int)reader["Id"],
                            Status = (Status)reader["Status"],
                            CreatedDate = (DateTime)reader["CreatedDate"],
                            UpdatedDate = (DateTime)reader["UpdatedDate"],
                            ProductId = (int)reader["ProductId"]
                        };

                        filteredOrders.Add(order);
                    }

                    return filteredOrders;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return null;
        }

        public void BulkDelete(int? month = null, Status? status = null, int? year = null, int? productId = null)
        {
            using var db = new SqlConnection(connectionString);
            db.Open();
            var command = new SqlCommand("spBulkDelete", db)
            {
                CommandType = CommandType.StoredProcedure
            };
            if (month.HasValue)
                command.Parameters.AddWithValue("@Month", month.Value);
            if (status.HasValue)
                command.Parameters.AddWithValue("@Status", (int)status.Value);
            if (year.HasValue)
                command.Parameters.AddWithValue("@Year", year.Value);
            if (productId.HasValue)
                command.Parameters.AddWithValue("@ProductId", productId.Value);

            command.ExecuteNonQuery();

            GetAllOrdersCommand();
        }

        public override void Create(Order entity)
        {
            CreateOrderCommand(entity);
            _orders.Add(entity);
        }

        public override void Delete(Order entity)
        {
            DeleteOrderCommand(entity.Id);
            _orders.Remove(entity);
        }

        public override Order Read(int id)
        {
            return _orders.FirstOrDefault(order => order.Id == id);
        }

        public override List<Order> ReadAll()
        {
            return new List<Order>(_orders);
        }

        public override Order Update(Order entity)
        {
            var index = _orders.FindIndex(order => order.Id == entity.Id);

            if (index != -1)
            {
                UpdateOrderCommand(entity);
                _orders[index] = entity;
                return entity;
            }

            return null;
        }

        private void CreateOrderCommand(Order entity)
        {
            using var connection = new SqlConnection(connectionString);
            connection.Open();
            var command = new SqlCommand("INSERT [Order](Status, CreatedDate, UpdatedDate, ProductId) OUTPUT Inserted.Id " +
                "VALUES (@status, @createdDate, @updatedDate, @productId);", connection);
            command.Parameters.AddWithValue("@status", entity.Status);
            command.Parameters.AddWithValue("@createdDate", entity.CreatedDate);
            command.Parameters.AddWithValue("@updatedDate", entity.UpdatedDate);
            command.Parameters.AddWithValue("@productId", entity.ProductId);

            var reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    entity.Id = (int)reader.GetValue(0);
                }
            }
        }

        private void UpdateOrderCommand(Order entity)
        {
            using var connection = new SqlConnection(connectionString);
            connection.Open();

            var commandText = $"UPDATE [Order] SET Status=@status, CreatedDate=@createdDate, " +
                $"UpdatedDate=@updatedDate, ProductId=@productId WHERE Id = @id;";

            var command = new SqlCommand(commandText, connection);

            command.Parameters.AddWithValue("@status", entity.Status);
            command.Parameters.AddWithValue("@createdDate", entity.CreatedDate);
            command.Parameters.AddWithValue("@updatedDate", entity.UpdatedDate);
            command.Parameters.AddWithValue("@productId", entity.ProductId);
            command.Parameters.AddWithValue("@id", entity.Id);

            command.ExecuteNonQuery();
        }

        private void GetAllOrdersCommand()
        {
            _orders = new List<Order>();
            using var connection = new SqlConnection(connectionString);
            connection.Open();
            var command = new SqlCommand("SELECT * FROM [Order];", connection);
            var reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                try
                {
                    while (reader.Read())
                    {

                        var order = new Order
                        {
                            Id = (int)reader["Id"],
                            Status = (Status)reader["Status"],
                            CreatedDate = (DateTime)reader["CreatedDate"],
                            UpdatedDate = (DateTime)reader["UpdatedDate"],
                            ProductId = (int)reader["ProductId"]
                        };

                        _orders.Add(order);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        private void DeleteOrderCommand(int id)
        {
            using var connection = new SqlConnection(connectionString);
            connection.Open();
            var command = new SqlCommand($"DELETE FROM [Order] WHERE Id = {id};", connection);
            command.ExecuteNonQuery();
        }
    }
}
