CREATE TABLE [dbo].[CastMaster](
	[CastId] [int] IDENTITY(1,1) NOT NULL,
	[CastName] [varchar](50) NULL,
	[IsActive] [char](1) NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL DEFAULT (getdate()),
	[UpdatedBy] [bigint] NULL,
	[UpdatedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[CastId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CityMaster]    Script Date: 1/29/2019 11:37:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CityMaster](
	[CityId] [int] IDENTITY(1,1) NOT NULL,
	[CityName] [varchar](100) NULL,
	[StateId] [int] NULL,
	[IsActive] [char](1) NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL DEFAULT (getdate()),
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[CityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CountryMaster]    Script Date: 1/29/2019 11:37:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CountryMaster](
	[CountryId] [int] IDENTITY(1,1) NOT NULL,
	[CountryName] [varchar](100) NULL,
	[IsActive] [char](1) NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL DEFAULT (getdate()),
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[CountryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DBLogs]    Script Date: 1/29/2019 11:37:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DBLogs](
	[ErrMessage] [varchar](max) NULL,
	[StackTrace] [varchar](max) NULL,
	[UserId] [int] NULL,
	[CreatedDate] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[LanguageMaster]    Script Date: 1/29/2019 11:37:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[LanguageMaster](
	[LanguageId] [int] IDENTITY(1,1) NOT NULL,
	[LanguageName] [varchar](50) NULL,
	[IsActive] [char](1) NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL DEFAULT (getdate()),
	[UpdatedBy] [bigint] NULL,
	[UpdatedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[LanguageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[OTPDetails]    Script Date: 1/29/2019 11:37:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[OTPDetails](
	[OTP] [varchar](10) NULL,
	[MobileNo] [varchar](20) NULL,
	[IsActive] [char](1) NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedDate] [datetime] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProfileVisitors]    Script Date: 1/29/2019 11:37:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProfileVisitors](
	[VisitedUserId] [bigint] NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL DEFAULT (getdate()),
	[UpdatedDate] [datetime] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ReligionMaster]    Script Date: 1/29/2019 11:37:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ReligionMaster](
	[ReligionId] [int] IDENTITY(1,1) NOT NULL,
	[ReligionName] [varchar](50) NULL,
	[IsActive] [char](1) NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL DEFAULT (getdate()),
	[UpdatedBy] [bigint] NULL,
	[UpdatedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ReligionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SendRequest]    Script Date: 1/29/2019 11:37:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SendRequest](
	[SendRequestId] [bigint] IDENTITY(1,1) NOT NULL,
	[RequestedUserId] [bigint] NULL,
	[Status] [varchar](20) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [bigint] NULL,
	[UpdatedBy] [bigint] NULL,
	[UpdatedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[SendRequestId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ShortlistedProfiles]    Script Date: 1/29/2019 11:37:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ShortlistedProfiles](
	[ShortlistedUserId] [int] NULL,
	[Status] [varchar](30) NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [datetime] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SMSLogs]    Script Date: 1/29/2019 11:37:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SMSLogs](
	[SMSLogId] [bigint] IDENTITY(1,1) NOT NULL,
	[MobileNo] [varchar](20) NULL,
	[SMSText] [varchar](500) NULL,
	[APIRequest] [varchar](500) NULL,
	[APIResponse] [varchar](500) NULL,
	[CreatedDate] [datetime] NULL CONSTRAINT [DF__SMSLogs__Created__4CA06362]  DEFAULT (getdate()),
 CONSTRAINT [PK__SMSLogs__5968248053A5EC20] PRIMARY KEY CLUSTERED 
(
	[SMSLogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[StateMaster]    Script Date: 1/29/2019 11:37:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[StateMaster](
	[StateId] [int] IDENTITY(1,1) NOT NULL,
	[StateName] [varchar](100) NULL,
	[CountryId] [int] NULL,
	[IsActive] [char](1) NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL DEFAULT (getdate()),
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[StateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UploadedImages]    Script Date: 1/29/2019 11:37:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UploadedImages](
	[ImageId] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [bigint] NULL,
	[ImagePath] [varchar](100) NULL,
	[CreatedDate] [datetime] NULL DEFAULT (getdate()),
	[ImageType] [varchar](30) NULL,
PRIMARY KEY CLUSTERED 
(
	[ImageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserAddressDetails]    Script Date: 1/29/2019 11:37:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserAddressDetails](
	[AddressId] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [bigint] NULL,
	[Address1] [varchar](500) NULL,
	[Address2] [varchar](500) NULL,
	[CityId] [int] NULL,
	[StateId] [int] NULL,
	[CountryId] [int] NULL,
	[Pincode] [varchar](10) NULL,
	[AlternateAddress1] [varchar](500) NULL,
	[AlternateAddress2] [varchar](500) NULL,
	[AlternateCityId] [int] NULL,
	[AlternateStateId] [int] NULL,
	[AlternateCountryId] [int] NULL,
	[AlternatePincode] [varchar](10) NULL,
	[IsActive] [char](1) NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedBy] [bigint] NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_UserAddressDetails] PRIMARY KEY CLUSTERED 
(
	[AddressId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserFamilyDetails]    Script Date: 1/29/2019 11:37:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserFamilyDetails](
	[UserFamilyDetailsId] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [bigint] NULL,
	[FatherName] [varchar](50) NULL,
	[MotherName] [varchar](50) NULL,
	[FatherProfession] [varchar](50) NULL,
	[MotherProfession] [varchar](50) NULL,
	[FamilyLocation] [varchar](50) NULL,
	[FamilyDescription] [varchar](500) NULL,
	[IsActive] [char](1) NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedBy] [bigint] NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_UserFamilyDetails] PRIMARY KEY CLUSTERED 
(
	[UserFamilyDetailsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserMaster]    Script Date: 1/29/2019 11:37:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserMaster](
	[UserId] [bigint] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](50) NULL,
	[LastName] [varchar](50) NULL,
	[ProfileId] [varchar](50) NULL,
	[MobileNo] [varchar](20) NULL,
	[Password] [varchar](50) NULL,
	[DOB] [varchar](20) NULL,
	[Age] [int] NULL,
	[Gender] [char](1) NULL,
	[EmailId] [varchar](50) NULL,
	[IsSurnameVisible] [char](1) NULL,
	[IsDPVisible] [char](1) NULL,
	[IsActive] [char](1) NULL,
	[ShareCount] [int] NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedBy] [bigint] NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK__UserMast__1788CC4C4AC6F4DB] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserOtherDetails]    Script Date: 1/29/2019 11:37:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserOtherDetails](
	[UserOtherDetailsId] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [bigint] NULL,
	[MaritialStatus] [varchar](20) NULL,
	[MotherTounge] [int] NULL,
	[BirthCountry] [int] NULL,
	[BirthPlace] [varchar](50) NULL,
	[BirthTime] [varchar](20) NULL,
	[Height] [varchar](20) NULL,
	[BodyType] [varchar](20) NULL,
	[SkinTone] [varchar](20) NULL,
	[BloodGroup] [varchar](20) NULL,
	[IsSmoke] [char](1) NULL,
	[IsDrink] [char](1) NULL,
	[IsPhysicalDisable] [char](1) NULL,
	[IdealpartnerDescription] [varchar](500) NULL,
	[CallTime] [varchar](20) NULL,
	[ProfileCreatedBy] [varchar](30) NULL,
	[ProfilePicPath] [varchar](200) NULL,
	[IsActive] [char](1) NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedBy] [bigint] NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_UserOtherDetails] PRIMARY KEY CLUSTERED 
