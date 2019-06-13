using System.ComponentModel.DataAnnotations;

namespace HappyStorage.Common.Ui.Customers.Models
{
    public class NewCustomerModel : BindableBase
    {
        [Display(Name = "Customer Number")]
        public string CustomerNumber { get; set; }

        [Display(Name = "Full Name")]
        public string FullName => $"{FirstName} {LastName}";

        private string firstName;
        [Required]
        [Display(Name = "First Name")]
        public string FirstName
        {
            get => firstName;
            set
            {
                SetField(ref firstName, value);
                OnPropertyChanged(nameof(FullName));
            }
        }

        private string lastName;
        [Required]
        [Display(Name = "Last Name")]
        public string LastName
        {
            get => lastName;
            set
            {
                SetField(ref lastName, value);
                OnPropertyChanged(nameof(FullName));
            }
        }

        [Display(Name = "Address")]
        public string Address => $"{Street}, {City}, {State} {PostalCode}";

        private string street;

        [Required]
        [Display(Name = "Street Address")]
        public string Street
        {
            get => street;
            set
            {
                SetField(ref street, value);
                OnPropertyChanged(nameof(Address));
            }
        }
        private string city;

        [Required]
        [Display(Name = "City")]
        public string City
        {
            get => city;
            set
            {
                SetField(ref city, value);
                OnPropertyChanged(nameof(Address));
            }
        }
        private string state;

        [Required]
        [Display(Name = "State")]
        public string State
        {
            get => state;
            set
            {
                SetField(ref state, value);
                OnPropertyChanged(nameof(Address));
            }
        }
        private string postalCode;

        [Required]
        [Display(Name = "Zip Code")]
        public string PostalCode
        {
            get => postalCode;
            set
            {
                SetField(ref postalCode, value);
                OnPropertyChanged(nameof(Address));
            }
        }
    }
}
