// <copyright file="Driver.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Testing
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Edge;
    using OpenQA.Selenium.Firefox;
    using OpenQA.Selenium.Safari;

    // TODO Add checks to ensure that Ensure that the instance hasn't yet been
    // initialized by another thread while this one
    // has been waiting for the lock's release.

    /// <summary>
    /// Main driver class responsible for the Selenium Web Driver initialization.
    /// </summary>
    public class Driver
    {
        private static IWebDriver? driver;

        private Driver()
        {
            // Private constructor to prevent instantiation
        }

        /// <summary>
        /// The method which initializes the web driver.
        /// </summary>
        /// <param name="driverName">This method takes only one parameter i.e. the driver name which translates to the driver type that is to be initialized.</param>
        /// <returns>IWebDriver.</returns>
        public static IWebDriver GetDriver(string driverName)
        {
            if (driver == null)
            {
                // TODO these should be turned into an ENUM
                switch (driverName.ToLower())
                {
                    case "chrome":
                        driver = new ChromeDriver();
                        break;
                    case "edge":
                        driver = new EdgeDriver();
                        break;
                    case "firefox":
                        driver = new FirefoxDriver();
                        break;
                    case "safari":
                        driver = new SafariDriver();
                        break;
                    default:
                        break;
                }
            }

            ConfigureDriverOptions();

            return driver!;
        }

        private static void ConfigureDriverOptions()
        {
            var driverType = driver?.GetType();

            Console.WriteLine(driverType);
        }
    }
}
