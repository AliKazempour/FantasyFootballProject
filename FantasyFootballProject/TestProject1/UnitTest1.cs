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
    }
}

