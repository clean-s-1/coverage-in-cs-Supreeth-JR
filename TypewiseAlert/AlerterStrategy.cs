using TypewiseAlert.Enums;

namespace TypewiseAlert
{
    public class AlerterStrategy : IAlerterStrategy
    {
        private IAlerter _alerter;

        public void SetStrategy(IAlerter alerter)
        {
            _alerter = alerter;
        }

        public void SendAlert(BreachType breachType)
        {
            _alerter.SendAlert(breachType);
        }
    }
}
