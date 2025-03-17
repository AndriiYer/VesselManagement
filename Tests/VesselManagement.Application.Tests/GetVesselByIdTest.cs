using Moq;
using Moq.EntityFrameworkCore;
using Shouldly;
using VesselManagement.Application.Vessels.Queries;
using VesselManagement.Data;
using VesselManagement.Data.Entities;

namespace VesselManagement.Application.Tests;

public class GetVesselByIdTest
{
    [Fact]
    public async Task Handle_ShouldGetVesselByIdFromDatabase()
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
                },
                new Vessel
                {
                    Id = new Guid("1BF2CD17-71B3-4BD9-B594-6F43FD03DB90"),
                    Name = "TestVessel2",
                    Imo = "123",
                    Type = "Tanker",
                    Capacity = 200000
                }]);
        var handler = new GetVesselById.Handler(dbContextMock.Object);
        var query = new GetVesselById.Query(new Guid("163E8579-6920-4BFB-AB29-3AB446CEED09"));
        // Act
        var result = await handler.Handle(query, CancellationToken.None);
        // Assert
        result.ShouldNotBeNull();
        result.Name.ShouldBe("TestVessel");
    }
}