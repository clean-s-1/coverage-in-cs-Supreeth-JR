using Moq;
using TypewiseAlert.Enums;
using TypewiseAlert.Models;
using Xunit;

namespace TypewiseAlert.Test
{
    public class TypewiseAlertTest
    {
        [Fact]
        public void InfersBreachAsPerLimits_High()
        {
            Mock<IAlerter> alerter = new Mock<IAlerter>();
            Mock<ITemperatureMonitor<double>> tempMonitor = new Mock<ITemperatureMonitor<double>>();
            tempMonitor.Setup(x =>
                x.classifyTemperatureBreach(CoolingType.HI_ACTIVE_COOLING, 46)).Returns(BreachType.TOO_HIGH);

            Mock<IAlerterStrategy> alertStrategy = new Mock<IAlerterStrategy>();
            alertStrategy.Setup(x => x.SetStrategy(alerter.Object));
            alertStrategy.Setup(x => x.SendAlert(BreachType.TOO_HIGH));

            var typewise = new TypewiseAlert(alertStrategy.Object, tempMonitor.Object);
            BatteryCharacter batteryCharacter =
                new BatteryCharacter() { brand = "Siemens", coolingType = CoolingType.HI_ACTIVE_COOLING };
            typewise.CheckAndAlert(AlertTarget.TO_EMAIL, batteryCharacter, 46);
        }

        [Fact]
        public void InfersBreachAsPerLimits_low()
        {
            Mock<IAlerter> alerter = new Mock<IAlerter>();
            Mock<ITemperatureMonitor<double>> tempMonitor = new Mock<ITemperatureMonitor<double>>();
            tempMonitor.Setup(x =>
                x.classifyTemperatureBreach(CoolingType.PASSIVE_COOLING, 0)).Returns(BreachType.TOO_LOW);

            Mock<IAlerterStrategy> alertStrategy = new Mock<IAlerterStrategy>();
            alertStrategy.Setup(x => x.SetStrategy(alerter.Object));
            alertStrategy.Setup(x => x.SendAlert(BreachType.TOO_LOW));

            var typewise = new TypewiseAlert(alertStrategy.Object, tempMonitor.Object);
            BatteryCharacter batteryCharacter =
                new BatteryCharacter() { brand = "Siemens", coolingType = CoolingType.PASSIVE_COOLING };
            typewise.CheckAndAlert(AlertTarget.TO_CONTROLLER, batteryCharacter, 0);
        }

        [Fact]
        public void InfersBreachAsPerLimits_Normal()
        {
            Mock<IAlerter> alerter = new Mock<IAlerter>();
            Mock<ITemperatureMonitor<double>> tempMonitor = new Mock<ITemperatureMonitor<double>>();
            tempMonitor.Setup(x =>
                x.classifyTemperatureBreach(CoolingType.PASSIVE_COOLING, 15)).Returns(BreachType.NORMAL);

            Mock<IAlerterStrategy> alertStrategy = new Mock<IAlerterStrategy>();
            alertStrategy.Setup(x => x.SetStrategy(alerter.Object));
            alertStrategy.Setup(x => x.SendAlert(BreachType.NORMAL));

            var typewise = new TypewiseAlert(alertStrategy.Object, tempMonitor.Object);
            BatteryCharacter batteryCharacter =
                new BatteryCharacter() { brand = "Siemens", coolingType = CoolingType.MED_ACTIVE_COOLING };
            typewise.CheckAndAlert(AlertTarget.TO_CONTROLLER, batteryCharacter, 0);
        }

        [Fact]
        public void BreachType_Normal()
        {
            var temperatureMonitor = new TemperatureMonitor<double>();
            Assert.True(temperatureMonitor.classifyTemperatureBreach(CoolingType.MED_ACTIVE_COOLING,0) == BreachType.NORMAL);
        }

        [Fact]
        public void BreachType_To_Low()
        {
            var temperatureMonitor = new TemperatureMonitor<double>();
            Assert.True(temperatureMonitor.classifyTemperatureBreach(CoolingType.PASSIVE_COOLING, -2.0) == BreachType.TOO_LOW);
        }

        [Fact]
        public void BreachType_To_High()
        {
            var temperatureMonitor = new TemperatureMonitor<double>();
            Assert.True(temperatureMonitor.classifyTemperatureBreach(CoolingType.PASSIVE_COOLING, 46) == BreachType.TOO_HIGH);
        }

        [Fact]
        public void EmailAlert_Too_Low()
        {
            IAlerter alerter = new EmailAlerter();
            alerter.SendAlert(BreachType.TOO_LOW);
        }

        [Fact]
        public void EmailAlert_Too_High()
        {
            IAlerter alerter = new EmailAlerter();
            alerter.SendAlert(BreachType.TOO_HIGH);
        }

        [Fact]
        public void EmailAlert_Normal()
        {
            IAlerter alerter = new EmailAlerter();
            alerter.SendAlert(BreachType.NORMAL);
        }

        [Fact]
        public void ControllerAlert_Too_Low()
        {
            IAlerter alerter = new ControllerAlerter();
            alerter.SendAlert(BreachType.TOO_LOW);
        }

        [Fact]
        public void ControllerAlert_Too_High()
        {
            IAlerter alerter = new ControllerAlerter();
            alerter.SendAlert(BreachType.TOO_HIGH);
        }

        [Fact]
        public void ControllerAlert_Normal()
        {
            IAlerter alerter = new ControllerAlerter();
            alerter.SendAlert(BreachType.NORMAL);
        }
    }
}
