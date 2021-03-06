USE [master]
GO
/****** Object:  Database [app]    Script Date: 9/27/2020 3:27:36 PM ******/
CREATE DATABASE [app]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'app', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\app.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'app_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\app_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [app] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [app].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [app] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [app] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [app] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [app] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [app] SET ARITHABORT OFF 
GO
ALTER DATABASE [app] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [app] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [app] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [app] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [app] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [app] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [app] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [app] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [app] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [app] SET  ENABLE_BROKER 
GO
ALTER DATABASE [app] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [app] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [app] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [app] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [app] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [app] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [app] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [app] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [app] SET  MULTI_USER 
GO
ALTER DATABASE [app] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [app] SET DB_CHAINING OFF 
GO
ALTER DATABASE [app] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [app] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [app] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [app] SET QUERY_STORE = OFF
GO
USE [app]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 9/27/2020 3:27:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 9/27/2020 3:27:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 9/27/2020 3:27:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 9/27/2020 3:27:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 9/27/2020 3:27:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 9/27/2020 3:27:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 9/27/2020 3:27:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[Age] [int] NOT NULL,
	[CreateDate] [datetime2](7) NOT NULL,
	[FirstName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
	[FullName] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 9/27/2020 3:27:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Blogs]    Script Date: 9/27/2020 3:27:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Blogs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Published] [bit] NOT NULL,
	[CreateDate] [datetime2](7) NOT NULL,
	[CoverImageName] [nvarchar](max) NULL,
	[ImageName] [nvarchar](max) NULL,
	[ThumbnailImageName] [nvarchar](max) NULL,
	[VideoUrl] [nvarchar](max) NULL,
 CONSTRAINT [PK_Blogs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BlogTranslation]    Script Date: 9/27/2020 3:27:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BlogTranslation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LanguageCode] [nvarchar](450) NULL,
	[BlogId] [int] NOT NULL,
	[Title] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[Slug] [nvarchar](450) NULL,
 CONSTRAINT [PK_BlogTranslation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Languages]    Script Date: 9/27/2020 3:27:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Languages](
	[Code] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_Languages] PRIMARY KEY CLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200926145912_Initial', N'3.1.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200926150559_Languages', N'3.1.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200927101139_Blog', N'3.1.8')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'6ceb8c71-75b1-4722-bfb1-3b276b1b140f', N'user', N'USER', N'1c7f3326-7ef3-4df9-a486-508b37c228c7')
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'bddf2739-7093-419e-a5d5-2f05932a5eea', N'admin', N'ADMIN', N'f49047f4-7094-43d9-8e77-5b1a4778131f')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'25729e64-33ad-4ba2-96e8-b9fb04148232', N'6ceb8c71-75b1-4722-bfb1-3b276b1b140f')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'ca8561d2-a6f2-4549-a772-d36207654ab9', N'6ceb8c71-75b1-4722-bfb1-3b276b1b140f')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'fe38dff3-3dbc-482e-9f95-3894fd7c7812', N'6ceb8c71-75b1-4722-bfb1-3b276b1b140f')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'81ac43b5-51b9-42bc-87b9-4f233fd44b2b', N'bddf2739-7093-419e-a5d5-2f05932a5eea')
GO
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Age], [CreateDate], [FirstName], [LastName], [FullName]) VALUES (N'25729e64-33ad-4ba2-96e8-b9fb04148232', N'user@domain.com', N'USER@DOMAIN.COM', N'user@domain.com', N'USER@DOMAIN.COM', 1, N'AQAAAAEAACcQAAAAEJJ8l8ZPtKTML88rlLZmfJmDBZTUT4NYOGSow4gbH5cwolVgnnLd0pjiE4xO1FW7Qg==', N'2WGLWJ7YBLNO7YFIZZVPAXLLJ6APFXH2', N'2fc33430-b3d9-4636-a464-63b4a310fa39', N'123321', 0, 0, NULL, 1, 0, 22, CAST(N'2020-09-27T14:50:23.7069885' AS DateTime2), N'Emma', N'Brown', N' ')
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Age], [CreateDate], [FirstName], [LastName], [FullName]) VALUES (N'81ac43b5-51b9-42bc-87b9-4f233fd44b2b', N'johndoe@example.com', N'JOHNDOE@EXAMPLE.COM', N'johndoe@example.com', N'JOHNDOE@EXAMPLE.COM', 1, N'AQAAAAEAACcQAAAAEOA6AmXM3vUI0LgVj7xvKs2iRHfdiKjW4WXAzGQRwzsBSFQOufK3XcuXKsJTetT57A==', N'TUKG6MOUSOITPQTEFVQUEFVPSH7E7FPZ', N'4ae9b3df-66f8-4af1-b649-14ecf8728d63', N'555888111', 1, 0, NULL, 1, 0, 55, CAST(N'2020-09-26T23:58:12.9474019' AS DateTime2), N'John', N'Doe', N' ')
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Age], [CreateDate], [FirstName], [LastName], [FullName]) VALUES (N'ca8561d2-a6f2-4549-a772-d36207654ab9', N'some@domain.com', N'SOME@DOMAIN.COM', N'some@domain.com', N'SOME@DOMAIN.COM', 1, N'AQAAAAEAACcQAAAAEI/+ugMTKy/brsrDz0R2tc/758TIs5FFBM7Tq3mLNcFyOY1GHEGveemWMVKR/8MnbQ==', N'E2G7XIMFUVDGPFDS2MLTLLNDDAJQYZUO', N'ac1dee35-8c3a-44a0-aeba-c61c5c4365b5', N'555123321', 0, 0, NULL, 1, 0, 23, CAST(N'2020-09-27T14:53:38.9891201' AS DateTime2), N'David', N'Kuzana', N' ')
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Age], [CreateDate], [FirstName], [LastName], [FullName]) VALUES (N'fe38dff3-3dbc-482e-9f95-3894fd7c7812', N'davidkuzanashvili@gmail.com', N'DAVIDKUZANASHVILI@GMAIL.COM', N'davidkuzanashvili@gmail.com', N'DAVIDKUZANASHVILI@GMAIL.COM', 1, N'AQAAAAEAACcQAAAAEGt+fSCrBVioFnXEp4Fe35hrK61UUJzILVQyKgWybbeZFP1WS1weU6dw1q/6cVWL7Q==', N'YOD3V4EJ5GSPOHV22UJT2HGAEVDS5M23', N'd3b14774-0d14-4a7f-b33e-ec82f86cd813', N'555123321', 0, 0, NULL, 1, 0, 22, CAST(N'2020-09-27T01:08:16.2329296' AS DateTime2), N'Davit', N'Kuzanashvili', N' ')
GO
INSERT [dbo].[Languages] ([Code], [Name]) VALUES (N'en', N'English')
INSERT [dbo].[Languages] ([Code], [Name]) VALUES (N'ka', N'ქართული')
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetRoleClaims_RoleId]    Script Date: 9/27/2020 3:27:36 PM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetRoleClaims_RoleId] ON [dbo].[AspNetRoleClaims]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 9/27/2020 3:27:36 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[NormalizedName] ASC
)
WHERE ([NormalizedName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserClaims_UserId]    Script Date: 9/27/2020 3:27:36 PM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserClaims_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserLogins_UserId]    Script Date: 9/27/2020 3:27:36 PM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserLogins_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserRoles_RoleId]    Script Date: 9/27/2020 3:27:36 PM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserRoles_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [EmailIndex]    Script Date: 9/27/2020 3:27:36 PM ******/
CREATE NONCLUSTERED INDEX [EmailIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 9/27/2020 3:27:36 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedUserName] ASC
)
WHERE ([NormalizedUserName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_BlogTranslation_BlogId]    Script Date: 9/27/2020 3:27:36 PM ******/
CREATE NONCLUSTERED INDEX [IX_BlogTranslation_BlogId] ON [dbo].[BlogTranslation]
(
	[BlogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_BlogTranslation_LanguageCode]    Script Date: 9/27/2020 3:27:36 PM ******/
CREATE NONCLUSTERED INDEX [IX_BlogTranslation_LanguageCode] ON [dbo].[BlogTranslation]
(
	[LanguageCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_BlogTranslation_Slug]    Script Date: 9/27/2020 3:27:36 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_BlogTranslation_Slug] ON [dbo].[BlogTranslation]
(
	[Slug] ASC
)
WHERE ([Slug] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Languages_Code]    Script Date: 9/27/2020 3:27:36 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Languages_Code] ON [dbo].[Languages]
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[BlogTranslation]  WITH CHECK ADD  CONSTRAINT [FK_BlogTranslation_Blogs_BlogId] FOREIGN KEY([BlogId])
REFERENCES [dbo].[Blogs] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BlogTranslation] CHECK CONSTRAINT [FK_BlogTranslation_Blogs_BlogId]
GO
ALTER TABLE [dbo].[BlogTranslation]  WITH CHECK ADD  CONSTRAINT [FK_BlogTranslation_Languages_LanguageCode] FOREIGN KEY([LanguageCode])
REFERENCES [dbo].[Languages] ([Code])
GO
ALTER TABLE [dbo].[BlogTranslation] CHECK CONSTRAINT [FK_BlogTranslation_Languages_LanguageCode]
GO
USE [master]
GO
ALTER DATABASE [app] SET  READ_WRITE 
GO
