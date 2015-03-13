using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
using PhotoSharingApplication_2015.Models;

namespace PhotoSharingApplication_2015.Controllers
{
    //Used for registering an action filter that is connected to this controller.
    [ValueReporter] 
    [HandleError(View ="Error")]
    public class PhotoController : Controller
    {
        //private PhotoSharingContext dbContext = new PhotoSharingContext();
        private IPhotoSharingContext context;

        public PhotoController()
        {
            context = new PhotoSharingContext();
        }

        public PhotoController(IPhotoSharingContext Context)
        {
            context = Context;
        }

        //
        // GET: /Photo/

        public ActionResult Index()
        {
            return View("Index");
        }




        public ActionResult Detail(int id)
        {
            Photo photo = context.FindPhotoById(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            else return View("Detail", photo);
        }

        //Get
        [HttpGet]
        public ActionResult Create()
        {
            Photo newPhoto = new Photo();
            newPhoto.CreationDate = DateTime.Today;
            return View("Create", newPhoto);
        }


        [HttpPost]
        public ActionResult Create(Photo photo, HttpPostedFileBase image)
        {
            photo.CreationDate = DateTime.Today;
            if (!ModelState.IsValid)
            {
                return View("Create", photo);
            }
            else
            {
                //if image object is not empty update the photo attribute, using image info.
                if (image != null)
                {
                    photo.ImageMimeType = image.ContentType;
                    photo.PhotoFile = new byte[image.ContentLength];
                    //save the photo file by using image.InputStream.Read method.
                    image.InputStream.Read(photo.PhotoFile, 0, image.ContentLength);
                }
                context.Add(photo);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Photo photoToReturn = context.FindPhotoById(id);
            if (photoToReturn == null)
            {
                return HttpNotFound();
            }
            else 
            {
                return View("Delete", photoToReturn);
            }
        }

        [HttpPost]
        public ActionResult ConfirmDelete(int id)
        {
            Photo photoToDelete = context.FindPhotoById(id);
            context.Delete(photoToDelete);
            context.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult _PhotoGallery(int number = 0) 
        { 
            List<Photo> photos = new List<Photo>();
            if (number == 0)
            {
                photos = context.Photos.ToList();
            }
            else 
            {
                photos = (from p in context.Photos
                                    orderby p.CreationDate descending
                                    select p).Take(number).ToList();  
            }
            return PartialView("_PhotoGallery", photos);
        }

        //used to get an image
        public FileContentResult GetImage(int id)
        {
            Photo photoToGet = context.FindPhotoById(id);
            if (photoToGet != null)
            {
                //File used to return a binary content and the contenttype of the returned photo
                return File(photoToGet.PhotoFile, photoToGet.ImageMimeType); 
            }
            else 
            {
                return null;
            }
        }

        public ActionResult SlideShow()
        {
            return View("SlideShow", context.Photos.ToList() );
        }


        public ActionResult DisplayByTitle(string title)
        {
            Photo photoToGet = context.FindPhotoByTitle(title);
            if (photoToGet == null)
            {
                return HttpNotFound();
            }
            else 
            {
                return View("Detail", photoToGet);
            }
              
  


                

 
        }

    }
}

