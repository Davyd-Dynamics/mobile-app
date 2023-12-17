using CareWatch.Mobile.Models;

namespace CareWatch.Mobile.Views
{
    [QueryProperty(nameof(PatientId), "Id")]
    public partial class EditPatientPage : ContentPage
    {
        private Patient _patient;

        public EditPatientPage()
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

                    if (_patient != null)
                    {
                        patientCtrl.FirstName = _patient.Contact.FirstName;
                        patientCtrl.LastName = _patient.Contact.LastName;
                        patientCtrl.Phone = _patient.Contact.Phone;
                        patientCtrl.Email = _patient.Contact.Email;
                    }
                }              
            }
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            _patient.Contact.FirstName = patientCtrl.FirstName;
            _patient.Contact.LastName = patientCtrl.LastName;
            _patient.Contact.Phone = patientCtrl.Phone;
            _patient.Contact.Email = patientCtrl.Email;

            PatientRepository.UpdatePatient(_patient);
            await Shell.Current.Navigation.PopAsync();
        }

        private async void CancelButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.Navigation.PopAsync();
        }
    }
}
