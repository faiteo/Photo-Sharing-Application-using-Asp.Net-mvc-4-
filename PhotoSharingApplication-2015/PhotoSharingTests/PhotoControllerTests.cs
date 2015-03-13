using System;
using System.Collections.Generic;
using System.Web.Mvc;
using PhotoSharingApplication_2015.Controllers;
using PhotoSharingApplication_2015.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using PhotoSharingTests.Doubles;

namespace PhotoSharingTests
{
    [TestClass]
    public class PhotoControllerTests
    {
        [TestMethod]
        public void Test_Index_Return_View()
        {
            var fps = new FakePhotoSharingContext();
            var controller = new PhotoController(fps); 
            var returnResult = controller.Index() as ViewResult;
            Assert.AreEqual(returnResult.ViewName, "Index");
        }

        [TestMethod]
        public void Test_PhotoGallery_Model_Type()
        {
            var fps = new FakePhotoSharingContext();
            fps.Photos = new List<Photo> 
                {
                    new Photo(),
                    new Photo(),
                    new Photo(),
                    new Photo()
                }.AsQueryable();

            var controller = new PhotoController(fps);
            var retList = controller._PhotoGallery() as PartialViewResult;
            Assert.AreEqual(typeof(List<Photo>), retList.Model.GetType()); 
        }

        [TestMethod]
        public void Test_GetImage_ReturnType()
        {
            var fps = new FakePhotoSharingContext();
            fps.Photos = new[] {
            new Photo{ PhotoID = 1, PhotoFile = new byte[1], ImageMimeType = "image/jpeg"},
            new Photo{ PhotoID = 2, PhotoFile = new byte[1], ImageMimeType = "image/jpeg"},
            new Photo{ PhotoID = 3, PhotoFile = new byte[1], ImageMimeType = "image/jpeg"},
            new Photo{ PhotoID = 4, PhotoFile = new byte[1], ImageMimeType = "image/jpeg"}
            }.AsQueryable();
            var controller = new PhotoController(fps);
            var photo = controller.GetImage(2) as ActionResult;
            Assert.AreEqual(photo.GetType(), typeof(FileContentResult)); 
        }

        [TestMethod]
        public void Test_PhotoGallery_No_Parameter()
        {
            var fps = new FakePhotoSharingContext();
           //Create and add 4 photo objects to fps new instance and pass it to 
           //the controller constructor.
            fps.Photos = new[]{
                new Photo{},
                new Photo{},
                new Photo{},
                new Photo{}
            }.AsQueryable();
            var controller = new PhotoController(fps);
            var retResult = controller._PhotoGallery() as PartialViewResult;
            //cast retResult.Model property as an IEnumerable<Photo> collection
            var modelPhotos = (IEnumerable<Photo>)retResult.Model;
            //check that the number of photos in the collection is 4
            Assert.AreEqual(4, modelPhotos.Count());
        }


        [TestMethod]
        public void Test_PhotoGallery_Int_Parameter(int num)
        {
            var fps = new FakePhotoSharingContext();
            //Create and add 4 photo objects to fps new instance and pass it to 
            //the controller constructor.
            fps.Photos = new[]{
                new Photo{},
                new Photo{},
                new Photo{},
                new Photo{}
            }.AsQueryable();
            var controller = new PhotoController(fps);
            var retResult = controller._PhotoGallery(3) as PartialViewResult;
            //cast retResult.Model property as an IEnumerable<Photo> collection
            var modelPhotos = (IEnumerable<Photo>)retResult.Model;
            //check that the number of photos in the collection is 3
            Assert.AreEqual(3, modelPhotos.Count());
        }

    }
}
