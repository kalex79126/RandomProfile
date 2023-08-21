using Microsoft.AspNetCore.Mvc;
using RandomProfileAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RandomProfileAPI.Models
{
    [ModelMetadataType(typeof(ProfileImageMetaData))]
    public class ProfileImage
    {
        public int ProfileImageID { get; set; }
        public string Name { get; set; }
        public byte[] ProfileImageBytes { get; set; }
        public string Description { get; set; }
        public ICollection<Profile> Profiles { get; set; }

    }
}
