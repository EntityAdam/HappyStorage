using System;
using HappyStorage.Core;

namespace HappyStorage.UnitTests
{
    internal class DateServiceDummy : IDateService
    {
		public DateTime GetCurrentDateTime() => throw new NotSupportedException();
	}
}
