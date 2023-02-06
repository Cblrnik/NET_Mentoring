using ORM_Fundamentals.Interfaces;
using ORM_Fundamentals.Models;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using ORM_Fundamentals.Enums;
using System;
using static Dapper.SqlMapper;

namespace ORM_Fundamentals.DAO
{
    public class OrderDao : EssenceDao<Order>
    {
        private static readonly OrderDao _instance;

        private OrderDao()
        {
        }

        public static OrderDao GetInstance()
        {
            return _instance ?? new OrderDao();
        }

        public IEnumerable<Order> FetchOrdersByParameter(int? month = null, Status? status = null, int? year = null, int? productId = null)
        {
            using var db = new SqlConnection(connectionString);
            return db.Query<Order>("spFetchOrders", new { Month = month, Year = year, Status = status, ProductId = productId }, commandType: CommandType.StoredProcedure).ToList();
        }

        public void BulkDelete(int? month = null, Status? status = null, int? year = null, int? productId = null)
        {
            using var db = new SqlConnection(connectionString);
            db.Execute("spBulkDelete", new { Month = month, Year = year, Status = status, ProductId = productId }, commandType: CommandType.StoredProcedure);
        }

        public override void Create(Order entity)
        {
            using var db = new SqlConnection(connectionString);
            var createQuery = @"INSERT INTO [Order] ([Status], [CreatedDate], [UpdatedDate], [ProductId]) 
                                VALUES (@Status, @CreatedDate, @UpdatedDate, @ProductId);
								SELECT CAST(SCOPE_IDENTITY() as int);";

            entity.Id = db.QuerySingle<int>(createQuery, entity);
        }

        public override void Delete(Order entity)
        {
            using var db = new SqlConnection(connectionString);
            db.Query(@"Delete from [Order] where [Id] = @Id", new { Id = entity.Id });
        }

        public override Order Read(int id)
        {
            using var db = new SqlConnection(connectionString);
            return db.QuerySingle<Order>(@"SELECT * FROM [Order] where [Id] = @OrderId;", new { OrderId = id });

        }

        public override List<Order> ReadAll()
        {
            using var db = new SqlConnection(connectionString);
            return db.Query<Order>(@"SELECT * FROM [Order];").ToList();
        }

        public override void Update(Order entity)
        {
            using var db = new SqlConnection(connectionString);
            db.Execute(@"Update [Order] set [Status] = @Status, [UpdatedDate]=GETDATE() where [Id]=@Id;", entity);
        }
    }
}
