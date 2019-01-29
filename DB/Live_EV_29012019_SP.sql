/****** Object:  StoredProcedure [dbo].[GetAcceptedRequest]    Script Date: 1/29/2019 11:39:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--GetAcceptedRequest @UserId=2,@RequestType='Accepted'
CREATE PROC [dbo].[GetAcceptedRequest]
@UserId INT,
@RequestType VARCHAR(10)
AS
BEGIN
	IF(@RequestType = 'Accepted')
	BEGIN
		SELECT ISNULL(UM.UserId,'') AS UserId,ISNULL(UM.FirstName,'') AS FirstName,ISNULL(UM.LastName,'') AS LastName,ISNULL(UM.ProfileId,'') AS ProfileId,
			   ISNULL(UM.MobileNo,'') AS MobileNo,ISNULL(UM.DOB,'') AS DOB,ISNULL(UM.Age,'') AS Age,ISNULL(UM.Gender,'') AS Gender,
			   ISNULL(UM.EmailId,'Not Specified') AS EmailId,ISNULL(UM.IsSurnameVisible,'') AS IsSurnameVisible,ISNULL(UM.IsDPVisible,'') AS IsDPVisible,
		   
			   ISNULL(UO.MaritialStatus,'') AS MaritialStatus,ISNULL(UO.Height,'Not Specified') AS Height,ISNULL(UO.ProfilePicPath,'') AS ProfilePicPath,
			   ISNULL(UR.ReligionId,'') AS ReligionId,ISNULL(RM.ReligionName,'Not Specified') AS ReligionName,
			   ISNULL(UR.CastId,'') AS CastId, CASE WHEN ISNULL(UR.CastId,'') = '' THEN 'Not Specified' ELSE CM.CastName END AS CastName,ISNULL(UR.SubCastId,'') AS SubCastId,
			   (dbo.GetPhotoCount(UM.UserId)) AS PhotoCount,
			   UI.ImagePath,
			   'A' AS RequestStatus
		FROM SendRequest SR
		JOIN UserMaster UM ON UM.UserId = SR.CreatedBy AND UM.IsActive = '1'
		LEFT JOIN UserOtherDetails UO ON UO.UserId = UM.UserId AND UO.IsActive = '1'
		LEFT JOIN UserReligionDetails UR ON UR.UserId = UM.UserId AND UR.IsActive = '1'
		LEFT JOIN ReligionMaster RM ON RM.ReligionId = UR.ReligionId
		LEFT JOIN CastMaster CM ON CM.CastId = UR.CastId
		LEFT JOIN UploadedImages UI ON UI.UserId = UM.UserId AND UI.ImageType = 'Cover'
		WHERE SR.RequestedUserId = @UserId AND SR.Status = 'Accepted'
	END
	ELSE
	BEGIN
		SELECT ISNULL(UM.UserId,'') AS UserId,ISNULL(UM.FirstName,'') AS FirstName,ISNULL(UM.LastName,'') AS LastName,ISNULL(UM.ProfileId,'') AS ProfileId,
			   ISNULL(UM.MobileNo,'') AS MobileNo,ISNULL(UM.DOB,'') AS DOB,ISNULL(UM.Age,'') AS Age,ISNULL(UM.Gender,'') AS Gender,
			   ISNULL(UM.EmailId,'Not Specified') AS EmailId,ISNULL(UM.IsSurnameVisible,'') AS IsSurnameVisible,ISNULL(UM.IsDPVisible,'') AS IsDPVisible,
		   
			   ISNULL(UO.MaritialStatus,'') AS MaritialStatus,ISNULL(UO.Height,'Not Specified') AS Height,ISNULL(UO.ProfilePicPath,'') AS ProfilePicPath,
			   ISNULL(UR.ReligionId,'') AS ReligionId,ISNULL(RM.ReligionName,'Not Specified') AS ReligionName,
			   ISNULL(UR.CastId,'') AS CastId, CASE WHEN ISNULL(UR.CastId,'') = '' THEN 'Not Specified' ELSE CM.CastName END AS CastName,ISNULL(UR.SubCastId,'') AS SubCastId,
			   (dbo.GetPhotoCount(UM.UserId)) AS PhotoCount,
				UI.ImagePath,
				'A' AS RequestStatus
		FROM SendRequest SR
		JOIN UserMaster UM ON UM.UserId = SR.RequestedUserId AND UM.IsActive = '1'
		LEFT JOIN UserOtherDetails UO ON UO.UserId = UM.UserId AND UO.IsActive = '1'
		LEFT JOIN UserReligionDetails UR ON UR.UserId = UM.UserId AND UR.IsActive = '1'
		LEFT JOIN ReligionMaster RM ON RM.ReligionId = UR.ReligionId
		LEFT JOIN CastMaster CM ON CM.CastId = UR.CastId
		LEFT JOIN UploadedImages UI ON UI.UserId = UM.UserId AND UI.ImageType = 'Cover'
		WHERE SR.CreatedBy = @UserId AND SR.Status = 'Accepted'
	END
END



GO
/****** Object:  StoredProcedure [dbo].[GetBlockedRequest]    Script Date: 1/29/2019 11:39:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--GetBlockedRequest @UserId=1
CREATE PROC [dbo].[GetBlockedRequest]
@ReligionId INT = NULL,
@CastId INT = NULL,
@UserId INT
AS
BEGIN
	SELECT ISNULL(UM.UserId,'') AS UserId,ISNULL(UM.FirstName,'') AS FirstName,ISNULL(UM.LastName,'') AS LastName,ISNULL(UM.ProfileId,'') AS ProfileId,
		   ISNULL(UM.MobileNo,'') AS MobileNo,ISNULL(UM.DOB,'') AS DOB,ISNULL(UM.Age,'') AS Age,ISNULL(UM.Gender,'') AS Gender,
		   ISNULL(UM.EmailId,'Not Specified') AS EmailId,ISNULL(UM.IsSurnameVisible,'') AS IsSurnameVisible,ISNULL(UM.IsDPVisible,'') AS IsDPVisible,
		   ISNULL(UO.MaritialStatus,'') AS MaritialStatus,ISNULL(UO.Height,'Not Specified') AS Height,ISNULL(UO.ProfilePicPath,'') AS ProfilePicPath,
		   ISNULL(UR.ReligionId,'') AS ReligionId,ISNULL(RM.ReligionName,'Not Specified') AS ReligionName,
		   ISNULL(UR.CastId,'') AS CastId, CASE WHEN ISNULL(UR.CastId,'') = '' THEN 'Not Specified' ELSE CM.CastName END AS CastName,ISNULL(UR.SubCastId,'') AS SubCastId,
		   (dbo.GetPhotoCount(UM.UserId)) AS PhotoCount,
		   UI.ImagePath

	FROM SendRequest SR
	JOIN UserMaster UM ON UM.UserId = SR.CreatedBy AND UM.IsActive = '1'
	LEFT JOIN UserOtherDetails UO ON UO.UserId = UM.UserId AND UO.IsActive = '1'
	LEFT JOIN UserReligionDetails UR ON UR.UserId = UM.UserId AND UR.IsActive = '1'
	LEFT JOIN ReligionMaster RM ON RM.ReligionId = UR.ReligionId
	LEFT JOIN CastMaster CM ON CM.CastId = UR.CastId
	LEFT JOIN UploadedImages UI ON UI.UserId = UM.UserId AND UI.ImageType = 'Cover'
	WHERE SR.RequestedUserId = @UserId AND SR.Status = 'Blocked'

	
END



GO
/****** Object:  StoredProcedure [dbo].[GetCastMaster]    Script Date: 1/29/2019 11:39:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROC [dbo].[GetCastMaster]
AS
BEGIN
	SELECT * FROM CastMaster WHERE IsActive = '1'
END

GO
/****** Object:  StoredProcedure [dbo].[GetCityMaster]    Script Date: 1/29/2019 11:39:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetCityMaster]
AS
BEGIN
	SELECT * FROM CityMaster
	WHERE IsActive= '1'
	ORDER BY CityName
END




GO
/****** Object:  StoredProcedure [dbo].[GetCountryMaster]    Script Date: 1/29/2019 11:39:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetCountryMaster]
AS
BEGIN
	SELECT CountryId,CountryName FROM CountryMaster
	WHERE IsActive = 1
END

GO
/****** Object:  StoredProcedure [dbo].[GetDeclinedMeRequest]    Script Date: 1/29/2019 11:39:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--GetDeclinedMeRequest @UserId=1
CREATE PROC [dbo].[GetDeclinedMeRequest]
@ReligionId INT = NULL,
@CastId INT = NULL,
@UserId INT
AS
BEGIN
	SELECT ISNULL(UM.UserId,'') AS UserId,ISNULL(UM.FirstName,'') AS FirstName,ISNULL(UM.LastName,'') AS LastName,ISNULL(UM.ProfileId,'') AS ProfileId,
		   ISNULL(UM.MobileNo,'') AS MobileNo,ISNULL(UM.DOB,'') AS DOB,ISNULL(UM.Age,'') AS Age,ISNULL(UM.Gender,'') AS Gender,
		   ISNULL(UM.EmailId,'Not Specified') AS EmailId,ISNULL(UM.IsSurnameVisible,'') AS IsSurnameVisible,ISNULL(UM.IsDPVisible,'') AS IsDPVisible,
		   
		   ISNULL(UO.MaritialStatus,'') AS MaritialStatus,ISNULL(UO.Height,'Not Specified') AS Height,ISNULL(UO.ProfilePicPath,'') AS ProfilePicPath,
		   ISNULL(UR.ReligionId,'') AS ReligionId,ISNULL(RM.ReligionName,'Not Specified') AS ReligionName,
		   ISNULL(UR.CastId,'') AS CastId, CASE WHEN ISNULL(UR.CastId,'') = '' THEN 'Not Specified' ELSE CM.CastName END AS CastName,ISNULL(UR.SubCastId,'') AS SubCastId,
		   (dbo.GetPhotoCount(UM.UserId)) AS PhotoCount,
		   'R' AS RequestStatus,
			UI.ImagePath
		FROM SendRequest SR
		JOIN UserMaster UM ON UM.UserId = SR.RequestedUserId AND UM.IsActive = '1'
		LEFT JOIN UserOtherDetails UO ON UO.UserId = UM.UserId AND UO.IsActive = '1'
		LEFT JOIN UserReligionDetails UR ON UR.UserId = UM.UserId AND UR.IsActive = '1'
		LEFT JOIN ReligionMaster RM ON RM.ReligionId = UR.ReligionId
		LEFT JOIN CastMaster CM ON CM.CastId = UR.CastId
		LEFT JOIN UploadedImages UI ON UI.UserId = UM.UserId AND UI.ImageType = 'Cover'
		WHERE SR.CreatedBy = @UserId AND SR.Status = 'Rejected'
END



GO
/****** Object:  StoredProcedure [dbo].[GetLanguageMaster]    Script Date: 1/29/2019 11:39:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetLanguageMaster]
AS
BEGIN
	SELECT * FROM LanguageMaster WHERE IsActive = '1'
END


GO
/****** Object:  StoredProcedure [dbo].[GetMatchingProfile]    Script Date: 1/29/2019 11:39:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetMatchingProfile] --GetMatchingProfile @UserId=74,@ReligionId=3
@ReligionId INT = NULL,
@CastId INT = NULL,
@UserId INT
AS
BEGIN
	DECLARE @MatchGender VARCHAR(10);
	SET @MatchGender = (SELECT Gender FROM UserMaster WHERE UserId = @UserId);


	SELECT ISNULL(UM.UserId,'') AS UserId,ISNULL(UM.FirstName,'') AS FirstName,ISNULL(UM.LastName,'') AS LastName,ISNULL(UM.ProfileId,'') AS ProfileId,
		   ISNULL(UM.MobileNo,'') AS MobileNo,ISNULL(UM.DOB,'') AS DOB,ISNULL(UM.Age,'') AS Age,ISNULL(UM.Gender,'') AS Gender,
		   ISNULL(UM.EmailId,'Not Specified') AS EmailId,ISNULL(UM.IsSurnameVisible,'') AS IsSurnameVisible,ISNULL(UM.IsDPVisible,'') AS IsDPVisible,
		   
		   ISNULL(UO.MaritialStatus,'') AS MaritialStatus,ISNULL(UO.Height,'Not Specified') AS Height,ISNULL(UO.ProfilePicPath,'') AS ProfilePicPath,ISNULL(LM.LanguageName,'') AS MotherTounge,
		   ISNULL(UP.Designation,'') AS Designation,ISNULL(UP.Degree,'') AS Degree,
		   ISNULL(UR.ReligionId,'') AS ReligionId,ISNULL(RM.ReligionName,'Not Specified') AS ReligionName,
		   ISNULL(UR.CastId,'') AS CastId, CASE WHEN ISNULL(UR.CastId,'') = '' THEN 'Not Specified' ELSE CM.CastName END AS CastName,ISNULL(UR.SubCastId,'') AS SubCastId,
		   (dbo.GetPhotoCount(UM.UserId)) AS PhotoCount,
		   CASE WHEN SR.Status = 'Pending' THEN 'P' 				
				WHEN SR.Status = 'Cancelled' THEN 'C'  
				WHEN SR.Status = 'Accepted' THEN 'A'  
				WHEN SR.Status = 'Rejected' THEN 'R'  

			ELSE 'N' END AS RequestStatus,
			UI.ImagePath
			--,SR.CreatedBY,SR.RequestedUserId
	FROM UserReligionDetails UR
	LEFT JOIN UserMaster UM ON UM.UserId = UR.UserId AND UM.IsActive = '1'
	LEFT JOIN UserOtherDetails UO ON UO.UserId = UM.UserId AND UO.IsActive = '1'
	LEFT JOIN UserProfessionalDetails UP ON UP.UserId = UM.UserId AND UP.IsActive = '1'
	LEFT JOIN ReligionMaster RM ON RM.ReligionId = UR.ReligionId
	LEFT JOIN CastMaster CM ON CM.CastId = UR.CastId
	LEFT JOIN LanguageMaster LM ON LM.LanguageId = UO.MotherTounge
	LEFT JOIN SendRequest SR ON SR.RequestedUserId = UM.UserId AND SR.CreatedBy = @UserId
	LEFT JOIN UploadedImages UI ON UI.UserId = UM.UserId AND UI.ImageType = 'Cover'
	WHERE UM.UserId <> @UserId 
		  AND UM.Gender <> @MatchGender
		  --AND UR.ReligionId = @ReligionId 
		  --AND UM.UserId NOT IN (select RequestedUserId from SendRequest where CreatedBy = @UserId)
	ORDER BY UM.UserId DESC

	/*To Get Shortlisted Profiles*/
	SELECT ShortlistedUserId,Status
	FROM ShortlistedProfiles
	WHERE CreatedBy = @UserId
