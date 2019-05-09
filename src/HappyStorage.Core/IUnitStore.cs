﻿using System;
using System.Collections.Generic;

namespace HappyStorage.Core
{
	public interface IUnitStore
	{
		void Create(NewUnit newUnit);
		void Delete(string unitNumber);
		decimal GetPricePerMonth(string unitNumber);
		IEnumerable<AvailableUnit> SearchUnits(bool? isClimateControlled, bool? isVehicleAccessible, int? minimumCubicFeet);
		bool UnitExists(string unitNumber);
	}
}
