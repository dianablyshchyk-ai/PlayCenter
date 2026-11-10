using Domain.PlayRooms;

namespace Api.Dtos;

public record PlayRoomDto(
    Guid Id,
    string RoomNumber,
    string Name,
    decimal PricePerHour,
    int Capacity,
    bool HasAnimator,
    PlayRoomTheme Theme,
    DateTime CreatedAt)
{
    public static PlayRoomDto FromDomainModel(PlayRoom room)
        => new(
            room.Id,
            room.RoomNumber,
            room.Name,
            room.PricePerHour,
            room.Capacity,
            room.HasAnimator,
            room.Theme,
            room.CreatedAt);
}

public record CreatePlayRoomDto(
    string RoomNumber,
    string Name,
    decimal PricePerHour,
    int Capacity,
    bool HasAnimator,
    PlayRoomTheme Theme);

public record UpdatePlayRoomDto(
    string RoomNumber,
    string Name,
    decimal PricePerHour,
    int Capacity,
    bool HasAnimator,
    PlayRoomTheme Theme);