using TypewiseAlert.Common;
using TypewiseAlert.Enums;

namespace TypewiseAlert
{
    public class EmailAlerter : IAlerter
    {
        public void SendAlert(BreachType breachType)
        {
            string recepient = "a.b@c.com";
            switch (breachType)
            {
                case BreachType.TOO_LOW:
                    Logger.LogMessage($"To: {recepient}\n");
                    Logger.LogMessage("Hi, the temperature is too low\n");
                    break;
                case BreachType.TOO_HIGH:
                    Logger.LogMessage($"To: {recepient}\n");
                    Logger.LogMessage("Hi, the temperature is too high\n");
                    break;
                case BreachType.NORMAL:
                    break;
            }
        }
    }
}
