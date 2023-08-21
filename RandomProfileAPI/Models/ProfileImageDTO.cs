using Microsoft.AspNetCore.Mvc;
using RandomProfileAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace RandomProfileAPI.Models
{
    [ModelMetadataType(typeof(ProfileMetaData))]

    public class ProfileImageDTO
    {
        public int ProfileImageID { get; set; }
        public string Name { get; set; }
        public byte[] ProfileImageBytes { get; set; }
        public string Description { get; set; }
        public ICollection<ProfileDTO> Profiles { get; set; }
    }
}
