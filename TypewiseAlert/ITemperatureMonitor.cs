using TypewiseAlert.Enums;

namespace TypewiseAlert
{
    public interface ITemperatureMonitor<T>
    {
        BreachType classifyTemperatureBreach(CoolingType coolingType, T temperatureInC);
    }
}
