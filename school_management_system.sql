USE [master]
GO
/****** Object:  Database [University]    Script Date: 6/20/2018 6:31:10 PM ******/
CREATE DATABASE [University]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'University', FILENAME = N'C:\Program Files (x86)\Microsoft SQL Server\MSSQL.1\MSSQL\Data\University.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'University_log', FILENAME = N'C:\Program Files (x86)\Microsoft SQL Server\MSSQL.1\MSSQL\Data\University_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [University] SET COMPATIBILITY_LEVEL = 90
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [University].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [University] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [University] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [University] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [University] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [University] SET ARITHABORT OFF 
GO
ALTER DATABASE [University] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [University] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [University] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [University] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [University] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [University] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [University] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [University] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [University] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [University] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [University] SET  DISABLE_BROKER 
GO
ALTER DATABASE [University] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [University] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [University] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [University] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [University] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [University] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [University] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [University] SET RECOVERY FULL 
GO
ALTER DATABASE [University] SET  MULTI_USER 
GO
ALTER DATABASE [University] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [University] SET DB_CHAINING OFF 
GO
ALTER DATABASE [University] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [University] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
USE [University]
GO
/****** Object:  StoredProcedure [dbo].[FiltroKurse_NgaLeksionet]    Script Date: 6/20/2018 6:31:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[FiltroKurse_NgaLeksionet]
			(@username varchar(50))

AS
Begin
SELECT DISTINCT dbo.Gjej_Emer_Kursi(IdKursi) as Emer_Kursi, IdKursi
FROM Leksione
WHERE IdKursi in (SELECT IdKursi
					FROM StudentKursi
					Where SsnStudent IN (SELECT Ssn
											FROM Users
											WHERE Username= @username))

END
GO
/****** Object:  StoredProcedure [dbo].[FiltroLeksion]    Script Date: 6/20/2018 6:31:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[FiltroLeksion] (
								@Idleksioni integer,
								@User varchar(50))
AS
BEGIN
SELECT [IdLeksioni]
	  ,[NumerLeksioni]
      ,[Titulli]
      ,[Permbajtja]
      ,[Data]
      ,[Statusi]
      ,[SsnP]
      ,[IdKursi]
	  ,[Link_Dokumenti]
	  ,University.dbo.Gjej_Emer_Kursi(IdKursi) AS Emer_Kursi
	  ,University.dbo.Gjej_Emer_Pedagog(SsnP) AS Emer_Pedagogu
	  ,University.dbo.Gjej_Mbiemer_Pedagog(SsnP) As Mbiemer_Pedagogu

  FROM [University].[dbo].Leksione
  WHERE SsnP in (SELECT [University].[dbo].Kursi.SsnP
				FROM [University].[dbo].Kursi 
				WHERE IdKursi in (SELECT University.dbo.StudentKursi.IdKursi
									FROM University.dbo.StudentKursi
									WHERE University.dbo.StudentKursi.SsnStudent in (SELECT [dbo].[Users].Ssn
																					FROM [dbo].[Users]
																					WHERE [Username] = @User)))
 AND IdKursi in (SELECT University.dbo.StudentKursi.IdKursi
					FROM University.dbo.StudentKursi
					WHERE University.dbo.StudentKursi.SsnStudent in (SELECT [dbo].[Users].Ssn
																					FROM [dbo].[Users]
																					WHERE [Username] = @User))
 AND Idleksioni = @Idleksioni
END

GO
/****** Object:  StoredProcedure [dbo].[FiltroLeksione]    Script Date: 6/20/2018 6:31:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****** Script for SelectTopNRows command from SSMS  ******/
CREATE PROCEDURE [dbo].[FiltroLeksione]
	(@User varchar (50))
AS
BEGIN
SELECT [IdLeksioni]
	  ,[NumerLeksioni]
      ,[Titulli]
      ,[Permbajtja]
      ,[Data]
      ,[Statusi]
      ,[SsnP]
      ,[IdKursi]
	  ,[Link_Dokumenti]
	  ,University.dbo.Gjej_Emer_Kursi(IdKursi) AS Emer_Kursi
	  ,University.dbo.Gjej_Emer_Pedagog(SsnP) AS Emer_Pedagogu
	  ,University.dbo.Gjej_Mbiemer_Pedagog(SsnP) As Mbiemer_Pedagogu

  FROM [University].[dbo].Leksione
  WHERE SsnP in (SELECT [University].[dbo].Kursi.SsnP
				FROM [University].[dbo].Kursi 
				WHERE IdKursi in (SELECT University.dbo.StudentKursi.IdKursi
									FROM University.dbo.StudentKursi
									WHERE University.dbo.StudentKursi.SsnStudent in (SELECT [dbo].[Users].Ssn
																					FROM [dbo].[Users]
																					WHERE [Username] = @User)))
 AND IdKursi in (SELECT University.dbo.StudentKursi.IdKursi
					FROM University.dbo.StudentKursi
					WHERE University.dbo.StudentKursi.SsnStudent in (SELECT [dbo].[Users].Ssn
																					FROM [dbo].[Users]
																					WHERE [Username] = @User))
