namespace HappyStorage.Common.Ui.Customers
{
    public interface ICustomerCreateViewModel
    {
        NewCustomerModel NewCustomer { get; set; }

        void Create(NewCustomerModel newCustomer);
    }
}