namespace CareWatch.Mobile.Views.Controls
{
    public partial class PatientControl : ContentView
    {
        public PatientControl()
        {
            InitializeComponent();
        }

        public string FirstName
        {
            get => entryFirstName.Text;
            set => entryFirstName.Text = value;
        }

        public string LastName
        {
            get => entryLastName.Text;
            set => entryLastName.Text = value;
        }

        public string Phone
        {
            get => entryPhoneNumber.Text;
            set => entryPhoneNumber.Text = value;
        }

        public string Email
        {
            get => entryEmail.Text;
            set => entryEmail.Text = value;
        }

        public event EventHandler SaveButtonClicked;

        private void btnSave_Clicked(object sender, EventArgs e)
        {
            SaveButtonClicked?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler CancelButtonClicked;

        private void btnCancel_Clicked(object sender, EventArgs e)
        {
            CancelButtonClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
