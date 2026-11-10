namespace Domain.PlayRooms;
public class PlayRoom
{
    public Guid Id { get; }
    public string RoomNumber { get; private set; }
    public string Name { get; private set; }
    public decimal PricePerHour { get; private set; }
    public int Capacity { get; private set; }
    public bool HasAnimator { get; private set; }
    public PlayRoomTheme Theme { get; private set; }
    public DateTime CreatedAt { get; }

    private PlayRoom(Guid id, string roomNumber, string name, decimal pricePerHour, int capacity, bool hasAnimator, PlayRoomTheme theme, DateTime createdAt)
        => (Id, RoomNumber, Name, PricePerHour, Capacity, HasAnimator, Theme, CreatedAt) = (id, roomNumber, name, pricePerHour, capacity, hasAnimator, theme, createdAt);

    public static PlayRoom New(Guid id, string roomNumber, string name, decimal pricePerHour, int capacity, bool hasAnimator, PlayRoomTheme theme)
        => new(id, roomNumber, name, pricePerHour, capacity, hasAnimator, theme, DateTime.UtcNow);

    public void UpdateDetails(string roomNumber, string name, decimal pricePerHour, int capacity, bool hasAnimator, PlayRoomTheme theme)
        => (RoomNumber, Name, PricePerHour, Capacity, HasAnimator, Theme) = (roomNumber, name, pricePerHour, capacity, hasAnimator, theme);
}