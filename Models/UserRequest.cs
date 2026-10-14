using System.ComponentModel.DataAnnotations;

namespace UserManagementAPI.Models;

// Used for POST and PUT bodies so clients can't set the Id themselves.
public class UserRequest
{
    [Required(ErrorMessage = "Name is required.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters.")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Email format is invalid.")]
    public string? Email { get; set; }

    [Range(18, 100, ErrorMessage = "Age must be between 18 and 100.")]
    public int Age { get; set; }

    [Required(ErrorMessage = "Department is required.")]
    public string? Department { get; set; }
}
