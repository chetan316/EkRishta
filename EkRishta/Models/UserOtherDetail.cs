namespace Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UserOtherDetail
    {
        [Key]
        public long UserOtherDetailsId { get; set; }

        public long? UserId { get; set; }

        public int? MaritialStatus { get; set; }

        [StringLength(20)]
        public string MotherTounge { get; set; }

        public int? BirthCountry { get; set; }

        [StringLength(50)]
        public string BirthPlace { get; set; }

        [StringLength(10)]
        public string BirthTime { get; set; }

        [StringLength(10)]
        public string Height { get; set; }

        [StringLength(20)]
        public string BodyType { get; set; }

        [StringLength(20)]
        public string SkinTone { get; set; }

        [StringLength(5)]
        public string BloodGroup { get; set; }

        [StringLength(1)]
        public string IsSmoke { get; set; }

        [StringLength(1)]
        public string IsDrink { get; set; }

        [StringLength(1)]
        public string IsPhysicalDisable { get; set; }

        [StringLength(500)]
        public string IdealpartnerDescription { get; set; }

        [StringLength(20)]
        public string CallTime { get; set; }

        [StringLength(30)]
        public string ProfileCreatedBy { get; set; }

        [StringLength(200)]
        public string ProfilePicPath { get; set; }

        [StringLength(1)]
        public string IsActive { get; set; }

        public long? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public long? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}
