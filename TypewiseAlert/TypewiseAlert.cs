using System.Collections.Generic;
using TypewiseAlert.Enums;
using TypewiseAlert.Models;

namespace TypewiseAlert
{
    public class TypewiseAlert
    {
        private static IAlerterStrategy _alerterStrategy;
        private static ITemperatureMonitor<double> _temperatureMonitor;
        private static  IDictionary<AlertTarget, IAlerter> _alerters;

        public TypewiseAlert(IAlerterStrategy alerterStrategy, ITemperatureMonitor<double> temperatureMonitor)
        {
            _alerterStrategy = alerterStrategy;
            _temperatureMonitor = temperatureMonitor;
            _alerters = new Dictionary<AlertTarget, IAlerter>();
            InitializeAlerter();
        }

        public void CheckAndAlert(AlertTarget alertTarget, BatteryCharacter batteryChar, double temperatureInC)
        {
            var breachType = _temperatureMonitor.classifyTemperatureBreach(
              batteryChar.coolingType, temperatureInC
            );

            var alerter = _alerters[alertTarget];

            _alerterStrategy.SetStrategy(alerter);
            _alerterStrategy.SendAlert(breachType);
        }

        private void InitializeAlerter()
        {
            _alerters.Add(AlertTarget.TO_EMAIL, new EmailAlerter());
            _alerters.Add(AlertTarget.TO_CONTROLLER,new ControllerAlerter());
        }
    }
}
