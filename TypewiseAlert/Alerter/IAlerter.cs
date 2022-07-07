using TypewiseAlert.Enums;

namespace TypewiseAlert
{
    public interface IAlerter
    {
        void SendAlert(BreachType breachType);
    }
}
