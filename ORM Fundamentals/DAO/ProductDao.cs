using Dapper;
using ORM_Fundamentals.Enums;
using ORM_Fundamentals.Interfaces;
using ORM_Fundamentals.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ORM_Fundamentals.DAO
{
    public class ProductDao : EssenceDao<Product>
    {
        private static readonly ProductDao _instance;

        private ProductDao()
        {
        }

        public static ProductDao GetInstance()
        {
            return _instance ?? new ProductDao();
        }

        public override void Create(Product entity)
        {
            using var db = new SqlConnection(connectionString);
            var createQuery = @"INSERT INTO [Product] ([Weight], [Height], [Width], [Length], [Name], [Description]) 
                                VALUES (@Weight, @Height, @Width, @Length, @Name, @Description);
	                            SELECT CAST(SCOPE_IDENTITY() as int);"
;

            entity.Id = db.QuerySingle<int>(createQuery, entity);
        }

        public override void Delete(Product entity)
        {
            using var db = new SqlConnection(connectionString);
            db.Query(@"Delete from [Product] where [Id] = @Id;", new { Id = entity.Id });
        }

        public override Product Read(int id)
        {
            using var db = new SqlConnection(connectionString);
            return db.QuerySingle<Product>(@"SELECT * FROM [Product] where [Id] = @ProductId;", new { ProductId = id });
        }

        public override List<Product> ReadAll()
        {
            using var db = new SqlConnection(connectionString);
            return db.Query<Product>(@"SELECT * FROM [Product]").ToList();
        }

        public override void Update(Product entity)
        {
            using var db = new SqlConnection(connectionString);
            db.Execute(@"Update [Product] set [Weight]=@Weight, [Height]=@Height, [Width]=@Width, [Length]=@Length, [Name]=@Name, [Description]=@Description where Id=@Id;", entity);
        }
    }
}
