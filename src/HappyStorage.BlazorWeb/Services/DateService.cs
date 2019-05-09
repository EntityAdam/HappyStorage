using HappyStorage.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HappyStorage.BlazorWeb.Services
{
    public class DateService : IDateService
    {
        public DateTime GetCurrentDateTime() => DateTime.UtcNow;
    }
}
