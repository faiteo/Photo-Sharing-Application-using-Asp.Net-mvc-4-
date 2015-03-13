using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhotoSharingApplication_2015.Models
{
    public class Photo
    {
        
        public int PhotoID { get; set; }
        
        [Required]
        [DisplayName("Photo Title")]
        public string Title { get; set; }

        [DisplayName("Upload Photo")]
        public byte[] PhotoFile { get; set; }
        public string ImageMimeType { get; set; }
        
        [DataType(DataType.MultilineText)]
        [DisplayName("A Brief Description")]
        public string Description { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayName("Created Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yy}", ApplyFormatInEditMode = true)]
        public DateTime CreationDate { get; set; }
        
        public string UserName { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }

       
    }
}