using RandomProfileAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace RandomProfileAPI.Models
{
    public class ProfileMetaData : IValidatableObject
    {
        [Display(Name = "User ")]
        public string FullName
        {
            get
            {
                return FirstName
                    + (string.IsNullOrEmpty(MiddleName) ? " " :
                        (" " + (char?)MiddleName[0] + ". ").ToUpper())
                    + LastName;
            }
        }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "The first name cannot be left blank.")]
        [StringLength(50, ErrorMessage = "First name cannot be more than 50 characters long.")]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        [StringLength(50, ErrorMessage = "Middle name cannot be more than 50 characters long.")]
        public string MiddleName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "The last name cannot be left blank.")]
        public string LastName { get; set; }

        [Display(Name = "Bio")]
        [StringLength(200, ErrorMessage = "Bio cannot be more than 200 characters long.")]
        public string Bio { get; set; }

        [Display(Name = "Nickname")]
        [StringLength(50, ErrorMessage = "Nickname cannot be more than 20 characters long.")]
        public string NickName { get; set; }

        [Required(ErrorMessage = "You must enter the Date of Birth.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DOB { get; set; }

        [Display(Name = "Profile Image")]
        [Required(ErrorMessage = "You must select the profile image.")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a profile image.")]
        public int ProfileImageID { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (DOB > DateTime.Today.AddDays(1))
            {
                yield return new ValidationResult("Date of Birth cannot be in the future.", new[] { "DOB" });
            }
        }
    }
}
