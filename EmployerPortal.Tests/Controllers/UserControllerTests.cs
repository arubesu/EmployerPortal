using EmployerPortal.Controllers;
using EmployerPortal.Data;
using EmployerPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

public class UsersControllerTests
{
    private readonly EmployerPortalDbContext _context;
    private readonly Mock<ILogger<UsersController>> _loggerMock;
    private readonly UsersController _controller;

    public UsersControllerTests()
    {
        // Set up in-memory database
        var options = new DbContextOptionsBuilder<EmployerPortalDbContext>()
            .UseInMemoryDatabase("EmployerPortalTestDb")
            .Options;

        _context = new EmployerPortalDbContext(options);

        // Seed data
        _context.Users.AddRange(
            new User { Username = "admin" },
            new User { Username = "developer" },
            new User { Username = "manager" }
        );
        _context.SaveChanges();

        // Set up logger mock
        _loggerMock = new Mock<ILogger<UsersController>>();

        // Create controller instance
        _controller = new UsersController(_context, _loggerMock.Object);
    }

    [Fact]
    public async Task Login_ReturnsBadRequest_WhenUsernameIsEmpty()
    {
        // Act
        var result = await _controller.Login("");

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(400, badRequestResult.StatusCode);
        var responseBody = badRequestResult.Value as dynamic;
        Assert.NotNull(responseBody);
        Assert.Equivalent(responseBody, new { message = "Username is required." });
    }

    [Fact]
    public async Task Login_ReturnsNotFound_WhenUsernameDoesNotExist()
    {
        // Act
        var result = await _controller.Login("nonexistent");

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal(404, notFoundResult.StatusCode);
        var responseBody = notFoundResult.Value as dynamic;
        Assert.NotNull(responseBody);
        Assert.Equivalent(responseBody, new { message = "Invalid username." });
    }

    [Fact]
    public async Task Login_ReturnsOk_WhenUsernameExists()
    {
        // Act
        var result = await _controller.Login("admin");

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
        var responseBody = okResult.Value as dynamic;
        Assert.NotNull(okResult);
        Assert.Equivalent(responseBody, new { message = $"Welcome admin" });
    }
}
