USE [AuthTest]
GO
/****** Object:  Table [dbo].[User]    Script Date: 04/03/2016 10:19:39 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[User]') AND type in (N'U'))
DROP TABLE [dbo].[User]
GO
/****** Object:  Table [dbo].[RefreshToken]    Script Date: 04/03/2016 10:19:39 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RefreshToken]') AND type in (N'U'))
DROP TABLE [dbo].[RefreshToken]
GO
/****** Object:  Table [dbo].[Client]    Script Date: 04/03/2016 10:19:39 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Client]') AND type in (N'U'))
DROP TABLE [dbo].[Client]
GO
/****** Object:  Table [dbo].[Client]    Script Date: 04/03/2016 10:19:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Client]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Client](
	[Id] [varchar](50) NULL,
	[Secret] [varchar](255) NULL,
	[Name] [varchar](50) NULL,
	[ApplicationType] [int] NOT NULL,
	[Active] [tinyint] NOT NULL,
	[RefreshTokenLiftTime] [int] NOT NULL,
	[AllowOrigin] [varchar](255) NULL
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RefreshToken]    Script Date: 04/03/2016 10:19:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RefreshToken]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[RefreshToken](
	[Id] [varchar](255) NOT NULL,
	[Subject] [varchar](50) NOT NULL,
	[ClientId] [varchar](50) NOT NULL,
	[IssuedUtc] [datetime] NOT NULL,
	[ExpiresUtc] [datetime] NOT NULL,
	[ProtectedTicket] [varchar](500) NOT NULL
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[User]    Script Date: 04/03/2016 10:19:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[User]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[User](
	[UserId] [bigint] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](50) NULL,
	[Password] [varchar](50) NULL,
 CONSTRAINT [pk_usr_2DA17977] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[Client] ([Id], [Secret], [Name], [ApplicationType], [Active], [RefreshTokenLiftTime], [AllowOrigin]) VALUES (N'WebClient', N'E10ADC3949BA59ABBE56E057F20F883E', N'Web Application', 1, 1, 14400, N'*')
INSERT [dbo].[RefreshToken] ([Id], [Subject], [ClientId], [IssuedUtc], [ExpiresUtc], [ProtectedTicket]) VALUES (N'5F8683F85726A4BE4D72D6FBF186AA63', N'austinwu', N'WebClient', CAST(N'2016-04-03 05:13:54.627' AS DateTime), CAST(N'2016-04-13 05:13:54.627' AS DateTime), N'0kQg0k3GMJ_delmeVrFe2k2rSevgVSuwzKO4o5wgMVV5EG1oT-HEORKy0fVhB1ZNfQSXagiQLeWDU4DUGP8L3PY3xr69EGzxhjmZqKzQTQPrfSjWXGuheKESp21tkRQJSiX0_lPaG2FvNmq_rmD0NOmJJWrlpKVVhRtDPBYH2MBi7JS3DI3EX1P5ypfSaLDBDwfh_6-EsjU8MfID1_omkKK1puBcpSrBzj6P7txRAiE5MRBOWVSA8B4Z2kbvCF_IVrq_2ILz4hOuRtnzU3Qe0A-6iRcwk8DdqX9EgErbBoI')
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([UserId], [UserName], [Password]) VALUES (1, N'AustinWu', N'123456')
SET IDENTITY_INSERT [dbo].[User] OFF
