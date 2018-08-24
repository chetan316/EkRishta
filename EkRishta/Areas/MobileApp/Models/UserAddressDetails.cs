using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Areas.Models
{
    public class UserAddressDetails
    {
        public int AddressId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public int StateId { get; set; }
        public string StateName { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public string Pincode { get; set; }
        public string AlternateAddress1 { get; set; }
        public string AlternateAddress2 { get; set; }
        public int AlternateCityId { get; set; }
        public string AlternateCityName { get; set; }
        public int AlternateStateId { get; set; }
        public string AlternateStateName { get; set; }
        public int AlternateCountryId { get; set; }
        public string AlternateCountryName { get; set; }
        public string AlternatePincode { get; set; }
    }
}