ORDER BY IdLeksioni DESC
END

GO
/****** Object:  StoredProcedure [dbo].[FiltroLeksioneKursi]    Script Date: 6/20/2018 6:31:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[FiltroLeksioneKursi] (
								@Idkursi varchar(50),
								@User varchar(50))
AS
BEGIN
SELECT [IdLeksioni]
	  ,[NumerLeksioni]
      ,[Titulli]
      ,[Permbajtja]
      ,[Data]
      ,[Statusi]
      ,[SsnP]
      ,[IdKursi]
	  ,[Link_Dokumenti]
	  ,University.dbo.Gjej_Emer_Kursi(IdKursi) AS Emer_Kursi
	  ,University.dbo.Gjej_Emer_Pedagog(SsnP) AS Emer_Pedagogu
	  ,University.dbo.Gjej_Mbiemer_Pedagog(SsnP) As Mbiemer_Pedagogu

  FROM [University].[dbo].Leksione
  WHERE SsnP in (SELECT [University].[dbo].Kursi.SsnP
				FROM [University].[dbo].Kursi 
				WHERE IdKursi in (SELECT University.dbo.StudentKursi.IdKursi
									FROM University.dbo.StudentKursi
									WHERE University.dbo.StudentKursi.SsnStudent in (SELECT [dbo].[Users].Ssn
																					FROM [dbo].[Users]
																					WHERE [Username] = @User)))
 AND IdKursi = @Idkursi

END


GO
/****** Object:  StoredProcedure [dbo].[FiltroLeksionetEReja]    Script Date: 6/20/2018 6:31:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****** Script for SelectTopNRows command from SSMS  ******/
CREATE PROCEDURE [dbo].[FiltroLeksionetEReja]
	(@Idleksioni int,
	@User varchar (50))
AS
BEGIN
SELECT [IdLeksioni]
	  ,[NumerLeksioni]
      ,[Titulli]
      ,[Permbajtja]
      ,[Data]
      ,[Statusi]
      ,[SsnP]
      ,[IdKursi]
	  ,[Link_Dokumenti]
	  ,University.dbo.Gjej_Emer_Kursi(IdKursi) AS Emer_Kursi
	  ,University.dbo.Gjej_Emer_Pedagog(SsnP) AS Emer_Pedagogu
	  ,University.dbo.Gjej_Mbiemer_Pedagog(SsnP) As Mbiemer_Pedagogu

  FROM [University].[dbo].Leksione
  WHERE SsnP in (SELECT [University].[dbo].Kursi.SsnP
				FROM [University].[dbo].Kursi 
				WHERE IdKursi in (SELECT University.dbo.StudentKursi.IdKursi
									FROM University.dbo.StudentKursi
									WHERE University.dbo.StudentKursi.SsnStudent in (SELECT [dbo].[Users].Ssn
																					FROM [dbo].[Users]
																					WHERE [Username] = @User)))
 AND IdKursi in (SELECT University.dbo.StudentKursi.IdKursi
					FROM University.dbo.StudentKursi
					WHERE University.dbo.StudentKursi.SsnStudent in (SELECT [dbo].[Users].Ssn
																					FROM [dbo].[Users]
																					WHERE [Username] = @User))
 AND IdLeksioni > @Idleksioni

ORDER BY IdLeksioni DESC
END

