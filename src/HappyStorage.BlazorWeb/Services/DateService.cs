using HappyStorage.Core;
using System;

namespace HappyStorage.BlazorWeb.Services
{
    public class DateService : IDateService
    {
        public DateTime GetCurrentDateTime() => DateTime.UtcNow;
    }
}