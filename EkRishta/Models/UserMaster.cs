namespace Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserMaster")]
    public partial class UserMaster
    {
        [Key]
        public long UserId { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(50)]
        public string ProfileId { get; set; }

        [StringLength(20)]
        public string MobileNo { get; set; }

        [StringLength(50)]
        public string Password { get; set; }

        public DateTime? DOB { get; set; }

        public int? Age { get; set; }

        [StringLength(1)]
        public string Gender { get; set; }

        [StringLength(50)]
        public string EmailId { get; set; }

        [StringLength(1)]
        public string IsSurnameVisible { get; set; }

        [StringLength(1)]
        public string IsDPVisible { get; set; }

        [StringLength(1)]
        public string IsActive { get; set; }

        public long? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public long? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}
