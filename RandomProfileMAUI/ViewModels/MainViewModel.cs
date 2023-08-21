using System.Collections.ObjectModel;
using System.Windows.Input;
using RandomProfileMAUI.ViewModels;
using Microsoft.Toolkit.Mvvm.Input;
using RandomProfileMAUI.Data;
using RandomProfileMAUI.Models;

namespace RandomProfileMAUI.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private ObservableCollection<string> _profileImages;
        public ObservableCollection<string> ProfileImages
        {
            get => _profileImages;
            set => SetProperty(ref _profileImages, value);
        }

        private ObservableCollection<string> _profiles;
        public ObservableCollection<string> Profiles
        {
            get => _profiles;
            set => SetProperty(ref _profiles, value);
        }
        public ICommand NewCommand { get; }
        public ICommand SelectProfileCommand { get; }

        private string _selectedProfileImage;
        public string SelectedProfileImage
        {
            get => _selectedProfileImage;
            set => SetProperty(ref _selectedProfileImage, value);
        }

        private readonly ProfileRepository _profileRepository;
        private ObservableCollection<Profile> profilelist;

        public MainPageViewModel()
        {
            LoadProfileImages();
            LoadProfiles();
            NewCommand = new AsyncRelayCommand(NewprofileAsync);
            _profileRepository = new ProfileRepository();
        }

        private async Task NewprofileAsync()
        {
            await Shell.Current.GoToAsync(nameof(Views.AddProfilePage));
        }

        private void LoadProfileImages()
        {
            // Replace this with code to load profile images from your data source
            var images = new List<string>
        {
            "profile1.jpg",
            "profile2.jpg",
            "profile3.jpg"
        };

            ProfileImages = new ObservableCollection<string>(images);
        }

        private void LoadProfiles()
        {
            // Replace this with code to load profiles from your data source
            var profiles = new List<string>
        {
            "profile1.jpg",
            "profile2.jpg",
            "profile3.jpg"
        };

            Profiles = new ObservableCollection<string>(profiles);
        }
        public async Task LoadProfilesByProfileImage(int profileId)
        {
            try
            {
                profilelist = new ObservableCollection<Profile>(await _profileRepository.GetProfileImageByProfileImageId(profileId));
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}