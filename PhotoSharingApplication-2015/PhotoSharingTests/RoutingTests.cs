using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using PhotoSharingTests.Doubles;
using System.Web.Routing;
using PhotoSharingApplication_2015;



namespace PhotoSharingTests
{
    [TestClass]
    public class RoutingTests
    {
        [TestMethod]
        public void Test_Default_Route_ControllerOnly()
        {
            var context = new FakeHttpContextForRouting(requestUrl: "~/ControllerName");
            var routes = new RouteCollection();
            PhotoSharingApplication_2015.RouteConfig.RegisterRoutes(routes);

            RouteData routeData = routes.GetRouteData(context);

            Assert.IsNotNull(routeData);
            //Assert that the controller value is ControllerName
            Assert.AreEqual("ControllerName", routeData.Values["controller"]);
            //Assert that the action value in routedata is Index
            Assert.AreEqual("Index", routeData.Values["action"]);
            Assert.AreEqual(UrlParameter.Optional, routeData.Values["id"]);
        }

        [TestMethod]
        public void Test_PhotoRoute_With_PhotoID()
        {
            //This test checks the PhotoRoute route. 
            //Arrange
            var context = new FakeHttpContextForRouting(requestUrl: "~/photo/2");
            var routes = new RouteCollection();
            PhotoSharingApplication_2015.RouteConfig.RegisterRoutes(routes);

            // Act
            RouteData routeData = routes.GetRouteData(context);

            // Assert
            //Assert that the routedata is not null
            Assert.IsNotNull(routeData);
            //Assert that the controller value in routeData is Photo
            Assert.AreEqual("Photo", routeData.Values["controller"]);
            //Assert that the action value in routedata is Display
            Assert.AreEqual("Display", routeData.Values["action"]);
            //Assert that the id value in routedata is 2
            Assert.AreEqual("2", routeData.Values["id"]);
        }

        [TestMethod]
        public void Test_Photo_Title_Route()
        {
            //This test checks the PhotoTitleRoute route a title is specified
            //Arrange
            var context = new FakeHttpContextForRouting(requestUrl: "~/Photo/title/my%20title");
            var routes = new RouteCollection();
            PhotoSharingApplication_2015.RouteConfig.RegisterRoutes(routes);

            // Act
            RouteData routeData = routes.GetRouteData(context);

            // Assert
            //Assert that the routedata is not null
            Assert.IsNotNull(routeData);
            //Assert that the controller value in routeData is Photo
            Assert.AreEqual("Photo", routeData.Values["controller"]);
            //Assert that the action value in routedata is DisplayByTitle            
            Assert.AreEqual("DisplayByTitle", routeData.Values["action"]);
            //Assert that the title value in routedata is my%20title            
            Assert.AreEqual("my%20title", routeData.Values["title"]);
        }


    }
}
