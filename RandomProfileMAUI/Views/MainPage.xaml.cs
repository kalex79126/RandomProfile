using RandomProfileMAUI.ViewModels;
using RandomProfileMAUI.Views;

namespace RandomProfileMAUI.Views;

public partial class MainPage : ContentPage
{
	public MainPageViewModel vm; 
	public MainPage()
	{
		InitializeComponent();
	}

    private async void ddlProfileImage_SelectedIndexChanged(object sender, EventArgs e)
    {
		await vm.LoadProfilesByProfileImage(ddlProfileImage.SelectedIndex);
    }
}