using System.ComponentModel.DataAnnotations;

namespace UserManagementAPI.DTOs;

public class UserUpdateDto : IValidatableObject
{
    [Required, MinLength(2), MaxLength(100)]
    public string FirstName { get; set; } = string.Empty;

    [Required, MinLength(2), MaxLength(100)]
    public string LastName { get; set; } = string.Empty;

    [Required, EmailAddress, MaxLength(200)]
    public string Email { get; set; } = string.Empty;

    [Required, MinLength(2), MaxLength(100)]
    public string Department { get; set; } = string.Empty;
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrWhiteSpace(FirstName))
            yield return new ValidationResult("FirstName cannot be empty.", new[] { nameof(FirstName) });
        if (string.IsNullOrWhiteSpace(LastName))
            yield return new ValidationResult("LastName cannot be empty.", new[] { nameof(LastName) });
        if (string.IsNullOrWhiteSpace(Email))
            yield return new ValidationResult("Email cannot be empty.", new[] { nameof(Email) });
        if (string.IsNullOrWhiteSpace(Department))
            yield return new ValidationResult("Department cannot be empty.", new[] { nameof(Department) });
    }

}
