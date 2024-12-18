
using System.ComponentModel.DataAnnotations;

namespace Business.Models;

public class ContactModel
{

    [Required(ErrorMessage = "Firstname is required.")]
    [MinLength(2, ErrorMessage = "The name must contain at least two characters")]
    public string FirstName { get; set; } = null!;

    [Required(ErrorMessage = "Lastname is required.")]
    [MinLength(2, ErrorMessage = "The lastname must contain at least two characters")]

    public string LastName { get; set; } = null!;

    [Required(ErrorMessage = "Email is required.")]
    [RegularExpression(@"^[\p{L}0-9._%+-]+@[\p{L}0-9.-]+\.[\p{L}]{2,}$", ErrorMessage = "Invalid email format, try again.")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Phone number is required.")]
    [Phone(ErrorMessage = "Invalid phone number format, try again.")]
    public string Phone { get; set; } = null!;

    [Required(ErrorMessage = "Lastname is required.")]
    public string Address { get; set; } = null!;

    [Required(ErrorMessage = "Lastname is required.")]
    public string PostalCode { get; set; } = null!;

    [Required(ErrorMessage = "Lastname is required.")]
    public string City { get; set; } = null!;
    public string FullName => $"{FirstName} {LastName}";

}
