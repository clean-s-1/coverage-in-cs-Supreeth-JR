using TypewiseAlert.Enums;

namespace TypewiseAlert
{
    public interface IAlerterStrategy
    {
        void SetStrategy(IAlerter alerter);
        void SendAlert(BreachType breachType);
    }
}