GO
/****** Object:  StoredProcedure [dbo].[FiltroNjoftim]    Script Date: 6/20/2018 6:31:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[FiltroNjoftim] (
								@Idnjoftimi integer,
								@User varchar(50))
AS
BEGIN
SELECT [IdNjoftimi]
      ,[Titulli]
      ,[Permbajtja]
      ,[DataN]
      ,[Statusi]
      ,[SsnP]
      ,[IdKursi]
	  ,University.dbo.Gjej_Emer_Kursi(IdKursi) AS Emer_Kursi
	  ,University.dbo.Gjej_Emer_Pedagog(SsnP) AS Emer_Pedagogu
	  ,University.dbo.Gjej_Mbiemer_Pedagog(SsnP) As Mbiemer_Pedagogu

  FROM [University].[dbo].Njoftime
  WHERE SsnP in (SELECT [University].[dbo].Kursi.SsnP
				FROM [University].[dbo].Kursi 
				WHERE IdKursi in (SELECT University.dbo.StudentKursi.IdKursi
									FROM University.dbo.StudentKursi
									WHERE University.dbo.StudentKursi.SsnStudent in (SELECT [dbo].[Users].Ssn
																					FROM [dbo].[Users]
																					WHERE [Username] = @User)))
 AND IdKursi in (SELECT University.dbo.StudentKursi.IdKursi
					FROM University.dbo.StudentKursi
					WHERE University.dbo.StudentKursi.SsnStudent in (SELECT [dbo].[Users].Ssn
																					FROM [dbo].[Users]
																					WHERE [Username] = @User))
 AND IdNjoftimi = @Idnjoftimi
END
GO
/****** Object:  StoredProcedure [dbo].[FiltroNjoftime]    Script Date: 6/20/2018 6:31:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Script for SelectTopNRows command from SSMS  ******/
CREATE PROCEDURE [dbo].[FiltroNjoftime]
	(@User varchar (50))
AS
BEGIN
SELECT [IdNjoftimi]
      ,[Titulli]
      ,[Permbajtja]
      ,[DataN]
      ,[Statusi]
      ,[SsnP]
      ,[IdKursi]
	  ,University.dbo.Gjej_Emer_Kursi(IdKursi) AS Emer_Kursi
	  ,University.dbo.Gjej_Emer_Pedagog(SsnP) AS Emer_Pedagogu
	  ,University.dbo.Gjej_Mbiemer_Pedagog(SsnP) As Mbiemer_Pedagogu

  FROM [University].[dbo].Njoftime
  WHERE SsnP in (SELECT [University].[dbo].Kursi.SsnP
				FROM [University].[dbo].Kursi 
				WHERE IdKursi in (SELECT University.dbo.StudentKursi.IdKursi
									FROM University.dbo.StudentKursi
									WHERE University.dbo.StudentKursi.SsnStudent in (SELECT [dbo].[Users].Ssn
																					FROM [dbo].[Users]
																					WHERE [Username] = @User)))
 AND IdKursi in (SELECT University.dbo.StudentKursi.IdKursi
					FROM University.dbo.StudentKursi
					WHERE University.dbo.StudentKursi.SsnStudent in (SELECT [dbo].[Users].Ssn
																					FROM [dbo].[Users]
																					WHERE [Username] = @User))
UNION
SELECT [IdNjoftimi]
      ,[Titulli]
      ,[Permbajtja]
      ,[DataN]
      ,[Statusi]
      ,[SsnP]
      ,[IdKursi]
	  ,'Fakulteti' AS Emer_Kursi
	  ,'Struktura' AS Emer_Pedagogu
	  ,'Administrative' As Mbiemer_Pedagogu
FROM [University].[dbo].Njoftime
WHERE IdKursi = 'Everybody'
ORDER BY IdNjoftimi DESC
END
GO
/****** Object:  StoredProcedure [dbo].[FiltroNjoftimetEReja]    Script Date: 6/20/2018 6:31:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Script for SelectTopNRows command from SSMS  ******/
CREATE PROCEDURE [dbo].[FiltroNjoftimetEReja]
	(@Idnjoftimi int,
	@User varchar (50))
AS
BEGIN
SELECT [IdNjoftimi]
      ,[Titulli]
      ,[Permbajtja]
      ,[DataN]
      ,[Statusi]
      ,[SsnP]
      ,[IdKursi]
	  ,University.dbo.Gjej_Emer_Kursi(IdKursi) AS Emer_Kursi
	  ,University.dbo.Gjej_Emer_Pedagog(SsnP) AS Emer_Pedagogu
	  ,University.dbo.Gjej_Mbiemer_Pedagog(SsnP) As Mbiemer_Pedagogu

  FROM [University].[dbo].Njoftime
  WHERE SsnP in (SELECT [University].[dbo].Kursi.SsnP
				FROM [University].[dbo].Kursi 
				WHERE IdKursi in (SELECT University.dbo.StudentKursi.IdKursi
									FROM University.dbo.StudentKursi
									WHERE University.dbo.StudentKursi.SsnStudent in (SELECT [dbo].[Users].Ssn
																					FROM [dbo].[Users]
																					WHERE [Username] = @User)))
 AND IdKursi in (SELECT University.dbo.StudentKursi.IdKursi
					FROM University.dbo.StudentKursi
					WHERE University.dbo.StudentKursi.SsnStudent in (SELECT [dbo].[Users].Ssn
																					FROM [dbo].[Users]
																					WHERE [Username] = @User))
 AND IdNjoftimi > @Idnjoftimi
