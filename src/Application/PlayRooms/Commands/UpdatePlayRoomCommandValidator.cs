using Domain.PlayRooms;
using FluentValidation;

namespace Application.PlayRooms.Commands;

public class UpdatePlayRoomCommandValidator : AbstractValidator<UpdatePlayRoomCommand>
{
    public UpdatePlayRoomCommandValidator()
    {
        RuleFor(x => x.PlayRoomId)
            .NotEmpty();

        RuleFor(x => x.RoomNumber)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(255);

        RuleFor(x => x.PricePerHour)
            .InclusiveBetween(0.01m, 1_000_000m);

        RuleFor(x => x.Capacity)
            .InclusiveBetween(1, 1000);

        RuleFor(x => x.Theme)
            .NotEmpty()
            .IsEnumName(typeof(PlayRoomTheme), caseSensitive: false);
    }
}