namespace Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UserAddressDetail
    {
        [Key]
        public long AddressId { get; set; }

        public long? UserId { get; set; }

        [StringLength(500)]
        public string Address1 { get; set; }

        [StringLength(500)]
        public string Address2 { get; set; }

        public int? CityId { get; set; }

        public int? StateId { get; set; }

        public int? CountryId { get; set; }

        [StringLength(10)]
        public string Pincode { get; set; }

        [StringLength(500)]
        public string AlternateAddress1 { get; set; }

        [StringLength(500)]
        public string AlternateAddress2 { get; set; }

        public int? AlternateCityId { get; set; }

        public int? AlternateStateId { get; set; }

        public int? AlternateCountryId { get; set; }

        [StringLength(10)]
        public string AlternatePincode { get; set; }

        [StringLength(1)]
        public string IsActive { get; set; }

        public long? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public long? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}
