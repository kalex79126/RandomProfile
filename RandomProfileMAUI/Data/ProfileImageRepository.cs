using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RandomProfileMAUI.Models;
using RandomProfileMAUI.Utilities;

namespace RandomProfileMAUI.Data
{
    public class ProfileImageRepository : IProfileImageRepository
    {
        readonly HttpClient client = new HttpClient();
        private readonly string apiUrl = "api/ProfileImages";

        public ProfileImageRepository()
        {
            client.BaseAddress = Jeeves.DBUri;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<ProfileImage>> GetProfileImages()
        {
            HttpResponseMessage response = await client.GetAsync($"api/GetProfileImages");
            if (response.IsSuccessStatusCode)
            {
                List<ProfileImage> profileImages = await response.Content.ReadAsAsync<List<ProfileImage>>();
                return profileImages;
            }
            else
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }

        public async Task<ProfileImage> GetProfileImage(int id)
        {
            HttpResponseMessage response = await client.GetAsync($"api/ProfileImages/{id}");
            if (response.IsSuccessStatusCode)
            {
                ProfileImage profileImage = await response.Content.ReadAsAsync<ProfileImage>();
                return profileImage;
            }
            else
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }

        public async Task<ProfileImage> GetProfileImageByProfileId(int profileId)
        {
            HttpResponseMessage response = await client.GetAsync($"api/ProfileImages/{profileId}");
            if (response.IsSuccessStatusCode)
            {
                ProfileImage profileImage = await response.Content.ReadAsAsync<ProfileImage>();
                return profileImage;
            }
            else
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }

        public async Task AddProfileImage(ProfileImage profileImage)
        {
            var serializedProfileImage = JsonConvert.SerializeObject(profileImage);
            var content = new StringContent(serializedProfileImage, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(apiUrl, content);
            if (!response.IsSuccessStatusCode)
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }

        public async Task UpdateProfileImage(ProfileImage profileImage)
        {
            var serializedProfile = JsonConvert.SerializeObject(profileImage);
            var content = new StringContent(serializedProfile, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PutAsync($"api/Profiles/{profileImage.ProfileImageID}", content);
            if (!response.IsSuccessStatusCode)
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }

        public async Task DeleteProfileImage(int id)
        {
            HttpResponseMessage response = await client.DeleteAsync($"api/ProfileImages/{id}");
            if (!response.IsSuccessStatusCode)
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }
    }
}
