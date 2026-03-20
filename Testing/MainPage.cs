// <copyright file="MainPage.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace Testing
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Interactions;
    using OpenQA.Selenium.Support.UI;

    public class MainPage
    {
        private const string MainPageUrl = "https://practice.qabrains.com/ecommerce";
        private const string FavouriteProductsPageUrl = "https://practice.qabrains.com/ecommerce/favorites";
        private const string CorrectLoginUsername = "test@qabrains.com";
        private const string CorrectLoginPassword = "Password123";
        private const string NoFavouriteProductsFoundHeadingSelector = "/html/body/div[2]/div/div[2]/div/h2";
        private const string LoginFormHeadingSelector = "/html/body/div[2]/div/div/div/h2";
        private const string EmailInputFiledSelector = "email";
        private const string PasswordInputFieldSelector = "password";

        private readonly IWebDriver webDriver;
        private readonly WebDriverWait wait;

        public MainPage(IWebDriver webDriver)
        {
            this.webDriver = webDriver ?? throw new ArgumentNullException(nameof(webDriver));
            this.wait = new WebDriverWait(this.webDriver, TimeSpan.FromSeconds(2));
        }

        public MainPage Open()
        {
            this.webDriver.Url = FavouriteProductsPageUrl;
            return this;
        }

        public void AddToFavourites()
        {

            if (!IsLoggedIn())
            {
                Login();
            }

            //var noFavouriteProductsFoundText = this.webDriver.FindElement(By.XPath(NoFavouriteProductsFoundHeadingSelector));
            //this.wait.Until(d => noFavouriteProductsFoundText.Displayed);
        }


        private void IsLoggedIn()
        {
            var loginHeading = this.webDriver.FindElement(By.XPath(LoginFormHeadingSelector));


        }

        private void Login()
        {
            var emailInputField = this.webDriver.FindElement(By.Id(EmailInputFiledSelector));
            var passwordInputField = this.webDriver.FindElement(By.Id(PasswordInputFieldSelector));

            this.wait.Until(el => emailInputField.Displayed);
            this.wait.Until(el => passwordInputField.Displayed);

            var actions = new Actions(this.webDriver);

        }
    }
}