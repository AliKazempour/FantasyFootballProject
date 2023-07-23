using static FantasyFootballProject.Business.memberLogic;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestIsValidPassword()
        {
            // Arrange
            string validPassword = "Abc12345";
            string invalidPassword = "abc12345";

            // Act
            bool isValid1 = IsValidPassword(validPassword);
            bool isValid2 = IsValidPassword(invalidPassword);

            // Assert
            Assert.IsTrue(isValid1);
            Assert.IsFalse(isValid2);
        }
        [TestMethod]
        public void IsValidEmail_ValidEmail_ReturnsTrue()
        {
            // Arrange
            string email = "example@example.com";

            // Act
            bool result = IsValidEmail(email);

            // Assert
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void IsValidEmail_InvalidEmail_ReturnsFalse()
        {
            // Arrange
            string email = "invalidemail";

            // Act
            bool result = IsValidEmail(email);

            // Assert
            Assert.IsFalse(result);
        }
        [TestMethod]
        public void TestIsValidUsername()
        {
            // Arrange
            string validUsername = "john_doe1";
            string invalidUsername1 = "a";
            string invalidUsername2 = "abcdefghijklmnopqrstuvwxyz1";

            // Act
            bool isValid1 = IsValidUsername(validUsername);
            bool isValid2 = IsValidUsername(invalidUsername1);
            bool isValid3 = IsValidUsername(invalidUsername2);

            // Assert
            Assert.IsTrue(isValid1);
            Assert.IsFalse(isValid2);
            Assert.IsFalse(isValid3);
        }
    }
}

