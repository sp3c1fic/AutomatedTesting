namespace QABrainsTests
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using Testing;

    public class QABrainsTests
    {
        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
             this.driver = new ChromeDriver();
        }

        [Test]
        public void LoginTestWithWrongUsername()
        {
            // Arrange
            var loginPage = new LoginPage(this.driver);

            // Act
            var loginResult = loginPage.Open().Login("stefka@stefka.abv.bg", "Password123");
            var failedLoginReason = loginResult.Item2[0];

            // Assert
            Assert.That(failedLoginReason, Is.EqualTo("Username is incorrect."));
        }

        [Test]
        public void LoginTestWithWrongPassword()
        {
            // Arrange
            var loginPage = new LoginPage(this.driver);

            // Act
            var loginResult = loginPage.Open().Login("practice@qabrains.com", "asjif18294u");
            var failedLoginReason = loginResult.Item2[0];

            // Assert
            Assert.That(failedLoginReason, Is.EqualTo("Password is incorrect."));
        }

        [Test]
        public void LoginTestWithWrongUsernameAndPassword()
        {
            // Arrange
            var loginPage = new LoginPage(this.driver);

            // Act
            var loginResult = loginPage.Open().Login("auiohdsfi@asufbiuas.sadas", "jashbdians");
            var failedLoginReasons = loginResult.Item2;

            // Assert
            Assert.That(failedLoginReasons.Count, Is.EqualTo(2));
            Assert.That(failedLoginReasons[0], Is.EqualTo("Username is incorrect."));
            Assert.That(failedLoginReasons[^1], Is.EqualTo("Password is incorrect."));
        }

        [Test]
        public void LoginTestWithCorrectCredentials()
        {
            // Arrange
            var loginPage = new LoginPage(this.driver);

            // Act
            var loginResult = loginPage.Open().Login("test@qabrains.com", "Password123");
            var successfulLoginReason = loginResult.Item2[0];

            // Assert
            Assert.That(successfulLoginReason, Is.EqualTo("Login was successful"));
        }

        [TearDown]
        public void TearDown()
        {
            this.driver.Close();
        }
    }
}
