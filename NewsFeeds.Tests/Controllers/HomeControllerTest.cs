using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NewsFeeds.Controllers;

namespace NewsFeeds.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void ShowFeeds()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.ShowFeeds("news", "asc", false) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DownloadFile()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.DownloadFile("http://google.com") as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
