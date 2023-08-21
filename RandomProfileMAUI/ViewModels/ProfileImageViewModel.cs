using RandomProfileMAUI.Models;
using RandomProfileMAUI.Data;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace RandomProfileMAUI.ViewModels
{
    public class ProfileImageViewModel : BaseViewModel
    {
        private readonly ProfileImageRepository _profileImage;
        private ObservableCollection<ProfileImage> _profileImages;

        public ObservableCollection<ProfileImage> ProfileImages
        {
            get { return _profileImages; }
            set { SetProperty(ref _profileImages, value); }
        }

        public ProfileImageViewModel()
        {
            _profileImage = new ProfileImageRepository();
        }

        public async Task LoadProfileImages()
        {
            try
            {
                ProfileImages = new ObservableCollection<ProfileImage>(await _profileImage.GetProfileImages());
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }


        public async Task LoadProfileImageByID(int Id)
        {
            try
            {
                ProfileImage profileImage = await _profileImage.GetProfileImage(Id);
                ProfileImages = new ObservableCollection<ProfileImage>() { profileImage };
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        public async Task AddProfile(ProfileImage profileImage)
        {
            try
            {
                await _profileImage.AddProfileImage(profileImage);
                await App.Current.MainPage.DisplayAlert("Success", "Profile image added", "OK");
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        public async Task EditProfile(ProfileImage profileImage)
        {
            try
            {
                await _profileImage.UpdateProfileImage(profileImage);
                await App.Current.MainPage.DisplayAlert("Success", "Profile updated", "OK");
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        public async Task DeleteProfileImage(int ID)
        {
            try
            {
                await _profileImage.DeleteProfileImage(ID);
                await App.Current.MainPage.Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}
