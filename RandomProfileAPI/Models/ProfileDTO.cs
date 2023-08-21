using Microsoft.AspNetCore.Mvc;
using RandomProfileAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace RandomProfileAPI.Models
{
    [ModelMetadataType(typeof(ProfileMetaData))]
    public class ProfileDTO : IValidatableObject
    {
        public int ID { get; set; }
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
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Bio { get; set; }
        public string NickName { get; set; }
        public DateTime DOB { get; set; }
        public int ProfileImageID { get; set; }
        public ProfileImageDTO ProfileImage { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (DOB > DateTime.Today.AddDays(1))
            {
                yield return new ValidationResult("Date of Birth cannot be in the future.", new[] { "DOB" });
            }
        }
    }
}
