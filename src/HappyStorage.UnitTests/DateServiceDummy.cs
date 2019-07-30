using HappyStorage.Core;
using System;

namespace HappyStorage.UnitTests
{
    internal class DateServiceDummy : IDateService
    {
        public DateTime GetCurrentDateTime() => throw new NotSupportedException();
    }
}