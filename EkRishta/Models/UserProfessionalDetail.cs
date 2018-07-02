namespace Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UserProfessionalDetail
    {
        [Key]
        public long UserProfessionalDetailsId { get; set; }

        public long? UserId { get; set; }

        [StringLength(50)]
        public string CollegeName { get; set; }

        [StringLength(50)]
        public string Degree { get; set; }

        [StringLength(50)]
        public string Field { get; set; }

        [StringLength(10)]
        public string CompanyName { get; set; }

        [StringLength(20)]
        public string Designation { get; set; }

        [StringLength(20)]
        public string Income { get; set; }

        [StringLength(1)]
        public string IsActive { get; set; }

        public long? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public long? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}
