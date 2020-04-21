using Microsoft.AspNetCore.Mvc;
using Unitial.Web.Controllers;
using Xunit;

namespace Unitial.Tests.Controllers
{
    public class PrivacyControllerTests
    {
        [Fact]
        public void TestViewForHomePage()
        {
            var controller = new PrivacyController();
            var result = controller.Privacy();
            Assert.IsType<ViewResult>(result);
        }
    }
}
