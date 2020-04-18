using Microsoft.AspNetCore.Mvc;
using Unitial.Web.Controllers;
using Xunit;

namespace Unitial.Tests.Controllers
{
    public class AboutControllerTests
    {
        [Fact]
        public void TestViewForHomePage()
        {
            var controller = new AboutController();
            var result = controller.About();
            Assert.IsType<ViewResult>(result);
        }
    }
}
