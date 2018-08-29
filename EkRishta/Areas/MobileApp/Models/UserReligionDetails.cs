using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Areas.Models
{
    public class UserReligionDetails
    {
        public int UserReligionId { get; set; }
        public int ReligionId { get; set; }
        public string ReligionName { get; set; }
        public int CastId { get; set; }
        public string CastName { get; set; }
        //public int SubCastId { get; set; }
        public string MoonSign { get; set; }
        public string Star { get; set; }
        public string Gotra { get; set; }
        public int UserId { get; set; }
        public SelectList ReligionDetails { get; set; }
        public SelectList CastDetails { get; set; }
    }
}