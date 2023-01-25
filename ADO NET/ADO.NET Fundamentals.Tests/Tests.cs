using NUnit.Framework;
using System.Collections.Generic;
using System;
using ADO.NET_Fundamentals.Models;
using Moq;
using FluentAssertions;
using ADO.NET_Fundamentals.DAO;
using ADO.NET_Fundamentals.Enums;
using System.Linq;
using System.Xml.Linq;
using NUnit.Framework.Internal.Execution;

namespace ADO.NET_Fundamentals.Tests
{
    public class Tests
    {
        private readonly OrderDao orderDao = OrderDao.GetInstance();
        private readonly ProductDao productDao = ProductDao.GetInstance();

        private Order testOrder = new Order()
        {
            Status = Status.Loading,
            CreatedDate = new DateTime(2022, 1, 1),
            UpdatedDate = new DateTime(2022, 1, 1),
            ProductId = 1
        };

        private Product testProduct = new Product()
        {
            Description = "test2",
            Name = "test",
            Height = 1,
            Length = 1,
            Weight = 1,
            Width = 1
        };

        [Test]
        public void Library_GetProduct_ValidData_ReturnsValidProduct()
        {
            // Arrange
            var expected = new Product()
            {
                Description = "test2",
                Name = "test",
                Height = 1,
                Length = 1,
                Weight = 1,
                Width = 1
            };

            productDao.Create(expected);
            // Act
            var actual = productDao.Read(expected.Id);

            // Assert
            expected.Should().BeEquivalentTo(actual);
        }

        [Test]
        public void Library_GetOrder_ValidData_ReturnsValidOrder()
        {
            // Arrange
            var expected = new Order()
            {
                Status = Status.NotStarted,
                CreatedDate = new DateTime(2022, 1, 1),
                UpdatedDate = new DateTime(2022, 1, 1),
                ProductId = 1
            };

            orderDao.Create(expected);

            // Act
            var actual = orderDao.Read(expected.Id);

            // Assert
            expected.Should().BeEquivalentTo(actual);

            DisposeTestOrder(expected);
        }

        [Test]
        public void Library_CreateOrder_ValidData_CreatesCorrectOrder()
        {
            // Arrange
            var order2 = new Order()
            {
                Status = Status.Loading,
                CreatedDate = new DateTime(2022, 1, 1),
                UpdatedDate = new DateTime(2022, 1, 1),
                ProductId = 1
            };

            orderDao.Create(testOrder);

            var currentList = orderDao.ReadAll();

            var currentOrdersCount = currentList.Count;

            var lastId = currentList.Last().Id;

            // Act
            orderDao.Create(order2);
            var newCount = orderDao.ReadAll().Count;

            // Assert
            newCount.Should().Be(currentOrdersCount + 1);
            order2.Id.Should().Be(lastId + 1);

            DisposeTestOrder(testOrder);
            DisposeTestOrder(order2);
        }

        [Test]
        public void Library_CreateProduct_ValidData_CreatesCorrectProduct()
        {
            // Arrange
            var product = new Product()
            {
                Description = "test2",
                Name = "test",
                Height = 1,
                Length = 1,
                Weight = 1,
                Width = 1
            };

            productDao.Create(testProduct);

            var currentList = productDao.ReadAll();

            var currentOrdersCount = currentList.Count;

            var lastId = currentList.Last().Id;

            // Act
            productDao.Create(product);
            var newCount = productDao.ReadAll().Count;

            // Assert
            newCount.Should().Be(currentOrdersCount + 1);
            product.Id.Should().Be(lastId + 1);
        }

        [Test]
        public void Library_UpdateProductDescription_ValidData_UpdatesCorrectProduct()
        {
            // Arrange
            testProduct.Description = "qwerty";
            productDao.Create(testProduct);
            testProduct.Description = "SomeDesc";

            // Act
            productDao.Update(testProduct);

            // Assert
            var updated = productDao.Read(testProduct.Id);
            updated.Description.Should().BeEquivalentTo("SomeDesc");
        }

