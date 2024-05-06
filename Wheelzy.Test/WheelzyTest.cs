using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Moq;
using Wheelzy.API.Controllers;
using Wheelzy.Core.DTO;
using Wheelzy.Core.Interface;
using Wheelzy.Core.Services;
using Xunit;

namespace Wheelzy.Test;

public class WheelzyTest
{
    private readonly WheelzyController _controller;
    private readonly AppDbContext _context;

    public WheelzyTest()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlServer(connectionString);
        var dbContextOptions = optionsBuilder.Options;

        _context = new AppDbContext(dbContextOptions);

        var carService = new CarService(dbContextOptions);
        var orderService = new OrderService(dbContextOptions);

        _controller = new WheelzyController(carService, orderService);
    }

    [Fact]
    public async Task GetOrder_Returns_OkResult_With_Valid_Data()
    {
        // Arrange
        var dateFrom = DateTime.UtcNow.AddDays(-7);
        var dateTo = DateTime.UtcNow;
        var customerIds = new List<int> { 1, 2, 3 };
        var statusIds = new List<int> { 1 };
        var isActive = true;

        // Act
        var result = await _controller.GetOrder(dateFrom, dateTo, customerIds, statusIds, isActive);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var orders = Assert.IsType<OrderDTO>(okResult.Value);
        Assert.NotNull(orders);
        Assert.NotNull(orders.OrderId); 
        Assert.NotNull(orders.OrderDate); 
        Assert.NotNull(orders.CustomerId); 
        Assert.NotNull(orders.StatusId);
    }

    [Fact]
    public async Task GetOrder_Returns_NotFound_When_No_Data_Found()
    {
        // Arrange
        var dateFrom = DateTime.UtcNow.AddDays(-7);
        var dateTo = DateTime.UtcNow;
        var customerIds = new List<int> { 6, 7, 8 };
        var statusIds = new List<int> { 1, 2 };
        var isActive = true;

        // Act
        var result = await _controller.GetOrder(dateFrom, dateTo, customerIds, statusIds, isActive);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task GetOrder_Returns_OkResult_With_Partial_Fields_When_Some_Data_Found()
    {
        // Arrange
        var dateFrom = DateTime.UtcNow.AddDays(-7);
        var dateTo = DateTime.UtcNow;
        var customerIds = new List<int> { 1, 2, 3 };
        var statusIds = new List<int> { 1 };
        var isActive = true;

        // Act
        var result = await _controller.GetOrder(dateFrom, dateTo, customerIds, statusIds, isActive);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var orderDto = Assert.IsType<OrderDTO>(okResult.Value);
        Assert.NotNull(orderDto); 
    }

    [Fact]
    public async Task GetOrder_Returns_BadRequest_When_EndDate_Is_Greater_Than_StartDate()
    {
        // Arrange
        var dateFrom = DateTime.UtcNow.AddDays(7);
        var dateTo = DateTime.UtcNow;
        var customerIds = new List<int> { 1, 2, 3 };
        var statusIds = new List<int> { 1 };
        var isActive = true;

        // Act
        var result = await _controller.GetOrder(dateFrom, dateTo, customerIds, statusIds, isActive);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(400, badRequestResult.StatusCode);
        Assert.Equal("End date must be greater than or equal to start date.", badRequestResult.Value);
    }

}