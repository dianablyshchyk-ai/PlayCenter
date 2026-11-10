using Application.Common.Interfaces.Repositories;
using Domain.PlayRooms;
using MediatR;
namespace Application.PlayRooms.Commands;

public record CreatePlayRoomCommand : IRequest<PlayRoom>
{
    public required string RoomNumber { get; init; }
    public required string Name { get; init; }
    public required decimal PricePerHour { get; init; }
    public required int Capacity { get; init; }
    public required bool HasAnimator { get; init; }
    public required string Theme { get; init; }
}

public class CreatePlayRoomCommandHandler(IPlayRoomRepository playRoomRepository)
    : IRequestHandler<CreatePlayRoomCommand, PlayRoom>
{
    public async Task<PlayRoom> Handle(CreatePlayRoomCommand request, CancellationToken cancellationToken)
    {
        var existingRoom = await playRoomRepository.GetByRoomNumber(request.RoomNumber, cancellationToken);
        if (existingRoom is not null)
        {
            throw new ArgumentException($"PlayRoom with room number '{request.RoomNumber}' already exists");
        }

        var playRoom = PlayRoom.New(
            Guid.NewGuid(),
            request.RoomNumber,
            request.Name,
            request.PricePerHour,
            request.Capacity,
            request.HasAnimator,
            Enum.Parse<PlayRoomTheme>(request.Theme, ignoreCase: true));

        return await playRoomRepository.Add(playRoom, cancellationToken);
    }
}