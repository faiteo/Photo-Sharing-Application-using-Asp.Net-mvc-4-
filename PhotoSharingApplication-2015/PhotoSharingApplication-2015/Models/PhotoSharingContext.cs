using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PhotoSharingApplication_2015.Models
{
    public class PhotoSharingContext:DbContext, IPhotoSharingContext
    {
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Comment> Comments { get; set; }



        IQueryable<Photo> IPhotoSharingContext.Photos
        {
            get { return Photos; }
        }

        IQueryable<Comment> IPhotoSharingContext.Comments
        {
            get { return Comments; }
        }

        int IPhotoSharingContext.SaveChanges()
        {
            return SaveChanges();
        }

        T IPhotoSharingContext.Add<T>(T entity) 
        {
            return Set<T>().Add(entity);
        }

        Photo IPhotoSharingContext.FindPhotoById(int ID)
        {
            return Set<Photo>().Find(ID); 
            //Photo p = Photos.Find(ID);
            //return p;
        }

        Comment IPhotoSharingContext.FindCommentById(int ID)
        {
            return Set<Comment>().Find(ID); 
        }

        T IPhotoSharingContext.Delete<T>(T entity) 
        {
            return Set<T>().Remove(entity) ;
        }


        public Photo FindPhotoByTitle(string photoTitle)
        {
            Photo photo = (from p in Set<Photo>()
                           where p.Title == photoTitle
                           select p).FirstOrDefault();
            return photo;
        }
    }
}