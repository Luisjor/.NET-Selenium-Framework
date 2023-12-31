﻿using AutomatedTestSolution.PageObjects;
using AutomationFramework.Classes;
using ConfigurationProvider.Classes;
using TechTalk.SpecFlow;
using WebDriverProvider.Configurations;

namespace AutomatedTestSolution.Hooks
{
    [Binding]
    public sealed class Hooks
    {
        private const string _webDriverConfigurationJson = "Configurations\\ChromeDriverConfiguration.json";

        [BeforeScenario]
        public void BeforeScenario(ScenarioContext scenarioContext)
        {
            var configurationReader = new ConfigurationReader(AppContext.BaseDirectory + _webDriverConfigurationJson);
            var configuration = configurationReader.GetConfiguration<WebDriverConfiguration>();
            var factory = new WebDriverFactory(configuration);

            scenarioContext["DriverFactory"] = factory;
        }

        [AfterScenario]
        public void AfterScenario(ScenarioContext scenarioContext)
        {
            var factory = (WebDriverFactory)scenarioContext["DriverFactory"];

            if(factory is not null)
            {
                var driver = factory.GetInstanceOf();

                if(driver is not null)
                {
                    factory.TerminateWebDriver();
                }
            }
        }

    }
}
