using System.Text;
using System.Threading.Tasks;
using RandomProfileMAUI.Data;
using RandomProfileMAUI.Utilities;
using RandomProfileMAUI.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
namespace RandomProfileMAUI.Data
{
    public class ProfileRepository : IProfileRepository
    {
        readonly HttpClient client = new HttpClient();

        public ProfileRepository()
        {
            client.BaseAddress = Jeeves.DBUri;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<Profile>> GetProfiles()
        {
            HttpResponseMessage response = await client.GetAsync("api/Profiles");
            if (response.IsSuccessStatusCode)
            {
                List<Profile> profiles = await response.Content.ReadAsAsync<List<Profile>>();
                return profiles;
            }
            else
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }

        public async Task<Profile> GetProfile(int id)
        {
            var response = await client.GetAsync($"api/Profiles/{id}");
            if (response.IsSuccessStatusCode)
            {
                Profile profile = await response.Content.ReadAsAsync<Profile>();
                return profile;
            }
            else
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }

        public async Task<List<Profile>> GetProfileImageByProfileImageId(int profileId)
        {
            HttpResponseMessage response = await client.GetAsync($"api/Profile/ByProfileImage/{profileId}");
            if (response.IsSuccessStatusCode)
            {
                List<Profile> profile = await response.Content.ReadAsAsync < List<Profile>>();
                return profile;
            }
            else
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }

        public async Task AddProfile(Profile profile)
        {
            var serializedProfile = JsonConvert.SerializeObject(profile);
            var content = new StringContent(serializedProfile, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync("api/Profiles", content);
            if (!response.IsSuccessStatusCode)
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }

        public async Task UpdateProfile(Profile profile)
        {
            var serializedProfile = JsonConvert.SerializeObject(profile);
            var content = new StringContent(serializedProfile, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PutAsync($"api/Profiles/{profile.ID}", content);
            if (!response.IsSuccessStatusCode)
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }

        public async Task DeleteProfile(int id)
        {
            HttpResponseMessage response = await client.DeleteAsync($"api/Profiles/{id}");
            if (!response.IsSuccessStatusCode)
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }
    }
}
