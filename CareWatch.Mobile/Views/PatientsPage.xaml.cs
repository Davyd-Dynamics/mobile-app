using CareWatch.Mobile.Models;
using System.Collections.ObjectModel;

namespace CareWatch.Mobile.Views
{
    public partial class PatientsPage : ContentPage
    {
        public PatientsPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            //searchBar.Text = string.Empty;

            await LoadPatients();
        }

        private async void patientsList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                var selectedPatient = (Patient)e.SelectedItem;

                await Shell.Current.GoToAsync($"{nameof(PatientDetailsPage)}?Id={selectedPatient.Id}");

                patientsList.SelectedItem = null;
            }
        }

        private void patientsList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            patientsList.SelectedItem = null;
        }

        private async void EditPatientButton_Clicked(object sender, EventArgs e)
        {
            if (sender is MenuItem menuItem && menuItem.BindingContext is Patient selectedPatient)
            {
                await Shell.Current.GoToAsync($"{nameof(EditPatientPage)}?Id={selectedPatient.Id}");
            }
        }

        private async void DeletePatientButton_Clicked(object sender, EventArgs e)
        {
            var menuItem = sender as MenuItem;
            var patient = menuItem.BindingContext as Patient;
            PatientRepository.DeletePatient(patient.Id);
            await LoadPatients();
        }

        //private void searchBar_SearchButtonPressed(object sender, EventArgs e)
        //{
        //    string searchText = searchBar.Text;
        //    patientsList.ItemsSource = new ObservableCollection<Patient>(PatientRepository.SearchPatients(searchText));
        //}

        private async Task LoadPatients()
        {
            PatientApiRepository patientApiRepository = new PatientApiRepository();
            var patients = await patientApiRepository.GetAllPatientsAsync();
            patientsList.ItemsSource = new ObservableCollection<Patient>(patients);
        }
    }
}
