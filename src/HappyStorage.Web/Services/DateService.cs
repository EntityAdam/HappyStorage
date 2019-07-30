using HappyStorage.Core;
using System;

namespace HappyStorage.Web.Services
{
    public class DateService : IDateService
    {
        public DateTime GetCurrentDateTime() => DateTime.UtcNow;
    }
}