        [Test]
        public void Library_UpdateOrder_ValidData_UpdatesCorrectOrder()
        {
            // Arrange
            testOrder.Status = Status.NotStarted;
            orderDao.Create(testOrder);
            testOrder.Status = Status.Arrived;

            // Act
            orderDao.Update(testOrder);

            // Assert
            var updated = orderDao.Read(testOrder.Id);
            updated.Status.Should().HaveSameValueAs(Status.Arrived);

            DisposeTestOrder(testOrder);
        }

        [Test]
        public void Library_DeleteOrder_ValidData_DeletesCorrectOrder()
        {
            // Arrange
            orderDao.Create(testOrder);

            var currentOrdersCount = orderDao.ReadAll().Count;

            // Act
            orderDao.Delete(testOrder);

            var newCount = orderDao.ReadAll().Count;

            // Assert
            newCount.Should().Be(currentOrdersCount - 1);
        }

        [Test]
        public void Library_DeleteProduct_ValidData_DeletesCorrectProduct()
        {
            // Arrange
            productDao.Create(testProduct);

            var currentOrdersCount = productDao.ReadAll().Count;

            // Act
            productDao.Delete(testProduct);

            var newCount = productDao.ReadAll().Count;

            // Assert
            newCount.Should().Be(currentOrdersCount - 1);
        }

        [Test]
        public void Library_BulkDelete_ValidData_DeletesOrderWithYear2022()
        {
            // Arrange
            var order = new Order()
            {
                Status = Status.Cancelled,
                ProductId = 1,
                CreatedDate = new DateTime(2022, 1, 1),
                UpdatedDate = new DateTime(2022, 1, 1),
            };
            var order2 = new Order()
            {
                Status = Status.Cancelled,
                ProductId = 1,
                CreatedDate = new DateTime(2022, 1, 1),
                UpdatedDate = new DateTime(2022, 1, 1),
            };
            var order3 = new Order()
            {
                Status = Status.Cancelled,
                ProductId = 1,
                CreatedDate = new DateTime(2021, 1, 1),
                UpdatedDate = new DateTime(2021, 1, 1),
            };

            var list = new List<Order>()
            {
                order,
                order2,
                order3,
            };

            foreach (var item in list)
            {
                orderDao.Create(item);
            }

            var currentOrdersCount = orderDao.ReadAll().Count;

            // Act
            orderDao.BulkDelete(year : 2022);

            var newCount = orderDao.ReadAll().Count;

            // Assert
            newCount.Should().Be(currentOrdersCount - 2);

            DisposeTestOrder(order3);
        }

        [Test]
        public void Library_FetchOrdersByParameter_ValidData_FetchOrdersWithYear2021()
        {
            // Arrange
            var order = new Order()
            {
                Status = Status.Loading,
                ProductId = 1,
                CreatedDate = new DateTime(2022, 1, 1),
                UpdatedDate = new DateTime(2022, 1, 1),
            };
            var order2 = new Order()
            {
                Status = Status.Loading,
                ProductId = 1,
                CreatedDate = new DateTime(2022, 1, 1),
                UpdatedDate = new DateTime(2022, 1, 1),
            };
            var order3 = new Order()
            {
                Status = Status.Cancelled,
                ProductId = 1,
                CreatedDate = new DateTime(2021, 1, 1),
                UpdatedDate = new DateTime(2021, 1, 1),
            };

            var list = new List<Order>()
            {
                order,
                order2,
                order3,
            };

            foreach (var item in list)
            {
                orderDao.Create(item);
            }

            // Act
            var fetchedOrders = orderDao.FetchOrdersByParameter(year: 2021);

            DisposeTestOrder(order);
            DisposeTestOrder(order2);
            DisposeTestOrder(order3);

            // Assert
            fetchedOrders.Count().Should().Be(1);
        }

        private void DisposeTestOrder(Order order)
        {
            orderDao.Delete(order);
        }
    }
}