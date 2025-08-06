using FluentValidation;
using RecordStore.Services.DTOs;

namespace RecordStore.Services.Validators
{
    public class UpdateAlbumDtoValidator : AbstractValidator<UpdateAlbumDto>
    {
        public UpdateAlbumDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Album title is required")
                .MaximumLength(200).WithMessage("Album title cannot exceed 200 characters")
                .MinimumLength(1).WithMessage("Album title must be at least 1 character");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0")
                .LessThanOrEqualTo(9999.99m).WithMessage("Price cannot exceed $9999.99")
                .PrecisionScale(6, 2, false).WithMessage("Price can have maximum 2 decimal places");

            RuleFor(x => x.ReleaseDate)
                .NotEmpty().WithMessage("Release date is required")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Release date cannot be in the future");

            RuleFor(x => x.CatalogNumber)
                .MaximumLength(20).WithMessage("Catalog number cannot exceed 20 characters")
                .When(x => !string.IsNullOrEmpty(x.CatalogNumber));

            RuleFor(x => x.Description)
                .MaximumLength(1000).WithMessage("Description cannot exceed 1000 characters")
                .When(x => !string.IsNullOrEmpty(x.Description));

            RuleFor(x => x.Stock)
                .GreaterThanOrEqualTo(0).WithMessage("Stock cannot be negative")
                .LessThanOrEqualTo(9999).WithMessage("Stock cannot exceed 9999");

            RuleFor(x => x.TrackCount)
                .GreaterThan(0).WithMessage("Track count must be greater than 0")
                .LessThanOrEqualTo(99).WithMessage("Track count cannot exceed 99");

            RuleFor(x => x.DurationMinutes)
                .GreaterThan(0).WithMessage("Duration must be greater than 0 minutes")
                .LessThanOrEqualTo(999).WithMessage("Duration cannot exceed 999 minutes");

            RuleFor(x => x.ArtistId)
                .GreaterThan(0).WithMessage("Valid artist must be selected");

            RuleFor(x => x.GenreId)
                .GreaterThan(0).WithMessage("Valid genre must be selected");
        }
    }
}
