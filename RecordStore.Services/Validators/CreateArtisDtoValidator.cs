using FluentValidation;
using RecordStore.Services.DTOs;

namespace RecordStore.Services.Validators
{
    public class CreateArtistDtoValidator : AbstractValidator<CreateArtistDto>
    {
        public CreateArtistDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Artist name is required")
                .MaximumLength(100).WithMessage("Artist name cannot exceed 100 characters")
                .MinimumLength(1).WithMessage("Artist name must be at least 1 character");

            RuleFor(x => x.Biography)
                .MaximumLength(500).WithMessage("Biography cannot exceed 500 characters")
                .When(x => !string.IsNullOrEmpty(x.Biography));

            RuleFor(x => x.Country)
                .MaximumLength(100).WithMessage("Country cannot exceed 100 characters")
                .When(x => !string.IsNullOrEmpty(x.Country));

            RuleFor(x => x.FormedDate)
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Formation date cannot be in the future")
                .When(x => x.FormedDate.HasValue);
        }
    }
}
