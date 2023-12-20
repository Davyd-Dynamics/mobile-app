using CareWatch.Mobile.Models.Entities;
using CareWatch.Mobile.Models.Services;

namespace CareWatch.Mobile.Views;

[QueryProperty(nameof(DoctorId), "Id")]
public partial class DoctorDetailsPage : ContentPage
{
    private Doctor _doctor;

    public DoctorDetailsPage()
    {
        InitializeComponent();
    }

    public string DoctorId
    {
        set
        {
            if (Guid.TryParse(value, out Guid doctorGuid))
            {
                var apiRepository = Application.Current.Handler.MauiContext.Services.GetService<DoctorApiRepository>();
                _doctor = apiRepository.GetDoctorByIdAsync(doctorGuid).Result;
                BindingContext = _doctor;
            }
        }
    }

    private async void BackButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.Navigation.PopAsync();
    }
}