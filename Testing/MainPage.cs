// <copyright file="MainPage.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace Testing
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Interactions;
    using OpenQA.Selenium.Support.UI;
    using SeleniumExtras.WaitHelpers;

    /// <summary>
    /// Class that represents the Main Page where products of the website are displayed so they can be interacted with.
    /// </summary>
    public class MainPage
    {
        //TODO put in a separate constants class
        private const string MainPageUrl = "https://practice.qabrains.com/ecommerce";
        private const string FavouriteProductsPageUrl = "https://practice.qabrains.com/ecommerce/favorites";
        private const string LoginSubmitButtonCssSelectorLocator = ".btn-submit";

        private const string ProfileButtonCssSelector = ".profile";
        private const string NoFavouriteProductsFoundHeadingSelector = "/html/body/div[2]/div/div[2]/div/h2";
        private const string DropDownAccountMenuXpathLocator = "/html/body/div[3]/div/div[1]/div";
        private const string AddToFavouritesButtonXpathLocator = "/html/body/div[3]/div/div[1]/div";

        private const string EmailInputFiledSelector = "email";
        private const string PasswordInputFieldSelector = "password";

        private readonly IWebDriver webDriver;
        private readonly WebDriverWait wait;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainPage"/> class.
        /// </summary>
        /// <param name="webDriver">The selenium webdriver instance which is responsible for interacting with browser element.</param>
        /// <exception cref="ArgumentNullException">Throws a NullReferenceException if the WebDriver instance is null.</exception>
        public MainPage(IWebDriver webDriver)
        {
            this.webDriver = webDriver ?? throw new ArgumentNullException(nameof(webDriver));
            this.wait = new WebDriverWait(this.webDriver, TimeSpan.FromSeconds(4));
        }

        /// <summary>
        /// Opens the desired page which will be interacted with.
        /// </summary>
        /// <returns>This instance of the MainPage class.</returns>
        public MainPage Open()
        {
            this.webDriver.Url = FavouriteProductsPageUrl;
            return this;
        }

        /// <summary>
        /// Method responsible for testing and automating the AddToFavourites functionality of the website.
        /// </summary>
        public void NavigateToFavourites()
        {

            // Loggin in if user is not already logged in which is assumed to be the case by default.
            if (this.IsNotLoggedIn())
            {
                this.Login("test@qabrains.com", "Password123"); // TODO hide credentials away from here.
            }

            // Checking to see if we managed to logged in and were actually redirected to the main page of the website.
            this.wait.Until(d => d.Url.Contains("ecommerce"));

            // Looking for the Profile Button which is a parent element of the "Add to favourites" and "Login" buttons.
            var profileButton = this.webDriver.FindElement(By.CssSelector(ProfileButtonCssSelector));
            this.wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(ProfileButtonCssSelector)));

            // Getting the actual button which toggles the dropdown menu.
            var accountButton = profileButton.FindElement(By.TagName("button"));
            this.wait.Until(ExpectedConditions.ElementToBeClickable(By.TagName("button")));

            var actions = new Actions(this.webDriver);

            actions
                .Click(accountButton)
                .Pause(TimeSpan.FromSeconds(2))
                .Perform();

            if (accountButton.GetAttribute("data-state") == "open")
            {
                Console.WriteLine("Drop down menu has been toggled");

                var dropdownAccountMenu = this.webDriver.FindElement(By.XPath(DropDownAccountMenuXpathLocator));
                this.wait.Until(d => dropdownAccountMenu.Displayed);

                var favouritesButton = this.webDriver.FindElement(By.XPath(AddToFavouritesButtonXpathLocator));
                this.wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(AddToFavouritesButtonXpathLocator)));

                actions
                    .Click(favouritesButton)
                    .Pause(TimeSpan.FromSeconds(2))
                    .Perform();
            }
        }

        private bool IsNotLoggedIn()
        {
            this.wait.Until(d => d.Url.Contains("login"));
            var submitButton = this.webDriver.FindElement(By.CssSelector(LoginSubmitButtonCssSelectorLocator));
            this.wait.Until(d => submitButton.Displayed);
            return submitButton != null;
        }

        private void Login(string emailText, string passwordText)
        {
            var emailInputField = this.webDriver.FindElement(By.Id(EmailInputFiledSelector));
            var passwordInputField = this.webDriver.FindElement(By.Id(PasswordInputFieldSelector));
            var submitButton = this.webDriver.FindElement(By.CssSelector(LoginSubmitButtonCssSelectorLocator));

            this.wait.Until(d => emailInputField.Displayed);
            this.wait.Until(d => passwordInputField.Displayed);
            this.wait.Until(d => submitButton.Displayed);

            var actions = new Actions(this.webDriver);

            WebDriverUtils.InteractWithInputElement(actions, emailInputField, emailText);
            WebDriverUtils.InteractWithInputElement(actions, passwordInputField, passwordText);
            WebDriverUtils.InteractWithSubmitWithButton(actions, submitButton);
        }
    }
}