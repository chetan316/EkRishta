namespace Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UserReligionDetail
    {
        [Key]
        public long UserReligionId { get; set; }

        public long? UserId { get; set; }

        public int? ReligionId { get; set; }

        public int? CastId { get; set; }

        public int? SubCastId { get; set; }

        [StringLength(20)]
        public string MoonSign { get; set; }

        [StringLength(20)]
        public string Star { get; set; }

        [StringLength(20)]
        public string Gotra { get; set; }

        [StringLength(1)]
        public string IsActive { get; set; }

        public long? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public long? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}
