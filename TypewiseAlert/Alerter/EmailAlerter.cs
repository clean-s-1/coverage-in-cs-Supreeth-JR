using TypewiseAlert.Common;
using TypewiseAlert.Enums;

namespace TypewiseAlert
{
    public class EmailAlerter : IAlerter
    {
        public void SendAlert(BreachType breachType)
        {
            string recepient = "a.b@c.com";
            string msgToBeLogged = null;
            switch (breachType)
            {
                case BreachType.TOO_LOW:
                    msgToBeLogged = $"To: {recepient}\n Hi, the temperature is too low\n";
                    break;
                case BreachType.TOO_HIGH:
                    msgToBeLogged = $"To: {recepient}\n Hi, the temperature is too high\n";
                    break;
            }
            Logger.LogMessage(msgToBeLogged);
        }
    }
}
