using CareWatch.Mobile.Models;

namespace CareWatch.Mobile.Views;

[QueryProperty(nameof(PatientId), "Id")]
public partial class PatientDetailsPage : ContentPage
{
    private Patient _patient;

    public PatientDetailsPage()
	{
		InitializeComponent();
	}

    public string PatientId
    {
        set
        {
            if (Guid.TryParse(value, out Guid patientGuid))
            {
                _patient = PatientRepository.GetPatientById(patientGuid);
                BindingContext = _patient;
            }
        }
    }

    private async void BackButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.Navigation.PopAsync();
    }

    private async void CancelButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.Navigation.PopAsync();
    }

    private async void EditPatientButton_Clicked(object sender, EventArgs e)
    {
        if (sender is Button editButton && editButton.BindingContext is Patient selectedPatient)
        {
            await Shell.Current.Navigation.PopAsync();
        }
    }

}