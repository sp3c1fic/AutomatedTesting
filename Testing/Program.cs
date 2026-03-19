using OpenQA.Selenium;
using Testing;

IWebDriver webDriver = Driver.GetDriver("chrome");

try
{
    var loginPage = new LoginPage(webDriver);

    loginPage
        .Open()
        .Login("test@qabrains.com", "Password123");
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
finally
{
    webDriver.Quit();
}