using HappyStorage.Core;
using System;

namespace HappyStorage.UnitTests
{
    internal class DateServiceMock : IDateService
    {
        internal DateTime CurrentDateTime { get; set; }

        public DateTime GetCurrentDateTime() => CurrentDateTime;
    }
}