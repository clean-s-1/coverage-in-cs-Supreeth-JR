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
            Mock<IAlerter> mckalerter = new Mock<IAlerter>();
            Mock<ITemperatureMonitor<double>> mcktempMonitor = new Mock<ITemperatureMonitor<double>>();
            mcktempMonitor.Setup(x =>
                x.classifyTemperatureBreach(CoolingType.HI_ACTIVE_COOLING, 46)).Returns(BreachType.TOO_HIGH);

            Mock<IAlerterStrategy> mckalertStrategy = new Mock<IAlerterStrategy>();
            mckalertStrategy.Setup(x => x.SetStrategy(mckalerter.Object));
            mckalertStrategy.Setup(x => x.SendAlert(BreachType.TOO_HIGH));

            var typewiseObj = new TypewiseAlert(mckalertStrategy.Object, mcktempMonitor.Object);
            BatteryCharacter batteryCharacter =
                new BatteryCharacter() { brand = "Siemens", coolingType = CoolingType.HI_ACTIVE_COOLING };
            typewiseObj.CheckAndAlert(AlertTarget.TO_EMAIL, batteryCharacter, 46);
        }

        [Fact]
        public void InfersBreachAsPerLimits_low()
        {
            Mock<IAlerter> alerter6 = new Mock<IAlerter>();
            Mock<ITemperatureMonitor<double>> tempMonitor6 = new Mock<ITemperatureMonitor<double>>();
            tempMonitor6.Setup(x =>
                x.classifyTemperatureBreach(CoolingType.PASSIVE_COOLING, 0)).Returns(BreachType.TOO_LOW);

            Mock<IAlerterStrategy> alertStrategy6 = new Mock<IAlerterStrategy>();
            alertStrategy6.Setup(x => x.SetStrategy(alerter6.Object));
            alertStrategy6.Setup(x => x.SendAlert(BreachType.TOO_LOW));

            var typewise6 = new TypewiseAlert(alertStrategy6.Object, tempMonitor6.Object);
            BatteryCharacter batteryCharacter =
                new BatteryCharacter() { brand = "Siemens", coolingType = CoolingType.PASSIVE_COOLING };
            typewise6.CheckAndAlert(AlertTarget.TO_CONTROLLER, batteryCharacter, 0);
        }

        [Fact]
        public void InfersBreachAsPerLimits_Normal()
        {
            Mock<IAlerter> alerter7 = new Mock<IAlerter>();
            Mock<ITemperatureMonitor<double>> tempMonitor7 = new Mock<ITemperatureMonitor<double>>();
            tempMonitor7.Setup(x =>
                x.classifyTemperatureBreach(CoolingType.PASSIVE_COOLING, 15)).Returns(BreachType.NORMAL);

            Mock<IAlerterStrategy> alertStrategy7 = new Mock<IAlerterStrategy>();
            alertStrategy7.Setup(x => x.SetStrategy(alerter7.Object));
            alertStrategy7.Setup(x => x.SendAlert(BreachType.NORMAL));

            var typewise7 = new TypewiseAlert(alertStrategy7.Object, tempMonitor7.Object);
            BatteryCharacter batteryCharacter =
                new BatteryCharacter() { brand = "Siemens", coolingType = CoolingType.MED_ACTIVE_COOLING };
            typewise7.CheckAndAlert(AlertTarget.TO_CONTROLLER, batteryCharacter, 0);
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
            var temperatureMonitor1 = new TemperatureMonitor<double>();
            Assert.True(temperatureMonitor1.classifyTemperatureBreach(CoolingType.PASSIVE_COOLING, -2.0) == BreachType.TOO_LOW);
        }

        [Fact]
        public void BreachType_To_High()
        {
            var temperatureMonitor2 = new TemperatureMonitor<double>();
            Assert.True(temperatureMonitor2.classifyTemperatureBreach(CoolingType.PASSIVE_COOLING, 46) == BreachType.TOO_HIGH);
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
            IAlerter alerter1 = new EmailAlerter();
            alerter1.SendAlert(BreachType.TOO_HIGH);
        }

        [Fact]
        public void EmailAlert_Normal()
        {
            IAlerter alerter2 = new EmailAlerter();
            alerter2.SendAlert(BreachType.NORMAL);
        }

        [Fact]
        public void ControllerAlert_Too_Low()
        {
            IAlerter alerter3 = new ControllerAlerter();
            alerter3.SendAlert(BreachType.TOO_LOW);
        }

        [Fact]
        public void ControllerAlert_Too_High()
        {
            IAlerter alerter4 = new ControllerAlerter();
            alerter4.SendAlert(BreachType.TOO_HIGH);
        }

        [Fact]
        public void ControllerAlert_Normal()
        {
            IAlerter alerter5 = new ControllerAlerter();
            alerter5.SendAlert(BreachType.NORMAL);
        }
    }
}
