using System.ComponentModel.DataAnnotations;

namespace RandomProfileAPI.Models
{
    public class ProfileImageMetaData
    {
        [Display(Name = "Profile Image")]
        [Required(ErrorMessage = "Profile Image name cannot be left blank.")]
        [StringLength(50, ErrorMessage = "Profile Image cannot be more than 50 characters long.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Profile Image us required.")]
        public byte[] ProfileImageBytes { get; set; }

        [Display(Name = "Profile Image Desciprtion")]
        [StringLength(200, ErrorMessage = "Profile Image Desciprtion cannot be more than 200 characters long.")]
        public string Description { get; set; }
    }
}
