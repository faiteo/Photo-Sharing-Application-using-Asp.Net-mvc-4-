using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;

namespace PhotoSharingApplication_2015.Models
{
    public class PhotoSharingInitializer :DropCreateDatabaseAlways<PhotoSharingContext>
    {

        //This method puts sample data into the database
        protected override void Seed(PhotoSharingContext context)
        {
            base.Seed(context);

            //Create some photos
            var photos = new List<Photo>
            {
                new Photo {
                    Title = "On the top of a mountain",
                    Description = "I was very impressed with myself",
                    UserName = "Fred",
                    PhotoFile = getFileBytes("\\Images\\Man-on-top-of-mountain.JPG"),
                    ImageMimeType = "image/jpeg",
                    CreationDate = DateTime.Today
                },
                new Photo {
                    Title = "Biking around the Town",
                    Description = "Some description",
                    UserName = "Fred",
                    PhotoFile = getFileBytes("\\Images\\biking.jpg"),
                    ImageMimeType = "image/jpeg",
                    CreationDate = DateTime.Today
                },

                new Photo {
                    Title = "Flower",
                    Description = "Some description",
                    UserName = "Freddy",
                    PhotoFile = getFileBytes("\\Images\\flower.jpg"),
                    ImageMimeType = "image/jpeg",
                    CreationDate = DateTime.Today
                },

                new Photo {
                    Title = "Mountain Orchard",
                    Description = "Some description",
                    UserName = "Francis",
                    PhotoFile = getFileBytes("\\Images\\orchard.jpg"),
                    ImageMimeType = "image/jpeg",
                    CreationDate = DateTime.Today
                },

                new Photo {
                    Title = "Pink Flower",
                    Description = "Some description",
                    UserName = "Phillipe",
                    PhotoFile = getFileBytes("\\Images\\pinkflower.jpg"),
                    ImageMimeType = "image/jpeg",
                    CreationDate = DateTime.Today
                },
                new Photo {
                    Title = "View from the hotel",
                    Description = "I took this photo just before sunset at the hotel balcony.",
                    UserName = "Sue",
                    PhotoFile = getFileBytes("\\Images\\balcony_view.jpg"),
                    ImageMimeType = "image/jpeg",
                    CreationDate = DateTime.Today
                },

                new Photo {
                    Title = "Forest Path",
                    Description = "Some description",
                    UserName = "Julia",
                    PhotoFile = getFileBytes("\\Images\\path.jpg"),
                    ImageMimeType = "image/jpeg",
                    CreationDate = DateTime.Today
                }

            };

            photos.ForEach(p => context.Photos.Add(p));
            context.SaveChanges();

            //Create some comments
            var comments = new List<Comment>
            {
                new Comment {
                    PhotoID = 1,
                    UserName = "Bert",
                    Subject = "A Big Mountain",
                    Body = "That looks like a very high mountain you have climbed"
                },
                new Comment {
                    PhotoID = 1,
                    UserName = "Sue",
                    Subject = "So?",
                    Body = "I climbed a mountain that high before breakfast everyday"
                },
                new Comment {
                    PhotoID = 2,
                    UserName = "Fred",
                    Subject = "Great",
                    Body = "Wow, that new bike looks great!"
                },


                    new Comment {
                    PhotoID = 3,
                    UserName = "Fred",
                    Subject = "Great",
                    Body = "Test comment for any photo"
                },

                new Comment {
                    PhotoID = 4,
                    UserName = "Fred",
                    Subject = "Great",
                    Body = "Test comment for any photo"
                },

                new Comment {
                    PhotoID = 5,
                    UserName = "Fred",
                    Subject = "Great",
                    Body = "Test comment for any photo"
                },

                new Comment {
                    PhotoID = 6,
                    UserName = "Fred",
                    Subject = "Great",
                    Body = "Test comment for any photo"
                },













                new Comment {
                    PhotoID = 2,
                    UserName = "Fred",
                    Subject = "Hey",
                    Body = "Wow, great view !"
                }
            };
            comments.ForEach(c => context.Comments.Add(c));
            context.SaveChanges();
        }
        
        //This gets a byte array for a file at the path specified
        //The path is relative to the route of the web site
        //It is used to seed images
        private byte[] getFileBytes(string path)
        {
            FileStream fileOnDisk = new FileStream(HttpRuntime.AppDomainAppPath + path, FileMode.Open);
            byte[] fileBytes;
            using (BinaryReader br = new BinaryReader(fileOnDisk))
            {
                fileBytes = br.ReadBytes((int)fileOnDisk.Length);
            }
            return fileBytes;
        }
    }
}