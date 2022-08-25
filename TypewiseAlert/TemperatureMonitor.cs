using System.Collections.Generic;
using TypewiseAlert.Enums;

namespace TypewiseAlert
{
    public class TemperatureMonitor<T> : ITemperatureMonitor<T>
    {
        private IDictionary<CoolingType, (object, object)> _coolingType;

        public TemperatureMonitor()
        {
            _coolingType = new Dictionary<CoolingType, (object, object)>();
            InitializeCoolingType();
        }
        public BreachType classifyTemperatureBreach(CoolingType coolingType,T temperatureInC)
        {
            object lowerLimit = _coolingType[coolingType].Item1;
            object upperLimit = _coolingType[coolingType].Item2;
            return inferBreach((T)temperatureInC, (T)lowerLimit, (T)upperLimit);
        }
        private BreachType inferBreach(T value, T lowerLimit, T upperLimit)
        {
            if (Comparer<T>.Default.Compare(value, lowerLimit) < 0)
            {
                return BreachType.TOO_LOW;
            }

            if (Comparer<T>.Default.Compare(value, upperLimit) > 0)
            {
                return BreachType.TOO_HIGH;
            }
            return BreachType.NORMAL;
        }

        private void InitializeCoolingType()
        {
            _coolingType.Add(CoolingType.PASSIVE_COOLING, (0.0, 35.0));
            _coolingType.Add(CoolingType.HI_ACTIVE_COOLING, (0.0, 45.0));
            _coolingType.Add(CoolingType.MED_ACTIVE_COOLING, (0.0, 40.0));
        }
    }
}
