using Api.Dtos;
using Application.Common.Interfaces.Queries;
using Application.PlayRooms.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("playrooms")]
[ApiController]
public class PlayRoomsController(ISender sender, IPlayRoomQueries playRoomQueries) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<PlayRoomDto>>> GetPlayRooms(CancellationToken cancellationToken)
    {
        var playRooms = await playRoomQueries.GetAll(cancellationToken);
        return Ok(playRooms.Select(PlayRoomDto.FromDomainModel).ToList());
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<PlayRoomDto>> GetPlayRoom(Guid id, CancellationToken cancellationToken)
    {
        var playRoom = await playRoomQueries.GetById(id, cancellationToken);
        if (playRoom is null)
        {
            return NotFound();
        }

        return Ok(PlayRoomDto.FromDomainModel(playRoom));
    }

    [HttpPost]
    public async Task<ActionResult<PlayRoomDto>> CreatePlayRoom(
        [FromBody] CreatePlayRoomDto request,
        CancellationToken cancellationToken)
    {
        var command = new CreatePlayRoomCommand
        {
            RoomNumber = request.RoomNumber,
            Name = request.Name,
            PricePerHour = request.PricePerHour,
            Capacity = request.Capacity,
            HasAnimator = request.HasAnimator,
            Theme = request.Theme.ToString()
        };

        try
        {
            var playRoom = await sender.Send(command, cancellationToken);
            var dto = PlayRoomDto.FromDomainModel(playRoom);
            return CreatedAtAction(nameof(GetPlayRoom), new { id = dto.Id }, dto);
        }
        catch (ArgumentException exception)
        {
            return Conflict(exception.Message);
        }
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<PlayRoomDto>> UpdatePlayRoom(
        Guid id,
        [FromBody] UpdatePlayRoomDto request,
        CancellationToken cancellationToken)
    {
        var command = new UpdatePlayRoomCommand
        {
            PlayRoomId = id,
            RoomNumber = request.RoomNumber,
            Name = request.Name,
            PricePerHour = request.PricePerHour,
            Capacity = request.Capacity,
            HasAnimator = request.HasAnimator,
            Theme = request.Theme.ToString()
        };

        try
        {
            var playRoom = await sender.Send(command, cancellationToken);
            if (playRoom is null)
            {
                return NotFound();
            }

            return Ok(PlayRoomDto.FromDomainModel(playRoom));
        }
        catch (ArgumentException exception)
        {
            return Conflict(exception.Message);
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeletePlayRoom(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeletePlayRoomCommand { PlayRoomId = id };
        var result = await sender.Send(command, cancellationToken);
        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}