(
	[UserOtherDetailsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserPreferences]    Script Date: 1/29/2019 11:37:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserPreferences](
	[UserPreferenceId] [bigint] IDENTITY(1,1) NOT NULL,
	[FromAge] [int] NULL,
	[ToAge] [int] NULL,
	[FromHeight] [varchar](30) NULL,
	[ToHeight] [varchar](30) NULL,
	[MaritialStatus] [varchar](30) NULL,
	[CityId] [int] NULL,
	[CountryId] [int] NULL,
	[ReligionId] [int] NULL,
	[CasteId] [int] NULL,
	[MotherToungeId] [int] NULL,
	[Income] [varchar](50) NULL,
	[Diet] [varchar](20) NULL,
	[IsDrink] [char](1) NULL,
	[IsSmoke] [char](1) NULL,
	[IsPhysicalDisable] [char](1) NULL,
	[SkinTone] [varchar](30) NULL,
	[BodyType] [varchar](50) NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedBy] [bigint] NULL,
	[UpdatedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[UserPreferenceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserProfessionalDetails]    Script Date: 1/29/2019 11:37:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserProfessionalDetails](
	[UserProfessionalDetailsId] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [bigint] NULL,
	[CollegeName] [varchar](50) NULL,
	[Degree] [varchar](50) NULL,
	[Field] [varchar](50) NULL,
	[CompanyName] [varchar](50) NULL,
	[Designation] [varchar](20) NULL,
	[Income] [varchar](50) NULL,
	[IsActive] [char](1) NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedBy] [bigint] NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_UserProfessionalDetails] PRIMARY KEY CLUSTERED 
(
	[UserProfessionalDetailsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserReligionDetails]    Script Date: 1/29/2019 11:37:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserReligionDetails](
	[UserReligionId] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [bigint] NULL,
	[ReligionId] [int] NULL,
	[CastId] [int] NULL,
	[SubCastId] [int] NULL,
	[MoonSign] [varchar](20) NULL,
	[Star] [varchar](20) NULL,
	[Gotra] [varchar](20) NULL,
	[IsActive] [char](1) NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedBy] [bigint] NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_UserReligionDetails] PRIMARY KEY CLUSTERED 
(
	[UserReligionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
