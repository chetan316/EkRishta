namespace Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DBEntity : DbContext
    {
        public DBEntity()
            : base("name=DBEntity")
        {
        }

        public virtual DbSet<SMSLog> SMSLogs { get; set; }
        public virtual DbSet<UserAddressDetail> UserAddressDetails { get; set; }
        public virtual DbSet<UserFamilyDetail> UserFamilyDetails { get; set; }
        public virtual DbSet<UserMaster> UserMasters { get; set; }
        public virtual DbSet<UserOtherDetail> UserOtherDetails { get; set; }
        public virtual DbSet<UserProfessionalDetail> UserProfessionalDetails { get; set; }
        public virtual DbSet<UserReligionDetail> UserReligionDetails { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SMSLog>()
                .Property(e => e.MobileNo)
                .IsUnicode(false);

            modelBuilder.Entity<SMSLog>()
                .Property(e => e.SMSText)
                .IsUnicode(false);

            modelBuilder.Entity<SMSLog>()
                .Property(e => e.APIRequest)
                .IsUnicode(false);

            modelBuilder.Entity<SMSLog>()
                .Property(e => e.APIResponse)
                .IsUnicode(false);

            modelBuilder.Entity<UserAddressDetail>()
                .Property(e => e.Address1)
                .IsUnicode(false);

            modelBuilder.Entity<UserAddressDetail>()
                .Property(e => e.Address2)
                .IsUnicode(false);

            modelBuilder.Entity<UserAddressDetail>()
                .Property(e => e.Pincode)
                .IsUnicode(false);

            modelBuilder.Entity<UserAddressDetail>()
                .Property(e => e.AlternateAddress1)
                .IsUnicode(false);

            modelBuilder.Entity<UserAddressDetail>()
                .Property(e => e.AlternateAddress2)
                .IsUnicode(false);

            modelBuilder.Entity<UserAddressDetail>()
                .Property(e => e.AlternatePincode)
                .IsUnicode(false);

            modelBuilder.Entity<UserAddressDetail>()
                .Property(e => e.IsActive)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<UserFamilyDetail>()
                .Property(e => e.FatherName)
                .IsUnicode(false);

            modelBuilder.Entity<UserFamilyDetail>()
                .Property(e => e.MotherName)
                .IsUnicode(false);

            modelBuilder.Entity<UserFamilyDetail>()
                .Property(e => e.FatherProfession)
                .IsUnicode(false);

            modelBuilder.Entity<UserFamilyDetail>()
                .Property(e => e.MotherProfession)
                .IsUnicode(false);

            modelBuilder.Entity<UserFamilyDetail>()
                .Property(e => e.FamilyLocation)
                .IsUnicode(false);

            modelBuilder.Entity<UserFamilyDetail>()
                .Property(e => e.FamilyDescription)
                .IsUnicode(false);

            modelBuilder.Entity<UserFamilyDetail>()
                .Property(e => e.IsActive)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<UserMaster>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<UserMaster>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<UserMaster>()
                .Property(e => e.ProfileId)
                .IsUnicode(false);

            modelBuilder.Entity<UserMaster>()
                .Property(e => e.MobileNo)
                .IsUnicode(false);

            modelBuilder.Entity<UserMaster>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<UserMaster>()
                .Property(e => e.Gender)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<UserMaster>()
                .Property(e => e.EmailId)
                .IsUnicode(false);

            modelBuilder.Entity<UserMaster>()
                .Property(e => e.IsSurnameVisible)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<UserMaster>()
                .Property(e => e.IsDPVisible)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<UserMaster>()
                .Property(e => e.IsActive)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<UserOtherDetail>()
                .Property(e => e.MotherTounge)
                .IsUnicode(false);

            modelBuilder.Entity<UserOtherDetail>()
                .Property(e => e.BirthPlace)
                .IsUnicode(false);

            modelBuilder.Entity<UserOtherDetail>()
                .Property(e => e.BirthTime)
                .IsUnicode(false);

            modelBuilder.Entity<UserOtherDetail>()
                .Property(e => e.Height)
                .IsUnicode(false);

            modelBuilder.Entity<UserOtherDetail>()
                .Property(e => e.BodyType)
                .IsUnicode(false);

            modelBuilder.Entity<UserOtherDetail>()
                .Property(e => e.SkinTone)
                .IsUnicode(false);

            modelBuilder.Entity<UserOtherDetail>()
                .Property(e => e.BloodGroup)
                .IsUnicode(false);

            modelBuilder.Entity<UserOtherDetail>()
                .Property(e => e.IsSmoke)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<UserOtherDetail>()
                .Property(e => e.IsDrink)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<UserOtherDetail>()
                .Property(e => e.IsPhysicalDisable)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<UserOtherDetail>()
                .Property(e => e.IdealpartnerDescription)
                .IsUnicode(false);

            modelBuilder.Entity<UserOtherDetail>()
                .Property(e => e.CallTime)
                .IsUnicode(false);

            modelBuilder.Entity<UserOtherDetail>()
                .Property(e => e.ProfileCreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<UserOtherDetail>()
                .Property(e => e.ProfilePicPath)
                .IsUnicode(false);

            modelBuilder.Entity<UserOtherDetail>()
                .Property(e => e.IsActive)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<UserProfessionalDetail>()
                .Property(e => e.CollegeName)
                .IsUnicode(false);

            modelBuilder.Entity<UserProfessionalDetail>()
                .Property(e => e.Degree)
                .IsUnicode(false);

            modelBuilder.Entity<UserProfessionalDetail>()
                .Property(e => e.Field)
                .IsUnicode(false);

            modelBuilder.Entity<UserProfessionalDetail>()
                .Property(e => e.CompanyName)
                .IsUnicode(false);

            modelBuilder.Entity<UserProfessionalDetail>()
                .Property(e => e.Designation)
                .IsUnicode(false);

            modelBuilder.Entity<UserProfessionalDetail>()
                .Property(e => e.Income)
                .IsUnicode(false);

            modelBuilder.Entity<UserProfessionalDetail>()
                .Property(e => e.IsActive)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<UserReligionDetail>()
                .Property(e => e.MoonSign)
                .IsUnicode(false);

            modelBuilder.Entity<UserReligionDetail>()
                .Property(e => e.Star)
                .IsUnicode(false);

            modelBuilder.Entity<UserReligionDetail>()
                .Property(e => e.Gotra)
                .IsUnicode(false);

            modelBuilder.Entity<UserReligionDetail>()
                .Property(e => e.IsActive)
                .IsFixedLength()
                .IsUnicode(false);
        }
    }
}
