using System.ComponentModel.DataAnnotations;

namespace TelecomApp.Models.MetaData
{
    public partial class UserMetaData
    {
        [Display(Name = "First Name")]
        //[Required(ErrorMessage = "First Name is required")]
        //[CustomValidation(typeof(UserMetaData), nameof(ValidateFirstName))]
        public string? FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string? LastName { get; set; }

        [Display(Name = "Username")]
        public int UserId { get; set; }

        //public static ValidationResult? ValidateFirstName(string? firstName, ValidationContext context)
        //{
        //    if (string.IsNullOrWhiteSpace(firstName))
        //    {
        //        return new ValidationResult("First Name cannot be empty or whitespace.");
        //    }
        //    return ValidationResult.Success;
        //}
    }
}