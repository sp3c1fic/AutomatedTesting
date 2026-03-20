// <copyright file="LoginPage.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Testing
{
    using log4net;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Interactions;
    using OpenQA.Selenium.Support.UI;

    /// <summary>
    /// Login Page class which represents the Login page and contains all its functionality.
    /// </summary>
    public class LoginPage
    {
        // Miscellaneous constants
        private const string SuccessfulLoginString = "Login was successful";
        private const string LoginPageUrl = "https://practice.qabrains.com/ecommerce/login";

        // Locators represented as constants
        private const string EmailInputFieldIdLocator = "email";
        private const string PasswordInputFieldIdLocator = "password";
        private const string LoginSubmitButtonCssSelectorLocator = ".btn-submit";
        private const string FailedLoginNotificationCssSelectorLocator = "p.text-red-500";
        private const string ProductSortIdLocator = "product-sort";

        private static readonly ILog Log = LogManager.GetLogger(typeof(Logger));
        private readonly IWebDriver webDriver;
        private readonly WebDriverWait wait;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginPage"/> class.
        /// </summary>
        /// <param name="webDriver"> The instance of the web driver element passed as a parameter. </param>
        public LoginPage(IWebDriver webDriver)
        {
            this.webDriver = webDriver ?? throw new ArgumentNullException(nameof(webDriver));
            this.wait = new WebDriverWait(this.webDriver, TimeSpan.FromSeconds(2));
        }

        /// <summary>
        /// Opens up the given logen web page.
        /// </summary>
        /// <returns> <see cref="LoginPage"/> instance.</returns>
        public LoginPage Open()
        {
            this.webDriver.Url = LoginPageUrl;
            return this;
        }

        /// <summary>
        /// Method responsible for testing whether a login to a web page was successful or not.
        /// </summary>
        /// <param name="emailText">This elements represent the string value of the email used to test the login page functionality.</param>
        /// <param name="passwordText">This elements represent the string value of the password used to test the login page functionality.</param>
        /// <returns>Tuple of a bool and List of strings where the bool is indicates whether a login failed or succeeded and the List contains the reason for a successful or failed login.</returns>
        public (bool, List<string>) Login(string emailText, string passwordText)
        {
            var emailInputField = this.webDriver.FindElement(By.Id(EmailInputFieldIdLocator));
            var passwordInputField = this.webDriver.FindElement(By.Id(PasswordInputFieldIdLocator));
            var submitButton = this.webDriver.FindElement(By.CssSelector(LoginSubmitButtonCssSelectorLocator));

            this.wait.Until(d => emailInputField.Displayed);
            this.wait.Until(d => passwordInputField.Displayed);
            this.wait.Until(d => submitButton.Displayed);

            var actions = new Actions(this.webDriver);

            WebDriverUtils.InteractWithInputElement(actions, emailInputField, emailText);
            WebDriverUtils.InteractWithInputElement(actions, passwordInputField, passwordText);
            WebDriverUtils.InteractWithLoginSubmitButton(actions, submitButton);

            var loginResult = this.IsLoginSuccessful();

            GetLoginResults(loginResult);

            return loginResult;
        }

        private static void GetLoginResults((bool, List<string>) loginResult)
        {
            Log.Info($"Result from login: {loginResult.Item1}");
            Log.Info($"Number of reasons: {loginResult.Item2.Count}");

            if (!loginResult.Item1 && loginResult.Item2.Count > 0)
            {
                Log.Info($" Reasons:");
                foreach (var failedLoginReason in loginResult.Item2)
                {
                    Log.Info($" - {failedLoginReason}");
                }
            }
        }

        private (bool, List<string>) IsLoginSuccessful()
        {
            var notifications = this.webDriver.FindElements(By.CssSelector(FailedLoginNotificationCssSelectorLocator));
            this.wait.Until(d => notifications.All(n => n.Displayed));

            var reasons = new List<string>();

            if (notifications.Count == 0 && this.IsRedirectUponLoginSuccessful())
            {
                reasons.Add(SuccessfulLoginString);
                return (true, reasons);
            }

            foreach (var notification in notifications!)
            {
                var reason = notification.Text;
                reasons.Add(reason);
            }

            return (false, reasons);
        }

        private bool IsRedirectUponLoginSuccessful()
        {
            var productSortElement = this.webDriver.FindElement(By.Id(ProductSortIdLocator));

            this.wait.Until(d => productSortElement.Displayed);

            return productSortElement != null;
        }
    }
}
