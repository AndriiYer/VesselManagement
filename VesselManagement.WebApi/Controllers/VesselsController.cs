using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using VesselManagement.Application.Vessels.Commands;
using VesselManagement.Application.Vessels.Models;
using VesselManagement.Application.Vessels.Queries;

namespace VesselManagement.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Consumes("application/json")]
[Produces("application/json")]
public class VesselsController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    /// <summary>
    /// Registers a new vessel in the system.
    /// </summary>
    /// <param name="model">
    /// The <see cref="RegisterVesselModel"/> containing the details of the vessel to be created.
    /// </param>
    /// <returns>
    /// An <see cref="IActionResult"/> containing:
    /// <list type="bullet">
    /// <item>
    /// <description>A <see cref="VesselModel"/> representing the registered vessel (HTTP 200).</description>
    /// </item>
    /// </list>
    /// </returns>
    /// <remarks>
    /// This endpoint is used to add a new vessel to the system. The request body must include all required vessel details.
    /// </remarks>
    [HttpPost]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(VesselModel))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(void))]
    public async Task<IActionResult> RegisterVessel([FromBody]RegisterVesselModel model)
    {
        var query = new RegisterVessel.Command(model);
        var response = await _mediator.Send(query);

        return Ok(response);
    }

    /// <summary>
    /// Updates an existing vessel in the system.
    /// </summary>
    /// <param name="model">
    /// The <see cref="VesselModel"/> containing the updated details of the vessel.
    /// </param>
    /// <returns>
    /// An <see cref="IActionResult"/> containing:
    /// <list type="bullet">
    /// <item>
    /// <description>A <see cref="VesselModel"/> representing the updated vessel (HTTP 200).</description>
    /// </item>
    /// <item>
    /// <description>An empty response indicating a bad request if the input is invalid (HTTP 400).</description>
    /// </item>
    /// <item>
    /// <description>An empty response indicating the vessel was not found (HTTP 404).</description>
    /// </item>
    /// </list>
    /// </returns>
    /// <remarks>
    /// This endpoint is used to update the details of an existing vessel. The request body must include all required vessel details.
    /// </remarks>
    [HttpPut]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(VesselModel))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(void))]
    [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(void))]
    public async Task<IActionResult> UpdateVessel([FromBody] VesselModel model)
    {
        var query = new UpdateVessel.Command(model);
        var response = await _mediator.Send(query);

        return Ok(response);
    }

    /// <summary>
    /// Retrieves a list of all vessels.
    /// </summary>
    /// <returns>
    /// An <see cref="IActionResult"/> containing:
    /// <list type="bullet">
    /// <item>
    /// <description>A list of <see cref="VesselModel"/> objects representing the vessels (HTTP 200).</description>
    /// </item>
    /// </list>
    /// </returns>
    /// <remarks>
    /// This endpoint is used to fetch all vessels stored in the system.
    /// </remarks>
    [HttpGet]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(List<VesselModel>))]
    public async Task<IActionResult> GetVessels()
    {
        var query = new GetAllVessels.Query();
        var response = await _mediator.Send(query);

        return Ok(response);
    }

    /// <summary>
    /// Retrieves a specific vessel by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier (GUID) of the vessel to retrieve.</param>
    /// <returns>
    /// An <see cref="IActionResult"/> containing:
    /// <list type="bullet">
    /// <item>
    /// <description>A <see cref="VesselModel"/> with the vessel details if found (HTTP 200).</description>
    /// </item>
    /// <item>
    /// <description>An empty response if the vessel is not found (HTTP 404).</description>
    /// </item>
    /// </list>
    /// </returns>
    /// <remarks>
    /// This endpoint is used to fetch details of a specific vessel using its GUID.
    /// </remarks>
    [HttpGet("{id:guid}")]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(VesselModel))]
    [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(void))]
    public async Task<IActionResult> GetVessel([FromRoute]Guid id)
    {
        var query = new GetVesselById.Query(id);
        var response = await _mediator.Send(query);

        return Ok(response);
    }
}