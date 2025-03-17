using Moq;
using Moq.EntityFrameworkCore;
using Shouldly;
using VesselManagement.Application.Vessels.Commands;
using VesselManagement.Application.Vessels.Models;
using VesselManagement.Data;
using VesselManagement.Data.Entities;

namespace VesselManagement.Application.Tests;

public class RegisterVesselTests
{
    [Fact]
    public async Task Handle_ShouldAddVesselToDatabase()
    {
        // Arrange
        var dbContextMock = new Mock<VesselDbContext>();
        dbContextMock.Setup(x => x.Vessels)
            .ReturnsDbSet(new List<Vessel>());
        var handler = new RegisterVessel.Handler(dbContextMock.Object);
        var vesselModel = new RegisterVesselModel
        {
            Name = "Test Vessel",
            Imo = "12345678",
            Type = "Cargo",
            Capacity = 100000
        };
        var command = new RegisterVessel.Command(vesselModel);
        // Act
        var result = await handler.Handle(command, CancellationToken.None);
        // Assert
        result.ShouldNotBeNull();
        result.Id.ShouldNotBe(Guid.Empty);
        result.Name.ShouldBe(vesselModel.Name);
        result.Imo.ShouldBe(vesselModel.Imo);
        result.Type.ShouldBe(vesselModel.Type);
        result.Capacity.ShouldBe(vesselModel.Capacity);
        dbContextMock.Verify(x => x.Vessels.AddAsync(It.IsAny<Vessel>(), It.IsAny<CancellationToken>()), Times.Once);
        dbContextMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}