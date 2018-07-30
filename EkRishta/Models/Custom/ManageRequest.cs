using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.Custom
{
    public class ManageRequest
    {
        public int SendRequestId { get; set; }
        public int RequestedUserId { get; set; }
        public string RequestStatus { get; set; }
    }
}