UNION
SELECT [Idnjoftimi]
      ,[Titulli]
      ,[Permbajtja]
      ,[DataN]
      ,[Statusi]
      ,[SsnP]
      ,[IdKursi]
	  ,'Fakulteti' AS Emer_Kursi
	  ,'Struktura' AS Emer_Pedagogu
	  ,'Administrative' As Mbiemer_Pedagogu

FROM [University].[dbo].Njoftime
WHERE IdKursi = 'Everybody'
AND IdNjoftimi > @Idnjoftimi
ORDER BY IdNjoftimi DESC
END
GO
/****** Object:  StoredProcedure [dbo].[loginUser]    Script Date: 6/20/2018 6:31:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[loginUser]
(
@pUser varchar(50),
@pPassword varchar(max),
@responseMessage varchar(250) Out
)
As
BEGIN
	DECLARE @UserId varchar(50)

	IF EXISTS (SELECT TOP 1 Username
				From Users
				Where Username=@pUser
			  )
		BEGIN 
			SET @UserId=(SELECT Username
						  FROM Users
						  WHERE Username=@pUser AND Pass=SUBSTRING(master.dbo.fn_varbintohexstr(HashBytes('SHA2_512',@pPassword)), 3, 128)
						  )
				
				IF(@UserId IS NULL)
					Begin
						SET @responseMessage = 'Incorrect Username or Password'
					END

				Else
					Begin
						SET @responseMessage = 'User authentication succesful'
					END
		END
		ELSE
			BEGIN
				SET @responseMessage = 'Upss...dicka nuk shkoi sic duhet..'
			END

END

GO
/****** Object:  StoredProcedure [dbo].[NderroPassword]    Script Date: 6/20/2018 6:31:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[NderroPassword] (@pUser varchar(50),
										@pPassword varchar(max),
										@responseMessage varchar(250) Out)

AS
BEGIN
BEGIN TRY
		UPDATE Users
		   SET Pass = SUBSTRING(master.dbo.fn_varbintohexstr(HASHBYTES('SHA2_512',@pPassword)), 3, 128)
		 WHERE Username=@pUser
		 Set @responseMessage='Success'
		 End TRY

	BEGIN CATCH
	Set @responseMessage= ERROR_MESSAGE()
	END CATCH
END



GO
/****** Object:  StoredProcedure [dbo].[ShtoLeksion]    Script Date: 6/20/2018 6:31:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[ShtoLeksion]
          (@Numerleksioni int,
		   @Titulli varchar(250),
           @Permbajtja varchar(max),
           @DataN datetime,
           @Statusi bit,
           @SsnP varchar(50),
		   @Idkursi varchar(50),
		   @link_dokumenti nvarchar(50),
           @responseMessage varchar(250) Out)
As
Begin
	Begin try
	INSERT INTO [dbo].[Leksione]
           ([NumerLeksioni]
		   ,[Titulli]
           ,[Permbajtja]
           ,[Data]
           ,[Statusi]
           ,[SsnP]
		   ,[IdKursi]
		   ,[Link_Dokumenti])
	Values (@Numerleksioni, @Titulli, @Permbajtja, @DataN, @Statusi, @SsnP, @Idkursi, @link_dokumenti)

	Set @responseMessage='Success'
	End Try

	Begin Catch
	Set @responseMessage= ERROR_MESSAGE()
	End Catch
END
GO
/****** Object:  StoredProcedure [dbo].[ShtoNjoftim]    Script Date: 6/20/2018 6:31:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[ShtoNjoftim]
          (@Titulli varchar(250),
           @Permbajtja varchar(max),
           @DataN datetime,
           @Statusi bit,
           @SsnP varchar(50),
		   @Idkursi varchar(50),
           @responseMessage varchar(250) Out)
As
Begin
	Begin try
	INSERT INTO [dbo].[Njoftime]
           ([Titulli]
           ,[Permbajtja]
           ,[DataN]
           ,[Statusi]
           ,[SsnP]
		   ,[IdKursi])
	Values (@Titulli, @Permbajtja, @DataN, @Statusi, @SsnP, @Idkursi)

	Set @responseMessage='Success'
	End Try

	Begin Catch
	Set @responseMessage= ERROR_MESSAGE()
	End Catch
END

GO
/****** Object:  StoredProcedure [dbo].[ShtoPedagog]    Script Date: 6/20/2018 6:31:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[ShtoPedagog]
          (@pSsnPedagog varchar(50),
           @pEmerP varchar(50),
           @pMbiemerP varchar(50),
           @pAtesiP varchar(50),
           @pDitelindjeP date,
           @pGjiniaP varchar(50),
           @pEmailP varchar(50),
           @pTelefonP varchar(50),
           @pTitulli varchar(50),
           @pIdDep varchar(50),
           @responseMessage varchar(250) Out)
As
Begin
	Begin try
	INSERT INTO [dbo].[Pedagog]
           ([SsnPedagog]
           ,[EmerP]
           ,[MbiemerP]
           ,[AtesiP]
           ,[DitelindjeP]
           ,[GjiniaP]
           ,[EmailP]
           ,[TelefonP]
           ,[Titulli]
           ,[IdDep])
	Values (@pSsnPedagog ,
           @pEmerP ,
           @pMbiemerP ,
           @pAtesiP ,
           @pDitelindjeP ,
           @pGjiniaP ,
           @pEmailP ,
           @pTelefonP ,
           @pTitulli ,
           @pIdDep)

	Set @responseMessage='Success'
	End Try

	Begin Catch
	Set @responseMessage= ERROR_MESSAGE()
	End Catch
END

GO
/****** Object:  StoredProcedure [dbo].[ShtoStudent]    Script Date: 6/20/2018 6:31:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[ShtoStudent]
          (@sSsnStudent varchar(50),
           @sEmerS varchar(50),
           @sMbiemerS varchar(50),
           @sAtesiS varchar(50),
           @sDitelindjeS date,
           @sGjiniaS varchar(50),
           @sDateRegjistrimi date,
           @sVendlindje varchar(50),
           @sVendbanim varchar(50),
           @sEmailS varchar(50),
           @sTelefonS varchar(50),
           @sVitiS int,
           @sGrupiS varchar(50),
           @sIdDep varchar(50),
           @sIdDega varchar(50),
           @responseMessage varchar(250) Out)
As
Begin
	Begin try
	INSERT INTO [dbo].[Student]
           ([SsnStudent]
           ,[EmerS]
           ,[MbiemerS]
           ,[AtesiS]
           ,[DitelindjeS]
           ,[GjiniaS]
           ,[DateRegjistrimi]
           ,[Vendlindje]
           ,[Vendbanim]
           ,[EmailS]
           ,[TelefonS]
           ,[VitiS]
           ,[GrupiS]
           ,[IdDep]
           ,[IdDega])
	Values (@sSsnStudent ,
           @sEmerS ,
           @sMbiemerS ,
           @sAtesiS ,
           @sDitelindjeS ,
           @sGjiniaS ,
           @sDateRegjistrimi ,
           @sVendlindje ,
           @sVendbanim ,
           @sEmailS ,
           @sTelefonS ,
           @sVitiS ,
           @sGrupiS ,
           @sIdDep ,
           @sIdDega)

	Set @responseMessage='Success'
	End Try

	Begin Catch
	Set @responseMessage= ERROR_MESSAGE()
	End Catch
END

GO
/****** Object:  StoredProcedure [dbo].[ShtoUser]    Script Date: 6/20/2018 6:31:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[ShtoUser]
(
@pUser varchar(50),
@pPassword varchar(max),
@pSSN varchar(50),
@pRoli varchar(50),
@pLinku varchar(50),
@responseMessage varchar(250) Out
)
As
Begin
	Begin try
	Insert Into Users(Username,Pass,Ssn,Roli,Link_Imazh)
	Values (@pUser,SUBSTRING(master.dbo.fn_varbintohexstr(HASHBYTES('SHA2_512',@pPassword)), 3, 128),@pSSN,@pRoli,@pLinku)

	Set @responseMessage='Success'
	End Try

	Begin Catch
	Set @responseMessage= ERROR_MESSAGE()
	End Catch
END

GO
/****** Object:  UserDefinedFunction [dbo].[Gjej_Emer_Kursi]    Script Date: 6/20/2018 6:31:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[Gjej_Emer_Kursi] (@idkursi varchar(50))
RETURNS VARCHAR(50)
AS 
BEGIN
DECLARE @emerkursi VARCHAR(50);
	SELECT @emerkursi=EmerK
	FROM Kursi	
	WHERE IdKursi = @idkursi 
RETURN @emerkursi;
END 
GO
/****** Object:  UserDefinedFunction [dbo].[Gjej_Emer_Pedagog]    Script Date: 6/20/2018 6:31:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[Gjej_Emer_Pedagog] (@ssn varchar(50))
RETURNS VARCHAR(50)
AS 
BEGIN
DECLARE @emer VARCHAR(50);
	SELECT @emer=EmerP
	FROM Pedagog	
	WHERE SsnPedagog = @ssn 
RETURN @emer;
END 
GO
/****** Object:  UserDefinedFunction [dbo].[Gjej_Mbiemer_Pedagog]    Script Date: 6/20/2018 6:31:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[Gjej_Mbiemer_Pedagog] (@ssn varchar(50))
RETURNS VARCHAR(50)
AS 
BEGIN
DECLARE @mbiememer VARCHAR(50);
	SELECT @mbiememer=EmerP
	FROM Pedagog	
	WHERE SsnPedagog = @ssn 
RETURN @mbiememer;
END 
GO
/****** Object:  Table [dbo].[Aktivitetet]    Script Date: 6/20/2018 6:31:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Aktivitetet](
	[IdAktiviteti] [varchar](50) NOT NULL,
	[EmriA] [varchar](50) NOT NULL,
	[Pershkrimi] [varchar](max) NOT NULL,
	[Vendndodhja] [varchar](50) NOT NULL,
	[Orari] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Aktivitetet] PRIMARY KEY CLUSTERED 
(
	[IdAktiviteti] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Dega]    Script Date: 6/20/2018 6:31:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Dega](
	[IdDege] [varchar](50) NOT NULL,
	[EmerD] [varchar](50) NOT NULL,
	[NrStudentesh] [int] NOT NULL,
	[Tarifa] [int] NOT NULL,
	[IdDep] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdDege] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[EmerD] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Departament]    Script Date: 6/20/2018 6:31:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Departament](
	[IdDep] [varchar](50) NOT NULL,
	[EmerD] [varchar](50) NOT NULL,
	[NrStudentesh] [int] NOT NULL,
	[NrDegesh] [int] NOT NULL,
	[NrPunonjesish] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdDep] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[EmerD] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Kursi]    Script Date: 6/20/2018 6:31:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Kursi](
	[IdKursi] [varchar](50) NOT NULL,
	[EmerK] [varchar](50) NOT NULL,
	[NrKredite] [int] NOT NULL,
	[Pershkrimi] [varchar](max) NULL,
	[Semestri] [varchar](50) NOT NULL,
	[Viti] [int] NOT NULL,
	[SsnP] [varchar](50) NOT NULL,
	[IdDege] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdKursi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Leksione]    Script Date: 6/20/2018 6:31:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Leksione](
	[IdLeksioni] [int] IDENTITY(1,1) NOT NULL,
	[NumerLeksioni] [int] NOT NULL,
	[Titulli] [varchar](250) NOT NULL,
	[Permbajtja] [varchar](max) NOT NULL,
	[Data] [datetime] NOT NULL,
	[Statusi] [bit] NOT NULL,
	[SsnP] [varchar](50) NOT NULL,
	[IdKursi] [varchar](50) NOT NULL,
	[Link_Dokumenti] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdLeksioni] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Njoftime]    Script Date: 6/20/2018 6:31:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Njoftime](
	[IdNjoftimi] [int] IDENTITY(1,1) NOT NULL,
	[Titulli] [varchar](250) NOT NULL,
	[Permbajtja] [varchar](max) NOT NULL,
	[DataN] [datetime] NOT NULL,
	[Statusi] [bit] NOT NULL,
	[SsnP] [varchar](50) NOT NULL,
	[IdKursi] [varchar](50) NOT NULL,
 CONSTRAINT [PK__Njoftime__11B935110F87F154] PRIMARY KEY CLUSTERED 
(
	[IdNjoftimi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Pedagog]    Script Date: 6/20/2018 6:31:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Pedagog](
	[NrPedagogu] [int] IDENTITY(1,1) NOT NULL,
	[SsnPedagog] [varchar](50) NOT NULL,
	[EmerP] [varchar](50) NOT NULL,
	[MbiemerP] [varchar](50) NOT NULL,
	[AtesiP] [varchar](50) NOT NULL,
	[DitelindjeP] [date] NOT NULL,
	[GjiniaP] [varchar](50) NOT NULL,
	[EmailP] [varchar](50) NOT NULL,
	[TelefonP] [varchar](50) NOT NULL,
	[Titulli] [varchar](50) NOT NULL,
	[IdDep] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[NrPedagogu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[SsnPedagog] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PedagogAktivitete]    Script Date: 6/20/2018 6:31:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PedagogAktivitete](
	[SsnPedagog] [varchar](50) NOT NULL,
	[IdAktiviteti] [varchar](50) NOT NULL,
 CONSTRAINT [PKPA] PRIMARY KEY CLUSTERED 
(
	[SsnPedagog] ASC,
	[IdAktiviteti] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Sezoni]    Script Date: 6/20/2018 6:31:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Sezoni](
	[IdSezoni] [varchar](50) NOT NULL,
	[Emri] [varchar](50) NOT NULL,
	[Tipi] [varchar](50) NOT NULL,
	[Semestri] [varchar](50) NOT NULL,
	[VitiAkademik] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdSezoni] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Emri] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Student]    Script Date: 6/20/2018 6:31:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Student](
	[NrStudenti] [int] IDENTITY(1,1) NOT NULL,
	[SsnStudent] [varchar](50) NOT NULL,
	[EmerS] [varchar](50) NOT NULL,
	[MbiemerS] [varchar](50) NOT NULL,
	[AtesiS] [varchar](50) NOT NULL,
	[DitelindjeS] [date] NOT NULL,
	[GjiniaS] [varchar](50) NOT NULL,
	[DateRegjistrimi] [date] NOT NULL,
	[Vendlindje] [varchar](50) NOT NULL,
	[Vendbanim] [varchar](50) NOT NULL,
	[EmailS] [varchar](50) NOT NULL,
	[TelefonS] [varchar](50) NOT NULL,
	[VitiS] [int] NOT NULL,
	[GrupiS] [varchar](50) NOT NULL,
	[IdDep] [varchar](50) NOT NULL,
	[IdDega] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[NrStudenti] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[SsnStudent] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[StudentAktivitete]    Script Date: 6/20/2018 6:31:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[StudentAktivitete](
	[SsnStudent] [varchar](50) NOT NULL,
	[IdAktiviteti] [varchar](50) NOT NULL,
	[Certifikim] [bit] NOT NULL,
 CONSTRAINT [PKSA] PRIMARY KEY CLUSTERED 
(
	[SsnStudent] ASC,
	[IdAktiviteti] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[StudentKursi]    Script Date: 6/20/2018 6:31:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[StudentKursi](
	[SsnStudent] [varchar](50) NOT NULL,
	[IdKursi] [varchar](50) NOT NULL,
 CONSTRAINT [PKSK] PRIMARY KEY CLUSTERED 
(
	[SsnStudent] ASC,
	[IdKursi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[StudentKursiSezoni]    Script Date: 6/20/2018 6:31:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[StudentKursiSezoni](
	[SsnStudent] [varchar](50) NOT NULL,
	[IdKursi] [varchar](50) NOT NULL,
	[IdSezoni] [varchar](50) NOT NULL,
	[Nota] [int] NULL,
	[DataP] [date] NOT NULL,
	[OraP] [time](7) NOT NULL,
	[Vendndodhja] [varchar](50) NOT NULL,
 CONSTRAINT [PKSKZ] PRIMARY KEY CLUSTERED 
(
	[SsnStudent] ASC,
	[IdKursi] ASC,
	[IdSezoni] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Users]    Script Date: 6/20/2018 6:31:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Users](
	[Username] [varchar](50) NOT NULL,
	[Pass] [varchar](max) NOT NULL,
	[Ssn] [varchar](50) NOT NULL,
	[Roli] [varchar](50) NOT NULL,
	[Link_Imazh] [varchar](50) NULL,
 CONSTRAINT [PK__Users__CA33E0E59D442C2C] PRIMARY KEY CLUSTERED 
(
	[Ssn] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UQ__Users__536C85E4DE7C342D] UNIQUE NONCLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Njoftime] ADD  CONSTRAINT [DF_Njoftime_IdKursi]  DEFAULT ('Everybody') FOR [IdKursi]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_Link_Imazh]  DEFAULT ('~/Assets/img/profil.png') FOR [Link_Imazh]
GO
ALTER TABLE [dbo].[Dega]  WITH CHECK ADD FOREIGN KEY([IdDep])
REFERENCES [dbo].[Departament] ([IdDep])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Kursi]  WITH CHECK ADD FOREIGN KEY([IdDege])
REFERENCES [dbo].[Dega] ([IdDege])
GO
ALTER TABLE [dbo].[Kursi]  WITH CHECK ADD FOREIGN KEY([SsnP])
REFERENCES [dbo].[Pedagog] ([SsnPedagog])
GO
ALTER TABLE [dbo].[Leksione]  WITH CHECK ADD FOREIGN KEY([IdKursi])
REFERENCES [dbo].[Kursi] ([IdKursi])
GO
ALTER TABLE [dbo].[Leksione]  WITH CHECK ADD FOREIGN KEY([SsnP])
REFERENCES [dbo].[Users] ([Ssn])
GO
ALTER TABLE [dbo].[Njoftime]  WITH CHECK ADD  CONSTRAINT [FK__Njoftime__SsnP__300424B4] FOREIGN KEY([SsnP])
REFERENCES [dbo].[Users] ([Ssn])
GO
ALTER TABLE [dbo].[Njoftime] CHECK CONSTRAINT [FK__Njoftime__SsnP__300424B4]
GO
ALTER TABLE [dbo].[Pedagog]  WITH CHECK ADD FOREIGN KEY([IdDep])
REFERENCES [dbo].[Departament] ([IdDep])
GO
ALTER TABLE [dbo].[Pedagog]  WITH CHECK ADD  CONSTRAINT [FK__Pedagog__SsnPeda__31EC6D26] FOREIGN KEY([SsnPedagog])
REFERENCES [dbo].[Users] ([Ssn])
GO
ALTER TABLE [dbo].[Pedagog] CHECK CONSTRAINT [FK__Pedagog__SsnPeda__31EC6D26]
GO
ALTER TABLE [dbo].[PedagogAktivitete]  WITH CHECK ADD FOREIGN KEY([IdAktiviteti])
REFERENCES [dbo].[Aktivitetet] ([IdAktiviteti])
GO
ALTER TABLE [dbo].[PedagogAktivitete]  WITH CHECK ADD FOREIGN KEY([SsnPedagog])
REFERENCES [dbo].[Pedagog] ([SsnPedagog])
GO
ALTER TABLE [dbo].[Student]  WITH CHECK ADD FOREIGN KEY([IdDega])
REFERENCES [dbo].[Dega] ([IdDege])
GO
ALTER TABLE [dbo].[Student]  WITH CHECK ADD FOREIGN KEY([IdDep])
REFERENCES [dbo].[Departament] ([IdDep])
GO
ALTER TABLE [dbo].[Student]  WITH CHECK ADD  CONSTRAINT [FK__Student__SsnStud__36B12243] FOREIGN KEY([SsnStudent])
REFERENCES [dbo].[Users] ([Ssn])
GO
ALTER TABLE [dbo].[Student] CHECK CONSTRAINT [FK__Student__SsnStud__36B12243]
GO
ALTER TABLE [dbo].[StudentAktivitete]  WITH CHECK ADD FOREIGN KEY([IdAktiviteti])
REFERENCES [dbo].[Aktivitetet] ([IdAktiviteti])
GO
ALTER TABLE [dbo].[StudentAktivitete]  WITH CHECK ADD FOREIGN KEY([SsnStudent])
REFERENCES [dbo].[Student] ([SsnStudent])
GO
ALTER TABLE [dbo].[StudentKursi]  WITH CHECK ADD FOREIGN KEY([IdKursi])
REFERENCES [dbo].[Kursi] ([IdKursi])
GO
ALTER TABLE [dbo].[StudentKursi]  WITH CHECK ADD FOREIGN KEY([SsnStudent])
REFERENCES [dbo].[Student] ([SsnStudent])
GO
ALTER TABLE [dbo].[StudentKursiSezoni]  WITH CHECK ADD FOREIGN KEY([IdKursi])
REFERENCES [dbo].[Kursi] ([IdKursi])
GO
ALTER TABLE [dbo].[StudentKursiSezoni]  WITH CHECK ADD FOREIGN KEY([IdSezoni])
REFERENCES [dbo].[Sezoni] ([IdSezoni])
GO
ALTER TABLE [dbo].[StudentKursiSezoni]  WITH CHECK ADD FOREIGN KEY([SsnStudent])
REFERENCES [dbo].[Student] ([SsnStudent])
GO
ALTER TABLE [dbo].[Dega]  WITH CHECK ADD CHECK  (([Tarifa]>=(25000)))
GO
ALTER TABLE [dbo].[StudentKursiSezoni]  WITH CHECK ADD CHECK  (([Nota]>=(4) AND [Nota]<=(10)))
GO
USE [master]
GO
ALTER DATABASE [University] SET  READ_WRITE 
GO
