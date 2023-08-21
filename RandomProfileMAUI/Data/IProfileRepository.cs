using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RandomProfileMAUI.Models;

namespace RandomProfileMAUI.Data
{
        public interface IProfileRepository
        {
            Task<List<Profile>> GetProfiles();
            Task<Profile> GetProfile(int id);
            Task<List<Profile>> GetProfileImageByProfileImageId(int profileId);
            Task AddProfile(Profile profile);
            Task UpdateProfile(Profile profile);
            Task DeleteProfile(int id);
        }
}
