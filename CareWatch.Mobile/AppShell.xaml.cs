using CareWatch.Mobile.Views;

namespace CareWatch.Mobile
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(PatientsPage), typeof(PatientsPage));
            Routing.RegisterRoute(nameof(PatientDetailsPage), typeof(PatientDetailsPage));
            Routing.RegisterRoute(nameof(EditPatientPage), typeof(EditPatientPage));
        }
    }
}
