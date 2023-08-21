using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomProfileMAUI.Models
{
    public class ProfileImage
    {
        public int ProfileImageID { get; set; }
        public string Name { get; set; }
        public byte[] ProfileImageBytes { get; set; }
        public string Description { get; set; }
        public ICollection<Profile> Profiles { get; set; }

    }
}
