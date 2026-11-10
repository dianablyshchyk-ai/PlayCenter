using Domain.PlayRooms;
namespace Application.Common.Interfaces.Queries;

public interface IPlayRoomQueries
{
    Task<IReadOnlyList<PlayRoom>> GetAll(CancellationToken cancellationToken);
    Task<PlayRoom?> GetById(Guid id, CancellationToken cancellationToken);
}