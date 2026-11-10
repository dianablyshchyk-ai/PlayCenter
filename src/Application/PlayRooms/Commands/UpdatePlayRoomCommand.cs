using Application.Common.Interfaces.Repositories;
using Domain.PlayRooms;
using MediatR;

namespace Application.PlayRooms.Commands;

public record UpdatePlayRoomCommand : IRequest<PlayRoom?>
{
    public required Guid PlayRoomId { get; init; }
    public required string RoomNumber { get; init; }
    public required string Name { get; init; }
    public required decimal PricePerHour { get; init; }
    public required int Capacity { get; init; }
    public required bool HasAnimator { get; init; }
    public required string Theme { get; init; }
}

public class UpdatePlayRoomCommandHandler(IPlayRoomRepository playRoomRepository)
    : IRequestHandler<UpdatePlayRoomCommand, PlayRoom?>
{
    public async Task<PlayRoom?> Handle(UpdatePlayRoomCommand request, CancellationToken cancellationToken)
    {
        var playRoom = await playRoomRepository.GetById(request.PlayRoomId, cancellationToken);
        if (playRoom is null)
        {
            return null;
        }

        var existingWithNumber = await playRoomRepository.GetByRoomNumber(request.RoomNumber, cancellationToken);
        if (existingWithNumber is not null && existingWithNumber.Id != request.PlayRoomId)
        {
            throw new ArgumentException($"PlayRoom with room number '{request.RoomNumber}' already exists");
        }

        // Змінено Update на UpdateDetails:
        playRoom.UpdateDetails(
            request.RoomNumber,
            request.Name,
            request.PricePerHour,
            request.Capacity,
            request.HasAnimator,
            Enum.Parse<PlayRoomTheme>(request.Theme, ignoreCase: true));

        return await playRoomRepository.Update(playRoom, cancellationToken);
    }
}