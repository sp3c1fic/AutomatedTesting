using OpenQA.Selenium;
using Testing;

IWebDriver webDriver = Driver.GetDriver("chrome");

try
{
    // var loginPage = new LoginPage(webDriver);

    var mainPage = new MainPage(webDriver);

    Logger.ConfigureLogger();

    // loginPage
    //    .Open()
    //    .Login("test@qabrains.com", "Password123");

    mainPage
        .Open()

        // .AddToFavourites.
        .NavigateToFavourites();
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
finally
{
    webDriver.Quit();
}