using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RandomProfileMAUI.Models;

namespace RandomProfileMAUI.Data
{
        public interface IProfileImageRepository
        {
            Task<List<ProfileImage>> GetProfileImages();
            Task<ProfileImage> GetProfileImage(int id);
            Task<ProfileImage> GetProfileImageByProfileId(int profileId);
            Task AddProfileImage(ProfileImage profileImage);
            Task UpdateProfileImage(ProfileImage profileImage);
            Task DeleteProfileImage(int id);
        }
}
