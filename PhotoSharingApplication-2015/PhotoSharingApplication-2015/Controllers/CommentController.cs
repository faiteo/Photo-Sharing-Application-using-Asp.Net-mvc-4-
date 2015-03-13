using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhotoSharingApplication_2015.Models;

namespace PhotoSharingApplication_2015.Controllers
{
    public class CommentController : Controller
    {
        private IPhotoSharingContext context;

        //Constructors
        public CommentController()
        {
            context = new PhotoSharingContext();
        }

        public CommentController(IPhotoSharingContext Context)
        {
            context = Context;
        }

        //
        // GET: /Comment/Delete/5
        public ActionResult Delete(int id = 0)
        {
            Comment comment = context.FindCommentById(id);
            ViewBag.PhotoID = comment.PhotoID;
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        //
        // POST: /Comment/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Comment comment = context.FindCommentById(id);
            context.Delete<Comment>(comment);
            context.SaveChanges();
            return RedirectToAction("Detail", "Photo", new { id = comment.PhotoID });
        }


        // GET: A Partial View for displaying in the Photo Details view
        //The attribute "ChildActionOnly" ensures that the action cannot be accessed from the browser's address bar
        [ChildActionOnly]
        public PartialViewResult _CommentsForPhoto(int photoId)
        {
            //Save the PhotoID in the ViewBag because we'll need it in the view
            ViewBag.PhotoId = photoId;
            //The comments for a particular photo have been requested. Get those comments.
            var commList = (from c in context.Comments
                                     where c.PhotoID == photoId
                                     select c).ToList();
            return PartialView(commList);
        }

        //GET
        public PartialViewResult _Create(int photoid)
        {
            Comment com = new Comment
            {
                PhotoID = photoid
            };
            ViewBag.PhotoID = photoid;
            return PartialView("_CreateAComment");
        }


        //POST method
        [HttpPost]
        //POST: This action creates the comment when the AJAX comment create tool is used
        public PartialViewResult _CommentsForPhoto(Comment comm, int photoid)
        {
            //Save the new comment
            context.Add<Comment>(comm);
            context.SaveChanges();

            //Get the updated list of comments
            var comments = from c in context.Comments
                           where c.PhotoID == photoid
                           select c;

            //var comments = _CommentsForPhoto(photoid);

            //Save the PhotoID in the ViewBag because we'll need it in the view
            ViewBag.PhotoId = photoid;

            //Return the view with the new list of comments
            return PartialView("_CommentsForPhoto", comments);
        }


    }
}