END
GO
/****** Object:  StoredProcedure [dbo].[GetProfileRequest]    Script Date: 1/29/2019 11:39:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetProfileRequest] --GetProfileRequest @UserId=5
@UserId INT

AS

BEGIN

	SELECT ISNULL(UM.UserId,'') AS UserId,ISNULL(UM.FirstName,'') AS FirstName,ISNULL(UM.LastName,'') AS LastName,ISNULL(UM.ProfileId,'') AS ProfileId,ISNULL(UM.MobileNo,'') AS MobileNo,ISNULL(UM.DOB,'') AS DOB,ISNULL(UM.Age,'') AS Age,ISNULL(UM.Gender,'') AS Gender,ISNULL(UM.EmailId,'Not Specified') AS EmailId,ISNULL(UM.IsSurnameVisible,'') AS IsSurnameVisible,ISNULL(UM.IsDPVisible,'') AS IsDPVisible,
		   ISNULL(UO.MaritialStatus,'') AS MaritialStatus,ISNULL(UO.Height,'Not Specified') AS Height,ISNULL(UO.ProfilePicPath,'') AS ProfilePicPath,
		   ISNULL(UR.ReligionId,'') AS ReligionId,ISNULL(RM.ReligionName,'Not Specified') AS ReligionName,
		   ISNULL(UR.CastId,'') AS CastId, CASE WHEN ISNULL(UR.CastId,'') = '' THEN 'Not Specified' ELSE CM.CastName END AS CastName,ISNULL(UR.SubCastId,'') AS SubCastId,
		   CASE WHEN SR.Status = 'Pending' THEN 'P' 
				WHEN SR.Status = 'Cancelled' THEN 'C'  
				WHEN SR.Status = 'Accepted' THEN 'A'
				WHEN SR.Status = 'Rejected' THEN 'R'
			ELSE 'N' END AS RequestStatus,
			SR.CreatedBy AS RequestingUserId,
			(dbo.GetPhotoCount(UM.UserId)) AS PhotoCount,
			UI.ImagePath
	FROM SendRequest SR
	JOIN UserMaster UM ON UM.UserId = SR.CreatedBy AND UM.IsActive = '1'
	LEFT JOIN UserOtherDetails UO ON UO.UserId = UM.UserId AND UO.IsActive = '1'
	LEFT JOIN UserReligionDetails UR ON UR.UserId = UM.UserId AND UR.IsActive = '1'
	LEFT JOIN ReligionMaster RM ON RM.ReligionId = UR.ReligionId
	LEFT JOIN CastMaster CM ON CM.CastId = UR.CastId
	LEFT JOIN UploadedImages UI ON UI.UserId = UM.UserId AND UI.ImageType = 'Cover'
	--FROM UserReligionDetails UR

	--LEFT JOIN UserMaster UM ON UM.UserId = UR.UserId AND UM.IsActive = '1'
	--LEFT JOIN UserOtherDetails UO ON UO.UserId = UM.UserId AND UO.IsActive = '1'
	--LEFT JOIN ReligionMaster RM ON RM.ReligionId = UR.ReligionId
	--LEFT JOIN CastMaster CM ON CM.CastId = UR.CastId
	--LEFT JOIN SendRequest SR ON SR.RequestedUserId = UM.UserId 

	WHERE SR.RequestedUserId = @UserId

END

GO
/****** Object:  StoredProcedure [dbo].[GetRejectedRequest]    Script Date: 1/29/2019 11:39:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--GetRejectedRequest @UserId=1
CREATE PROC [dbo].[GetRejectedRequest]
@ReligionId INT = NULL,
@CastId INT = NULL,
@UserId INT
AS
BEGIN
	SELECT ISNULL(UM.UserId,'') AS UserId,ISNULL(UM.FirstName,'') AS FirstName,ISNULL(UM.LastName,'') AS LastName,ISNULL(UM.ProfileId,'') AS ProfileId,
		   ISNULL(UM.MobileNo,'') AS MobileNo,ISNULL(UM.DOB,'') AS DOB,ISNULL(UM.Age,'') AS Age,ISNULL(UM.Gender,'') AS Gender,
		   ISNULL(UM.EmailId,'Not Specified') AS EmailId,ISNULL(UM.IsSurnameVisible,'') AS IsSurnameVisible,ISNULL(UM.IsDPVisible,'') AS IsDPVisible,
		   
		   ISNULL(UO.MaritialStatus,'') AS MaritialStatus,ISNULL(UO.Height,'Not Specified') AS Height,ISNULL(UO.ProfilePicPath,'') AS ProfilePicPath,
		   ISNULL(UR.ReligionId,'') AS ReligionId,ISNULL(RM.ReligionName,'Not Specified') AS ReligionName,
		   ISNULL(UR.CastId,'') AS CastId, CASE WHEN ISNULL(UR.CastId,'') = '' THEN 'Not Specified' ELSE CM.CastName END AS CastName,ISNULL(UR.SubCastId,'') AS SubCastId,
		   (dbo.GetPhotoCount(UM.UserId)) AS PhotoCount,
			UI.ImagePath
	FROM SendRequest SR
	JOIN UserMaster UM ON UM.UserId = SR.CreatedBy AND UM.IsActive = '1'
	LEFT JOIN UserOtherDetails UO ON UO.UserId = UM.UserId AND UO.IsActive = '1'
	LEFT JOIN UserReligionDetails UR ON UR.UserId = UM.UserId AND UR.IsActive = '1'
	LEFT JOIN ReligionMaster RM ON RM.ReligionId = UR.ReligionId
	LEFT JOIN CastMaster CM ON CM.CastId = UR.CastId
	LEFT JOIN UploadedImages UI ON UI.UserId = UM.UserId AND UI.ImageType = 'Cover'
	WHERE SR.RequestedUserId = @UserId AND SR.Status = 'Rejected'
END



GO
/****** Object:  StoredProcedure [dbo].[GetReligionMaster]    Script Date: 1/29/2019 11:39:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetReligionMaster]
AS
BEGIN
	SELECT * FROM ReligionMaster WHERE IsActive = '1'
END


GO
/****** Object:  StoredProcedure [dbo].[GetRequestByStatus]    Script Date: 1/29/2019 11:39:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--GetRequestByStatus @UserId=1,@RequestStatus='Pending'
CREATE PROC [dbo].[GetRequestByStatus]
@ReligionId INT = NULL,
@CastId INT = NULL,
@UserId INT,
@RequestStatus VARCHAR(30)
AS
BEGIN
	SELECT ISNULL(UM.UserId,'') AS UserId,ISNULL(UM.FirstName,'') AS FirstName,ISNULL(UM.LastName,'') AS LastName,ISNULL(UM.ProfileId,'') AS ProfileId,
		   ISNULL(UM.MobileNo,'') AS MobileNo,ISNULL(UM.DOB,'') AS DOB,ISNULL(UM.Age,'') AS Age,ISNULL(UM.Gender,'') AS Gender,

		   ISNULL(UM.EmailId,'Not Specified') AS EmailId,ISNULL(UM.IsSurnameVisible,'') AS IsSurnameVisible,ISNULL(UM.IsDPVisible,'') AS IsDPVisible,
		   CASE WHEN SR.Status = 'Pending' THEN 'P' 
				WHEN SR.Status = 'Cancelled' THEN 'C'  
				WHEN SR.Status = 'Accepted' THEN 'A'
				WHEN SR.Status = 'Rejected' THEN 'R'
			ELSE 'N' END AS RequestStatus,
		   ISNULL(UO.MaritialStatus,'') AS MaritialStatus,ISNULL(UO.Height,'Not Specified') AS Height,ISNULL(UO.ProfilePicPath,'') AS ProfilePicPath,
		   ISNULL(UR.ReligionId,'') AS ReligionId,ISNULL(RM.ReligionName,'Not Specified') AS ReligionName,
		   ISNULL(UR.CastId,'') AS CastId, CASE WHEN ISNULL(UR.CastId,'') = '' THEN 'Not Specified' ELSE CM.CastName END AS CastName,
		   ISNULL(UR.SubCastId,'') AS SubCastId,
		   (dbo.GetPhotoCount(UM.UserId)) AS PhotoCount,
			UI.ImagePath

	FROM UserReligionDetails UR
	LEFT JOIN UserMaster UM ON UM.UserId = UR.UserId AND UM.IsActive = '1'
	LEFT JOIN UserOtherDetails UO ON UO.UserId = UM.UserId AND UO.IsActive = '1'
	LEFT JOIN ReligionMaster RM ON RM.ReligionId = UR.ReligionId
	LEFT JOIN CastMaster CM ON CM.CastId = UR.CastId
	LEFT JOIN SendRequest SR ON SR.RequestedUserId = UM.UserId AND SR.CreatedBy = @UserId
	LEFT JOIN UploadedImages UI ON UI.UserId = UM.UserId AND UI.ImageType = 'Cover'
	WHERE SR.CreatedBy = @UserId AND SR.Status = @RequestStatus
END


GO
/****** Object:  StoredProcedure [dbo].[GetSentRequest]    Script Date: 1/29/2019 11:39:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--GetSentRequest @UserId=1
CREATE PROC [dbo].[GetSentRequest]
@ReligionId INT = NULL,
@CastId INT = NULL,
@UserId INT
AS
BEGIN
	SELECT ISNULL(UM.UserId,'') AS UserId,ISNULL(UM.FirstName,'') AS FirstName,ISNULL(UM.LastName,'') AS LastName,ISNULL(UM.ProfileId,'') AS ProfileId,
		   ISNULL(UM.MobileNo,'') AS MobileNo,ISNULL(UM.DOB,'') AS DOB,ISNULL(UM.Age,'') AS Age,ISNULL(UM.Gender,'') AS Gender,
		   ISNULL(UM.EmailId,'Not Specified') AS EmailId,ISNULL(UM.IsSurnameVisible,'') AS IsSurnameVisible,ISNULL(UM.IsDPVisible,'') AS IsDPVisible,
		   
		   ISNULL(UO.MaritialStatus,'') AS MaritialStatus,ISNULL(UO.Height,'Not Specified') AS Height,ISNULL(UO.ProfilePicPath,'') AS ProfilePicPath,
		   ISNULL(UR.ReligionId,'') AS ReligionId,ISNULL(RM.ReligionName,'Not Specified') AS ReligionName,
		   ISNULL(UR.CastId,'') AS CastId, CASE WHEN ISNULL(UR.CastId,'') = '' THEN 'Not Specified' ELSE CM.CastName END AS CastName,ISNULL(UR.SubCastId,'') AS SubCastId
		 --  CASE WHEN SR.Status = 'Pending' THEN 'P' 				
			--	WHEN SR.Status = 'Cancelled' THEN 'C'  
			--	WHEN SR.Status = 'Accepted' THEN 'A'  
			--	WHEN SR.Status = 'Rejected' THEN 'R'  

			--ELSE 'N' END AS RequestStatus
			--,SR.CreatedBY,SR.RequestedUserId
	FROM UserReligionDetails UR
	LEFT JOIN UserMaster UM ON UM.UserId = UR.UserId AND UM.IsActive = '1'
	LEFT JOIN UserOtherDetails UO ON UO.UserId = UM.UserId AND UO.IsActive = '1'
	LEFT JOIN ReligionMaster RM ON RM.ReligionId = UR.ReligionId
	LEFT JOIN CastMaster CM ON CM.CastId = UR.CastId
	LEFT JOIN SendRequest SR ON SR.RequestedUserId = UM.UserId AND SR.CreatedBy = @UserId
	WHERE SR.CreatedBy = @UserId AND SR.Status = 'Pending'
		
  --AND UR.ReligionId = @ReligionId 
		  --AND UM.UserId NOT IN (select RequestedUserId from SendRequest where CreatedBy = @UserId)
END


GO
/****** Object:  StoredProcedure [dbo].[GetShortlistedProfile]    Script Date: 1/29/2019 11:39:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetShortlistedProfile] --GetShortlistedProfile @UserId = 1
@UserId BIGINT
AS
BEGIN
SELECT ISNULL(UM.UserId,'') AS UserId,ISNULL(UM.FirstName,'') AS FirstName,ISNULL(UM.LastName,'') AS LastName,ISNULL(UM.ProfileId,'') AS ProfileId,
	   ISNULL(UM.MobileNo,'') AS MobileNo,ISNULL(UM.Password,'') AS Password,ISNULL(UM.DOB,'') AS DOB,ISNULL(UM.Age,'') AS Age,ISNULL(UM.Gender,'') AS Gender,
	   ISNULL(UM.EmailId,'') AS EmailId,ISNULL(UM.IsSurnameVisible,'') AS IsSurnameVisible,ISNULL(UM.IsDPVisible,'') AS IsDPVisible,ISNULL(UM.IsActive,'') AS IsActive,
	   ISNULL(UM.CreatedBy,'') AS CreatedBy,

	   ISNULL(UA.AddressId,'') AS AddressId,ISNULL(UA.Address1,'') AS Address1,ISNULL(UA.Address2,'') AS Address2,
	   ISNULL(UA.CityId,'') AS CityId,ISNULL(CITY.CityName,'') AS CityName,
	   ISNULL(UA.StateId,'') AS StateId,ISNULL(SM.StateName,'') AS StateName,ISNULL(UA.CountryId,'') AS CountryId,ISNULL(COUNTRY.CountryName,'') AS CountryName,
	   ISNULL(UA.Pincode,'') AS Pincode,ISNULL(UA.AlternateAddress1,'') AS AlternateAddress1,
	   ISNULL(UA.AlternateAddress2,'') AS AlternateAddress2,ISNULL(UA.AlternateCityId,'') AS AlternateCityId,ISNULL(UA.AlternateStateId,'') AS AlternateStateId,
	   ISNULL(UA.AlternateCountryId,'') AS AlternateCountryId,ISNULL(UA.AlternatePincode,'') AS AlternatePincode,ISNULL(UA.IsActive,'') AS IsActive,

       ISNULL(UF.UserFamilyDetailsId,'') AS UserFamilyDetailsId,ISNULL(UF.FatherName,'') AS FatherName,ISNULL(UF.MotherName,'') AS MotherName,
	   ISNULL(UF.FatherProfession,'') AS FatherProfession,ISNULL(UF.MotherProfession,'') AS MotherProfession,ISNULL(UF.FamilyLocation,'') AS FamilyLocation,
	   ISNULL(UF.FamilyDescription,'') AS FamilyDescription,

       ISNULL(UO.UserOtherDetailsId,'') AS UserOtherDetailsId,ISNULL(UO.MaritialStatus,'') AS MaritialStatus,
	   ISNULL(UO.MotherTounge,'') AS MotherToungeId,ISNULL(LM.LanguageName,'Not Specified') AS MotherTounge,
	   ISNULL(UO.BirthCountry,'') AS BirthCountryId,ISNULL(COUNTRY.CountryName,'') AS BirthCountryName,
	   ISNULL(UO.BirthPlace,'Not Specified') AS BirthPlace,ISNULL(UO.BirthTime,'Not Specified') AS BirthTime,
	   ISNULL(UO.Height,'Not Specified') AS Height,ISNULL(UO.BodyType,'Not Specified') AS BodyType,ISNULL(UO.SkinTone,'Not Specified') AS SkinTone,
	   ISNULL(UO.BloodGroup,'Not Specified') AS BloodGroup,ISNULL(UO.IsSmoke,'') AS IsSmoke,ISNULL(UO.IsDrink,'') AS IsDrink,
	   ISNULL(UO.IsPhysicalDisable,'') AS IsPhysicalDisable,ISNULL(UO.IdealpartnerDescription,'Not Specified') AS IdealpartnerDescription,ISNULL(UO.CallTime,'') AS CallTime,
	   ISNULL(UO.ProfileCreatedBy,'') AS ProfileCreatedBy,ISNULL(UO.ProfilePicPath,'') AS ProfilePicPath,

       ISNULL(UP.UserProfessionalDetailsId,'') AS UserProfessionalDetailsId,ISNULL(UP.CollegeName,'Not Specified') AS CollegeName,
	   ISNULL(UP.Degree,'Not Specified') AS Degree,ISNULL(UP.Field,'Not Specified') AS Field,ISNULL(UP.CompanyName,'Not Specified') AS CompanyName,
	   ISNULL(UP.Designation,'Not Specified') AS Designation,ISNULL(UP.Income,'Not Specified') AS Income,

       ISNULL(UR.UserReligionId,'') AS UserReligionId,ISNULL(UR.ReligionId,'') AS ReligionId,ISNULL(RM.ReligionName,'Not Specified') AS ReligionName,
	   ISNULL(UR.CastId,'') AS CastId,ISNULL(CM.CastName,'Not Specified') AS CastName,ISNULL(UR.MoonSign,'Not Specified') AS MoonSign,
	   ISNULL(UR.Star,'Not Specified') AS Star,ISNULL(UR.Gotra,'Not Specified') AS Gotra,
	   (dbo.GetPhotoCount(UM.UserId)) AS PhotoCount,
		UI.ImagePath

FROM UserMaster UM
LEFT JOIN UserAddressDetails UA ON UA.UserId = UM.UserId AND UA.IsActive = '1'
LEFT JOIN UserFamilyDetails UF ON UF.UserId = UM.UserId AND UF.IsActive = '1'
LEFT JOIN UserOtherDetails UO ON UO.UserId = UM.UserId AND UO.IsActive = '1'
LEFT JOIN UserProfessionalDetails UP ON UP.UserId = UM.UserId AND UP.IsActive = '1'
LEFT JOIN UserReligionDetails UR ON UR.UserId = UM.UserId AND UR.IsActive = '1'
LEFT JOIN ReligionMaster RM ON RM.ReligionId = UR.ReligionId
LEFT JOIN CastMaster CM ON CM.CastId = UR.CastId
LEFT JOIN CountryMaster COUNTRY ON COUNTRY.CountryId = UA.CountryId
LEFT JOIN StateMaster SM ON SM.StateId = UA.StateId
LEFT JOIN CityMaster CITY ON CITY.CityId = UA.CityId
LEFT JOIN LanguageMaster LM ON LM.LanguageId = UO.MotherTounge
LEFT JOIN ShortlistedProfiles SP ON SP.ShortlistedUserId = UM.UserId
LEFT JOIN UploadedImages UI ON UI.UserId = UM.UserId AND UI.ImageType = 'Cover'
WHERE SP.CreatedBy = @UserId AND SP.Status = 'S'
END



GO
/****** Object:  StoredProcedure [dbo].[GetStateMaster]    Script Date: 1/29/2019 11:39:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetStateMaster]
AS
BEGIN
	SELECT * FROM StateMaster
	WHERE IsActive= '1'
	ORDER BY StateName
END


GO
/****** Object:  StoredProcedure [dbo].[GetUserProfile]    Script Date: 1/29/2019 11:39:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetUserProfile] --GetUserProfile @UserId = 1
@UserId BIGINT = NULL,
@MobileNo VARCHAR(20) = NULL
AS
BEGIN
SELECT ISNULL(UM.UserId,'') AS UserId,ISNULL(UM.FirstName,'') AS FirstName,ISNULL(UM.LastName,'') AS LastName,ISNULL(UM.ProfileId,'') AS ProfileId,
	   ISNULL(UM.MobileNo,'') AS MobileNo,ISNULL(UM.Password,'') AS Password,ISNULL(UM.DOB,'') AS DOB,ISNULL(UM.Age,'') AS Age,ISNULL(UM.Gender,'') AS Gender,
	   ISNULL(UM.EmailId,'') AS EmailId,ISNULL(UM.IsSurnameVisible,'') AS IsSurnameVisible,ISNULL(UM.IsDPVisible,'') AS IsDPVisible,ISNULL(UM.IsActive,'') AS IsActive,
	   ISNULL(UM.CreatedBy,'') AS CreatedBy,

	   ISNULL(UA.AddressId,'') AS AddressId,ISNULL(UA.Address1,'') AS Address1,ISNULL(UA.Address2,'') AS Address2,
	   ISNULL(UA.CityId,'') AS CityId,ISNULL(CITY.CityName,'') AS CityName,
	   ISNULL(UA.StateId,'') AS StateId,ISNULL(SM.StateName,'') AS StateName,ISNULL(UA.CountryId,'') AS CountryId,ISNULL(COUNTRY.CountryName,'') AS CountryName,
	   ISNULL(UA.Pincode,'') AS Pincode,ISNULL(UA.AlternateAddress1,'') AS AlternateAddress1,
	   ISNULL(UA.AlternateAddress2,'') AS AlternateAddress2,ISNULL(UA.AlternateCityId,'') AS AlternateCityId,ISNULL(UA.AlternateStateId,'') AS AlternateStateId,
	   ISNULL(UA.AlternateCountryId,'') AS AlternateCountryId,ISNULL(UA.AlternatePincode,'') AS AlternatePincode,ISNULL(UA.IsActive,'') AS IsActive,

       ISNULL(UF.UserFamilyDetailsId,'') AS UserFamilyDetailsId,ISNULL(UF.FatherName,'') AS FatherName,ISNULL(UF.MotherName,'') AS MotherName,
	   ISNULL(UF.FatherProfession,'') AS FatherProfession,ISNULL(UF.MotherProfession,'') AS MotherProfession,ISNULL(UF.FamilyLocation,'') AS FamilyLocation,
	   ISNULL(UF.FamilyDescription,'') AS FamilyDescription,

       ISNULL(UO.UserOtherDetailsId,'') AS UserOtherDetailsId,ISNULL(UO.MaritialStatus,'') AS MaritialStatus,
	   ISNULL(UO.MotherTounge,'') AS MotherToungeId,ISNULL(LM.LanguageName,'Not Specified') AS MotherTounge,
	   ISNULL(UO.BirthCountry,'') AS BirthCountryId,ISNULL(COUNTRY.CountryName,'') AS BirthCountryName,
	   ISNULL(UO.BirthPlace,'Not Specified') AS BirthPlace,ISNULL(UO.BirthTime,'Not Specified') AS BirthTime,
	   ISNULL(UO.Height,'Not Specified') AS Height,ISNULL(UO.BodyType,'Not Specified') AS BodyType,ISNULL(UO.SkinTone,'Not Specified') AS SkinTone,
	   ISNULL(UO.BloodGroup,'Not Specified') AS BloodGroup,ISNULL(UO.IsSmoke,'') AS IsSmoke,ISNULL(UO.IsDrink,'') AS IsDrink,
	   ISNULL(UO.IsPhysicalDisable,'') AS IsPhysicalDisable,ISNULL(UO.IdealpartnerDescription,'Not Specified') AS IdealpartnerDescription,ISNULL(UO.CallTime,'') AS CallTime,
	   ISNULL(UO.ProfileCreatedBy,'') AS ProfileCreatedBy,ISNULL(UO.ProfilePicPath,'') AS ProfilePicPath,

       ISNULL(UP.UserProfessionalDetailsId,'') AS UserProfessionalDetailsId,ISNULL(UP.CollegeName,'Not Specified') AS CollegeName,
	   ISNULL(UP.Degree,'Not Specified') AS Degree,ISNULL(UP.Field,'Not Specified') AS Field,ISNULL(UP.CompanyName,'Not Specified') AS CompanyName,
	   ISNULL(UP.Designation,'Not Specified') AS Designation,ISNULL(UP.Income,'Not Specified') AS Income,

       ISNULL(UR.UserReligionId,'') AS UserReligionId,ISNULL(UR.ReligionId,'') AS ReligionId,ISNULL(RM.ReligionName,'Not Specified') AS ReligionName,
	   ISNULL(UR.CastId,'') AS CastId,ISNULL(CM.CastName,'Not Specified') AS CastName,ISNULL(UR.MoonSign,'Not Specified') AS MoonSign,
	   ISNULL(UR.Star,'Not Specified') AS Star,ISNULL(UR.Gotra,'Not Specified') AS Gotra,
	   (dbo.GetPhotoCount(UM.UserId)) AS PhotoCount

	   FROM UserMaster UM
	   LEFT JOIN UserAddressDetails UA ON UA.UserId = UM.UserId AND UA.IsActive = '1'
	   LEFT JOIN UserFamilyDetails UF ON UF.UserId = UM.UserId AND UF.IsActive = '1'
	   LEFT JOIN UserOtherDetails UO ON UO.UserId = UM.UserId AND UO.IsActive = '1'
	   LEFT JOIN UserProfessionalDetails UP ON UP.UserId = UM.UserId AND UP.IsActive = '1'
	   LEFT JOIN UserReligionDetails UR ON UR.UserId = UM.UserId AND UR.IsActive = '1'
	   LEFT JOIN ReligionMaster RM ON RM.ReligionId = UR.ReligionId
	   LEFT JOIN CastMaster CM ON CM.CastId = UR.CastId
	   LEFT JOIN CountryMaster COUNTRY ON COUNTRY.CountryId = UA.CountryId
	   LEFT JOIN StateMaster SM ON SM.StateId = UA.StateId
	   LEFT JOIN CityMaster CITY ON CITY.CityId = UA.CityId
	   LEFT JOIN LanguageMaster LM ON LM.LanguageId = UO.MotherTounge
	   WHERE (UM.UserId = @UserId OR @UserId IS NULL)
			  AND (UM.MobileNo = @MobileNo OR @MobileNo IS NULL)

	   SELECT * FROM UploadedImages
	   WHERE UserId = @UserId
	  
END


GO
/****** Object:  StoredProcedure [dbo].[InsertLogs]    Script Date: 1/29/2019 11:39:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[InsertLogs]
@ErrMessage VARCHAR(Max),
@ErrStackTrace VARCHAR(Max),
@UserId int,
@DateTime DATETIME
AS
BEGIN
	INSERT INTO DBLogs(ErrMessage,StackTrace,UserId,CreatedDate)
	VALUES(@ErrMessage,@ErrStackTrace,@UserId,@DateTime)
END

--create table dbo.DBLogs
--(
--ErrMessage VARCHAR(Max),
--StackTrace VARCHAR(Max),
--UserId INT,
--CreatedDate datetime
--)
GO
/****** Object:  StoredProcedure [dbo].[InsertSMSLogs]    Script Date: 1/29/2019 11:39:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[InsertSMSLogs]
@MobileNo VARCHAR(20),
@SMSText VARCHAR(500),
@APIRequest VARCHAR(500),
@APIResponse VARCHAR(500)
AS
BEGIN
	INSERT INTO SMSLogs(MobileNo,SMSText,APIRequest,APIResponse,CreatedDate)
	VALUES(@MobileNo,@SMSText,@APIRequest,@APIResponse,GETDATE())
END




GO
/****** Object:  StoredProcedure [dbo].[ManageProfileVisitors]    Script Date: 1/29/2019 11:39:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[ManageProfileVisitors] --ManageProfileVisitors 0,17,'S'
@VisitedUserId BIGINT =NULL ,
@UserId BIGINT,
@Action CHAR(1)
AS
BEGIN
	IF(@Action = 'I')
	BEGIN
		IF NOT EXISTS(SELECT VisitedUserId FROM ProfileVisitors WHERE VisitedUserId = @VisitedUserId AND CreatedBy = @UserId)
		BEGIN
			INSERT INTO ProfileVisitors(VisitedUserId,CreatedBy,CreatedDate)
			VALUES(@VisitedUserId,@UserId,GETDATE())
		END
		ELSE
		BEGIN
			UPDATE ProfileVisitors
			SET UpdatedDate = GETDATE()
			WHERE VisitedUserId = @VisitedUserId AND CreatedBy = @UserId
		END
	END
	ELSE IF(@Action = 'S')
	BEGIN
		SELECT ISNULL(UM.UserId,'') AS UserId,ISNULL(UM.FirstName,'') AS FirstName,ISNULL(UM.LastName,'') AS LastName,ISNULL(UM.ProfileId,'') AS ProfileId,
	   ISNULL(UM.MobileNo,'') AS MobileNo,ISNULL(UM.Password,'') AS Password,ISNULL(UM.DOB,'') AS DOB,ISNULL(UM.Age,'') AS Age,ISNULL(UM.Gender,'') AS Gender,
	   ISNULL(UM.EmailId,'') AS EmailId,ISNULL(UM.IsSurnameVisible,'') AS IsSurnameVisible,ISNULL(UM.IsDPVisible,'') AS IsDPVisible,ISNULL(UM.IsActive,'') AS IsActive,
	   ISNULL(UM.CreatedBy,'') AS CreatedBy,

	   ISNULL(UA.AddressId,'') AS AddressId,ISNULL(UA.Address1,'') AS Address1,ISNULL(UA.Address2,'') AS Address2,
	   ISNULL(UA.CityId,'') AS CityId,ISNULL(CITY.CityName,'') AS CityName,
	   ISNULL(UA.StateId,'') AS StateId,ISNULL(SM.StateName,'') AS StateName,ISNULL(UA.CountryId,'') AS CountryId,ISNULL(COUNTRY.CountryName,'') AS CountryName,
	   ISNULL(UA.Pincode,'') AS Pincode,ISNULL(UA.AlternateAddress1,'') AS AlternateAddress1,
	   ISNULL(UA.AlternateAddress2,'') AS AlternateAddress2,ISNULL(UA.AlternateCityId,'') AS AlternateCityId,ISNULL(UA.AlternateStateId,'') AS AlternateStateId,
	   ISNULL(UA.AlternateCountryId,'') AS AlternateCountryId,ISNULL(UA.AlternatePincode,'') AS AlternatePincode,ISNULL(UA.IsActive,'') AS IsActive,

       ISNULL(UF.UserFamilyDetailsId,'') AS UserFamilyDetailsId,ISNULL(UF.FatherName,'') AS FatherName,ISNULL(UF.MotherName,'') AS MotherName,
	   ISNULL(UF.FatherProfession,'') AS FatherProfession,ISNULL(UF.MotherProfession,'') AS MotherProfession,ISNULL(UF.FamilyLocation,'') AS FamilyLocation,
	   ISNULL(UF.FamilyDescription,'') AS FamilyDescription,

       ISNULL(UO.UserOtherDetailsId,'') AS UserOtherDetailsId,ISNULL(UO.MaritialStatus,'') AS MaritialStatus,
	   ISNULL(UO.MotherTounge,'') AS MotherToungeId,ISNULL(LM.LanguageName,'Not Specified') AS MotherTounge,
	   ISNULL(UO.BirthCountry,'') AS BirthCountryId,ISNULL(COUNTRY.CountryName,'') AS BirthCountryName,
	   ISNULL(UO.BirthPlace,'Not Specified') AS BirthPlace,ISNULL(UO.BirthTime,'Not Specified') AS BirthTime,
	   ISNULL(UO.Height,'Not Specified') AS Height,ISNULL(UO.BodyType,'Not Specified') AS BodyType,ISNULL(UO.SkinTone,'Not Specified') AS SkinTone,
	   ISNULL(UO.BloodGroup,'Not Specified') AS BloodGroup,ISNULL(UO.IsSmoke,'') AS IsSmoke,ISNULL(UO.IsDrink,'') AS IsDrink,
	   ISNULL(UO.IsPhysicalDisable,'') AS IsPhysicalDisable,ISNULL(UO.IdealpartnerDescription,'Not Specified') AS IdealpartnerDescription,ISNULL(UO.CallTime,'') AS CallTime,
	   ISNULL(UO.ProfileCreatedBy,'') AS ProfileCreatedBy,ISNULL(UO.ProfilePicPath,'') AS ProfilePicPath,

       ISNULL(UP.UserProfessionalDetailsId,'') AS UserProfessionalDetailsId,ISNULL(UP.CollegeName,'Not Specified') AS CollegeName,
	   ISNULL(UP.Degree,'Not Specified') AS Degree,ISNULL(UP.Field,'Not Specified') AS Field,ISNULL(UP.CompanyName,'Not Specified') AS CompanyName,
	   ISNULL(UP.Designation,'Not Specified') AS Designation,ISNULL(UP.Income,'Not Specified') AS Income,

       ISNULL(UR.UserReligionId,'') AS UserReligionId,ISNULL(UR.ReligionId,'') AS ReligionId,ISNULL(RM.ReligionName,'Not Specified') AS ReligionName,
	   ISNULL(UR.CastId,'') AS CastId,ISNULL(CM.CastName,'Not Specified') AS CastName,ISNULL(UR.MoonSign,'Not Specified') AS MoonSign,
	   ISNULL(UR.Star,'Not Specified') AS Star,ISNULL(UR.Gotra,'Not Specified') AS Gotra,
	   (dbo.GetPhotoCount(UM.UserId)) AS PhotoCount,
	   ISNULL(UI.ImagePath,'') AS ImagePath

	   FROM UserMaster UM
	   LEFT JOIN UserAddressDetails UA ON UA.UserId = UM.UserId AND UA.IsActive = '1'
	   LEFT JOIN UserFamilyDetails UF ON UF.UserId = UM.UserId AND UF.IsActive = '1'
	   LEFT JOIN UserOtherDetails UO ON UO.UserId = UM.UserId AND UO.IsActive = '1'
	   LEFT JOIN UserProfessionalDetails UP ON UP.UserId = UM.UserId AND UP.IsActive = '1'
	   LEFT JOIN UserReligionDetails UR ON UR.UserId = UM.UserId AND UR.IsActive = '1'
	   LEFT JOIN ReligionMaster RM ON RM.ReligionId = UR.ReligionId
	   LEFT JOIN CastMaster CM ON CM.CastId = UR.CastId
	   LEFT JOIN CountryMaster COUNTRY ON COUNTRY.CountryId = UA.CountryId
	   LEFT JOIN StateMaster SM ON SM.StateId = UA.StateId
	   LEFT JOIN CityMaster CITY ON CITY.CityId = UA.CityId
	   LEFT JOIN LanguageMaster LM ON LM.LanguageId = UO.MotherTounge
	   JOIN ProfileVisitors PV ON PV.CreatedBy = UM.UserId
	   LEFT JOIN UploadedImages UI ON UI.UserId = UM.UserId AND UI.ImageType = 'Cover'
	   WHERE PV.VisitedUserId = @UserId
	   ORDER BY COALESCE(PV.UpdatedDate,PV.CreatedDate) DESC

		  /*To Get Shortlisted Profiles*/
		SELECT ShortlistedUserId,Status
		FROM ShortlistedProfiles
		WHERE CreatedBy = @UserId
	END
