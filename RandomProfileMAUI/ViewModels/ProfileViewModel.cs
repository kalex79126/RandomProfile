using RandomProfileMAUI.Models;
using RandomProfileMAUI.Data;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.Input;

namespace RandomProfileMAUI.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        private readonly ProfileRepository _profile;
        private ObservableCollection<Profile> _profiles;

        public ICommand SaveCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }

        public ProfileViewModel()
        {
            _profile = new ProfileRepository();

        }

        public ObservableCollection<Profile> Profiles
        {
            get { return _profiles; }
            set { SetProperty(ref _profiles, value); }
        }


        private async Task SelectProfileAsync(Profile profile)
        {
            if (profile != null)
                await Shell.Current.GoToAsync($"{nameof(Views.ProfileDetailedPage)}?load={profile.ID}");
        }

        public async Task LoadProfiles()
        {
            try
            {
                Profiles = new ObservableCollection<Profile>(await _profile.GetProfiles());
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }

        }
            public async Task LoadProfileByID(int Id)
        {
            try
            {
                Profile profile = await _profile.GetProfile(Id);
                Profiles = new ObservableCollection<Profile>() { profile };
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        public async Task LoadProfilesByProfileImage(int profileId)
        {
            try
            {
                Profiles = new ObservableCollection<Profile>(await _profile.GetProfileImageByProfileImageId(profileId));
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        public async Task AddProfile(Profile profile)
        {
            try
            {
                await _profile.AddProfile(profile);
                await App.Current.MainPage.DisplayAlert("Success", "Profile added", "OK");
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        public async Task EditProfile(Profile profile)
        {
            try
            {
                await _profile.UpdateProfile(profile);
                await App.Current.MainPage.DisplayAlert("Success", "Profile updated", "OK");
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        public async Task DeleteProfile(int ID)
        {
            try
            {
                await _profile.DeleteProfile(ID);
                await App.Current.MainPage.Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}