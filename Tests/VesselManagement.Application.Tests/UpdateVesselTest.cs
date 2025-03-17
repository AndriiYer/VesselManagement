using Moq;
using Moq.EntityFrameworkCore;
using Shouldly;
using VesselManagement.Application.Vessels.Commands;
using VesselManagement.Application.Vessels.Models;
using VesselManagement.Data;
using VesselManagement.Data.Entities;

namespace VesselManagement.Application.Tests;

public class UpdateVesselTest
{
    [Fact]
    public async Task Handle_ShouldUpdateVesselInDatabase()
    {
        // Arrange
        var dbContextMock = new Mock<VesselDbContext>();
        dbContextMock.Setup(x => x.Vessels)
            .ReturnsDbSet([
                new Vessel
                {
                    Id = new Guid("163E8579-6920-4BFB-AB29-3AB446CEED09"),
                    Name = "TestVessel",
                    Imo = "1234567",
                    Type = "Cargo",
                    Capacity = 100000
                }]);
        var handler = new UpdateVessel.Handler(dbContextMock.Object);
        var vesselModel = new VesselModel
        {
            Id = new Guid("163E8579-6920-4BFB-AB29-3AB446CEED09"),
            Name = "Test Vessel 2",
            Imo = "123456789",
            Type = "Tanker",
            Capacity = 200000
        };
        var command = new UpdateVessel.Command(vesselModel);
        // Act
        var result = await handler.Handle(command, CancellationToken.None);
        // Assert
        result.ShouldNotBeNull();
        result.Id.ShouldBe(vesselModel.Id);
        result.Name.ShouldBe(vesselModel.Name);
        result.Imo.ShouldBe(vesselModel.Imo);
        result.Type.ShouldBe(vesselModel.Type);
        result.Capacity.ShouldBe(vesselModel.Capacity);
        dbContextMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}