END


GO
/****** Object:  StoredProcedure [dbo].[ManageSendRequest]    Script Date: 1/29/2019 11:39:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[ManageSendRequest] -- ManageSendRequest @UserId=1,@RequestedUserId=108,@Status='Cancelled'
@UserId BIGINT,
@RequestedUserId BIGINT = NULL,
@Status VARCHAR(20),
@SendRequestId BIGINT = NULL
AS
BEGIN
	IF ((@Status ='Pending' OR @Status = 'Blocked') AND NOT EXISTS(SELECT SendRequestId 
								     	  FROM SendRequest 
										  WHERE CreatedBy = @UserId AND RequestedUserId = @RequestedUserId AND Status IN ('Pending','Cancelled','Rejected','Blocked')))
	BEGIN
		INSERT INTO SendRequest(RequestedUserId,Status,CreatedBy,CreatedDate)
		VALUES(@RequestedUserId,@Status,@UserId,GETDATE())
		SELECT SCOPE_IDENTITY() AS Result;
	END
	ELSE
	BEGIN
		IF EXISTS (SELECT SendRequestId FROM SendRequest WHERE CreatedBy = @UserId AND RequestedUserId = @RequestedUserId AND Status IN ('Pending','Cancelled'))
		BEGIN
			UPDATE SendRequest
			SET Status = @Status,
				UpdatedBy = @UserId,
				UpdatedDate = GETDATE()
			WHERE CreatedBy = @UserId AND RequestedUserId = @RequestedUserId

			SELECT @@ROWCOUNT AS Result;
		END
		--ELSE IF EXISTS (SELECT SendRequestId FROM SendRequest WHERE CreatedBy = @UserId AND RequestedUserId = @RequestedUserId AND Status = 'Pending')
		--BEGIN
		--	SELECT 'Request already Sent' AS Result;
		--END
		ELSE 
--IF(@Status = 'Accepted')
		BEGIN
			UPDATE SendRequest
			SET Status = @Status,
				UpdatedBy = @UserId,
				UpdatedDate = GETDATE()
			WHERE RequestedUserId = @UserId AND CreatedBy = @RequestedUserId

			SELECT @@ROWCOUNT AS Result;

		END
		--ELSE
		--BEGIN
		--	UPDATE SendRequest
		--	SET Status = @Status,
		--		UpdatedBy = @UserId,
		--		UpdatedDate = GETDATE()
		--	WHERE RequestedUserId = @UserId AND CreatedBy = @RequestedUserId

		--	SELECT @@ROWCOUNT AS Result;
		--END
	END
END


GO
/****** Object:  StoredProcedure [dbo].[ManageShortlistedProfiles]    Script Date: 1/29/2019 11:39:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[ManageShortlistedProfiles] --ManageShortlistedProfiles 
@UserId INT,
@ShortlistedUserId INT,
@Status VARCHAR(30)
AS
BEGIN
	IF NOT EXISTS(SELECT ShortlistedUserId FROM ShortlistedProfiles WHERE ShortlistedUserId = @ShortlistedUserId AND CreatedBy = @UserId)
	BEGIN
		INSERT INTO ShortlistedProfiles(ShortlistedUserId,Status,CreatedBy,CreatedDate)
		VALUES(@ShortlistedUserId,@Status,@UserId,GETDATE())

		SELECT @@ROWCOUNT AS Result
	END
	ELSE
	BEGIN
		UPDATE ShortlistedProfiles
		SET Status = @Status,
			UpdatedBy = @UserId,
			UpdatedDate = GETDATE()
		WHERE ShortlistedUserId = @ShortlistedUserId AND CreatedBy = @UserId

		SELECT @@ROWCOUNT AS Result
	END
END

GO
/****** Object:  StoredProcedure [dbo].[ManageUserPreferences]    Script Date: 1/29/2019 11:39:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create Proc [dbo].[ManageUserPreferences]
@UserPreferenceId BIGINT = NULL ,
@FromAge INT = NULL ,
@ToAge INT = NULL ,
@FromHeight VARCHAR(30) = NULL ,
@ToHeight VARCHAR(30) = NULL ,
@MaritialStatus VARCHAR(30) = NULL ,
@CityId INT = NULL ,
@CountryId INT = NULL ,
@ReligionId INT = NULL ,
@CasteId INT = NULL ,
@MotherToungeId INT = NULL ,
@Income VARCHAR(50) = NULL ,
@Diet VARCHAR(20) = NULL ,
@IsDrink CHAR(1) = NULL ,
@IsSmoke CHAR(1) = NULL ,
@IsPhysicalDisable CHAR(1) = NULL ,
@SkinTone VARCHAR(30) = NULL ,
@BodyType VARCHAR(50) = NULL ,
@UserId BIGINT,
@Action CHAR(1)
AS
BEGIN
	IF(@Action = 'U')
	BEGIN
		IF EXISTS (SELECT UserPreferenceId FROM UserPreferences WHERE CreatedBy = @UserId)
		BEGIN
			UPDATE UserPreferences
			SET FromAge = @FromAge,
				ToAge = @ToAge,
				FromHeight = @FromHeight,
				ToHeight = @ToHeight,
				MaritialStatus = @MaritialStatus,
				CityId = @CityId,
				CountryId = @CountryId,
				ReligionId = @ReligionId,
				CasteId = @CasteId,
				MotherToungeId = @MotherToungeId,
				Income = @Income,
				Diet = @Diet,
				IsDrink = @IsDrink,
				IsSmoke = @IsSmoke,
				IsPhysicalDisable = @IsPhysicalDisable,
				SkinTone = @SkinTone,
				BodyType = @BodyType,
				UpdatedBy = @UserId,
				UpdatedDate = GETDATE()
			WHERE CreatedBy = @UserId
		END
		ELSE
		BEGIN
			INSERT INTO UserPreferences(FromAge,ToAge,FromHeight,ToHeight,MaritialStatus,CityId,CountryId,ReligionId,CasteId,MotherToungeId,Income,Diet,IsDrink,IsSmoke,
										IsPhysicalDisable,SkinTone,BodyType,CreatedBy,CreatedDate)
			VALUES(@FromAge,@ToAge,@FromHeight,@ToHeight,@MaritialStatus,@CityId,@CountryId,@ReligionId,@CasteId,@MotherToungeId,@Income,@Diet,@IsDrink,@IsSmoke,
					@IsPhysicalDisable,@SkinTone,@BodyType,@UserId,GETDATE())
		END

		SELECT UserPreferenceId,ISNULL(pref.FromAge,'') AS FromAge,ISNULL(pref.ToAge,'') AS ToAge,ISNULL(pref.FromHeight,'') AS FromHeight,ISNULL(pref.ToHeight,'') AS ToHeight,
				ISNULL(pref.MaritialStatus,'') AS MaritialStatus,ISNULL(pref.CityId,'') AS CityId,ISNULL(city.CityName,'') AS CityName,
				ISNULL(pref.CountryId,'') AS CountryId,ISNULL(country.CountryName,'') AS CountryName,
				ISNULL(pref.ReligionId,'') AS ReligionId,ISNULL(RM.ReligionName,'') AS ReligionName,
				ISNULL(pref.CasteId,'') AS CasteId,ISNULL(CM.CastName,'') AS CasteName,
				ISNULL(pref.MotherToungeId,'') AS MotherToungeId,ISNULL(LM.LanguageName,'') AS MotherTounge,
				ISNULL(pref.Income,'') AS Income,ISNULL(pref.Diet,'') AS Diet,
				ISNULL(pref.IsDrink,'') AS IsDrink,ISNULL(pref.IsSmoke,'') AS IsSmoke,ISNULL(pref.IsPhysicalDisable,'') AS IsPhysicalDisable,
				ISNULL(pref.SkinTone,'') AS SkinTone,ISNULL(pref.BodyType,'') AS BodyType
		FROM UserPreferences pref
		LEFT JOIN CityMaster city ON city.CityId = pref.CityId
		LEFT JOIN CountryMaster country ON country.CountryId = pref.CountryId
		LEFT JOIN ReligionMaster RM ON RM.ReligionId = pref.ReligionId
		LEFT JOIN CastMaster CM ON CM.CastId = pref.CasteId
		LEFT JOIN LanguageMaster LM ON LM.LanguageId = pref.MotherToungeId
		WHERE pref.CreatedBy = @UserId

	END
	ELSE IF(@Action = 'S')
	BEGIN
		SELECT UserPreferenceId,ISNULL(pref.FromAge,'') AS FromAge,ISNULL(pref.ToAge,'') AS ToAge,ISNULL(pref.FromHeight,'') AS FromHeight,ISNULL(pref.ToHeight,'') AS ToHeight,
			   ISNULL(pref.MaritialStatus,'') AS MaritialStatus,ISNULL(pref.CityId,'') AS CityId,ISNULL(city.CityName,'') AS CityName,
			   ISNULL(pref.CountryId,'') AS CountryId,ISNULL(country.CountryName,'') AS CountryName,
			   ISNULL(pref.ReligionId,'') AS ReligionId,ISNULL(RM.ReligionName,'') AS ReligionName,
			   ISNULL(pref.CasteId,'') AS CasteId,ISNULL(CM.CastName,'') AS CasteName,
			   ISNULL(pref.MotherToungeId,'') AS MotherToungeId,ISNULL(LM.LanguageName,'') AS MotherTounge,
			   ISNULL(pref.Income,'') AS Income,ISNULL(pref.Diet,'') AS Diet,
			   ISNULL(pref.IsDrink,'') AS IsDrink,ISNULL(pref.IsSmoke,'') AS IsSmoke,ISNULL(pref.IsPhysicalDisable,'') AS IsPhysicalDisable,
			   ISNULL(pref.SkinTone,'') AS SkinTone,ISNULL(pref.BodyType,'') AS BodyType
		FROM UserPreferences pref
		LEFT JOIN CityMaster city ON city.CityId = pref.CityId
		LEFT JOIN CountryMaster country ON country.CountryId = pref.CountryId
		LEFT JOIN ReligionMaster RM ON RM.ReligionId = pref.ReligionId
		LEFT JOIN CastMaster CM ON CM.CastId = pref.CasteId
		LEFT JOIN LanguageMaster LM ON LM.LanguageId = pref.MotherToungeId
		WHERE pref.CreatedBy = @UserId
	END
END

GO
/****** Object:  StoredProcedure [dbo].[RegisterUser]    Script Date: 1/29/2019 11:39:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[RegisterUser]
@FirstName VARCHAR(50),
@LastName VARCHAR(50),
@Gender CHAR(1),
@MobileNo VARCHAR(20),
@EmailId VARCHAR(20),
@Password VARCHAR(50),
@DOB VARCHAR(20),
@Age INT,
@MaritialStatus VARCHAR(20),--Table Name - UserOtherDetails
@MotherTounge VARCHAR(20),--Table Name - UserOtherDetails
@CallTime VARCHAR(20) = NULL,--Table Name - UserOtherDetails
@ProfileCreatedBy VARCHAR(30),--Table Name - UserOtherDetails
@Income VARCHAR(50),--Table Name - UserProfessionalDetails
@ReligionId INT, --Table Name - UserReligionDetails
@StateId INT, --Table Name - UserAddressDetails
@CityId INT --Table Name - UserAddressDetails
AS
BEGIN
	IF NOT EXISTS (SELECT User
Id FROM UserMaster WHERE MobileNo = @MobileNo)
	BEGIN
		BEGIN TRAN T1
			BEGIN TRY
				INSERT INTO UserMaster(FirstName,LastName,Gender,MobileNo,EmailId,Password,DOB,Age,CreatedDate,IsActive)
				VALUES(@FirstName,@LastName,@Gender,@MobileNo,@EmailId,@Password,@DOB,@Age,GETDATE(),'1')

				--Get UserId
				DECLARE @UserId BIGINT;
				SET @UserId = SCOPE_IDENTITY();

				UPDATE UserMaster SET ProfileId = 'EV'+CONVERT(VARCHAR,@UserId) WHERE UserId = @UserId;

				INSERT INTO UserOtherDetails(UserId,MaritialStatus,MotherTounge,CallTime,ProfileCreatedBy,CreatedBy,CreatedDate,IsActive)
				VALUES(@UserId,@MaritialStatus,@MotherTounge,@CallTime,@ProfileCreatedBy,@UserId,GETDATE(),'1')

				INSERT INTO UserProfessionalDetails(UserId,Income,CreatedBy,CreatedDate,IsActive)
				VALUES(@UserId,@Income,@UserId,GETDATE(),'1')

				INSERT INTO UserReligionDetails(UserId,ReligionId,CreatedBy,CreatedDate,IsActive)
				VALUES(@UserId,@ReligionId,@UserId,GETDATE(),'1')

				INSERT INTO UserAddressDetails(UserId,StateId,CityId,CountryId,CreatedBy,CreatedDate,IsActive)
				VALUES(@UserId,@StateId,@CityId,1,@UserId,GETDATE(),'1')

				SELECT 'SUCCESS' AS 'Result',@UserId AS UserId
			END TRY
			BEGIN CATCH
				SELECT 'ERROR' AS 'Result','0' AS UserId
				ROLLBACK TRAN T1
			
END CATCH
		COMMIT 
TRAN
	END
	ELSE
	BEGIN
		SELECT 'User Already Exists' AS 'Result','0' AS UserId
	END
END




GO
/****** Object:  StoredProcedure [dbo].[SaveOTPDetails]    Script Date: 1/29/2019 11:39:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[SaveOTPDetails] --SaveOTPDetails '9768621235','121'
@MobileNo VARCHAR(20),
@OTP VARCHAR(10)
AS
BEGIN
	--DECLARE @Uid BIGINT;
	--SET @Uid = (SELECT UID FROM TBL_UserRegistration WHERE CONVERT(VARCHAR, mobNo) = @MobileNo)

	--IF(@Uid > 0)
	--BEGIN
		INSERT INTO OTPDetails(OTP,MobileNo,CreatedDate,IsActive)
		VALUES(@OTP, @MobileNo, GETDATE(),1)

		UPDATE OTPDetails SET IsActive = '0' WHERE MobileNo = @MobileNo AND OTP <> @OTP

		SELECT 'SUCCESS' AS Result
	--END
	--ELSE
	--BEGIN
	--	SELECT 'Enter valid Mobile Number' AS Result
	--END
END



GO
/****** Object:  StoredProcedure [dbo].[SearchProfile]    Script Date: 1/29/2019 11:39:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--SearchProfile @FromAge = 1,@ToAge=12,@ReligionId=1,@MotherToungeId=1,@Income=null,@MaritialStatus=null,@ProfileId=null
--SearchProfile @FromAge = null,@ToAge=null,@ReligionId=3,@MotherToungeId=null,@Income=null,@MaritialStatus=null,@ProfileId=null,@UserId=1
--SearchProfile @FromAge = null,@ToAge=null,@ReligionId=null,@MotherToungeId=null,@Income=null,@MaritialStatus=null,@ProfileId=pry3,@UserId=1
CREATE PROC [dbo].[SearchProfile] 
@FromAge INT = NULL,
@ToAge INT = NULL,
--@FromHeight INT = NULL,
--@ToHeight INT = NULL,
@ReligionId INT = NULL,
@MotherToungeId INT = NULL,
--@CountryId INT = NULL,
--@CityId INT = NULL,
@Income VARCHAR(50) = NULL,
@MaritialStatus VARCHAR(30) = NULL,
@ProfileId VARCHAR(50) = NULL,
@UserId BIGINT
AS
BEGIN

DECLARE @MatchGender VARCHAR(10);
	SET @MatchGender = (SELECT Gender FROM UserMaster WHERE UserId = @UserId);

SELECT ISNULL(UM.UserId,'') AS UserId,ISNULL(UM.FirstName,'') AS FirstName,ISNULL(UM.LastName,'') AS LastName,ISNULL(UM.ProfileId,'') AS ProfileId,
	   ISNULL(UM.MobileNo,'') AS MobileNo,ISNULL(UM.Password,'') AS Password,ISNULL(UM.DOB,'') AS DOB,ISNULL(UM.Age,'') AS Age,ISNULL(UM.Gender,'') AS Gender,
	   ISNULL(UM.EmailId,'') AS EmailId,ISNULL(UM.IsSurnameVisible,'') AS IsSurnameVisible,ISNULL(UM.IsDPVisible,'') AS IsDPVisible,ISNULL(UM.IsActive,'') AS IsActive,
	   ISNULL(UM.CreatedBy,'') AS CreatedBy,

	   ISNULL(UA.AddressId,'') AS AddressId,ISNULL(UA.Address1,'') AS Address1,ISNULL(UA.Address2,'') AS Address2,
	   ISNULL(UA.CityId,'') AS CityId,ISNULL(CITY.CityName,'') AS CityName,
	   ISNULL(UA.StateId,'') AS StateId,ISNULL(SM.StateName,'') AS StateName,ISNULL(UA.CountryId,'') AS CountryId,ISNULL(COUNTRY.CountryName,'') AS CountryName,
	   ISNULL(UA.Pincode,'') AS Pincode,ISNULL(UA.AlternateAddress1,'') AS AlternateAddress1,
	   ISNULL(UA.AlternateAddress2,'') AS AlternateAddress2,ISNULL(UA.AlternateCityId,'') AS AlternateCityId,ISNULL(UA.AlternateStateId,'') AS AlternateStateId,
	   ISNULL(UA.AlternateCountryId,'') AS AlternateCountryId,ISNULL(UA.AlternatePincode,'') AS AlternatePincode,ISNULL(UA.IsActive,'') AS IsActive,

       ISNULL(UF.UserFamilyDetailsId,'') AS UserFamilyDetailsId,ISNULL(UF.FatherName,'') AS FatherName,ISNULL(UF.MotherName,'') AS MotherName,
	   ISNULL(UF.FatherProfession,'') AS FatherProfession,ISNULL(UF.MotherProfession,'') AS MotherProfession,ISNULL(UF.FamilyLocation,'') AS FamilyLocation,
	   ISNULL(UF.FamilyDescription,'') AS FamilyDescription,

       ISNULL(UO.UserOtherDetailsId,'') AS UserOtherDetailsId,ISNULL(UO.MaritialStatus,'') AS MaritialStatus,
	   ISNULL(UO.MotherTounge,'') AS MotherToungeId,ISNULL(LM.LanguageName,'Not Specified') AS MotherTounge,
	   ISNULL(UO.BirthCountry,'') AS BirthCountryId,ISNULL(COUNTRY.CountryName,'') AS BirthCountryName,
	   ISNULL(UO.BirthPlace,'Not Specified') AS BirthPlace,ISNULL(UO.BirthTime,'Not Specified') AS BirthTime,
	   ISNULL(UO.Height,'Not Specified') AS Height,ISNULL(UO.BodyType,'Not Specified') AS BodyType,ISNULL(UO.SkinTone,'Not Specified') AS SkinTone,
	   ISNULL(UO.BloodGroup,'Not Specified') AS BloodGroup,ISNULL(UO.IsSmoke,'') AS IsSmoke,ISNULL(UO.IsDrink,'') AS IsDrink,
	   ISNULL(UO.IsPhysicalDisable,'') AS IsPhysicalDisable,ISNULL(UO.IdealpartnerDescription,'Not Specified') AS IdealpartnerDescription,ISNULL(UO.CallTime,'') AS CallTime,
	   ISNULL(UO.ProfileCreatedBy,'') AS ProfileCreatedBy,ISNULL(UO.ProfilePicPath,'') AS ProfilePicPath,

       ISNULL(UP.UserProfessionalDetailsId,'') AS UserProfessionalDetailsId,ISNULL(UP.CollegeName,'Not Specified') AS CollegeName,
	   ISNULL(UP.Degree,'Not Specified') AS Degree,ISNULL(UP.Field,'Not Specified') AS Field,ISNULL(UP.CompanyName,'Not Specified') AS CompanyName,
	   ISNULL(UP.Designation,'Not Specified') AS Designation,ISNULL(UP.Income,'Not Specified') AS Income,

       ISNULL(UR.UserReligionId,'') AS UserReligionId,ISNULL(UR.ReligionId,'') AS ReligionId,ISNULL(RM.ReligionName,'Not Specified') AS ReligionName,
	   ISNULL(UR.CastId,'') AS CastId,ISNULL(CM.CastName,'Not Specified') AS CastName,ISNULL(UR.MoonSign,'Not Specified') AS MoonSign,
	   ISNULL(UR.Star,'Not Specified') AS Star,ISNULL(UR.Gotra,'Not Specified') AS Gotra,
	   CASE WHEN SR.Status = 'Pending' THEN 'P' 				
			WHEN SR.Status = 'Cancelled' THEN 'C'  
			WHEN SR.Status = 'Accepted' THEN 'A'  
			WHEN SR.Status = 'Rejected' THEN 'R'  

			ELSE 'N' END AS RequestStatus,
	   (dbo.GetPhotoCount(UM.UserId)) AS PhotoCount,
	   UI.ImagePath

FROM UserMaster UM
LEFT JOIN UserAddressDetails UA ON UA.UserId = UM.UserId AND UA.IsActive = '1'
LEFT JOIN UserFamilyDetails UF ON UF.UserId = UM.UserId AND UF.IsActive = '1'
LEFT JOIN UserOtherDetails UO ON UO.UserId = UM.UserId AND UO.IsActive = '1'
LEFT JOIN UserProfessionalDetails UP ON UP.UserId = UM.UserId AND UP.IsActive = '1'
LEFT JOIN UserReligionDetails UR ON UR.UserId = UM.UserId AND UR.IsActive = '1'
LEFT JOIN ReligionMaster RM ON RM.ReligionId = UR.ReligionId
LEFT JOIN CastMaster CM ON CM.CastId = UR.CastId
LEFT JOIN CountryMaster COUNTRY ON COUNTRY.CountryId = UA.CountryId
LEFT JOIN StateMaster SM ON SM.StateId = UA.StateId
LEFT JOIN CityMaster CITY ON CITY.CityId = UA.CityId
LEFT JOIN LanguageMaster LM ON LM.LanguageId = UO.MotherTounge
LEFT JOIN SendRequest SR ON SR.RequestedUserId = UM.UserId AND SR.CreatedBy = @UserId
LEFT JOIN UploadedImages UI ON UI.UserId = UM.UserId AND UI.ImageType = 'Cover'
WHERE UM.UserId <> @UserId
	  AND UM.Gender <> @MatchGender
	  AND (UM.ProfileId = @ProfileId OR @ProfileId IS NULL)
	  AND (@FromAge IS NULL OR @ToAge IS NULL OR (UM.Age BETWEEN @FromAge AND @ToAge))
	  AND (@ReligionId IS NULL OR UR.ReligionId = @ReligionId)
	  AND (@MotherToungeId IS NULL OR UO.MotherTounge = @MotherToungeId)
	  AND (@Income IS NULL OR UP.Income = @Income)
	  AND (@MaritialStatus IS NULL OR UO.MaritialStatus = @MaritialStatus)
	  AND (@ProfileId IS NULL OR UM.ProfileId = @ProfileId)
END




GO
/****** Object:  StoredProcedure [dbo].[UpdateAddressDetails]    Script Date: 1/29/2019 11:39:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[UpdateAddressDetails]
@UserId BIGINT,
@Address1 VARCHAR(500),
@Address2 VARCHAR(500),
@CityId INT,
@StateId INT,
@CountryId INT,
@Pincode VARCHAR(10)
AS
BEGIN
	UPDATE UserAddressDetails
	SET Address1 = @Address1,
		Address2 = @Address2,
		CityId = @CityId,
		StateId = @StateId,
		CountryId = @CountryId,
		Pincode = @Pincode,
		UpdatedBy = @UserId,
		UpdatedDate = GETDATE()
	WHERE UserId = @UserId


	SELECT UA.AddressId,UA.UserId,UA.Address1,UA.Address2,
		   ISNULL(UA.CityId,0) AS CityId,ISNULL(CM.CityName,'Not Specified') AS CityName,
		   ISNULL(UA.StateId,0) AS StateId,ISNULL(SM.StateName,'') AS StateName,
		   ISNULL(UA.CountryId,0) AS CountryId,ISNULL(Country.CountryName,'Not Specified') AS CountryName,UA.Pincode,

		   UA.AlternateAddress1,UA.AlternateAddress2,
		   ISNULL(UA.AlternateCityId,0) AS AlternateCityId,ISNULL(CM1.CityName,'Not Specified') AS AlternameCityName,
		   ISNULL(UA.AlternateStateId,0) AS AlternateStateId,ISNULL(SM1.StateName,'') AS AlternateStateName,
		   ISNULL(UA.AlternateCountryId,0) AS AlternateCountryId,ISNULL(Country1.CountryName,'Not Specified') AS AlternateCountryName,UA.AlternatePincode
	FROM UserAddressDetails UA
	LEFT JOIN CityMaster CM ON CM.CityId = UA.CityId
	LEFT JOIN CityMaster CM1 ON CM.CityId = UA.AlternateCityId
	LEFT JOIN StateMaster SM ON SM.StateId = UA.StateId
	LEFT JOIN StateMaster SM1 ON SM.StateId = UA.AlternateStateId
	LEFT JOIN CountryMaster Country ON Country.CountryId = UA.CountryId
	LEFT JOIN CountryMaster Country1 ON Country.CountryId = UA.AlternateCountryId
	WHERE UserId = @UserId
END

GO
/****** Object:  StoredProcedure [dbo].[UpdateBasicDetails]    Script Date: 1/29/2019 11:39:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[UpdateBasicDetails]
@UserId BIGINT,
@FirstName VARCHAR(50),
@LastName VARCHAR(50),
@DOB VARCHAR(20),
@Age INT,
@Gender CHAR(1),
@EmailId VARCHAR(50),
@MaritialStatus VARCHAR(20),
@MotherTounge VARCHAR(50)
AS
BEGIN
	UPDATE UserMaster
	SET FirstName = @FirstName,
		LastName = @LastName,
		DOB = @DOB,
		Age = @Age,
		Gender = @Gender,
		EmailId = @EmailId,
		UpdatedBy = @UserId,
		UpdatedDate = GETDATE()
	WHERE UserId = @UserId

	UPDATE UserOtherDetails
	SET MaritialStatus = @MaritialStatus,
	
	MotherTounge = @MotherTounge,
		UpdatedBy = @UserId,
		UpdatedDate = GETDATE()
	WHERE UserId = @UserId


	SELECT UM.FirstName,UM.LastName,UM.DOB,UM.Age,UM.Gender,UM.EmailId,UM.MobileNo,UM.ProfileId,UO.MaritialStatus,UO.MotherTounge,LM.LanguageName
	FROM UserMaster UM
	JOIN UserOtherDetails UO ON UO.UserId = UM.UserId
	JOIN LanguageMaster LM ON LM.LanguageId = UO.MotherTounge
	WHERE UM.UserId = @UserId

END


GO
/****** Object:  StoredProcedure [dbo].[UpdateFamilyDetails]    Script Date: 1/29/2019 11:39:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--UpdateFamilyDetails @UserId=1,@FatherName='test Fname',@MotherName='Test Mname',@FatherProfession='test p',@MotherProfession='test mp',@FamilyDescription='teet\nasd\nasd\n'
CREATE proc [dbo].[UpdateFamilyDetails]
@UserId BIGINT,
@FatherName VARCHAR(50) = NULL,
@MotherName VARCHAR(50) = NULL,
@FatherProfession VARCHAR(50) = NULL,
@MotherProfession VARCHAR(50) = NULL,
--@CityId INT,
--@StateId INT,
--@CountryId INT,
--@Pincode VARCHAR(10)
@FamilyDescription VARCHAR(500) = NULL
AS
BEGIN
	IF EXISTS (SELECT UserFamilyDetailsId FROM UserFamilyDetails WHERE UserId = @UserId)
	BEGIN
		UPDATE UserFamilyDetails
		SET FatherName = @FatherName,
			MotherName = @MotherName,
			FatherProfession = @FatherProfession,

			MotherProfession = @MotherProfession,
			--CityId = @CityId,
			--StateId = @StateId,
			--CountryId = @CountryId,
			FamilyDescription = @FamilyDescription,
			UpdatedBy = @UserId,
			UpdatedDate = GETDATE()
		WHERE UserId = @UserId
	END
	ELSE
	BEGIN

		INSERT INTO UserFamilyDetails(UserId,FatherName,MotherName,FatherProfession,MotherProfession,FamilyDescription,IsActive, CreatedBy,CreatedDate)
		VALUES(@UserId,@FatherName,@MotherName,@FatherProfession,@MotherProfession,@FamilyDescription,'1',@UserId,GETDATE())
	END


	SELECT UserFamilyDetailsId,UserId,FatherName,MotherName,FatherProfession,MotherProfession,FamilyDescription
	--,CityId,StateId,CountryId,
	FROM UserFamilyDetails
	WHERE UserId = @UserId
END

GO
/****** Object:  StoredProcedure [dbo].[UpdateOtherDetails]    Script Date: 1/29/2019 11:39:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[UpdateOtherDetails]
@UserId BIGINT,
@Height VARCHAR(20),
@BodyType VARCHAR(20),
@SkinTone VARCHAR(20),
@BloodGroup VARCHAR(20),
@IsSmoke CHAR(1),
@IsDrink CHAR(1),
@IsPhysicalDisable CHAR(1),
@BirthPlace VARCHAR(50),
@BirthTime VARCHAR(20),
@BirthCountryId INT
AS
BEGIN
	UPDATE UserOtherDetails
	SET Height = @Height,
		BodyType = @BodyType,
		SkinTone = @SkinTone,
		BloodGroup = @BloodGroup,
		IsSmoke = @IsSmoke,
		IsDrink = @IsDrink,
		IsPhysicalDisable = @IsPhysicalDisable,
		BirthCountry = @BirthCountryId,
		BirthPlace = @BirthPlace,
		BirthTime = @BirthTime,
		UpdatedBy = @UserId,
		UpdatedDate = GETDATE()
	WHERE UserId = @UserId

	
	SELECT UO.UserOtherDetailsId,UO.UserId,ISNULL(UO.BirthCountry,0) AS BirthCountryId,ISNULL(CM.CountryName,'Not Specified') AS BirthCountryName,
		   ISNULL(UO.BirthPlace,'Not Specified') AS BirthPlace,
		   ISNULL(UO.BirthTime,'Not Specified') AS BirthTime,	ISNULL(UO.Height,'Not Specified') AS Height,
		   ISNULL(UO.BodyType,'Not Specified') AS BodyType,	ISNULL(UO.SkinTone,'Not Specified') AS SkinTone,
		   ISNULL(UO.BloodGroup,'Not Specified') AS BloodGroup,ISNULL(UO.IsSmoke,'Not Specified') AS IsSmoke,
		   ISNULL(UO.IsDrink,'Not Specified') AS IsDrink,ISNULL(UO.IsPhysicalDisable,'Not Specified') AS IsPhysicalDisable,
		   ISNULL(UO.IdealpartnerDescription,'Not Specified') AS IdealpartnerDescription
	FROM UserOtherDetails UO
	LEFT JOIN CountryMaster CM ON CM.CountryId = UO.BirthCountry
	WHERE UserId = @UserId
END

GO
/****** Object:  StoredProcedure [dbo].[UpdateProfessionalDetails]    Script Date: 1/29/2019 11:39:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[UpdateProfessionalDetails]
@UserId BIGINT,
@Degree VARCHAR(50),
@Field VARCHAR(50),
@CollegeName VARCHAR(50),
@CompanyName VARCHAR(50),
@Designation VARCHAR(20),
@Income VARCHAR(50)
AS
BEGIN
	UPDATE UserProfessionalDetails
	SET Degree = @Degree,
		Field = @Field,
		CollegeName = @CollegeName,
		CompanyName = @CompanyName,
		Designation = @Designation,
		Income = @Income,
		UpdatedBy = @UserId,
		UpdatedDate = GETDATE()
	WHERE UserId = @UserId

	
	SELECT UserProfessionalDetailsId,UserId,ISNULL(CollegeName,'Not Specified') AS CollegeName,
		   ISNULL(Degree,'Not Specified') AS Degree,ISNULL(Field,'Not Specified') AS Field,
		   ISNULL(CompanyName,'Not Specified') AS CompanyName,
		   ISNULL(Designation,'Not Specified') AS Designation, Income
	FROM UserProfessionalDetails UP
	WHERE UserId = @UserId
END

GO
/****** Object:  StoredProcedure [dbo].[UpdateReligionDetails]    Script Date: 1/29/2019 11:39:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[UpdateReligionDetails]
@UserId BIGINT,
@ReligionId VARCHAR(50),
@CastId VARCHAR(50),
@MoonSign VARCHAR(50),
@Star VARCHAR(50),
@Gotra VARCHAR(20)
AS
BEGIN
	UPDATE UserReligionDetails
	SET ReligionId = @ReligionId,
		CastId = @CastId,
		MoonSign = @MoonSign,
		Star = @Star,
		Gotra = @Gotra,
		UpdatedBy = @UserId,
		UpdatedDate = GETDATE()
	WHERE UserId = @UserId

	
	SELECT UR.ReligionId,RM.ReligionName,ISNULL(UR.CastId,0) AS CastId,ISNULL(CM.CastName,'Not Specified') AS CastName,
		   ISNULL(UR.MoonSign,'Not Specified') AS MoonSign, ISNULL(UR.Star,'Not Specified') AS Star,
		   ISNULL(UR.Gotra,'Not Specified') AS Gotra
	FROM UserReligionDetails UR
	LEFT JOIN ReligionMaster RM ON RM.ReligionId = UR.ReligionId
	LEFT JOIN CastMaster CM ON CM.CastId = UR.CastId
	WHERE UserId = @UserId
END


GO
/****** Object:  StoredProcedure [dbo].[UpdateUserDetails]    Script Date: 1/29/2019 11:39:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[UpdateUserDetails]
@Action VARCHAR(20) = NULL,
@ProfiePicPath VARCHAR(200) = NULL,
@UserId BIGINT
AS
BEGIN
	UPDATE UserOtherDetails
	SET ProfilePicPath = @ProfiePicPath
	WHERE UserId = @UserId

	SELECT UM.* ,UR.ReligionId
	FROM UserMaster UM
	LEFT JOIN UserReligionDetails UR ON UR.UserId = UM.UserId
	WHERE UM.UserId = @UserId

	INSERT INTO UploadedImages(UserId,ImagePath)
	VALUES(@UserId,@ProfiePicPath)
END

GO
/****** Object:  StoredProcedure [dbo].[UpdateUserProfilePic]    Script Date: 1/29/2019 11:39:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[UpdateUserProfilePic]
@ProfiePicPath VARCHAR(200) = NULL,
@UserId BIGINT
AS
BEGIN
	UPDATE UserOtherDetails
	SET ProfilePicPath = @ProfiePicPath
	WHERE UserId = @UserId

	IF NOT EXISTS(SELECT NULL FROM UploadedImages WHERE ImagePath = @ProfiePicPath)
	BEGIN
		INSERT INTO UploadedImages(UserId,ImagePath)
		VALUES(@UserId,@ProfiePicPath)
	END

	SELECT ProfilePicPath 
	FROM UserOtherDetails
	WHERE UserId = @UserId
END

GO
/****** Object:  StoredProcedure [dbo].[UploadCoverImage]    Script Date: 1/29/2019 11:39:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[UploadCoverImage]
@ImageId BIGINT = NULL,
@UserId BIGINT,
@ImagePath VARCHAR(100)
AS
BEGIN
	IF NOT EXISTS (SELECT NULL FROM UploadedImages WHERE UserId = @UserId AND ImageType = 'Cover')
	BEGIN
		INSERT INTO UploadedImages(UserId,ImagePath,ImageType)
		VALUES(@UserId,@ImagePath,'Cover')
	END
	ELSE
	BEGIN
		UPDATE UploadedImages
		SET ImagePath = @ImagePath,
			CreatedDate = GETDATE()
		WHERE UserId = @UserId AND ImageType = 'Cover'
	END

	SELECT * FROM UploadedImages
	WHERE UserId = @UserId AND ImageType = 'Cover'
END



GO
/****** Object:  StoredProcedure [dbo].[UploadImage]    Script Date: 1/29/2019 11:39:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[UploadImage]
@ImageId BIGINT = NULL,
@UserId BIGINT = NULL,
@ImagePath VARCHAR(100) = NULL,
@Action CHAR(1)
AS
BEGIN
	IF (@Action = 'I' AND @ImageId IS NULL)
	BEGIN
		INSERT INTO UploadedImages(UserId,ImagePath)
		VALUES(@UserId,@ImagePath)

		SELECT * FROM UploadedImages
		WHERE UserId = @UserId
	END
	ELSE IF(@Action = 'S')
	BEGIN
		SELECT * FROM UploadedImages
		WHERE UserId = @UserId
	END
	ELSE
	BEGIN
		DELETE FROM UploadedImages
		WHERE ImageId = @ImageId

		SELECT * FROM UploadedImages
		WHERE UserId = @UserId
	END
END

GO
/****** Object:  StoredProcedure [dbo].[UserLogin]    Script Date: 1/29/2019 11:39:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[UserLogin]
@MobileNo VARCHAR(20),
@Password VARCHAR(50)
AS
BEGIN
	SELECT UM.* ,UR.ReligionId,UO.ProfilePicPath
	FROM UserMaster UM
	LEFT JOIN UserReligionDetails UR ON UR.UserId = UM.UserId
	LEFT JOIN UserOtherDetails UO ON UO.UserId = UM.UserId
	WHERE (MobileNo = @MobileNo OR EmailId = @MobileNo) AND Password = @Password
END


GO
/****** Object:  StoredProcedure [dbo].[ValidateEmailId]    Script Date: 1/29/2019 11:39:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[ValidateEmailId] --ValidateEmailId '9773564027'
@EmailId VARCHAR(50),
@UserId BIGINT = NULL
AS
BEGIN
	SELECT EmailId FROM UserMaster WHERE EmailId = @EmailId
END
GO
/****** Object:  StoredProcedure [dbo].[ValidateMobileNo]    Script Date: 1/29/2019 11:39:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[ValidateMobileNo]
@MobileNo VARCHAR(20),
@UserId BIGINT = NULL
AS
BEGIN
	SELECT MobileNo FROM UserMaster WHERE MobileNo = @MobileNo
END


GO
/****** Object:  StoredProcedure [dbo].[ValidateOTP]    Script Date: 1/29/2019 11:39:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[ValidateOTP]
@MobileNo VARCHAR(20),
@OTP VARCHAR(10)
AS
BEGIN
	IF EXISTS(SELECT TOP 1 OTP FROM OTPDetails WHERE MobileNo = @MobileNo AND OTP = @OTP AND IsActive = '1')
	BEGIN
		UPDATE OTPDetails SET IsActive = '0', UpdatedDate = GETDATE() WHERE MobileNo = @MobileNo AND OTP = @OTP

		SELECT 'SUCCESS' as Result
	END
	ELSE
	BEGIN
		SELECT 'ERROR' as Result
	END
END



GO
/****** Object:  UserDefinedFunction [dbo].[GetPhotoCount]    Script Date: 1/29/2019 11:39:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create FUNCTION [dbo].[GetPhotoCount] (@UserId INT)
RETURNS INT
AS BEGIN
    DECLARE @PhotoCount INT

	SET @PhotoCount = (SELECT COUNT(*) FROM UploadedImages WHERE UserId = @UserId AND ImageType IS NULL)

    RETURN @PhotoCount
END
GO
