namespace HappyStorage.Common.Ui.Customers.ViewModels
{
    public interface ICustomerDeleteViewModel
    {
        string CustomerNumber { get; set; }
        void Delete();
        void Delete(string customerNumber);
    }
}