using HappyStorage.Core.Models;
using System;
using System.Collections.Generic;

namespace HappyStorage.Core
{
    public interface IFacade
    {

        void CommissionNewUnit(NewUnit newUnit);

        void DecommissionUnit(string unitNumber);

        void AddNewCustomer(NewCustomer newCustomer);

        void DeleteCustomer(string customerNumber);

        NewCustomer GetCustomerDetails(string customerNumber);

        IEnumerable<CustomerLookup> ListCustomersAndTenants();

        void UpdateCustomerDetails(NewCustomer newCustomerDetails);

        IEnumerable<AvailableUnit> SearchAvailableUnits(bool? isClimateControlled, bool? isVehicleAccessible, int? minimumCubicFeet);

        IEnumerable<TenantLookup> GetCustomerUnits(string customerNumber);

        void ReserveUnit(string unitNumber, string customerNumber);

        void LockUnit(string unitNumber, string customerNumber);

        void UnlockUnit(string unitNumber, string customerNumber);

        void ReleaseUnit(string unitNumber, string customerNumber);

        decimal CalculateAmountDue(string customerNumber);

        void Pay(string customerNumber, decimal amount);
    }

    public interface IBillingSystem
    {
        DateTime GetNextPaymentDue(string customerNumber);
        IEnumerable<Payment> GetLatestPayments(string customerNumber);
        IEnumerable<Payment> GetPaymentHistory(string customerNumber, DateTime startRange, DateTime endRange);

        IEnumerable<InvoiceLookup> LookupInvoices(string customerNumber);
        Invoice GetInvoice(DateTime period);
        void ProrateAndMovePaymentDueDate();
        void ApplyDiscountAmount(Invoice invoice);
        void ApplyDiscountPercent(Invoice invoice);
    }

    public class Payment
    {
        public string CustomerNumber { get; set; }
        public Invoice Invoice { get; set; }
        public decimal PaymentAmount { get; set; }
        public DateTime PaymentDateTime { get; set; }
    }

    public class InvoiceLookup
    {
        public string CustomerNumber { get; set; }
    }

    public class Invoice
    {
        public string CustomerNumber { get; set; }
        public string Address { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public IEnumerable<CustomerUnit> CustomerUnits { get; set; }
        public class CustomerUnit
        {
            public string CustomerNumber { get; set; }
            public DateTime DateOccupied { get; set; }
        }
    }
}