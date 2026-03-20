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

        /// <summary>
        /// Method that tests whether a login attempt with a wrong username was unsuccesssful.
        /// </summary>
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

        /// <summary>
        /// Method that tests whether a login attempt with a wrong password was unsuccessful.
        /// </summary>
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

        /// <summary>
        /// Method that tests whether a login attempt with both wrong username and password was unsuccessful.
        /// </summary>
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

        /// <summary>
        /// Method that tests wether a login attempt with correct credentials was actually successful.
        /// </summary>
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

        /// <summary>
        /// Standard TearDown method which closes the WebDriver when tests have been executed.
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            this.driver.Close();
        }
    }
}
