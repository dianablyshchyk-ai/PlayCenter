using Application.Common.Interfaces.Queries; 
using Application.Common.Interfaces.Repositories;
using Domain.PlayRooms;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class PlayRoomRepository(ApplicationDbContext context) 
    : IPlayRoomRepository, IPlayRoomQueries
{
    public async Task<IReadOnlyList<PlayRoom>> GetAll(CancellationToken cancellationToken)
    {
        return await context.PlayRooms
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<PlayRoom?> GetById(Guid id, CancellationToken cancellationToken)
    {
        return await context.PlayRooms.FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
    }

    public async Task<PlayRoom?> GetByRoomNumber(string roomNumber, CancellationToken cancellationToken)
    {
        return await context.PlayRooms.FirstOrDefaultAsync(
            r => r.RoomNumber.ToLower() == roomNumber.ToLower(), 
            cancellationToken);
    }

    public async Task<PlayRoom> Add(PlayRoom playRoom, CancellationToken cancellationToken)
    {
        await context.PlayRooms.AddAsync(playRoom, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return playRoom;
    }

    public async Task<PlayRoom> Update(PlayRoom playRoom, CancellationToken cancellationToken)
    {
        context.PlayRooms.Update(playRoom);
        await context.SaveChangesAsync(cancellationToken);
        return playRoom;
    }

    public async Task Delete(PlayRoom playRoom, CancellationToken cancellationToken)
    {
        context.PlayRooms.Remove(playRoom);
        await context.SaveChangesAsync(cancellationToken);
    }
}