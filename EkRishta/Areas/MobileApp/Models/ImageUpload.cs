using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Areas.Models
{
    public class ImageUpload
    {
        public string ImageId { get; set; }
        public string UserId { get; set; }
        public string ImagePath { get; set; }
    }
}