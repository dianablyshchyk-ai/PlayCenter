using Domain.PlayRooms;
namespace Application.Common.Interfaces.Repositories;

public interface IPlayRoomRepository
{
    Task<PlayRoom?> GetById(Guid id, CancellationToken cancellationToken);
    Task<PlayRoom?> GetByRoomNumber(string roomNumber, CancellationToken cancellationToken);
    Task<PlayRoom> Add(PlayRoom playRoom, CancellationToken cancellationToken);
    Task<PlayRoom> Update(PlayRoom playRoom, CancellationToken cancellationToken);
    Task Delete(PlayRoom playRoom, CancellationToken cancellationToken);
}