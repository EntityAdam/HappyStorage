using AutoMapper;
using HappyStorage.Core;
using HappyStorage.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace HappyStorage.UnitTests
{
    internal class UnitStoreMock : IUnitStore
    {
        internal class Unit
        {
            internal string UnitNumber { get; set; }
            internal int Length { get; set; }
            internal int Width { get; set; }
            internal int Height { get; set; }
            internal bool IsClimateControlled { get; set; }
            internal bool IsVehicleAccessible { get; set; }
            internal decimal PricePerMonth { get; set; }
        }

        internal readonly List<Unit> Units = new List<Unit>();

        private IMapper mapper = new MapperConfiguration(cfg =>
        {
            cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
            cfg.CreateMap<NewUnit, Unit>();
            cfg.CreateMap<Unit, AvailableUnit>();
        }).CreateMapper();

        public void Create(NewUnit newUnit) => Units.Add(mapper.Map<NewUnit, Unit>(newUnit));

        public void Delete(string unitNumber) => Units.RemoveAll(u => u.UnitNumber == unitNumber);

        public decimal GetPricePerMonth(string unitNumber) => Units.Single(u => u.UnitNumber == unitNumber).PricePerMonth;

        public IEnumerable<AvailableUnit> SearchAvailableUnits(bool? isClimateControlled, bool? isVehicleAccessible, int? minimumCubicFeet)
        {
            return Units
                .Where(u => (isClimateControlled.HasValue && u.IsClimateControlled == isClimateControlled.Value) || !isClimateControlled.HasValue)
                .Where(u => (isVehicleAccessible.HasValue && u.IsVehicleAccessible == isVehicleAccessible.Value) || !isVehicleAccessible.HasValue)
                .Where(u => (minimumCubicFeet.HasValue && (u.Length * u.Width * u.Height) >= minimumCubicFeet.Value) || !minimumCubicFeet.HasValue)
                .Select(u => mapper.Map<Unit, AvailableUnit>(u));
        }

        public bool UnitExists(string unitNumber) => Units.Any(u => u.UnitNumber == unitNumber);
    }
}