using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using RandomProfileAPI.Models;

namespace RandomProfileAPI.Models
{
    [ModelMetadataType(typeof(ProfileMetaData))]
    public class Profile : IValidatableObject
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Bio { get; set; }
        public string NickName { get; set; }
        public DateTime DOB { get; set; }
        public int ProfileImageID { get; set; }
        public ProfileImage ProfileImage { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (DOB > DateTime.Today.AddDays(1))
            {
                yield return new ValidationResult("Date of Birth cannot be in the future.", new[] { "DOB" });
            }
        }
    }
}
