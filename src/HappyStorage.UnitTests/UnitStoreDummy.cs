using HappyStorage.Core;
using System;
using System.Collections.Generic;

namespace HappyStorage.UnitTests
{
    internal class UnitStoreDummy : IUnitStore
    {
        public void Create(NewUnit newUnit) => throw new NotSupportedException();

        public void Delete(string unitNumber) => throw new NotSupportedException();

        public decimal GetPricePerMonth(string unitNumber) => throw new NotSupportedException();

        public IEnumerable<AvailableUnit> SearchUnits(bool? isClimateControlled, bool? isVehicleAccessible, int? minimumCubicFeet) => throw new NotSupportedException();

        public bool UnitExists(string unitNumber) => throw new NotSupportedException();
    }
}