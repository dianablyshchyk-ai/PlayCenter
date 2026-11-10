using Application.Common.Interfaces.Repositories;
using MediatR;

namespace Application.PlayRooms.Commands;

public record DeletePlayRoomCommand : IRequest<bool>
{
    public required Guid PlayRoomId { get; init; }
}

public class DeletePlayRoomCommandHandler(IPlayRoomRepository playRoomRepository)
    : IRequestHandler<DeletePlayRoomCommand, bool>
{
    public async Task<bool> Handle(DeletePlayRoomCommand request, CancellationToken cancellationToken)
    {
        var playRoom = await playRoomRepository.GetById(request.PlayRoomId, cancellationToken);
        if (playRoom is null)
        {
            return false;
        }

        await playRoomRepository.Delete(playRoom, cancellationToken);
        return true;
    }
}