namespace Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UserFamilyDetail
    {
        [Key]
        public long UserFamilyDetailsId { get; set; }

        public long? UserId { get; set; }

        [StringLength(50)]
        public string FatherName { get; set; }

        [StringLength(50)]
        public string MotherName { get; set; }

        [StringLength(50)]
        public string FatherProfession { get; set; }

        [StringLength(50)]
        public string MotherProfession { get; set; }

        [StringLength(50)]
        public string FamilyLocation { get; set; }

        [StringLength(500)]
        public string FamilyDescription { get; set; }

        [StringLength(1)]
        public string IsActive { get; set; }

        public long? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public long? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}
