using FluentValidation;
using MusicCollection.Models.Models;

namespace MusicCollection.Validators
{
    public class AddSongRequestValidator : AbstractValidator<Song>
    {
        public AddSongRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .MaximumLength(100)
                .MinimumLength(2);

            RuleForEach(x => x.Platforms)
                .NotEmpty().WithMessage("There must be at least one platform for this song!")
                .NotNull();
        }
    }
}
