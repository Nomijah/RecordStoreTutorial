using System.ComponentModel.DataAnnotations;

namespace RecordStore.Services.Attributes
{
    public class FutureDateValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            // Allow nulls (if your property is nullable, let [Required] handle non-nullable checks)
            if (value == null)
                return true;

            // If it's a DateTime, make sure it's not in the future.
            if (value is DateTime dt)
                return dt <= DateTime.Now;

            // Any other type: you probably aren’t validating that here, so say “valid.”
            return true;
        }

        public override string FormatErrorMessage(string name)
        {
            return $"{name} cannot be in the future.";
        }
    }
}
