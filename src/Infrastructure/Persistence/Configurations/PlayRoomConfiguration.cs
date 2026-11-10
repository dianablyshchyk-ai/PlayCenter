using Domain.PlayRooms;
using Infrastructure.Persistence.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Infrastructure.Persistence.Configurations;

public class PlayRoomConfiguration : IEntityTypeConfiguration<PlayRoom>
{
    public void Configure(EntityTypeBuilder<PlayRoom> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.RoomNumber)
            .HasColumnType("varchar(20)")
            .IsRequired();

        builder.HasIndex(x => x.RoomNumber)
            .IsUnique();

        builder.Property(x => x.Name)
            .HasColumnType("varchar(100)")
            .IsRequired();

        builder.Property(x => x.PricePerHour)
            .HasColumnType("numeric(18,2)")
            .IsRequired();

        builder.Property(x => x.Capacity)
            .IsRequired();

        builder.Property(x => x.HasAnimator)
            .IsRequired();

        builder.Property(x => x.Theme)
            .HasConversion<string>()
            .HasColumnType("varchar(50)")
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .HasConversion(new DateTimeUtcConverter())
            .HasDefaultValueSql("timezone('utc', now())")
            .IsRequired();
    }
}