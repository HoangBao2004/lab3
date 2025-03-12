using ASC.Tests.TestUtilities;
using ASC.WEB.Configuration;
using ASC.WEB.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace ASC.Tests
{
    public class HomeControllerTests
    {
        private readonly Mock<IOptions<ApplicationSettings>> optionsMock;
        private readonly Mock<ILogger<HomeController>> loggerMock;
        private readonly Mock<HttpContext> mockHttpContext;

        public HomeControllerTests()
        {
            // Mock dependencies
            optionsMock = new Mock<IOptions<ApplicationSettings>>();
            loggerMock = new Mock<ILogger<HomeController>>();
            mockHttpContext = new Mock<HttpContext>();

            // Set up FakeSession
            mockHttpContext.Setup(p => p.Session).Returns(new FakeSession());

            // Mock application settings
            optionsMock.Setup(ap => ap.Value).Returns(new ApplicationSettings
            {
                ApplicationTitle = "ASC"
            });
        }

        [Fact]
        public void HomeController_Index_View_Test()
        {
            var controller = new HomeController(loggerMock.Object, optionsMock.Object);
            controller.ControllerContext.HttpContext = mockHttpContext.Object;
            Assert.IsType<ViewResult>(controller.Index());
        }

        [Fact]
        public void HomeController_Index_NoModel_Test()
        {
            var controller = new HomeController(loggerMock.Object, optionsMock.Object);
            controller.ControllerContext.HttpContext = mockHttpContext.Object;
            Assert.Null((controller.Index() as ViewResult)?.ViewData.Model);
        }

        [Fact]
        public void HomeController_Index_Validation_Test()
        {
            var controller = new HomeController(loggerMock.Object, optionsMock.Object);
            controller.ControllerContext.HttpContext = mockHttpContext.Object;
            Assert.Equal(0, (controller.Index() as ViewResult)?.ViewData.ModelState.ErrorCount);
        }

        [Fact]
        public void HomeController_Index_Session_Test()
        {
            var controller = new HomeController(loggerMock.Object, optionsMock.Object);
            controller.ControllerContext.HttpContext = mockHttpContext.Object;
            controller.Index();
            Assert.NotNull(controller.HttpContext.Session.GetSession<ApplicationSettings>("Test"));
        }
    }

    public static class SessionExtensions
    {
        public static T GetSession<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : System.Text.Json.JsonSerializer.Deserialize<T>(value);
        }
    }
}