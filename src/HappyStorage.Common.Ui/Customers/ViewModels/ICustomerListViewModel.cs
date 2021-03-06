﻿using HappyStorage.Common.Ui.Customers.Models;
using Prism.Commands;
using System.ComponentModel;

namespace HappyStorage.Common.Ui.Customers.ViewModels
{
    public interface ICustomerListViewModel
    {
        BindingList<CustomerLookupModel> Customers { get; set; }
        DelegateCommand NextPageCommand { get; }
        DelegateCommand PrevPageCommand { get; }
        int CurrentPage { get; }
        bool HasNextPage { get; }
        bool HasPrevPage { get; }

        void JumpToPage(int? page);

        void Load();
    }
}