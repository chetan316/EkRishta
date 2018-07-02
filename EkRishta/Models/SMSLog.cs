namespace Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SMSLog
    {
        public long SMSLogId { get; set; }

        [StringLength(20)]
        public string MobileNo { get; set; }

        [StringLength(500)]
        public string SMSText { get; set; }

        [StringLength(500)]
        public string APIRequest { get; set; }

        [StringLength(500)]
        public string APIResponse { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}
