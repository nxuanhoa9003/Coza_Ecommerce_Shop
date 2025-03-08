USE [COZA_Ecommerce_Shop]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 3/8/2025 10:22:25 PM ******/
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
/****** Object:  Table [dbo].[Banner]    Script Date: 3/8/2025 10:22:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Banner](
	[Id] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](200) NOT NULL,
	[Description] [nvarchar](500) NULL,
	[Image] [nvarchar](500) NULL,
	[Link] [nvarchar](max) NULL,
	[Type] [int] NOT NULL,
	[CreateBy] [nvarchar](max) NULL,
	[CreateDate] [datetime2](7) NOT NULL,
	[ModifierDate] [datetime2](7) NOT NULL,
	[ModifiedBy] [nvarchar](max) NULL,
	[IsShow] [bit] NOT NULL,
 CONSTRAINT [PK_Banner] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CartItems]    Script Date: 3/8/2025 10:22:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CartItems](
	[Id] [uniqueidentifier] NOT NULL,
	[CartId] [uniqueidentifier] NOT NULL,
	[ProductId] [uniqueidentifier] NOT NULL,
	[VariantId] [uniqueidentifier] NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[Quantity] [int] NOT NULL,
 CONSTRAINT [PK_CartItems] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Carts]    Script Date: 3/8/2025 10:22:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Carts](
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[TotalPrice] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_Carts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 3/8/2025 10:22:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[Id] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](200) NOT NULL,
	[Slug] [nvarchar](250) NULL,
	[Description] [nvarchar](max) NULL,
	[SeoTitle] [nvarchar](250) NULL,
	[SeoDescription] [nvarchar](500) NULL,
	[SeoKeywords] [nvarchar](500) NULL,
	[Position] [int] NULL,
	[IsActive] [bit] NOT NULL,
	[CreateBy] [nvarchar](max) NULL,
	[CreateDate] [datetime2](7) NOT NULL,
	[ModifierDate] [datetime2](7) NOT NULL,
	[ModifiedBy] [nvarchar](max) NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Claims]    Script Date: 3/8/2025 10:22:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Claims](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[ClaimType] [nvarchar](50) NOT NULL,
	[ClaimValue] [nvarchar](255) NOT NULL,
	[Description] [nvarchar](500) NOT NULL,
	[TypeClaim] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Claims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Contact]    Script Date: 3/8/2025 10:22:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contact](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
	[Email] [nvarchar](150) NOT NULL,
	[Message] [nvarchar](max) NOT NULL,
	[CreateBy] [nvarchar](max) NULL,
	[CreateDate] [datetime2](7) NOT NULL,
	[ModifierDate] [datetime2](7) NOT NULL,
	[ModifiedBy] [nvarchar](max) NULL,
	[IsRead] [bit] NOT NULL,
 CONSTRAINT [PK_Contact] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Menu]    Script Date: 3/8/2025 10:22:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Menu](
	[Id] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](200) NOT NULL,
	[TitleSub] [nvarchar](200) NOT NULL,
	[Image] [nvarchar](500) NULL,
	[Link] [nvarchar](max) NULL,
	[Type] [int] NOT NULL,
	[IsShow] [bit] NOT NULL,
	[CreateBy] [nvarchar](max) NULL,
	[CreateDate] [datetime2](7) NOT NULL,
	[ModifierDate] [datetime2](7) NOT NULL,
	[ModifiedBy] [nvarchar](max) NULL,
 CONSTRAINT [PK_Menu] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[New]    Script Date: 3/8/2025 10:22:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[New](
	[Id] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](150) NOT NULL,
	[Slug] [nvarchar](250) NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Detail] [nvarchar](max) NOT NULL,
	[Image] [nvarchar](250) NULL,
	[CategoryId] [uniqueidentifier] NOT NULL,
	[SeoTitile] [nvarchar](250) NULL,
	[SeoDescription] [nvarchar](500) NULL,
	[SeoKeywords] [nvarchar](250) NULL,
	[IsActive] [bit] NOT NULL,
	[CreateBy] [nvarchar](max) NULL,
	[CreateDate] [datetime2](7) NOT NULL,
	[ModifierDate] [datetime2](7) NOT NULL,
	[ModifiedBy] [nvarchar](max) NULL,
 CONSTRAINT [PK_New] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 3/8/2025 10:22:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[Id] [uniqueidentifier] NOT NULL,
	[Code] [nvarchar](max) NOT NULL,
	[TotalAmount] [decimal](18, 2) NOT NULL,
	[Quantity] [int] NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[UpdatedAt] [datetime2](7) NOT NULL,
	[ModifiedBy] [nvarchar](max) NULL,
	[Address] [nvarchar](max) NOT NULL,
	[Status] [nvarchar](20) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[FullName] [nvarchar](max) NOT NULL,
	[Phone] [nvarchar](max) NOT NULL,
	[TypePayment] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetail]    Script Date: 3/8/2025 10:22:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetail](
	[OrderId] [uniqueidentifier] NOT NULL,
	[ProductId] [uniqueidentifier] NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[Quantity] [int] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[VariantId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_OrderDetail] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC,
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Post]    Script Date: 3/8/2025 10:22:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Post](
	[Id] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](150) NOT NULL,
	[Slug] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Detail] [nvarchar](max) NOT NULL,
	[Image] [nvarchar](max) NULL,
	[CategoryId] [uniqueidentifier] NOT NULL,
	[SeoTitile] [nvarchar](max) NULL,
	[SeoDescription] [nvarchar](max) NULL,
	[SeoKeywords] [nvarchar](max) NULL,
	[IsActive] [bit] NOT NULL,
	[CreateBy] [nvarchar](max) NULL,
	[CreateDate] [datetime2](7) NOT NULL,
	[ModifierDate] [datetime2](7) NOT NULL,
	[ModifiedBy] [nvarchar](max) NULL,
 CONSTRAINT [PK_Post] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 3/8/2025 10:22:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[Id] [uniqueidentifier] NOT NULL,
	[ProductCode] [nvarchar](50) NULL,
	[Title] [nvarchar](250) NOT NULL,
	[Slug] [nvarchar](450) NULL,
	[Description] [nvarchar](max) NULL,
	[Detail] [nvarchar](max) NULL,
	[Image] [nvarchar](250) NULL,
	[Quantity] [int] NOT NULL,
	[IsHome] [bit] NOT NULL,
	[IsSale] [bit] NOT NULL,
	[IsFeature] [bit] NOT NULL,
	[IsHot] [bit] NOT NULL,
	[ProductCategoryId] [uniqueidentifier] NOT NULL,
	[SeoTitile] [nvarchar](250) NULL,
	[SeoDescription] [nvarchar](500) NULL,
	[SeoKeywords] [nvarchar](250) NULL,
	[IsActive] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CategoryId] [uniqueidentifier] NULL,
	[CreateBy] [nvarchar](max) NULL,
	[CreateDate] [datetime2](7) NOT NULL,
	[ModifierDate] [datetime2](7) NOT NULL,
	[ModifiedBy] [nvarchar](max) NULL,
	[AttributeOptionIds] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductCategory]    Script Date: 3/8/2025 10:22:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductCategory](
	[Id] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](150) NOT NULL,
	[Slug] [nvarchar](150) NULL,
	[Description] [nvarchar](max) NULL,
	[SeoTitle] [nvarchar](150) NULL,
	[SeoDescription] [nvarchar](500) NULL,
	[SeoKeywords] [nvarchar](250) NULL,
	[IsDeleted] [bit] NOT NULL,
	[IsFeatured] [bit] NOT NULL,
	[ParentCategoryId] [uniqueidentifier] NULL,
	[CreateBy] [nvarchar](max) NULL,
	[CreateDate] [datetime2](7) NOT NULL,
	[ModifierDate] [datetime2](7) NOT NULL,
	[ModifiedBy] [nvarchar](max) NULL,
 CONSTRAINT [PK_ProductCategory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductImage]    Script Date: 3/8/2025 10:22:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductImage](
	[Id] [uniqueidentifier] NOT NULL,
	[ProductId] [uniqueidentifier] NOT NULL,
	[Image] [nvarchar](max) NOT NULL,
	[IsDefault] [bit] NOT NULL,
 CONSTRAINT [PK_ProductImage] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductVariants]    Script Date: 3/8/2025 10:22:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductVariants](
	[Id] [uniqueidentifier] NOT NULL,
	[SKU] [nvarchar](450) NULL,
	[ProductId] [uniqueidentifier] NOT NULL,
	[Quantity] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreateBy] [nvarchar](max) NULL,
	[CreateDate] [datetime2](7) NOT NULL,
	[ModifierDate] [datetime2](7) NOT NULL,
	[ModifiedBy] [nvarchar](max) NULL,
	[BasePrice] [decimal](18, 2) NULL,
	[PriceSale] [decimal](18, 2) NULL,
	[Size] [nvarchar](max) NULL,
	[IsDefault] [bit] NOT NULL,
	[Color] [nvarchar](max) NULL,
	[ReservedStock] [int] NOT NULL,
 CONSTRAINT [PK_ProductVariants] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoleClaims]    Script Date: 3/8/2025 10:22:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_RoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 3/8/2025 10:22:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SettingConfiguration]    Script Date: 3/8/2025 10:22:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SettingConfiguration](
	[SettingKey] [nvarchar](50) NOT NULL,
	[SettingValue] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_SettingConfiguration] PRIMARY KEY CLUSTERED 
(
	[SettingKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Subscribe]    Script Date: 3/8/2025 10:22:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Subscribe](
	[Id] [uniqueidentifier] NOT NULL,
	[Email] [nvarchar](150) NOT NULL,
	[CreateDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Subscribe] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transactions]    Script Date: 3/8/2025 10:22:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transactions](
	[TransactionId] [nvarchar](30) NOT NULL,
	[OrderId] [uniqueidentifier] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Transactions] PRIMARY KEY CLUSTERED 
(
	[TransactionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserClaims]    Script Date: 3/8/2025 10:22:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_UserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserLogins]    Script Date: 3/8/2025 10:22:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserLogins](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_UserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRoles]    Script Date: 3/8/2025 10:22:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 3/8/2025 10:22:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [nvarchar](450) NOT NULL,
	[FullName] [nvarchar](400) NOT NULL,
	[Address] [nvarchar](400) NULL,
	[BirthDate] [datetime2](7) NULL,
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
	[LastPasswordResetRequest] [datetime2](7) NULL,
	[FailedPasswordResetAttempts] [int] NOT NULL,
	[PasswordResetLockoutEnd] [datetime2](7) NULL,
	[AvatarUrl] [nvarchar](400) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserTokens]    Script Date: 3/8/2025 10:22:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_UserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Wishlists]    Script Date: 3/8/2025 10:22:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Wishlists](
	[Id] [uniqueidentifier] NOT NULL,
	[ProductId] [uniqueidentifier] NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[CreateDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Wishlists] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250118180754_InitialDB_COZA', N'8.0.10')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250123173238_UpdateModelVariant_24_1', N'8.0.10')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250126170426_Update_27_1_2025', N'8.0.10')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250201062534_Update_DB_1_2_2025', N'8.0.10')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250215104918_Update_ModelUser', N'8.0.11')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250215112535_Update_ModelUserP2', N'8.0.11')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250215151112_Update_ModelUserP3', N'8.0.11')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250216183451_AddModelClaims', N'8.0.11')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250217114457_UpdateModelClaim_P1', N'8.0.11')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250218112142_UpdateModel_AppUser', N'8.0.11')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250220165540_FixCollation', N'8.0.11')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250222170221_AddCartAndCartItem', N'8.0.11')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250225085730_UpdateModelOrder', N'8.0.11')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250225091424_UpdateModelProductVariant', N'8.0.11')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250225155206_AddModelTransaction', N'8.0.11')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250225191002_UpdateModepOrder', N'8.0.11')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250226072729_UpdateModelOrder_2', N'8.0.11')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250228113927_UpdateModelContact', N'8.0.11')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250228173012_addmodelwishlist', N'8.0.11')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250302092510_UpdateModelBanner', N'8.0.11')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250302202720_UpdateModelSettingConfiguration', N'8.0.11')
GO
INSERT [dbo].[Banner] ([Id], [Title], [Description], [Image], [Link], [Type], [CreateBy], [CreateDate], [ModifierDate], [ModifiedBy], [IsShow]) VALUES (N'48f1d53c-a7f3-4c86-b45d-ddc468e50c01', N'Áo Sơ Mi Nam 2025', N'Phong cách đột phá, đẳng cấp thời thượng', N'/Uploads/Banner/slide-03_20250302_234207.jpg', N'/product/list-product?search=áo+sơ+mi', 2, N'Nguyễn Tùng Dương', CAST(N'2025-03-02T16:46:24.6445816' AS DateTime2), CAST(N'2025-03-02T23:42:07.5409509' AS DateTime2), N'Nguyễn Tùng Dương', 1)
GO
INSERT [dbo].[Banner] ([Id], [Title], [Description], [Image], [Link], [Type], [CreateBy], [CreateDate], [ModifierDate], [ModifiedBy], [IsShow]) VALUES (N'e58997b4-425d-40a7-8033-f89d22bbe1de', N'Men Collection 2025', N'Bộ sưu tập nam 2025 đẳng cấp', N'/Uploads/Banner/slide-02_20250302_162601.jpg', N'/product/list-product?slug=thoi-trang-nam', 1, N'Nguyễn Tùng Dương', CAST(N'2025-03-02T16:26:01.4772908' AS DateTime2), CAST(N'2025-03-02T16:26:01.4772916' AS DateTime2), NULL, 1)
GO
INSERT [dbo].[CartItems] ([Id], [CartId], [ProductId], [VariantId], [Price], [Quantity]) VALUES (N'e25edc93-e80c-4b75-bab1-2c18959beb55', N'b6584534-50b7-4ae2-80cb-ebdec36d2273', N'c65520a9-78e9-4448-9624-0c6e0e5f39de', N'edbad518-43e0-42ef-9a7b-c77c070b7195', CAST(519000.00 AS Decimal(18, 2)), 1)
GO
INSERT [dbo].[CartItems] ([Id], [CartId], [ProductId], [VariantId], [Price], [Quantity]) VALUES (N'e983b06d-ec4f-4c69-a0ee-e15426e3f449', N'b6584534-50b7-4ae2-80cb-ebdec36d2273', N'fafa1eec-07cb-4631-b1dc-ffcee3ce8b05', N'a82976ac-b131-41bb-a80f-f0444019acac', CAST(799000.00 AS Decimal(18, 2)), 5)
GO
INSERT [dbo].[CartItems] ([Id], [CartId], [ProductId], [VariantId], [Price], [Quantity]) VALUES (N'693367fb-acb9-42b6-8d2a-fb6e5b04959c', N'b6584534-50b7-4ae2-80cb-ebdec36d2273', N'0929e105-20f1-4e8e-92d8-25113f9f0c9c', N'0e89f07f-bc3f-4919-85f9-3dd5c7a1aa84', CAST(599000.00 AS Decimal(18, 2)), 6)
GO
INSERT [dbo].[Carts] ([Id], [UserId], [TotalPrice]) VALUES (N'b6584534-50b7-4ae2-80cb-ebdec36d2273', N'90820a1b-cacb-4186-8b68-8e258b6eac53', CAST(8108000.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Category] ([Id], [Title], [Slug], [Description], [SeoTitle], [SeoDescription], [SeoKeywords], [Position], [IsActive], [CreateBy], [CreateDate], [ModifierDate], [ModifiedBy]) VALUES (N'650efc9d-1069-4643-84ac-218c7b5a96e1', N'Xu hướng Xuân/Hè & Thu/Đông', N'xu-huong-xuanhe-thuong', N'Xu hướng thời trang Xuân/Hè & Thu/Đông luôn thay đổi theo từng năm, mang đến những phong cách mới mẻ, độc đáo. Hãy cùng khám phá những mẫu thiết kế hot nhất, cách phối đồ theo mùa và gợi ý trang phục giúp bạn luôn thời thượng trong mọi hoàn cảnh.', N'Xu hướng thời trang Xuân/Hè & Thu/Đông mới nhất - Cập nhật ngay!', N'Cập nhật xu hướng thời trang Xuân/Hè & Thu/Đông mới nhất. Khám phá phong cách hot trend, cách phối đồ theo mùa, giúp bạn luôn nổi bật và thời thượng!', N'Xu hướng thời trang Xuân Hè, Xu hướng thời trang Thu Đông, Thời trang theo mùa', 1, 0, N'Nguyễn Tùng Dương', CAST(N'2025-02-26T22:09:19.5811744' AS DateTime2), CAST(N'2025-02-26T22:09:19.5820340' AS DateTime2), NULL)
GO
INSERT [dbo].[Category] ([Id], [Title], [Slug], [Description], [SeoTitle], [SeoDescription], [SeoKeywords], [Position], [IsActive], [CreateBy], [CreateDate], [ModifierDate], [ModifiedBy]) VALUES (N'4cd9d658-1b29-4243-b6e9-53d09a4de0a4', N'Trang phục dự tiệc', N'trang-phuc-du-tiec', N'Dự tiệc cần gì? Những bộ trang phục sang trọng, đẳng cấp chắc chắn sẽ giúp bạn tỏa sáng. Tìm hiểu xu hướng đầm dự tiệc, vest lịch lãm và cách phối đồ ấn tượng nhất!', N'Trang phục dự tiệc sang trọng – Gợi ý outfit giúp bạn tỏa sáng', N'Cập nhật những mẫu trang phục dự tiệc hot trend! Gợi ý váy dạ hội, vest sang trọng và cách phối đồ giúp bạn nổi bật trong mọi bữa tiệc.', N'Thời trang dự tiệc, Đầm dạ hội cao cấp, Phong cách tiệc tùng, Cách chọn trang phục dự tiệc', 2, 0, N'Nguyễn Tùng Dương', CAST(N'2025-02-26T22:27:21.8166286' AS DateTime2), CAST(N'2025-02-26T22:27:21.8166899' AS DateTime2), NULL)
GO
INSERT [dbo].[Category] ([Id], [Title], [Slug], [Description], [SeoTitle], [SeoDescription], [SeoKeywords], [Position], [IsActive], [CreateBy], [CreateDate], [ModifierDate], [ModifiedBy]) VALUES (N'0c8dacbd-53f5-43ba-859b-a457154dcc13', N'Outfit dạo phố', N'outfit-dao-pho', N'Cùng khám phá những outfit streetwear, năng động và trendy giúp bạn tỏa sáng mỗi khi xuống phố.', N'Outfit dạo phố đẹp – Phong cách thời trang streetwear năng động', N'Cập nhật những outfit dạo phố hot trend! Khám phá phong cách streetwear, cách phối đồ năng động, trẻ trung giúp bạn luôn nổi bật khi xuống phố.', N'Outfit dạo phố đẹp, Cách phối đồ đi chơi, Trang phục dạo phố cá tính', 3, 0, N'Nguyễn Tùng Dương', CAST(N'2025-02-26T22:28:22.6687556' AS DateTime2), CAST(N'2025-02-26T22:28:22.6687560' AS DateTime2), NULL)
GO
INSERT [dbo].[Category] ([Id], [Title], [Slug], [Description], [SeoTitle], [SeoDescription], [SeoKeywords], [Position], [IsActive], [CreateBy], [CreateDate], [ModifierDate], [ModifiedBy]) VALUES (N'2abc719a-8485-4025-a078-d1e115f8e495', N'Giới thiệu về HShop', N'gioi-thieu-ve-hshop', N'HShop là thương hiệu thời trang nam hàng đầu tại Việt Nam, chuyên cung cấp sản phẩm chất lượng với thiết kế hiện đại, tinh tế. ', N'HShop - Thương Hiệu Thời Trang Nam Hàng Đầu Tại Việt Nam', N'Khám phá HShop, thương hiệu thời trang nam uy tín tại Việt Nam. Chúng tôi cung cấp sản phẩm chất lượng, thiết kế tinh tế và dịch vụ hoàn hảo, giúp bạn thể hiện phong cách và sự tự tin.', N'HShop, thời trang nam, thương hiệu thời trang, sản phẩm thời trang nam, phong cách thanh lịch, mua sắm thời trang', 1, 0, N'Nguyễn Tùng Dương', CAST(N'2025-03-01T23:15:03.1044654' AS DateTime2), CAST(N'2025-03-01T23:15:03.1045339' AS DateTime2), NULL)
GO
INSERT [dbo].[Category] ([Id], [Title], [Slug], [Description], [SeoTitle], [SeoDescription], [SeoKeywords], [Position], [IsActive], [CreateBy], [CreateDate], [ModifierDate], [ModifiedBy]) VALUES (N'c701a4cf-0e1d-42fd-9664-d76c072b1674', N'Xu hướng thời trang', N'xu-huong-thoi-trang', N'Xu hướng thời trang luôn thay đổi qua từng mùa, mang đến những phong cách mới lạ, độc đáo. Cập nhật những xu hướng hot nhất về màu sắc, chất liệu, kiểu dáng đang làm mưa làm gió trên các sàn diễn thời trang và phong cách đường phố.', N'Xu hướng thời trang mới nhất – Cập nhật phong cách hot trend 2025', N'Khám phá xu hướng thời trang mới nhất 2025! Cập nhật các phong cách hot trend, cách phối đồ theo mùa và gợi ý outfit giúp bạn luôn nổi bật và sành điệu.', N'Xu hướng thời trang 2025, Xu hướng thời trang nữ, Phong cách thời trang mới nhất, Mẫu thời trang hot', 1, 0, N'Nguyễn Tùng Dương', CAST(N'2025-02-26T22:12:37.5470810' AS DateTime2), CAST(N'2025-02-26T22:12:37.5470816' AS DateTime2), NULL)
GO
INSERT [dbo].[Claims] ([Id], [Name], [ClaimType], [ClaimValue], [Description], [TypeClaim]) VALUES (N'3b426db7-0ade-4c2f-8475-023a3957f9e0', N'Xem danh sách vai trò', N'Permission', N'ViewRoles', N'Cho phép xem danh sách vai trò', N'Quản trị viên')
GO
INSERT [dbo].[Claims] ([Id], [Name], [ClaimType], [ClaimValue], [Description], [TypeClaim]) VALUES (N'4226b8eb-3804-4840-9df4-0b26be338c80', N'Chỉnh sửa tin tức', N'Permission', N'EditNews', N'Cho phép chỉnh sửa nội dung tin tức', N'Tin tức')
GO
INSERT [dbo].[Claims] ([Id], [Name], [ClaimType], [ClaimValue], [Description], [TypeClaim]) VALUES (N'84b4f990-c47b-4ebf-b7e9-18b315fe161c', N'Xóa tin tức', N'Permission', N'DeleteNews', N'Cho phép xóa tin tức', N'Tin tức')
GO
INSERT [dbo].[Claims] ([Id], [Name], [ClaimType], [ClaimValue], [Description], [TypeClaim]) VALUES (N'f0312bcb-9fbc-4c35-b2c3-191670cad3e0', N'Chỉnh sửa danh mục', N'Permission', N'EditProductCategory', N'Cho phép chỉnh sửa danh mục sản phẩm', N'Sản phẩm')
GO
INSERT [dbo].[Claims] ([Id], [Name], [ClaimType], [ClaimValue], [Description], [TypeClaim]) VALUES (N'f779e2e3-01ce-4701-b134-27947b5f8f40', N'Xóa bài viết', N'Permission', N'DeletePost', N'Cho phép xóa bài viết', N'Bài viết')
GO
INSERT [dbo].[Claims] ([Id], [Name], [ClaimType], [ClaimValue], [Description], [TypeClaim]) VALUES (N'2710f954-d9a1-4911-9099-2d041ba1293d', N'Cập nhật thông tin cá nhân', N'Permission', N'UpdateProfile', N'Cho phép người dùng cập nhật thông tin cá nhân', N'User')
GO
INSERT [dbo].[Claims] ([Id], [Name], [ClaimType], [ClaimValue], [Description], [TypeClaim]) VALUES (N'ce51cda8-51a8-43bb-b571-40d76578ba39', N'Chỉnh sửa bài viết', N'Permission', N'EditPost', N'Cho phép chỉnh sửa nội dung bài viết', N'Bài viết')
GO
INSERT [dbo].[Claims] ([Id], [Name], [ClaimType], [ClaimValue], [Description], [TypeClaim]) VALUES (N'9c71ff72-31a0-447d-bde9-50acb1b0e5ee', N'Viết bài mới', N'Permission', N'CreatePost', N'Cho phép tạo bài viết mới', N'Bài viết')
GO
INSERT [dbo].[Claims] ([Id], [Name], [ClaimType], [ClaimValue], [Description], [TypeClaim]) VALUES (N'dbeb6b43-555a-4b13-b713-54255c3c3471', N'Xem chi tiết tài khoản', N'Permission', N'ViewUserDetail', N'Cho phép xem thông tin tài khoản', N'Quản trị viên')
GO
INSERT [dbo].[Claims] ([Id], [Name], [ClaimType], [ClaimValue], [Description], [TypeClaim]) VALUES (N'b191038c-8268-4781-8646-59c2e83fd998', N'Thêm tài khoản', N'Permission', N'CreateUser', N'Cho phép tạo tài khoản mới', N'Quản trị viên')
GO
INSERT [dbo].[Claims] ([Id], [Name], [ClaimType], [ClaimValue], [Description], [TypeClaim]) VALUES (N'dbd01623-06ca-4e7f-ba9d-678aa68f7712', N'Chỉnh sửa tài khoản', N'Permission', N'EditUser', N'Cho phép chỉnh sửa tài khoản', N'Quản trị viên')
GO
INSERT [dbo].[Claims] ([Id], [Name], [ClaimType], [ClaimValue], [Description], [TypeClaim]) VALUES (N'c6363aa6-508d-4cfc-a54e-68978b579f4c', N'Thêm sản phẩm', N'Permission', N'CreateProduct', N'Cho phép thêm sản phẩm mới', N'Sản phẩm')
GO
INSERT [dbo].[Claims] ([Id], [Name], [ClaimType], [ClaimValue], [Description], [TypeClaim]) VALUES (N'a2ac5f23-24ce-413d-ba09-710e5b64ee38', N'Xem danh sách sản phẩm', N'Permission', N'ViewProduct', N'Cho phép xem danh sách sản phẩm', N'Sản phẩm')
GO
INSERT [dbo].[Claims] ([Id], [Name], [ClaimType], [ClaimValue], [Description], [TypeClaim]) VALUES (N'd32cd398-7a0e-4715-b8ed-8acf18bd000e', N'Xóa danh mục', N'Permission', N'DeleteProductCategory', N'Cho phép xóa danh mục sản phẩm', N'Sản phẩm')
GO
INSERT [dbo].[Claims] ([Id], [Name], [ClaimType], [ClaimValue], [Description], [TypeClaim]) VALUES (N'bf99f3e1-8a44-471e-bef1-8e5c3f0768e2', N'Chỉnh sửa sản phẩm', N'Permission', N'EditProduct', N'Cho phép chỉnh sửa thông tin sản phẩm', N'Sản phẩm')
GO
INSERT [dbo].[Claims] ([Id], [Name], [ClaimType], [ClaimValue], [Description], [TypeClaim]) VALUES (N'280f9240-48f8-4f77-bbd0-910920270597', N'Xem chi tiết sản phẩm', N'Permission', N'ViewProductDetail', N'Cho phép xem chi tiết sản phẩm', N'Sản phẩm')
GO
INSERT [dbo].[Claims] ([Id], [Name], [ClaimType], [ClaimValue], [Description], [TypeClaim]) VALUES (N'61a60e93-4894-4053-bea9-9688e15088e8', N'Xem danh sách bài viết', N'Permission', N'ViewPost', N'Cho phép xem danh sách bài viết', N'Bài viết')
GO
INSERT [dbo].[Claims] ([Id], [Name], [ClaimType], [ClaimValue], [Description], [TypeClaim]) VALUES (N'976f3949-58db-428c-b666-96f1977fbbf7', N'Xóa tài khoản', N'Permission', N'DeleteUser', N'Cho phép xóa tài khoản', N'Quản trị viên')
GO
INSERT [dbo].[Claims] ([Id], [Name], [ClaimType], [ClaimValue], [Description], [TypeClaim]) VALUES (N'7cdadef4-dea2-467e-ad24-a6501d0c6057', N'Xem danh sách tin tức', N'Permission', N'ViewNews', N'Cho phép xem danh sách tin tức', N'Tin tức')
GO
INSERT [dbo].[Claims] ([Id], [Name], [ClaimType], [ClaimValue], [Description], [TypeClaim]) VALUES (N'de2856b6-d1b0-4ddf-9551-add8e83712b7', N'Xem chi tiết tin tức', N'Permission', N'ViewNewsDetail', N'Cho phép xem nội dung tin tức', N'Tin tức')
GO
INSERT [dbo].[Claims] ([Id], [Name], [ClaimType], [ClaimValue], [Description], [TypeClaim]) VALUES (N'65b747f2-6534-4b87-b888-ae24fd3508fe', N'Thêm danh mục', N'Permission', N'CreateProductCategory', N'Cho phép tạo danh mục sản phẩm mới', N'Sản phẩm')
GO
INSERT [dbo].[Claims] ([Id], [Name], [ClaimType], [ClaimValue], [Description], [TypeClaim]) VALUES (N'0bf8f8b8-c2a8-4cbe-b1a8-c6c605ff49de', N'Xem chi tiết danh mục sản phẩm', N'Permission', N'ViewProductCategoryDetail', N'Cho phép xem chi tiết danh mục sản phẩm', N'Sản phẩm')
GO
INSERT [dbo].[Claims] ([Id], [Name], [ClaimType], [ClaimValue], [Description], [TypeClaim]) VALUES (N'f0cd2ded-9662-4a64-b3b9-cb4c49b9b6e4', N'Xem danh sách nhân viên', N'Permission', N'ViewUsers', N'Cho phép người dùng xem danh sách nhân viên', N'Quản trị viên')
GO
INSERT [dbo].[Claims] ([Id], [Name], [ClaimType], [ClaimValue], [Description], [TypeClaim]) VALUES (N'd967e64e-213c-45b5-9b4e-d811c4c546a7', N'Xóa sản phẩm', N'Permission', N'DeleteProduct', N'Cho phép xóa sản phẩm', N'Sản phẩm')
GO
INSERT [dbo].[Claims] ([Id], [Name], [ClaimType], [ClaimValue], [Description], [TypeClaim]) VALUES (N'fe7af361-1fad-4e1d-bb95-dc86d7fbfa54', N'Xem chi tiết bài viết', N'Permission', N'ViewPostDetail', N'Cho phép xem nội dung bài viết', N'Bài viết')
GO
INSERT [dbo].[Claims] ([Id], [Name], [ClaimType], [ClaimValue], [Description], [TypeClaim]) VALUES (N'cfc97798-140a-40b0-adf7-de1a9f0b062f', N'Quản lý vai trò', N'Permission', N'ManageRoles', N'Cho phép thêm, sửa, xóa vai trò', N'Quản trị viên')
GO
INSERT [dbo].[Claims] ([Id], [Name], [ClaimType], [ClaimValue], [Description], [TypeClaim]) VALUES (N'e2b3b14a-3b26-43f2-8625-e83b530306cf', N'Chỉnh sửa quyền của vai trò', N'Permission', N'EditRolePermission', N'Cho phép chỉnh sửa quyền của vai trò', N'Quản trị viên')
GO
INSERT [dbo].[Claims] ([Id], [Name], [ClaimType], [ClaimValue], [Description], [TypeClaim]) VALUES (N'767f1ddb-8db9-47e5-8e14-e86c96d36f63', N'Viết tin tức mới', N'Permission', N'CreateNews', N'Cho phép tạo tin tức mới', N'Tin tức')
GO
INSERT [dbo].[Claims] ([Id], [Name], [ClaimType], [ClaimValue], [Description], [TypeClaim]) VALUES (N'f222d2f1-d35e-4412-b743-f65c7b7c06ef', N'Xem danh mục sản phẩm', N'Permission', N'ViewProductCategory', N'Cho phép xem danh sách danh mục sản phẩm', N'Sản phẩm')
GO
INSERT [dbo].[Claims] ([Id], [Name], [ClaimType], [ClaimValue], [Description], [TypeClaim]) VALUES (N'40f8c61e-f615-4ae2-a00f-f73f44456768', N'Truy cập trang Admin', N'Permission', N'AccessAdminPage', N'Cho phép người dùng truy cập trang quản trị', N'Quản trị viên')
GO
INSERT [dbo].[Claims] ([Id], [Name], [ClaimType], [ClaimValue], [Description], [TypeClaim]) VALUES (N'89b3f6ec-8b7d-4200-b6c4-fb4091c3a107', N'Xem thông tin cá nhân', N'Permission', N'ViewProfile', N'Cho phép người dùng xem thông tin cá nhân', N'User')
GO
INSERT [dbo].[Claims] ([Id], [Name], [ClaimType], [ClaimValue], [Description], [TypeClaim]) VALUES (N'a595fb60-5bf4-441a-88ae-fd0298ed37e6', N'Xem danh sách quyền', N'Permission', N'ViewPermissions', N'Cho phép người dùng xem danh sách sách quyền', N'Quản trị viên')
GO
INSERT [dbo].[Contact] ([Id], [Name], [Email], [Message], [CreateBy], [CreateDate], [ModifierDate], [ModifiedBy], [IsRead]) VALUES (N'1b5be4fa-9e6b-4f3a-b809-1556db917f32', N'ngô xuân hoà', N'28rodina@rowdydow.com', N'sản phẩm chất lượng tốt', NULL, CAST(N'2025-03-08T19:21:29.9579888' AS DateTime2), CAST(N'2025-03-08T19:21:29.9579907' AS DateTime2), NULL, 0)
GO
INSERT [dbo].[Contact] ([Id], [Name], [Email], [Message], [CreateBy], [CreateDate], [ModifierDate], [ModifiedBy], [IsRead]) VALUES (N'4f1ea807-c361-461d-8433-1f0e9836d14b', N'Trần Thuỳ Dung', N'koyijid422@payposs.com', N'Sản phẩm chất lượng, shipper đẹp trai, shop nhiệt tình', NULL, CAST(N'2025-03-08T19:16:26.5498480' AS DateTime2), CAST(N'2025-03-08T19:16:26.5498504' AS DateTime2), NULL, 0)
GO
INSERT [dbo].[Contact] ([Id], [Name], [Email], [Message], [CreateBy], [CreateDate], [ModifierDate], [ModifiedBy], [IsRead]) VALUES (N'c3a06b57-8c99-4ac3-add8-b057487d91fd', N'tranhung', N'xuanhoa@wvfk5.onmicrosoft.com', N'skndjnd kfd', NULL, CAST(N'2025-03-08T19:27:33.7945695' AS DateTime2), CAST(N'2025-03-08T19:27:33.7945702' AS DateTime2), NULL, 0)
GO
INSERT [dbo].[New] ([Id], [Title], [Slug], [Description], [Detail], [Image], [CategoryId], [SeoTitile], [SeoDescription], [SeoKeywords], [IsActive], [CreateBy], [CreateDate], [ModifierDate], [ModifiedBy]) VALUES (N'19adef73-a406-4a39-8ed6-139cecaf9b8e', N'DỰ BÁO HỌA TIẾT THỜI TRANG SẼ HOT CHO MÙA HÈ 2025', N'du-bao-hoa-tiet-thoi-trang-se-hot-cho-mua-he-2025', N'Họa tiết luôn là một yếu tố quan trọng trong ngành thời trang, giúp tạo điểm nhấn và thể hiện cá tính. Mùa Hè 2025 dự báo sẽ bùng nổ với những họa tiết độc đáo, mang đậm phong cách hiện đại nhưng vẫn giữ được nét cổ điển. Hãy cùng khám phá những xu hướng họa tiết nổi bật nhất!', N'<header class="entry-header" style="color: rgb(45, 45, 45); font-family: sans-serif; font-size: 16px;"><div class="entry-image relative" style="position: relative !important;"><a href="https://360.com.vn/du-bao-hoa-tiet-thoi-trang-se-hot-cho-mua-he-2025/" style="background-color: transparent; touch-action: manipulation; color: rgb(109, 109, 109); font-family: SanFranciscoDisplayRegular !important;"></a></div></header><div class="entry-content single-page" style="padding-top: 1.5em; padding-bottom: 1.5em; color: rgb(45, 45, 45); font-family: sans-serif; font-size: 16px;"><p style="margin-bottom: 1.3em; font-family: SanFranciscoDisplayRegular !important;" class=""><span style="font-family: Arial; font-size: 1.7em; text-align: var(--bs-body-text-align);">1. Họa Tiết Hoa Nhiệt Đới – Rực Rỡ Và Tươi Mát</span></p><p style="margin-bottom: 1.3em; font-family: SanFranciscoDisplayRegular !important;"><span style="font-family: Arial;">Họa tiết hoa luôn là xu hướng không thể thiếu trong mùa hè. Năm 2025, các nhà thiết kế dự báo sự trở lại của:</span></p><ul data-spread="false" style="list-style-position: initial; list-style-image: initial; padding: 0px; margin-bottom: 1.3em;"><li style="margin-bottom: 0.6em; margin-left: 0px; font-family: SanFranciscoDisplayRegular !important;"><span style="font-weight: bolder; font-family: Arial;">Hoa lá nhiệt đới</span><span style="font-family: Arial;">&nbsp;với màu sắc rực rỡ như cam, vàng, xanh lá.</span></li><li style="margin-bottom: 0.6em; margin-left: 0px; font-family: SanFranciscoDisplayRegular !important;"><span style="font-weight: bolder; font-family: Arial;">Họa tiết hoa vintage</span><span style="font-family: Arial;">&nbsp;mang nét hoài cổ nhưng vẫn trẻ trung.</span></li><li style="margin-bottom: 0.6em; margin-left: 0px; font-family: SanFranciscoDisplayRegular !important;"><span style="font-weight: bolder; font-family: Arial;">Ứng dụng</span><span style="font-family: Arial;">: Áo sơ mi, Áo phông, Polo, quần short sẽ là những trang phục áp dụng họa tiết này nhiều nhất.</span></li></ul><p style="margin-bottom: 1.3em; font-family: SanFranciscoDisplayRegular !important;"><img decoding="async" class=" wp-image-87140 aligncenter" src="https://360.com.vn/wp-content/uploads/2024/04/STNTK510-QACTK408-1-SHNTK513-QACTK407-3.jpg" alt="Stntk510 Qactk408 1 Shntk513 Qactk407 (3)" width="910" height="1366" srcset="https://360.com.vn/wp-content/uploads/2024/04/STNTK510-QACTK408-1-SHNTK513-QACTK407-3.jpg 733w, https://360.com.vn/wp-content/uploads/2024/04/STNTK510-QACTK408-1-SHNTK513-QACTK407-3-510x765.jpg 510w" sizes="(max-width: 910px) 100vw, 910px" style="border-style: none; max-width: 100%; height: auto; display: block; transition: opacity 1s; opacity: 1; clear: both; margin: 0px auto;"></p><p style="margin-bottom: 1.3em; font-family: SanFranciscoDisplayRegular !important;"><span style="font-family: Arial;">&nbsp;</span></p><h1 style="color: rgb(45, 45, 45); width: 900px; margin-bottom: 0.5em; text-rendering: optimizespeed; font-size: 1.7em; line-height: 1.3; font-family: SanFranciscoDisplayBold !important;"><span style="font-family: Arial;">2. Họa Tiết Kẻ Sọc – Đơn Giản Nhưng Ấn Tượng</span></h1><p style="margin-bottom: 1.3em; font-family: SanFranciscoDisplayRegular !important;"><span style="font-family: Arial;">Kẻ sọc không bao giờ lỗi thời và sẽ tiếp tục lên ngôi trong mùa Hè 2025:</span></p><ul data-spread="false" style="list-style-position: initial; list-style-image: initial; padding: 0px; margin-bottom: 1.3em;"><li style="margin-bottom: 0.6em; margin-left: 0px; font-family: SanFranciscoDisplayRegular !important;"><span style="font-weight: bolder; font-family: Arial;">Kẻ sọc màu pastel</span><span style="font-family: Arial;">&nbsp;tạo cảm giác nhẹ nhàng, thanh lịch.</span></li><li style="margin-bottom: 0.6em; margin-left: 0px; font-family: SanFranciscoDisplayRegular !important;"><span style="font-weight: bolder; font-family: Arial;">Kẻ sọc lớn</span><span style="font-family: Arial;">&nbsp;mang lại phong cách hiện đại và cá tính.</span></li><li style="margin-bottom: 0.6em; margin-left: 0px; font-family: SanFranciscoDisplayRegular !important;"><span style="font-weight: bolder; font-family: Arial;">Ứng dụng</span><span style="font-family: Arial;">: Phù hợp với áo sơ mi, quần culottes.</span></li></ul><p style="margin-bottom: 1.3em; font-family: SanFranciscoDisplayRegular !important;"><img decoding="async" class=" wp-image-92714 aligncenter" src="https://360.com.vn/wp-content/uploads/2024/12/SKDTK518-QATTK408-1-6.jpg" alt="Skdtk518 Qattk408 1 (6)" width="929" height="1391" srcset="https://360.com.vn/wp-content/uploads/2024/12/SKDTK518-QATTK408-1-6.jpg 801w, https://360.com.vn/wp-content/uploads/2024/12/SKDTK518-QATTK408-1-6-768x1151.jpg 768w, https://360.com.vn/wp-content/uploads/2024/12/SKDTK518-QATTK408-1-6-510x764.jpg 510w" sizes="(max-width: 929px) 100vw, 929px" style="border-style: none; max-width: 100%; height: auto; display: block; transition: opacity 1s; opacity: 1; clear: both; margin: 0px auto;"></p><h1 style="color: rgb(45, 45, 45); width: 900px; margin-bottom: 0.5em; text-rendering: optimizespeed; font-size: 1.7em; line-height: 1.3; font-family: SanFranciscoDisplayBold !important;"><span style="font-family: Arial;">3. Họa Tiết Hình Học – Sự Pha Trộn Giữa Hiện Đại Và Nghệ Thuật</span></h1><p style="margin-bottom: 1.3em; font-family: SanFranciscoDisplayRegular !important;"><span style="font-family: Arial;">Họa tiết hình học sẽ trở thành xu hướng nhờ vào tính sáng tạo và khả năng biến tấu đa dạng:</span></p><ul data-spread="false" style="list-style-position: initial; list-style-image: initial; padding: 0px; margin-bottom: 1.3em;"><li style="margin-bottom: 0.6em; margin-left: 0px; font-family: SanFranciscoDisplayRegular !important;"><span style="font-weight: bolder; font-family: Arial;">Đường nét táo bạo</span><span style="font-family: Arial;">&nbsp;với các khối hình bất đối xứng.</span></li><li style="margin-bottom: 0.6em; margin-left: 0px; font-family: SanFranciscoDisplayRegular !important;"><span style="font-weight: bolder; font-family: Arial;">Màu sắc tương phản</span><span style="font-family: Arial;">&nbsp;giúp tạo hiệu ứng thị giác mạnh mẽ.</span></li><li style="margin-bottom: 0.6em; margin-left: 0px; font-family: SanFranciscoDisplayRegular !important;"><span style="font-weight: bolder; font-family: Arial;">Ứng dụng</span><span style="font-family: Arial;">: Áo khoác blazer, áo phông, áo polo,…</span></li></ul><p style="margin-bottom: 1.3em; font-family: SanFranciscoDisplayRegular !important;"><img decoding="async" class="size-full wp-image-90296 aligncenter" src="https://360.com.vn/wp-content/uploads/2024/07/POHTK516-4.jpg" alt="Pohtk516 (4)" width="960" height="1200" srcset="https://360.com.vn/wp-content/uploads/2024/07/POHTK516-4.jpg 960w, https://360.com.vn/wp-content/uploads/2024/07/POHTK516-4-768x960.jpg 768w, https://360.com.vn/wp-content/uploads/2024/07/POHTK516-4-510x638.jpg 510w" sizes="(max-width: 960px) 100vw, 960px" style="border-style: none; max-width: 100%; height: auto; display: block; transition: opacity 1s; opacity: 1; clear: both; margin: 0px auto; width: 900px;"></p><h1 style="color: rgb(45, 45, 45); width: 900px; margin-bottom: 0.5em; text-rendering: optimizespeed; font-size: 1.7em; line-height: 1.3; font-family: SanFranciscoDisplayBold !important;"><span style="font-family: Arial;">4. Họa Tiết Mini – Đơn Giản Và Tinh Tế</span></h1><p style="margin-bottom: 1.3em; font-family: SanFranciscoDisplayRegular !important;"><span style="font-family: Arial;">Những họa tiết lấy cảm hứng từ thiên nhiên được in nhỏ nhắn trên ngực, thân hoặc tay áo tạo điểm nhấn như:</span></p><ul data-spread="false" style="list-style-position: initial; list-style-image: initial; padding: 0px; margin-bottom: 1.3em;"><li style="margin-bottom: 0.6em; margin-left: 0px; font-family: SanFranciscoDisplayRegular !important;"><span style="font-weight: bolder; font-family: Arial;">Họa tiết da hình động vật</span></li><li style="margin-bottom: 0.6em; margin-left: 0px; font-family: SanFranciscoDisplayRegular !important;"><span style="font-weight: bolder; font-family: Arial;">Họa tiết graphic, chữ cái</span></li><li style="margin-bottom: 0.6em; margin-left: 0px; font-family: SanFranciscoDisplayRegular !important;"><span style="font-weight: bolder; font-family: Arial;">Ứng dụng</span><span style="font-family: Arial;">: áo phông, polo, sơ mi sẽ là những món đồ không thể thiếu.</span></li></ul><p style="margin-bottom: 1.3em; font-family: SanFranciscoDisplayRegular !important;"><img decoding="async" class=" wp-image-90163 aligncenter" src="https://360.com.vn/wp-content/uploads/2024/07/APHTK534-QJDTK501-2.jpg" alt="Aphtk534 Qjdtk501 (2)" width="950" height="1425" srcset="https://360.com.vn/wp-content/uploads/2024/07/APHTK534-QJDTK501-2.jpg 800w, https://360.com.vn/wp-content/uploads/2024/07/APHTK534-QJDTK501-2-768x1152.jpg 768w, https://360.com.vn/wp-content/uploads/2024/07/APHTK534-QJDTK501-2-510x765.jpg 510w" sizes="(max-width: 950px) 100vw, 950px" style="border-style: none; max-width: 100%; height: auto; display: block; transition: opacity 1s; opacity: 1; clear: both; margin: 0px auto;"></p><h1 style="color: rgb(45, 45, 45); width: 900px; margin-bottom: 0.5em; text-rendering: optimizespeed; font-size: 1.7em; line-height: 1.3; font-family: SanFranciscoDisplayBold !important;"><span style="font-family: Arial;">5. Họa Tiết Tie-dye – Phong Cách Phóng Khoáng</span></h1><p style="margin-bottom: 1.3em; font-family: SanFranciscoDisplayRegular !important;"><span style="font-family: Arial;">Tie-dye tiếp tục xuất hiện trong mùa Hè 2025 với nhiều cải tiến:</span></p><ul data-spread="false" style="list-style-position: initial; list-style-image: initial; padding: 0px; margin-bottom: 1.3em;"><li style="margin-bottom: 0.6em; margin-left: 0px; font-family: SanFranciscoDisplayRegular !important;"><span style="font-weight: bolder; font-family: Arial;">Tông màu pastel nhẹ nhàng</span><span style="font-family: Arial;">&nbsp;phù hợp với phong cách tối giản.</span></li><li style="margin-bottom: 0.6em; margin-left: 0px; font-family: SanFranciscoDisplayRegular !important;"><span style="font-weight: bolder; font-family: Arial;">Màu sắc rực rỡ</span><span style="font-family: Arial;">&nbsp;dành cho những ai yêu thích sự nổi bật.</span></li><li style="margin-bottom: 0.6em; margin-left: 0px; font-family: SanFranciscoDisplayRegular !important;"><span style="font-weight: bolder; font-family: Arial;">Ứng dụng</span><span style="font-family: Arial;">: Áo thun, quần short.</span></li></ul><h1 style="color: rgb(45, 45, 45); width: 900px; margin-bottom: 0.5em; text-rendering: optimizespeed; font-size: 1.7em; line-height: 1.3; font-family: SanFranciscoDisplayBold !important;"><span style="font-family: Arial;">6. Kết Luận</span></h1><p style="margin-bottom: 1.3em; font-family: SanFranciscoDisplayRegular !important;" class=""><span style="font-family: Arial;">Mùa Hè 2025 sẽ là sự bùng nổ của những họa tiết đa dạng, từ họa tiết nhiệt đới tươi mát đến hình khối cá tính hay tie-dye phá cách. Hãy cập nhật ngay những xu hướng này để luôn nổi bật và sành điệu!</span></p><p style="margin-bottom: 1.3em; font-family: SanFranciscoDisplayRegular !important;" class=""><span style="font-family: Arial;">Bạn yêu thích họa tiết nào nhất? Hãy chia sẻ và cùng đón chờ những bộ sưu tập thời trang đỉnh cao trong năm 2025.</span></p></div>', N'/Uploads/News/xh01_20250227_162848.jpg', N'c701a4cf-0e1d-42fd-9664-d76c072b1674', N'Dự Báo Họa Tiết Thời Trang Hot Nhất Mùa Hè 2025 – Xu Hướng Mới Nhất', N'Cập nhật xu hướng họa tiết thời trang hot nhất mùa hè 2025! Từ họa tiết tropical, gingham đến phong cách retro, khám phá ngay những xu hướng nổi bật.', N'họa tiết thời trang 2025, xu hướng thời trang hè 2025, họa tiết hot mùa hè, họa tiết tropical, họa tiết gingham, thời trang mùa hè, xu hướng thiết kế 2025', 1, N'Nguyễn Tùng Dương', CAST(N'2025-02-27T16:28:48.1008242' AS DateTime2), CAST(N'2025-02-27T16:28:48.1013527' AS DateTime2), NULL)
GO
INSERT [dbo].[New] ([Id], [Title], [Slug], [Description], [Detail], [Image], [CategoryId], [SeoTitile], [SeoDescription], [SeoKeywords], [IsActive], [CreateBy], [CreateDate], [ModifierDate], [ModifiedBy]) VALUES (N'8087ddab-028b-40bc-bf6a-324a3d1eda3b', N'4 TIPS PHỐI ĐỒ BIẾN SƠ MI THÀNH ÁO KHOÁC MỚI LẠ, BẢNH BAO', N'4-tips-phoi-o-bien-so-mi-thanh-ao-khoac-moi-la-banh-bao', N'Sơ mi nam không chỉ đơn thuần là item mặc bên trong, mà còn có thể biến tấu thành một chiếc áo khoác ngoài vô cùng phong cách. Với một chút sáng tạo, bạn hoàn toàn có thể tạo ra những set đồ mới lạ, độc đáo và cuốn hút. Cùng khám phá 4 tips phối đồ với Style áo sơ mi khoác ngoài nam cực chất dưới đây!', N'<blockquote style="width: 900px; margin-bottom: 0.5em; text-rendering: optimizespeed; line-height: 1.3;" class="blockquote"><span style="font-size: 24px; font-family: Arial;"><font color="#000000" style=""><b>4 kiểu áo sơ mi khoác ngoài nam phổ biến</b></font></span></blockquote><p style="width: 900px; margin-bottom: 0.5em; text-rendering: optimizespeed; font-size: 1.25em; font-weight: revert; font-family: SanFranciscoDisplayBold !important;" class=""><span style="font-weight: bolder; font-family: Arial; font-size: 18px;"><font color="#636363">1. Áo sơ mi khoác ngoài denim</font></span></p><p style="margin-bottom: 1.3em; color: rgb(45, 45, 45); font-size: 16px; font-family: SanFranciscoDisplayRegular !important;"><span style="font-weight: bolder;"><span style="font-weight: 400; font-family: Arial;">Áo sơ mi denim là một loại áo được làm từ chất liệu cotton cứng với kiểu dệt twill vô cùng chắc chắn.&nbsp;</span></span><span style="font-weight: bolder;"><span style="font-weight: 400; font-family: Arial;">Áo thường có màu xanh đặc trưng. Kiểu áo denim khoác ngoài này có kiểu dáng tương tự như áo sơ mi thông thường nhưng thường được thiết kế rộng rãi hơn. Nhờ đó, khi phối đồ sẽ tạo cảm giác thoải mái và phóng khoáng hơn.&nbsp;</span></span></p><p style="margin-bottom: 1.3em; color: rgb(45, 45, 45); font-size: 16px; font-family: SanFranciscoDisplayRegular !important;"><img decoding="async" class="size-full wp-image-91796 aligncenter" src="https://360.com.vn/wp-content/uploads/2024/10/1407cef90cfe9abd1e0342d050e613dc.jpg" alt="1407cef90cfe9abd1e0342d050e613dc" width="800" height="800" srcset="https://360.com.vn/wp-content/uploads/2024/10/1407cef90cfe9abd1e0342d050e613dc.jpg 800w, https://360.com.vn/wp-content/uploads/2024/10/1407cef90cfe9abd1e0342d050e613dc-768x768.jpg 768w, https://360.com.vn/wp-content/uploads/2024/10/1407cef90cfe9abd1e0342d050e613dc-510x510.jpg 510w, https://360.com.vn/wp-content/uploads/2024/10/1407cef90cfe9abd1e0342d050e613dc-100x100.jpg 100w" sizes="(max-width: 800px) 100vw, 800px" style="border-style: none; max-width: 100%; height: auto; display: block; transition: opacity 1s; opacity: 1; clear: both; margin: 0px auto; width: 900px;"></p><p style="width: 900px; margin-bottom: 0.5em; text-rendering: optimizespeed; font-size: 1.25em; font-weight: revert; font-family: SanFranciscoDisplayBold !important;" class=""><span style="font-weight: bolder; font-size: 18px; font-family: Arial;"><font color="#424242">2. Áo sơ mi khoác ngoài nhung tăm</font></span></p><p style="margin-bottom: 1.3em; color: rgb(45, 45, 45); font-size: 16px; font-family: SanFranciscoDisplayRegular !important;"><span style="font-weight: bolder;"><span style="font-weight: 400; font-family: Arial;">Áo sơ mi nhung tăm là một kiểu áo thời trang mà nam giới không thể thiếu trong tủ quần áo của mình. Được làm từ chất liệu nhung tăm mềm mại, mịn màng, mang lại cảm giác ấm áp và thoải mái, chiếc áo này sẽ đặc biệt phù hợp vào những ngày trời thu đông, khi không khí trở nên se lạnh.&nbsp;</span></span></p><p style="margin-bottom: 1.3em; color: rgb(45, 45, 45); font-size: 16px; font-family: SanFranciscoDisplayRegular !important;"><img decoding="async" class="size-full wp-image-91793 aligncenter" src="https://360.com.vn/wp-content/uploads/2024/10/rsz_1Anh_chInh-1-e1729493988539.jpg" alt="Rsz 1Ảnh ChÍnh (1)" width="976" height="518" srcset="https://360.com.vn/wp-content/uploads/2024/10/rsz_1Anh_chInh-1-e1729493988539.jpg 976w, https://360.com.vn/wp-content/uploads/2024/10/rsz_1Anh_chInh-1-e1729493988539-768x408.jpg 768w, https://360.com.vn/wp-content/uploads/2024/10/rsz_1Anh_chInh-1-e1729493988539-510x271.jpg 510w" sizes="(max-width: 976px) 100vw, 976px" style="border-style: none; max-width: 100%; height: auto; display: block; transition: opacity 1s; opacity: 1; clear: both; margin: 0px auto; width: 900px;"></p><p style="margin-bottom: 1.3em; color: rgb(45, 45, 45); font-size: 16px; font-family: SanFranciscoDisplayRegular !important;"><span style="font-family: Arial;">&nbsp;</span></p><p style="width: 900px; margin-bottom: 0.5em; text-rendering: optimizespeed; font-size: 1.25em; font-family: SanFranciscoDisplayBold !important;" class=""><span style="font-size: 18px; font-family: Arial;"><b style=""><font color="#424242">3. Áo sơ mi khoác ngoài màu trơn</font></b></span></p><p style="margin-bottom: 1.3em; color: rgb(45, 45, 45); font-size: 16px; font-family: SanFranciscoDisplayRegular !important;"><span style="font-weight: bolder;"><span style="font-weight: 400; font-family: Arial;">Áo sơ mi màu trơn có thiết kế tương tự như những chiếc áo thông thường. Áo có tay ngắn hoặc tay dài tùy vào thiết kế. Tuy nhiên, dáng áo khoác ngoài thường rộng rãi và thoải mái hơn, giúp bạn dễ dàng phối cùng các mẫu áo màu sắc bên trong.&nbsp;</span></span></p><p style="margin-bottom: 1.3em; color: rgb(45, 45, 45); font-size: 16px; font-family: SanFranciscoDisplayRegular !important;"><img decoding="async" class="size-full wp-image-91794 aligncenter" src="https://360.com.vn/wp-content/uploads/2024/10/rsz_1sdktk528_8_sdktk528_1-e1729494096501.jpg" alt="Rsz 1sdktk528 8 Sdktk528 1" width="1200" height="776" srcset="https://360.com.vn/wp-content/uploads/2024/10/rsz_1sdktk528_8_sdktk528_1-e1729494096501.jpg 1200w, https://360.com.vn/wp-content/uploads/2024/10/rsz_1sdktk528_8_sdktk528_1-e1729494096501-768x497.jpg 768w, https://360.com.vn/wp-content/uploads/2024/10/rsz_1sdktk528_8_sdktk528_1-e1729494096501-510x330.jpg 510w" sizes="(max-width: 1200px) 100vw, 1200px" style="border-style: none; max-width: 100%; height: auto; display: block; transition: opacity 1s; opacity: 1; clear: both; margin: 0px auto; width: 900px;"></p><p style="margin-bottom: 1.3em; color: rgb(45, 45, 45); font-size: 16px; font-family: SanFranciscoDisplayRegular !important;"><span style="font-family: Arial;">&nbsp;</span></p><p style="width: 900px; margin-bottom: 0.5em; text-rendering: optimizespeed; font-size: 1.25em; font-family: SanFranciscoDisplayBold !important;" class=""><b style=""><font color="#424242"><span style="font-family: Arial;">4. Áo sơ mi khoác ngoài nam họa tiết</span></font></b></p><p style="margin-bottom: 1.3em; color: rgb(45, 45, 45); font-size: 16px; font-family: SanFranciscoDisplayRegular !important;"><span style="font-weight: bolder;"><span style="font-weight: 400; font-family: Arial;">Áo sơ mi nam họa tiết khoác ngoài đang là một trong những xu hướng thời trang được giới trẻ yêu thích. Với sự đa dạng về kiểu dáng, màu sắc và họa tiết như kẻ sọc, hoa văn, họa tiết hình học,… item này không chỉ giúp bạn nổi bật và thu hút mọi ánh nhìn mà còn cực kì thời trang, giúp bạn thể hiện cá tính của riêng mình.&nbsp;</span></span></p><p style="margin-bottom: 1.3em; color: rgb(45, 45, 45); font-size: 16px; font-family: SanFranciscoDisplayRegular !important;"><img decoding="async" class="size-full wp-image-91795 aligncenter" src="https://360.com.vn/wp-content/uploads/2024/10/rsz_shntk513-e1729494324349.jpg" alt="Rsz Shntk513" width="1200" height="1034" srcset="https://360.com.vn/wp-content/uploads/2024/10/rsz_shntk513-e1729494324349.jpg 1200w, https://360.com.vn/wp-content/uploads/2024/10/rsz_shntk513-e1729494324349-768x662.jpg 768w, https://360.com.vn/wp-content/uploads/2024/10/rsz_shntk513-e1729494324349-510x439.jpg 510w" sizes="(max-width: 1200px) 100vw, 1200px" style="border-style: none; max-width: 100%; height: auto; display: block; transition: opacity 1s; opacity: 1; clear: both; margin: 0px auto; width: 900px;"></p><p style="color: rgb(45, 45, 45); width: 900px; margin-bottom: 0.5em; text-rendering: optimizespeed; font-size: 1.7em; line-height: 1.3; font-family: SanFranciscoDisplayBold !important;" class=""><span style="font-weight: bolder; font-family: Arial;">Cách phối áo sơ mi khoác ngoài nam đủ phong cách</span></p><p style="width: 900px; margin-bottom: 0.5em; text-rendering: optimizespeed; font-size: 1.25em; font-family: SanFranciscoDisplayBold !important;" class=""><span style="font-weight: bolder; font-family: Arial;"><font color="#424242">1. Sơ mi nhung tăm + quần jeans ống suông:</font></span></p><ul data-sourcepos="9:1-12:0" style="list-style-position: initial; list-style-image: initial; padding: 0px; margin-bottom: 1.3em; color: rgb(45, 45, 45); font-family: sans-serif; font-size: 16px;"><li data-sourcepos="9:1-9:168" style="margin-bottom: 0.6em; margin-left: 0px; font-family: SanFranciscoDisplayRegular !important;"><span style="font-weight: bolder; font-family: Arial;">Cách phối:</span><span style="font-family: Arial;">&nbsp;Chọn những chiếc sơ mi&nbsp; chất liệu nhung tăm mềm mại và có độ cứng cáp. Mặc sơ mi bên ngoài áo phông hoặc áo ba lỗ, kết hợp cùng quần jeans ống rộng.</span></li><li data-sourcepos="10:1-10:83" style="margin-bottom: 0.6em; margin-left: 0px; font-family: SanFranciscoDisplayRegular !important;"><span style="font-weight: bolder; font-family: Arial;">Gợi ý:</span><span style="font-family: Arial;">&nbsp;Để thêm phần cá tính, bạn có thể sơ vin một bên hoặc để thả tự nhiên.</span></li><li data-sourcepos="11:1-12:0" style="margin-bottom: 0.6em; margin-left: 0px; font-family: SanFranciscoDisplayRegular !important;"><span style="font-weight: bolder; font-family: Arial;">Điểm nhấn:</span><span style="font-family: Arial;">&nbsp;Thêm một chiếc túi tote hoặc kính mát để hoàn thiện phong cách.</span></li></ul><p style="margin-bottom: 1.3em; color: rgb(45, 45, 45); font-size: 16px; font-family: SanFranciscoDisplayRegular !important;"><img decoding="async" class="size-full wp-image-91292 aligncenter" src="https://360.com.vn/wp-content/uploads/2024/09/SHDTK525-APHTK512-QJDTK502-1.jpg" alt="Evoto" width="900" height="1200" srcset="https://360.com.vn/wp-content/uploads/2024/09/SHDTK525-APHTK512-QJDTK502-1.jpg 900w, https://360.com.vn/wp-content/uploads/2024/09/SHDTK525-APHTK512-QJDTK502-1-768x1024.jpg 768w, https://360.com.vn/wp-content/uploads/2024/09/SHDTK525-APHTK512-QJDTK502-1-510x680.jpg 510w" sizes="(max-width: 900px) 100vw, 900px" style="border-style: none; max-width: 100%; height: auto; display: block; transition: opacity 1s; opacity: 1; clear: both; margin: 0px auto; width: 900px;"></p><p style="width: 900px; margin-bottom: 0.5em; text-rendering: optimizespeed; font-size: 1.25em; font-family: SanFranciscoDisplayBold !important;" class=""><span style="font-weight: bolder; font-family: Arial;"><font color="#424242">2. Sơ mi kẻ caro + quần tây:</font></span></p><ul data-sourcepos="15:1-18:0" style="list-style-position: initial; list-style-image: initial; padding: 0px; margin-bottom: 1.3em; color: rgb(45, 45, 45); font-family: sans-serif; font-size: 16px;"><li data-sourcepos="15:1-15:173" style="margin-bottom: 0.6em; margin-left: 0px; font-family: SanFranciscoDisplayRegular !important;"><span style="font-weight: bolder; font-family: Arial;">Cách phối:</span><span style="font-family: Arial;">&nbsp;Sơ mi kẻ caro mang đến vẻ ngoài trẻ trung, năng động. Bạn có thể kết hợp với quần tây ống đứng hoặc ống côn để tạo nên một set đồ vừa lịch lãm vừa hiện đại.</span></li><li data-sourcepos="16:1-16:110" style="margin-bottom: 0.6em; margin-left: 0px; font-family: SanFranciscoDisplayRegular !important;"><span style="font-weight: bolder; font-family: Arial;">Gợi ý:</span><span style="font-family: Arial;">&nbsp;Để thêm phần ấn tượng, hãy chọn những chiếc áo sơ mi khoác ngoài với nhất liệu nhung tăm hoặc flanel có màu sắc nổi bật.</span></li><li data-sourcepos="17:1-18:0" style="margin-bottom: 0.6em; margin-left: 0px; font-family: SanFranciscoDisplayRegular !important;"><span style="font-weight: bolder; font-family: Arial;">Điểm nhấn:</span><span style="font-family: Arial;">&nbsp;Thêm một chiếc đồng hồ và giày da để hoàn thiện set đồ.</span></li></ul><p style="margin-bottom: 1.3em; color: rgb(45, 45, 45); font-size: 16px; font-family: SanFranciscoDisplayRegular !important;"><img decoding="async" class="size-full wp-image-91301 aligncenter" src="https://360.com.vn/wp-content/uploads/2024/09/SKDTK527-QASTK410-1.jpg" alt="sơ mi kẻ caro" width="900" height="1200" srcset="https://360.com.vn/wp-content/uploads/2024/09/SKDTK527-QASTK410-1.jpg 900w, https://360.com.vn/wp-content/uploads/2024/09/SKDTK527-QASTK410-1-768x1024.jpg 768w, https://360.com.vn/wp-content/uploads/2024/09/SKDTK527-QASTK410-1-510x680.jpg 510w" sizes="(max-width: 900px) 100vw, 900px" style="border-style: none; max-width: 100%; height: auto; display: block; transition: opacity 1s; opacity: 1; clear: both; margin: 0px auto; width: 900px;"></p><h3 data-sourcepos="19:1-19:36" style="color: rgb(45, 45, 45); width: 900px; margin-bottom: 0.5em; text-rendering: optimizespeed; font-size: 1.25em; font-family: SanFranciscoDisplayBold !important;"><span style="font-weight: bolder; font-family: Arial;">3. Sơ mi denim + quần jeans:</span></h3><ul data-sourcepos="21:1-24:0" style="list-style-position: initial; list-style-image: initial; padding: 0px; margin-bottom: 1.3em; color: rgb(45, 45, 45); font-family: sans-serif; font-size: 16px;"><li data-sourcepos="21:1-21:147" style="margin-bottom: 0.6em; margin-left: 0px; font-family: SanFranciscoDisplayRegular !important;"><span style="font-weight: bolder; font-family: Arial;">Cách phối:</span><span style="font-family: Arial;">&nbsp;Sơ mi denim mang đến vẻ ngoài bụi bặm, cá tính. Kết hợp với quần jeans và giày sneaker, bạn sẽ có một set đồ thoải mái, năng động.</span></li><li data-sourcepos="22:1-22:97" style="margin-bottom: 0.6em; margin-left: 0px; font-family: SanFranciscoDisplayRegular !important;"><span style="font-weight: bolder; font-family: Arial;">Gợi ý:</span><span style="font-family: Arial;">&nbsp;Để thêm phần nổi bật, bạn có thể chọn những chiếc sơ mi denim có rách hoặc phối màu.</span></li><li data-sourcepos="23:1-24:0" style="margin-bottom: 0.6em; margin-left: 0px; font-family: SanFranciscoDisplayRegular !important;"><span style="font-weight: bolder; font-family: Arial;">Điểm nhấn:</span><span style="font-family: Arial;">&nbsp;Thêm một chiếc túi đeo chéo hoặc một chiếc mũ beanie để hoàn thiện phong cách.</span></li></ul><p style="margin-bottom: 1.3em; color: rgb(45, 45, 45); font-size: 16px; font-family: SanFranciscoDisplayRegular !important;"><img decoding="async" class="size-full wp-image-91798 aligncenter" src="https://360.com.vn/wp-content/uploads/2024/10/rsz_462908026_2985962504900606_4800716454636956946_n-e1729495507492.jpg" alt="Rsz 462908026 2985962504900606 4800716454636956946 N" width="885" height="740" srcset="https://360.com.vn/wp-content/uploads/2024/10/rsz_462908026_2985962504900606_4800716454636956946_n-e1729495507492.jpg 885w, https://360.com.vn/wp-content/uploads/2024/10/rsz_462908026_2985962504900606_4800716454636956946_n-e1729495507492-768x642.jpg 768w, https://360.com.vn/wp-content/uploads/2024/10/rsz_462908026_2985962504900606_4800716454636956946_n-e1729495507492-510x426.jpg 510w" sizes="(max-width: 885px) 100vw, 885px" style="border-style: none; max-width: 100%; height: auto; display: block; transition: opacity 1s; opacity: 1; clear: both; margin: 0px auto; width: 900px;"></p><p style="margin-bottom: 1.3em; color: rgb(45, 45, 45); font-size: 16px; font-family: SanFranciscoDisplayRegular !important;"><span style="font-family: Arial;">&nbsp;</span></p><h3 data-sourcepos="25:1-25:41" style="color: rgb(45, 45, 45); width: 900px; margin-bottom: 0.5em; text-rendering: optimizespeed; font-size: 1.25em; font-family: SanFranciscoDisplayBold !important;"><span style="font-weight: bolder; font-family: Arial;">4. Sơ mi ngắn tay họa tiết + quần kaki/quần âu</span></h3><ul data-sourcepos="27:1-30:0" style="list-style-position: initial; list-style-image: initial; padding: 0px; margin-bottom: 1.3em; color: rgb(45, 45, 45); font-family: sans-serif; font-size: 16px;"><li data-sourcepos="27:1-27:158" style="margin-bottom: 0.6em; margin-left: 0px; font-family: SanFranciscoDisplayRegular !important;"><span style="font-weight: bolder; font-family: Arial;">Cách phối:</span><span style="font-family: Arial;">&nbsp;Áo sơ mi họa tiết mang đến vẻ ngoài phong khoáng, bảnh bao.&nbsp;</span><span style="font-weight: bolder;"><span style="font-weight: 400; font-family: Arial;">Khi kết hợp cùng quần đơn sắc giúp bạn hoàn thiện vẻ ngoài trẻ trung và đậm nét cá tính riêng.</span></span></li><li data-sourcepos="28:1-28:81" style="margin-bottom: 0.6em; margin-left: 0px; font-family: SanFranciscoDisplayRegular !important;"><span style="font-weight: bolder; font-family: Arial;">Gợi ý:</span><span style="font-family: Arial;">&nbsp;Chọn những chiếc sơ mi có với màu sắc nổi bật, họa tiết to để tạo điểm nhấn.</span></li><li data-sourcepos="29:1-30:0" style="margin-bottom: 0.6em; margin-left: 0px; font-family: SanFranciscoDisplayRegular !important;"><span style="font-weight: bolder; font-family: Arial;">Điểm nhấn:</span><span style="font-family: Arial;">&nbsp;Thêm kính râm, túi, giày hoặc sandal</span></li></ul><p style="margin-bottom: 1.3em; color: rgb(45, 45, 45); font-size: 16px; font-family: SanFranciscoDisplayRegular !important;"><img decoding="async" class=" wp-image-91797 aligncenter" src="https://360.com.vn/wp-content/uploads/2024/10/rsz_shntk513-qacol515_4-scaled.jpg" alt="Rsz Shntk513 Qacol515 4" width="895" height="1342" srcset="https://360.com.vn/wp-content/uploads/2024/10/rsz_shntk513-qacol515_4-scaled.jpg 1707w, https://360.com.vn/wp-content/uploads/2024/10/rsz_shntk513-qacol515_4-768x1152.jpg 768w, https://360.com.vn/wp-content/uploads/2024/10/rsz_shntk513-qacol515_4-1024x1536.jpg 1024w, https://360.com.vn/wp-content/uploads/2024/10/rsz_shntk513-qacol515_4-1365x2048.jpg 1365w, https://360.com.vn/wp-content/uploads/2024/10/rsz_shntk513-qacol515_4-510x765.jpg 510w" sizes="(max-width: 895px) 100vw, 895px" style="border-style: none; max-width: 100%; height: auto; display: block; transition: opacity 1s; opacity: 1; clear: both; margin: 0px auto;"></p><h1 data-sourcepos="31:1-31:51" style="color: rgb(45, 45, 45); width: 900px; margin-bottom: 0.5em; text-rendering: optimizespeed; font-size: 1.7em; line-height: 1.3; font-family: SanFranciscoDisplayBold !important;"><span style="font-weight: bolder; font-family: Arial;">Lưu ý khi phối đồ với áo sơ mi khoác ngoài:</span></h1><ul data-sourcepos="33:1-37:0" style="list-style-position: initial; list-style-image: initial; padding: 0px; margin-bottom: 1.3em; color: rgb(45, 45, 45); font-family: sans-serif; font-size: 16px;"><li data-sourcepos="33:1-33:123" style="margin-bottom: 0.6em; margin-left: 0px; font-family: SanFranciscoDisplayRegular !important;"><span style="font-weight: bolder; font-family: Arial;">Chọn size áo phù hợp:</span><span style="font-family: Arial;">&nbsp;Áo sơ mi khoác ngoài nên có độ rộng vừa phải, không quá bó sát cũng không quá rộng thùng thình.</span></li><li data-sourcepos="34:1-34:133" style="margin-bottom: 0.6em; margin-left: 0px; font-family: SanFranciscoDisplayRegular !important;"><span style="font-weight: bolder; font-family: Arial;">Chọn chất liệu phù hợp:</span><span style="font-family: Arial;">&nbsp;Ưu tiên chọn những chiếc áo sơ mi nam hàng hiệu được làm từ chất liệu chuẩn form, dễ chịu.</span></li><li data-sourcepos="35:1-35:96" style="margin-bottom: 0.6em; margin-left: 0px; font-family: SanFranciscoDisplayRegular !important;"><span style="font-weight: bolder; font-family: Arial;">Phối màu hài hòa:</span><span style="font-family: Arial;">&nbsp;Kết hợp các màu sắc sao cho hài hòa, tránh những sự kết hợp quá đối lập.</span></li><li data-sourcepos="36:1-37:0" style="margin-bottom: 0.6em; margin-left: 0px; font-family: SanFranciscoDisplayRegular !important;"><span style="font-weight: bolder; font-family: Arial;">Phụ kiện:</span><span style="font-family: Arial;">&nbsp;Sử dụng các phụ kiện như mũ, kính, túi xách để tạo điểm nhấn cho set đồ.</span></li></ul>', N'/Uploads/News/sm01_20250227_024419.jpg', N'0c8dacbd-53f5-43ba-859b-a457154dcc13', N'Xu Hướng T-Shirts Xuân Hè 2025 Cho Nam Giới - Phong Cách Mới Nhất', N'Khám phá 4 cách phối đồ độc đáo giúp biến chiếc áo sơ mi thành áo khoác phong cách. Mẹo mix đồ mới lạ, nam tính & sành điệu dành cho bạn!', N'phối đồ sơ mi, sơ mi thành áo khoác, cách phối đồ nam, mix đồ sơ mi, thời trang nam 2025, áo khoác sơ mi, mẹo phối đồ, xu hướng thời trang', 1, N'Nguyễn Tùng Dương', CAST(N'2025-02-27T02:44:19.4962874' AS DateTime2), CAST(N'2025-02-27T02:44:19.4963190' AS DateTime2), NULL)
GO
INSERT [dbo].[New] ([Id], [Title], [Slug], [Description], [Detail], [Image], [CategoryId], [SeoTitile], [SeoDescription], [SeoKeywords], [IsActive], [CreateBy], [CreateDate], [ModifierDate], [ModifiedBy]) VALUES (N'c7bbd37b-a4b8-4281-a87a-bd7765d3cd39', N'XU HƯỚNG T-SHIRTS XUÂN HÈ 2025 DÀNH CHO NAM GIỚI', N'xu-huong-t-shirts-xuan-he-2025-danh-cho-nam-gioi', N'T-shirt – item thời trang cơ bản nhưng không bao giờ lỗi mốt, tiếp tục khẳng định vị thế của mình trong tủ đồ của phái mạnh. Vậy xu hướng áo phông 2025 dành cho nam giới sẽ có những thay đổi gì? Hãy cùng 360 khám phá những dự đoán và phân tích chi tiết dưới đây để luôn bắt kịp xu hướng thời trang mới nhất.', N'<h1 data-sourcepos="5:1-5:35" style="color: rgb(45, 45, 45); width: 900px; margin-bottom: 0.5em; text-rendering: optimizespeed; font-size: 1.7em; line-height: 1.3; font-family: SanFranciscoDisplayBold !important;"><span style="font-weight: bolder; font-family: Arial; font-size: 20px;">1. Chất Liệu Bền Vững Lên Ngôi:</span></h1><p data-sourcepos="7:1-7:132" style="margin-bottom: 1.3em; color: rgb(45, 45, 45); font-size: 16px; font-family: SanFranciscoDisplayRegular !important;"><span style="font-family: Arial;">Năm 2025, xu hướng thời trang bền vững tiếp tục được đề cao. Điều này thể hiện rõ nét trong việc lựa chọn chất liệu cho áo phông:</span></p><ul data-sourcepos="9:1-12:0" style="list-style-position: initial; list-style-image: initial; padding: 0px; margin-bottom: 1.3em; color: rgb(45, 45, 45); font-family: sans-serif; font-size: 16px;"><li data-sourcepos="9:1-9:149" style="margin-bottom: 0.6em; margin-left: 0px; font-family: SanFranciscoDisplayRegular !important;"><span style="font-weight: bolder; font-family: Arial;">Cotton hữu cơ:</span><span style="font-family: Arial;">&nbsp;Được trồng trọt và sản xuất theo quy trình thân thiện với môi trường, cotton hữu cơ mềm mại, thoáng mát và an toàn cho làn da.</span></li></ul><p style="margin-bottom: 1.3em; color: rgb(45, 45, 45); font-size: 16px; font-family: SanFranciscoDisplayRegular !important;"><img decoding="async" class="aligncenter wp-image-91110 size-full" src="https://360.com.vn/wp-content/uploads/2024/09/APHTK533-QJDTK501-1-e1735552495681.jpg" alt="Aphtk533 Qjdtk501 (1)" width="800" height="790" srcset="https://360.com.vn/wp-content/uploads/2024/09/APHTK533-QJDTK501-1-e1735552495681.jpg 800w, https://360.com.vn/wp-content/uploads/2024/09/APHTK533-QJDTK501-1-e1735552495681-768x758.jpg 768w, https://360.com.vn/wp-content/uploads/2024/09/APHTK533-QJDTK501-1-e1735552495681-510x504.jpg 510w, https://360.com.vn/wp-content/uploads/2024/09/APHTK533-QJDTK501-1-e1735552495681-100x100.jpg 100w" sizes="(max-width: 800px) 100vw, 800px" style="border-style: none; max-width: 100%; height: 711.827px; display: block; transition: opacity 1s; opacity: 1; clear: both; margin: 0px auto; width: 720.837px;"></p><ul data-sourcepos="9:1-12:0" style="list-style-position: initial; list-style-image: initial; padding: 0px; margin-bottom: 1.3em; color: rgb(45, 45, 45); font-family: sans-serif; font-size: 16px;"><li data-sourcepos="10:1-10:103" style="margin-bottom: 0.6em; margin-left: 0px; font-family: SanFranciscoDisplayRegular !important;"><span style="font-weight: bolder; font-family: Arial;">Vải tái chế:</span><span style="font-family: Arial;">&nbsp;Sử dụng sợi tái chế từ chai nhựa, vải vụn giúp giảm thiểu tác động đến môi trường.</span></li><li data-sourcepos="11:1-12:0" style="margin-bottom: 0.6em; margin-left: 0px; font-family: SanFranciscoDisplayRegular !important;"><span style="font-weight: bolder; font-family: Arial;">Vải pha trộn tự nhiên:</span><span style="font-family: Arial;">&nbsp;Sự kết hợp giữa cotton với các loại sợi tự nhiên khác như tre, lanh… mang đến những trải nghiệm mới về độ mềm mại, thoáng khí và khả năng kháng khuẩn.</span></li></ul><p style="margin-bottom: 1.3em; color: rgb(45, 45, 45); font-size: 16px; font-family: SanFranciscoDisplayRegular !important;"><img decoding="async" class=" wp-image-93431 aligncenter" src="https://360.com.vn/wp-content/uploads/2024/12/rsz_aphtk458-qjdtk418_1-scaled.jpg" alt="Rsz Aphtk458 Qjdtk418 1" width="792" height="1189" srcset="https://360.com.vn/wp-content/uploads/2024/12/rsz_aphtk458-qjdtk418_1-scaled.jpg 1706w, https://360.com.vn/wp-content/uploads/2024/12/rsz_aphtk458-qjdtk418_1-768x1152.jpg 768w, https://360.com.vn/wp-content/uploads/2024/12/rsz_aphtk458-qjdtk418_1-1024x1536.jpg 1024w, https://360.com.vn/wp-content/uploads/2024/12/rsz_aphtk458-qjdtk418_1-1365x2048.jpg 1365w, https://360.com.vn/wp-content/uploads/2024/12/rsz_aphtk458-qjdtk418_1-510x765.jpg 510w" sizes="(max-width: 792px) 100vw, 792px" style="border-style: none; max-width: 100%; height: 1081.36px; display: block; transition: opacity 1s; opacity: 1; clear: both; margin: 0px auto; width: 720.908px;"></p><h1 data-sourcepos="15:1-15:45" style="color: rgb(45, 45, 45); width: 900px; margin-bottom: 0.5em; text-rendering: optimizespeed; font-size: 1.7em; line-height: 1.3; font-family: SanFranciscoDisplayBold !important;"><span style="font-weight: bolder; font-family: Arial; font-size: 20px;">2. Kiểu Dáng Tối Giản Vẫn Được Ưa Chuộng:</span></h1><p data-sourcepos="17:1-17:178" style="margin-bottom: 1.3em; color: rgb(45, 45, 45); font-size: 16px; font-family: SanFranciscoDisplayRegular !important;"><span style="font-family: Arial;">Phong cách tối giản (minimalism) tiếp tục thống trị làng thời trang. Áo t-shirts với thiết kế đơn giản, ít họa tiết, tập trung vào form dáng và chất liệu sẽ là lựa chọn hàng đầu.</span></p><ul data-sourcepos="19:1-22:0" style="list-style-position: initial; list-style-image: initial; padding: 0px; margin-bottom: 1.3em; color: rgb(45, 45, 45); font-family: sans-serif; font-size: 16px;"><li data-sourcepos="19:1-19:101" style="margin-bottom: 0.6em; margin-left: 0px; font-family: SanFranciscoDisplayRegular !important;"><span style="font-weight: bolder; font-family: Arial;">Áo t-shirt trơn basic:</span><span style="font-family: Arial;">&nbsp;Màu trắng, đen, xám, navy… vẫn là những gam màu kinh điển, dễ phối đồ.</span></li></ul><p style="margin-bottom: 1.3em; color: rgb(45, 45, 45); font-size: 16px; font-family: SanFranciscoDisplayRegular !important;"><img decoding="async" class=" wp-image-86241 aligncenter" src="https://360.com.vn/wp-content/uploads/2024/03/APTTK504-QSNTK507-8.jpg" alt="Apttk504 Qsntk507 (8)" width="786" height="982" style="border-style: none; max-width: 100%; height: 900.275px; display: block; transition: opacity 1s; opacity: 1; clear: both; margin: 0px auto; width: 720.22px;"></p><ul data-sourcepos="19:1-22:0" style="list-style-position: initial; list-style-image: initial; padding: 0px; margin-bottom: 1.3em; color: rgb(45, 45, 45); font-family: sans-serif; font-size: 16px;"><li data-sourcepos="20:1-20:108" style="margin-bottom: 0.6em; margin-left: 0px; font-family: SanFranciscoDisplayRegular !important;"><span style="font-weight: bolder; font-family: Arial;">Áo t-shirt cổ tròn/cổ tim:</span><span style="font-family: Arial;">&nbsp;Kiểu dáng quen thuộc nhưng được biến tấu với những đường cắt may tinh tế.</span></li><li data-sourcepos="21:1-22:0" style="margin-bottom: 0.6em; margin-left: 0px; font-family: SanFranciscoDisplayRegular !important;"><span style="font-weight: bolder; font-family: Arial;">Áo t-shirt oversize:</span><span style="font-family: Arial;">&nbsp;Mang đến sự thoải mái, phóng khoáng và phù hợp với phong cách đường phố (streetwear).</span></li></ul><p style="margin-bottom: 1.3em; color: rgb(45, 45, 45); font-size: 16px; font-family: SanFranciscoDisplayRegular !important;"><img decoding="async" class=" wp-image-74054 aligncenter" src="https://360.com.vn/wp-content/uploads/2023/02/APHTK407-QJDTK309-3.png" alt="Aphtk407 Qjdtk309 3.png" width="747" height="933" style="border-style: none; max-width: 100%; height: 899.819px; display: block; transition: opacity 1s; opacity: 1; clear: both; margin: 0px auto; width: 720.337px;"></p><h1 data-sourcepos="25:1-25:41" style="color: rgb(45, 45, 45); width: 900px; margin-bottom: 0.5em; text-rendering: optimizespeed; font-size: 1.7em; line-height: 1.3; font-family: SanFranciscoDisplayBold !important;"><span style="font-weight: bolder; font-family: Arial;">3. Họa Tiết Đồ Họa và In Ấn Sáng Tạo:</span></h1><p data-sourcepos="27:1-27:115" style="margin-bottom: 1.3em; color: rgb(45, 45, 45); font-size: 16px; font-family: SanFranciscoDisplayRegular !important;"><span style="font-family: Arial;">Bên cạnh sự tối giản, những họa tiết đồ họa và in ấn sáng tạo cũng sẽ là điểm nhấn trong xu hướng áo t-shirts 2025.</span></p><ul data-sourcepos="29:1-32:0" style="list-style-position: initial; list-style-image: initial; padding: 0px; margin-bottom: 1.3em; color: rgb(45, 45, 45); font-family: sans-serif; font-size: 16px;"><li data-sourcepos="29:1-29:97" style="margin-bottom: 0.6em; margin-left: 0px; font-family: SanFranciscoDisplayRegular !important;"><span style="font-weight: bolder; font-family: Arial;">Họa tiết trừu tượng:</span><span style="font-family: Arial;">&nbsp;Những hình khối, đường nét ngẫu hứng mang đến sự độc đáo và cá tính.</span></li></ul><p style="margin-bottom: 1.3em; color: rgb(45, 45, 45); font-size: 16px; font-family: SanFranciscoDisplayRegular !important;"><img decoding="async" class=" wp-image-87105 aligncenter" src="https://360.com.vn/wp-content/uploads/2024/04/APHTK518-QSNTK506-2.jpg" alt="Aphtk518 Qsntk506 (2)" width="753" height="502" srcset="https://360.com.vn/wp-content/uploads/2024/04/APHTK518-QSNTK506-2.jpg 1100w, https://360.com.vn/wp-content/uploads/2024/04/APHTK518-QSNTK506-2-768x512.jpg 768w, https://360.com.vn/wp-content/uploads/2024/04/APHTK518-QSNTK506-2-510x340.jpg 510w" sizes="(max-width: 753px) 100vw, 753px" style="border-style: none; max-width: 100%; height: auto; display: block; transition: opacity 1s; opacity: 1; clear: both; margin: 0px auto;"></p><ul data-sourcepos="29:1-32:0" style="list-style-position: initial; list-style-image: initial; padding: 0px; margin-bottom: 1.3em; color: rgb(45, 45, 45); font-family: sans-serif; font-size: 16px;"><li data-sourcepos="30:1-30:111" style="margin-bottom: 0.6em; margin-left: 0px; font-family: SanFranciscoDisplayRegular !important;"><span style="font-weight: bolder; font-family: Arial;">In typography:</span><span style="font-family: Arial;">&nbsp;Sử dụng những câu slogan, thông điệp ý nghĩa hoặc tên thương hiệu được thiết kế tinh tế.</span></li></ul><p style="margin-bottom: 1.3em; color: rgb(45, 45, 45); font-size: 16px; font-family: SanFranciscoDisplayRegular !important;"><img decoding="async" class=" wp-image-88532 aligncenter" src="https://360.com.vn/wp-content/uploads/2024/05/APHTK520-QSKTK512-3.jpg" alt="Aphtk520 Qsktk512 (3)" width="748" height="1122" srcset="https://360.com.vn/wp-content/uploads/2024/05/APHTK520-QSKTK512-3.jpg 733w, https://360.com.vn/wp-content/uploads/2024/05/APHTK520-QSKTK512-3-510x765.jpg 510w" sizes="(max-width: 748px) 100vw, 748px" style="border-style: none; max-width: 100%; height: 1080.25px; display: block; transition: opacity 1s; opacity: 1; clear: both; margin: 0px auto; width: 719.837px;"></p><ul data-sourcepos="29:1-32:0" style="list-style-position: initial; list-style-image: initial; padding: 0px; margin-bottom: 1.3em; color: rgb(45, 45, 45); font-family: sans-serif; font-size: 16px;"><li data-sourcepos="31:1-32:0" style="margin-bottom: 0.6em; margin-left: 0px; font-family: SanFranciscoDisplayRegular !important;"><span style="font-weight: bolder; font-family: Arial;">In hình ảnh:</span><span style="font-family: Arial;">&nbsp;Hình ảnh thiên nhiên, động vật, hoặc các tác phẩm nghệ thuật được in với công nghệ hiện đại, sắc nét.</span></li></ul><p style="margin-bottom: 1.3em; color: rgb(45, 45, 45); font-size: 16px; font-family: SanFranciscoDisplayRegular !important;"><img decoding="async" class="wp-image-93432 aligncenter" src="https://360.com.vn/wp-content/uploads/2024/12/rsz_aphtk414_1.jpg" alt="Rsz Aphtk414 1" width="749" height="749" srcset="https://360.com.vn/wp-content/uploads/2024/12/rsz_aphtk414_1.jpg 2240w, https://360.com.vn/wp-content/uploads/2024/12/rsz_aphtk414_1-768x768.jpg 768w, https://360.com.vn/wp-content/uploads/2024/12/rsz_aphtk414_1-1536x1536.jpg 1536w, https://360.com.vn/wp-content/uploads/2024/12/rsz_aphtk414_1-2048x2048.jpg 2048w, https://360.com.vn/wp-content/uploads/2024/12/rsz_aphtk414_1-510x510.jpg 510w, https://360.com.vn/wp-content/uploads/2024/12/rsz_aphtk414_1-100x100.jpg 100w" sizes="(max-width: 749px) 100vw, 749px" style="border-style: none; max-width: 100%; height: 719.337px; display: block; transition: opacity 1s; opacity: 1; clear: both; margin: 0px auto; width: 719.337px;"></p><h1 data-sourcepos="35:1-35:35" style="color: rgb(45, 45, 45); width: 900px; margin-bottom: 0.5em; text-rendering: optimizespeed; font-size: 1.7em; line-height: 1.3; font-family: SanFranciscoDisplayBold !important;"><span style="font-weight: bolder; font-family: Arial; font-size: 20px;">4. Màu Sắc Tươi Mới và Đa Dạng:</span></h1><p data-sourcepos="37:1-37:148" style="margin-bottom: 1.3em; color: rgb(45, 45, 45); font-size: 16px; font-family: SanFranciscoDisplayRegular !important;"><span style="font-family: Arial;">Bảng màu của áo t-shirts nam 2025 sẽ trở nên phong phú hơn với sự xuất hiện của những gam màu tươi mới bên cạnh những màu sắc trung tính quen thuộc.</span></p><ul data-sourcepos="39:1-42:0" style="list-style-position: initial; list-style-image: initial; padding: 0px; margin-bottom: 1.3em; color: rgb(45, 45, 45); font-family: sans-serif; font-size: 16px;"><li data-sourcepos="39:1-39:51" style="margin-bottom: 0.6em; margin-left: 0px; font-family: SanFranciscoDisplayRegular !important;"><span style="font-weight: bolder; font-family: Arial;">Màu pastel:</span><span style="font-family: Arial;">&nbsp;Mang đến sự nhẹ nhàng, tinh tế.</span></li></ul><p style="margin-bottom: 1.3em; color: rgb(45, 45, 45); font-size: 16px; font-family: SanFranciscoDisplayRegular !important;"><img decoding="async" class=" wp-image-86176 aligncenter" src="https://360.com.vn/wp-content/uploads/2024/03/APHTK502-QSNTK506-8.jpg" alt="Aphtk502 Qsntk506 (8)" width="759" height="949" style="border-style: none; max-width: 100%; height: 900.422px; display: block; transition: opacity 1s; opacity: 1; clear: both; margin: 0px auto; width: 720.337px;"></p><ul data-sourcepos="39:1-42:0" style="list-style-position: initial; list-style-image: initial; padding: 0px; margin-bottom: 1.3em; color: rgb(45, 45, 45); font-family: sans-serif; font-size: 16px;"><li data-sourcepos="40:1-40:56" style="margin-bottom: 0.6em; margin-left: 0px; font-family: SanFranciscoDisplayRegular !important;"><span style="font-weight: bolder; font-family: Arial;">Màu đất:</span><span style="font-family: Arial;">&nbsp;Gam màu ấm áp, gần gũi với thiên nhiên.</span></li></ul><p style="margin-bottom: 1.3em; color: rgb(45, 45, 45); font-size: 16px; font-family: SanFranciscoDisplayRegular !important;"><img decoding="async" class=" wp-image-90350 aligncenter" src="https://360.com.vn/wp-content/uploads/2024/07/APHTK528-2.jpg" alt="Aphtk528 (2)" width="750" height="938" style="border-style: none; max-width: 100%; height: 900.938px; display: block; transition: opacity 1s; opacity: 1; clear: both; margin: 0px auto; width: 720.75px;"></p><ul data-sourcepos="39:1-42:0" style="list-style-position: initial; list-style-image: initial; padding: 0px; margin-bottom: 1.3em; color: rgb(45, 45, 45); font-family: sans-serif; font-size: 16px;"><li data-sourcepos="41:1-42:0" style="margin-bottom: 0.6em; margin-left: 0px; font-family: SanFranciscoDisplayRegular !important;"><span style="font-weight: bolder; font-family: Arial;">Màu neon:</span><span style="font-family: Arial;">&nbsp;Tạo điểm nhấn nổi bật, cá tính.</span></li></ul><h1 data-sourcepos="45:1-45:36" style="color: rgb(45, 45, 45); width: 900px; margin-bottom: 0.5em; text-rendering: optimizespeed; font-size: 1.7em; line-height: 1.3; font-family: SanFranciscoDisplayBold !important;"><span style="font-weight: bolder; font-family: Arial; font-size: 20px;">5. Ứng Dụng Công Nghệ Tiên Tiến:</span></h1><p data-sourcepos="47:1-47:102" style="margin-bottom: 1.3em; color: rgb(45, 45, 45); font-size: 16px; font-family: SanFranciscoDisplayRegular !important;"><span style="font-family: Arial;">Công nghệ ngày càng được ứng dụng rộng rãi trong ngành thời trang, và áo t-shirts cũng không ngoại lệ.</span></p><ul data-sourcepos="49:1-51:0" style="list-style-position: initial; list-style-image: initial; padding: 0px; margin-bottom: 1.3em; color: rgb(45, 45, 45); font-family: sans-serif; font-size: 16px;"><li data-sourcepos="49:1-49:94" style="margin-bottom: 0.6em; margin-left: 0px; font-family: SanFranciscoDisplayRegular !important;"><span style="font-weight: bolder; font-family: Arial;">Vải chống nhăn, khử mùi, kháng khuẩn:</span><span style="font-family: Arial;">&nbsp;Mang đến sự tiện lợi và thoải mái cho người mặc.</span></li><li data-sourcepos="50:1-51:0" style="margin-bottom: 0.6em; margin-left: 0px; font-family: SanFranciscoDisplayRegular !important;"><span style="font-weight: bolder; font-family: Arial;">Công nghệ in 3D:</span><span style="font-family: Arial;">&nbsp;Tạo ra những hình in sống động, chân thực.</span></li></ul><h1 data-sourcepos="54:1-54:13" style="color: rgb(45, 45, 45); width: 900px; margin-bottom: 0.5em; text-rendering: optimizespeed; font-size: 1.7em; line-height: 1.3; font-family: SanFranciscoDisplayBold !important;"><span style="font-weight: bolder; font-family: Arial; font-size: 20px;">Kết Luận:</span></h1><p data-sourcepos="56:1-56:317" style="margin-bottom: 1.3em; color: rgb(45, 45, 45); font-size: 16px; font-family: SanFranciscoDisplayRegular !important;"><span style="font-family: Arial;">Xu hướng áo t-shirts 2025 dành cho nam giới tập trung vào sự bền vững, tối giản nhưng vẫn không thiếu những điểm nhấn sáng tạo. Việc lựa chọn chất liệu, kiểu dáng, họa tiết và màu sắc phù hợp sẽ giúp bạn thể hiện phong cách cá nhân một cách hoàn hảo. Hãy cập nhật những xu hướng mới nhất để luôn tự tin và thời trang.</span></p>', N'/Uploads/News/ts01_20250226_225217.jpg', N'c701a4cf-0e1d-42fd-9664-d76c072b1674', N'Xu Hướng T-Shirts Xuân Hè 2025 Cho Nam Giới - Phong Cách Mới Nhất', N'Khám phá xu hướng T-Shirts nam Xuân Hè 2025 với những thiết kế hiện đại, chất liệu thoáng mát và phong cách đa dạng. Cập nhật ngay để dẫn đầu xu hướng thời trang nam!', N'xu hướng t-shirts nam 2025, áo thun nam xuân hè 2025, thời trang nam mới nhất, áo thun phong cách, t-shirts nam cao cấp', 1, N'Nguyễn Tùng Dương', CAST(N'2025-02-26T22:52:17.2082787' AS DateTime2), CAST(N'2025-02-26T22:52:17.2083196' AS DateTime2), NULL)
GO
INSERT [dbo].[Order] ([Id], [Code], [TotalAmount], [Quantity], [UserId], [CreatedAt], [UpdatedAt], [ModifiedBy], [Address], [Status], [Email], [FullName], [Phone], [TypePayment]) VALUES (N'2d1b2137-ae0d-40d8-b702-fd91410d40b3', N'DH410D40B3', CAST(519000.00 AS Decimal(18, 2)), 1, N'90820a1b-cacb-4186-8b68-8e258b6eac53', CAST(N'2025-02-25T17:52:09.4206397' AS DateTime2), CAST(N'2025-02-25T17:52:09.4206397' AS DateTime2), NULL, N'vĩnh hưng, Xã Văn Lý, Huyện Lý Nhân, Tỉnh Hà Nam', N'Confirmed', N'hoamai@yogirt.com', N'Ngô Xuân Hoà', N'0358726645', N'COD')
GO
INSERT [dbo].[OrderDetail] ([OrderId], [ProductId], [Price], [Quantity], [Id], [VariantId]) VALUES (N'2d1b2137-ae0d-40d8-b702-fd91410d40b3', N'c65520a9-78e9-4448-9624-0c6e0e5f39de', CAST(519000.00 AS Decimal(18, 2)), 1, N'41661ef3-18c8-436f-86cb-1e23f2e8759a', N'edbad518-43e0-42ef-9a7b-c77c070b7195')
GO
INSERT [dbo].[Post] ([Id], [Title], [Slug], [Description], [Detail], [Image], [CategoryId], [SeoTitile], [SeoDescription], [SeoKeywords], [IsActive], [CreateBy], [CreateDate], [ModifierDate], [ModifiedBy]) VALUES (N'fddd8e53-59bb-4dec-a1c1-29289b59a425', N'Giới thiệu về HShop', N'gioi-thieu-ve-hshop', N'Thương hiệu thời trang nam HShop được thành lập vào tháng 3 năm 2010, nhanh chóng khẳng định vị thế là thương hiệu uy tín hàng đầu tại Việt Nam dành riêng cho phái mạnh.', N'<h1 style="--tw-border-spacing-x: 0; --tw-border-spacing-y: 0; --tw-translate-x: 0; --tw-translate-y: 0; --tw-rotate: 0; --tw-skew-x: 0; --tw-skew-y: 0; --tw-scale-x: 1; --tw-scale-y: 1; --tw-scroll-snap-strictness: proximity; --tw-ring-offset-width: 0px; --tw-ring-offset-color: #fff; --tw-ring-color: rgba(59,130,246,.5); --tw-ring-offset-shadow: 0 0 transparent; --tw-ring-shadow: 0 0 transparent; --tw-shadow: 0 0 transparent; --tw-shadow-colored: 0 0 transparent; margin-top: var(--md-h3-margin-top); margin-right: 0px; margin-bottom: 16px; margin-left: 0px; padding: 0px; line-height: var(--md-h3-line-height);" class=""><span style="color: var(--bs-body-color); font-family: var(--bs-body-font-family); font-size: 24px; text-align: var(--bs-body-text-align);"><b>Sứ mệnh</b></span></h1><p style="--tw-border-spacing-x: 0; --tw-border-spacing-y: 0; --tw-translate-x: 0; --tw-translate-y: 0; --tw-rotate: 0; --tw-skew-x: 0; --tw-skew-y: 0; --tw-scale-x: 1; --tw-scale-y: 1; --tw-scroll-snap-strictness: proximity; --tw-ring-offset-width: 0px; --tw-ring-offset-color: #fff; --tw-ring-color: rgba(59,130,246,.5); --tw-ring-offset-shadow: 0 0 transparent; --tw-ring-shadow: 0 0 transparent; --tw-shadow: 0 0 transparent; --tw-shadow-colored: 0 0 transparent; margin-top: var(--md-h3-margin-top); margin-right: 0px; margin-bottom: 16px; margin-left: 0px; padding: 0px; line-height: var(--md-h3-line-height);" class=""><span style="color: var(--bs-body-color); font-family: var(--bs-body-font-family); font-size: var(--bs-body-font-size); font-weight: var(--bs-body-font-weight); text-align: var(--bs-body-text-align);">Chúng tôi không ngừng sáng tạo và chú trọng từng chi tiết từ quy trình sản xuất đến dịch vụ khách hàng, nhằm mang đến cho Quý Khách Hàng những trải nghiệm mua sắm đặc biệt nhất. </span><span style="color: var(--bs-body-color); font-family: var(--bs-body-font-family); font-size: var(--bs-body-font-size); font-weight: var(--bs-body-font-weight); text-align: var(--bs-body-text-align); --tw-border-spacing-x: 0; --tw-border-spacing-y: 0; --tw-translate-x: 0; --tw-translate-y: 0; --tw-rotate: 0; --tw-skew-x: 0; --tw-skew-y: 0; --tw-scale-x: 1; --tw-scale-y: 1; --tw-scroll-snap-strictness: proximity; --tw-ring-offset-width: 0px; --tw-ring-offset-color: #fff; --tw-ring-color: rgba(59,130,246,.5); --tw-ring-offset-shadow: 0 0 transparent; --tw-ring-shadow: 0 0 transparent; --tw-shadow: 0 0 transparent; --tw-shadow-colored: 0 0 transparent;">Sản phẩm chất lượng, dịch vụ hoàn hảo</span><span style="color: var(--bs-body-color); font-family: var(--bs-body-font-family); font-size: var(--bs-body-font-size); font-weight: var(--bs-body-font-weight); text-align: var(--bs-body-text-align);"> và </span><span style="color: var(--bs-body-color); font-family: var(--bs-body-font-family); font-size: var(--bs-body-font-size); font-weight: var(--bs-body-font-weight); text-align: var(--bs-body-text-align); --tw-border-spacing-x: 0; --tw-border-spacing-y: 0; --tw-translate-x: 0; --tw-translate-y: 0; --tw-rotate: 0; --tw-skew-x: 0; --tw-skew-y: 0; --tw-scale-x: 1; --tw-scale-y: 1; --tw-scroll-snap-strictness: proximity; --tw-ring-offset-width: 0px; --tw-ring-offset-color: #fff; --tw-ring-color: rgba(59,130,246,.5); --tw-ring-offset-shadow: 0 0 transparent; --tw-ring-shadow: 0 0 transparent; --tw-shadow: 0 0 transparent; --tw-shadow-colored: 0 0 transparent;">xu hướng thời trang mới mẻ</span><span style="color: var(--bs-body-color); font-family: var(--bs-body-font-family); font-size: var(--bs-body-font-size); font-weight: var(--bs-body-font-weight); text-align: var(--bs-body-text-align);"> là những gì chúng tôi cam kết. Thông qua các sản phẩm thời trang, HShop mong muốn truyền tải những thông điệp tích cực cùng nguồn cảm hứng trẻ trung và năng động.</span><span style="color: var(--bs-body-color); font-family: var(--bs-body-font-family); font-size: var(--bs-body-font-size); font-weight: var(--bs-body-font-weight); text-align: var(--bs-body-text-align);"><br></span></p><p style="--tw-border-spacing-x: 0; --tw-border-spacing-y: 0; --tw-translate-x: 0; --tw-translate-y: 0; --tw-rotate: 0; --tw-skew-x: 0; --tw-skew-y: 0; --tw-scale-x: 1; --tw-scale-y: 1; --tw-scroll-snap-strictness: proximity; --tw-ring-offset-width: 0px; --tw-ring-offset-color: #fff; --tw-ring-color: rgba(59,130,246,.5); --tw-ring-offset-shadow: 0 0 transparent; --tw-ring-shadow: 0 0 transparent; --tw-shadow: 0 0 transparent; --tw-shadow-colored: 0 0 transparent; margin-top: var(--md-h3-margin-top); margin-right: 0px; margin-bottom: 16px; margin-left: 0px; padding: 0px; line-height: var(--md-h3-line-height);" class=""><span style="color: var(--bs-body-color); font-family: var(--bs-body-font-family); font-size: 24px; text-align: var(--bs-body-text-align);"><b>Tầm nhìn</b></span></p><p style="--tw-border-spacing-x: 0; --tw-border-spacing-y: 0; --tw-translate-x: 0; --tw-translate-y: 0; --tw-rotate: 0; --tw-skew-x: 0; --tw-skew-y: 0; --tw-scale-x: 1; --tw-scale-y: 1; --tw-scroll-snap-strictness: proximity; --tw-ring-offset-width: 0px; --tw-ring-offset-color: #fff; --tw-ring-color: rgba(59,130,246,.5); --tw-ring-offset-shadow: 0 0 transparent; --tw-ring-shadow: 0 0 transparent; --tw-shadow: 0 0 transparent; --tw-shadow-colored: 0 0 transparent; margin-top: var(--md-h3-margin-top); margin-right: 0px; margin-bottom: 16px; margin-left: 0px; padding: 0px; line-height: var(--md-h3-line-height);" class=""><span style="color: var(--bs-body-color); font-family: var(--bs-body-font-family); font-size: var(--bs-body-font-size); font-weight: var(--bs-body-font-weight); text-align: var(--bs-body-text-align);">Với mục tiêu xây dựng và phát triển những giá trị bền vững, trong 10 năm tới, HShop hướng đến việc trở thành thương hiệu dẫn đầu về thời trang nam tại thị trường Việt Nam.</span></p><h3 style="--tw-border-spacing-x: 0; --tw-border-spacing-y: 0; --tw-translate-x: 0; --tw-translate-y: 0; --tw-rotate: 0; --tw-skew-x: 0; --tw-skew-y: 0; --tw-scale-x: 1; --tw-scale-y: 1; --tw-scroll-snap-strictness: proximity; --tw-ring-offset-width: 0px; --tw-ring-offset-color: #fff; --tw-ring-color: rgba(59,130,246,.5); --tw-ring-offset-shadow: 0 0 transparent; --tw-ring-shadow: 0 0 transparent; --tw-shadow: 0 0 transparent; --tw-shadow-colored: 0 0 transparent; margin-top: var(--md-h3-margin-top); margin-right: 0px; margin-bottom: 16px; margin-left: 0px; padding: 0px; line-height: var(--md-h3-line-height);"><b><span style="font-size: 24px;">Thông Điệp từ HShop</span></b></h3><p style="--tw-border-spacing-x: 0; --tw-border-spacing-y: 0; --tw-translate-x: 0; --tw-translate-y: 0; --tw-rotate: 0; --tw-skew-x: 0; --tw-skew-y: 0; --tw-scale-x: 1; --tw-scale-y: 1; --tw-scroll-snap-strictness: proximity; --tw-ring-offset-width: 0px; --tw-ring-offset-color: #fff; --tw-ring-color: rgba(59,130,246,.5); --tw-ring-offset-shadow: 0 0 transparent; --tw-ring-shadow: 0 0 transparent; --tw-shadow: 0 0 transparent; --tw-shadow-colored: 0 0 transparent; margin-right: 0px; margin-bottom: var(--md-paragraph-spacing); margin-left: 0px; padding: 0px; line-height: var(--md-normal-text-line-height); overflow-wrap: break-word;">HShop muốn truyền cảm hứng tích cực đến các chàng trai: <span style="--tw-border-spacing-x: 0; --tw-border-spacing-y: 0; --tw-translate-x: 0; --tw-translate-y: 0; --tw-rotate: 0; --tw-skew-x: 0; --tw-skew-y: 0; --tw-scale-x: 1; --tw-scale-y: 1; --tw-scroll-snap-strictness: proximity; --tw-ring-offset-width: 0px; --tw-ring-offset-color: #fff; --tw-ring-color: rgba(59,130,246,.5); --tw-ring-offset-shadow: 0 0 transparent; --tw-ring-shadow: 0 0 transparent; --tw-shadow: 0 0 transparent; --tw-shadow-colored: 0 0 transparent;">Việc mặc đẹp rất quan trọng</span>, nó không chỉ thể hiện cá tính và sự tự tin mà còn phản ánh lối sống và cách suy nghĩ của bạn. Hãy mặc thanh lịch để sống thanh lịch.</p><p style="--tw-border-spacing-x: 0; --tw-border-spacing-y: 0; --tw-translate-x: 0; --tw-translate-y: 0; --tw-rotate: 0; --tw-skew-x: 0; --tw-skew-y: 0; --tw-scale-x: 1; --tw-scale-y: 1; --tw-scroll-snap-strictness: proximity; --tw-ring-offset-width: 0px; --tw-ring-offset-color: #fff; --tw-ring-color: rgba(59,130,246,.5); --tw-ring-offset-shadow: 0 0 transparent; --tw-ring-shadow: 0 0 transparent; --tw-shadow: 0 0 transparent; --tw-shadow-colored: 0 0 transparent; margin-right: 0px; margin-bottom: var(--md-paragraph-spacing); margin-left: 0px; padding: 0px; line-height: var(--md-normal-text-line-height); overflow-wrap: break-word;">Chọn HShop, bạn đang lựa chọn sự hoàn hảo cho phong cách thời trang của chính mình!</p>', N'/Uploads/Posts/logo_20250301_235541.png', N'2abc719a-8485-4025-a078-d1e115f8e495', N'HShop - Thương Hiệu Thời Trang Nam Hàng Đầu Tại Việt Nam', N'Khám phá HShop, thương hiệu thời trang nam uy tín tại Việt Nam. Chúng tôi cung cấp sản phẩm chất lượng, thiết kế tinh tế và dịch vụ hoàn hảo, giúp bạn thể hiện phong cách và sự tự tin.', N'HShop, thời trang nam, thương hiệu thời trang, sản phẩm thời trang nam, phong cách thanh lịch, mua sắm thời trang', 1, N'Nguyễn Tùng Dương', CAST(N'2025-03-01T23:55:41.3153163' AS DateTime2), CAST(N'2025-03-01T23:55:41.3153177' AS DateTime2), NULL)
GO
INSERT [dbo].[Product] ([Id], [ProductCode], [Title], [Slug], [Description], [Detail], [Image], [Quantity], [IsHome], [IsSale], [IsFeature], [IsHot], [ProductCategoryId], [SeoTitile], [SeoDescription], [SeoKeywords], [IsActive], [IsDeleted], [CategoryId], [CreateBy], [CreateDate], [ModifierDate], [ModifiedBy], [AttributeOptionIds]) VALUES (N'c65520a9-78e9-4448-9624-0c6e0e5f39de', N'SMD.1421', N'Áo Sơ Mi Cotton SMD.1421', N'ao-so-mi-cotton-smd1421-c65520a978e9444896240c6e0e5f39de', N'Áo sơ mi dài tay nam với thiết kế thanh lịch, chất liệu cao cấp, mang lại sự thoải mái và phong cách cho phái mạnh. Hoàn hảo cho cả công sở và các sự kiện trang trọng.', N'<li style="flex-shrink: 0; width: 1146.8px; max-width: 100%; padding-right: calc(var(--bs-gutter-x) * 0.5); padding-left: calc(var(--bs-gutter-x) * 0.5); margin-top: var(--bs-gutter-y);"><span style="font-weight: 700;">Chất liệu:&nbsp;</span>100% cotton</li><li style="flex-shrink: 0; width: 1146.8px; max-width: 100%; padding-right: calc(var(--bs-gutter-x) * 0.5); padding-left: calc(var(--bs-gutter-x) * 0.5); margin-top: var(--bs-gutter-y);"><span style="font-weight: 700;">Kiểu dáng:</span>&nbsp;Regular fit</li><li style="flex-shrink: 0; width: 1146.8px; max-width: 100%; padding-right: calc(var(--bs-gutter-x) * 0.5); padding-left: calc(var(--bs-gutter-x) * 0.5); margin-top: var(--bs-gutter-y);"><span style="font-weight: 700;">Màu sắc:</span>&nbsp;Trắng</li><li style="flex-shrink: 0; width: 1146.8px; max-width: 100%; padding-right: calc(var(--bs-gutter-x) * 0.5); padding-left: calc(var(--bs-gutter-x) * 0.5); margin-top: var(--bs-gutter-y);"><span style="font-weight: 700;">Tính năng:</span>&nbsp;Vải cotton cao cấp, mềm mịn, thấm hút tốt, bền chắc.</li><li style="flex-shrink: 0; width: 1146.8px; max-width: 100%; padding-right: calc(var(--bs-gutter-x) * 0.5); padding-left: calc(var(--bs-gutter-x) * 0.5); margin-top: var(--bs-gutter-y);"><span style="font-weight: 700; color: var(--bs-body-color); font-family: var(--bs-body-font-family); font-size: var(--bs-body-font-size); text-align: var(--bs-body-text-align);">Sự kiện:</span><span style="color: var(--bs-body-color); font-family: var(--bs-body-font-family); font-size: var(--bs-body-font-size); font-weight: var(--bs-body-font-weight); text-align: var(--bs-body-text-align);">&nbsp;Thích hợp cho công sở, tiệc tùng, dạo phố</span></li>', N'/Uploads/Products/ao-so-mi-cotton-smd1421-1_20250205_151948.jpeg', 39, 1, 0, 0, 0, N'e2eb9cbd-da4c-445d-aee3-3d2bc5c169d9', N'Áo Sơ Mi Dài Tay Nam - Thời Trang Lịch Lãm và Sang Trọng', N'Thiết kế tinh tế và hiện đại giúp bạn tỏa sáng trong mọi hoàn cảnh, từ công sở đến các buổi tiệc. Với khả năng thấm hút mồ hôi tốt và độ bền cao, áo sơ mi modal là lựa chọn lý tưởng cho phong cách thời trang của bạn.', N'áo sơ mi cotton , áo sơ mi nam, áo sơ mi thời trang, áo sơ mi thoải mái, áo sơ mi cao cấp, mua sắm áo sơ mi, thời trang nam', 1, 0, NULL, NULL, CAST(N'2025-02-05T15:19:48.4082328' AS DateTime2), CAST(N'2025-02-05T15:19:48.4082339' AS DateTime2), NULL, N'[]')
GO
INSERT [dbo].[Product] ([Id], [ProductCode], [Title], [Slug], [Description], [Detail], [Image], [Quantity], [IsHome], [IsSale], [IsFeature], [IsHot], [ProductCategoryId], [SeoTitile], [SeoDescription], [SeoKeywords], [IsActive], [IsDeleted], [CategoryId], [CreateBy], [CreateDate], [ModifierDate], [ModifiedBy], [AttributeOptionIds]) VALUES (N'12507b60-1fe3-441b-b760-1b6e2fda4dc8', N'SMC.410', N'Áo Sơ Mi Bamboo SMC.410', N'ao-so-mi-bamboo-smc410-12507b601fe3441bb7601b6e2fda4dc8', N'Áo sơ mi bamboo mang lại sự kết hợp hoàn hảo giữa phong cách và tính năng vượt trội. Chất liệu bamboo tự nhiên không chỉ mềm mại và thoáng mát mà còn có khả năng kháng khuẩn, giúp bạn luôn cảm thấy thoải mái và tự tin.', N'<li><strong>Chất liệu:</strong> 49% bamboo, 48% microfiber, 3% spandex</li>
<li><strong>Kiểu dáng:</strong> Regular fit</li>
<li><strong>Màu sắc:</strong> Caro to trắng, vàng, đen</li>
<li><strong>Tính năng:</strong>
<ul>
<li>Mềm mại</li>
<li>Nhẹ nhàng</li>
<li>Thoáng mát</li>
<li>Thấm hút tốt</li>
<li>Thân thiện với môi trường</li>
<li>Không gây hại cho da</li>
<li>Co giãn đàn hồi tốt</li>
<li>Kháng tia UV</li></ul></li>', N'/Uploads/Products/ao-so-mi-bamboo-smc410-1_20250206_001123.jpeg', 40, 1, 0, 0, 0, N'779249d6-cde0-4d26-a2aa-8fef28ef13cf', N'Áo Sơ Mi Bamboo Cộc Tay - Thoải Mái và Thời Trang', N'Khám phá áo sơ mi bamboo cộc tay với thiết kế hiện đại và chất liệu thân thiện với môi trường. Với họa tiết caro to màu trắng, vàng và đen, áo sơ mi này không chỉ tôn lên phong cách mà còn kháng tia UV, là lựa chọn hoàn hảo cho mùa hè.', N'áo sơ mi bamboo cộc tay, áo sơ mi bamboo, thời trang nam, áo sơ mi caro, áo sơ mi thoáng mát, áo sơ mi thân thiện với môi trường, áo sơ mi kháng tia UV', 1, 0, NULL, NULL, CAST(N'2025-02-06T00:11:23.4941854' AS DateTime2), CAST(N'2025-02-06T00:11:23.4941873' AS DateTime2), NULL, N'[]')
GO
INSERT [dbo].[Product] ([Id], [ProductCode], [Title], [Slug], [Description], [Detail], [Image], [Quantity], [IsHome], [IsSale], [IsFeature], [IsHot], [ProductCategoryId], [SeoTitile], [SeoDescription], [SeoKeywords], [IsActive], [IsDeleted], [CategoryId], [CreateBy], [CreateDate], [ModifierDate], [ModifiedBy], [AttributeOptionIds]) VALUES (N'bf27bd25-3729-4827-8d16-20cc23e48f4e', N'SMC.480', N'Áo Sơ Mi Linen SMC.480', N'ao-so-mi-linen-smc480-bf27bd25372948278d1620cc23e48f4e', N'Áo sơ mi linen mang lại sự thoải mái và phong cách tối giản, hoàn hảo cho những ngày hè oi ả. Với chất liệu linen tự nhiên, áo có khả năng thấm hút mồ hôi tốt, giúp bạn luôn cảm thấy mát mẻ.', N'<li><strong>Chất liệu:</strong> 100% linen</li><li><strong>Kiểu dáng:</strong> Regular fit</li><li><strong>Màu sắc:</strong> Xanh tím than họa tiết</li><p>


</p><li><strong>Tính năng:</strong>
<ul>
<li>Mềm mại</li>
<li>Nhẹ nhàng</li>
<li>Mát mẻ</li>
<li>Ít nhăn nhàu</li>
<li>Thoáng khí</li></ul></li>', N'/Uploads/Products/ao-so-mi-linen-smc480-1_20250205_234614.jpeg', 40, 1, 0, 0, 1, N'779249d6-cde0-4d26-a2aa-8fef28ef13cf', N'Áo Sơ Mi Linen Xanh Tím Than - Thoải Mái và Thanh Lịch', N'Khám phá áo sơ mi linen màu xanh tím than họa tiết với thiết kế regular fit. Chất liệu 100% linen mềm mại, nhẹ nhàng và thoáng mát giúp bạn luôn cảm thấy thoải mái trong những ngày hè.', N'áo sơ mi linen, áo sơ mi nam, áo sơ mi xanh tím than, áo sơ mi thoáng mát, thời trang nam, áo sơ mi regular fit, áo sơ mi họa tiết', 1, 0, NULL, NULL, CAST(N'2025-02-05T23:46:14.2283192' AS DateTime2), CAST(N'2025-02-05T23:46:14.2283214' AS DateTime2), NULL, N'[]')
GO
INSERT [dbo].[Product] ([Id], [ProductCode], [Title], [Slug], [Description], [Detail], [Image], [Quantity], [IsHome], [IsSale], [IsFeature], [IsHot], [ProductCategoryId], [SeoTitile], [SeoDescription], [SeoKeywords], [IsActive], [IsDeleted], [CategoryId], [CreateBy], [CreateDate], [ModifierDate], [ModifiedBy], [AttributeOptionIds]) VALUES (N'0929e105-20f1-4e8e-92d8-25113f9f0c9c', N'SMD.1415', N'Áo Sơ Mi Basic SMD.1415', N'ao-so-mi-basic-smd1415-0929e10520f14e8e92d825113f9f0c9c', N'Áo sơ mi dài tay basic với thiết kế đơn giản nhưng tinh tế, là lựa chọn hoàn hảo cho cả công sở lẫn dạo phố. Chất liệu mềm mại và thoải mái giúp bạn tự tin trong mọi hoạt động.', N'<li><strong>Chất liệu:</strong> 30% bamboo, 30% microfiber, 35% poly, 5% spandex</li><li><strong>Kiểu dáng:</strong> Slim fit, tôn dáng</li><li><strong>Màu sắc:</strong> Tím than</li><p>


</p><li><strong>Tính năng:</strong>
<ul>
<li>Nhẹ nhàng</li>
<li>Thoáng mát</li>
<li>Ít nhăn</li>
<li>Kháng khuẩn tự nhiên</li>
<li>Thiết kế đơn giản nhưng tinh tế</li></ul></li>', N'/Uploads/Products/ao-so-mi-basic-smd1415-1_20250205_233236.jpeg', 40, 1, 0, 0, 0, N'e2eb9cbd-da4c-445d-aee3-3d2bc5c169d9', N'Áo Sơ Mi Dài Tay Basic - Lịch Lãm và Thoải Mái', N'Chất liệu cao cấp gồm 30% bamboo, 30% microfiber, 35% poly và 5% spandex mang lại sự thoải mái tối ưu, ít nhăn và kháng khuẩn tự nhiên.', N'áo sơ mi dài tay basic, áo sơ mi nam, áo sơ mi slim fit, áo sơ mi tím than, thời trang nam, áo sơ mi thoáng mát, áo sơ mi kháng khuẩn', 1, 0, NULL, NULL, CAST(N'2025-02-05T23:32:36.4051010' AS DateTime2), CAST(N'2025-02-05T23:32:36.4051019' AS DateTime2), NULL, N'[]')
GO
INSERT [dbo].[Product] ([Id], [ProductCode], [Title], [Slug], [Description], [Detail], [Image], [Quantity], [IsHome], [IsSale], [IsFeature], [IsHot], [ProductCategoryId], [SeoTitile], [SeoDescription], [SeoKeywords], [IsActive], [IsDeleted], [CategoryId], [CreateBy], [CreateDate], [ModifierDate], [ModifiedBy], [AttributeOptionIds]) VALUES (N'df802c28-0344-4ce5-9d47-3309569ece4b', N'SMD.1420', N'Áo Sơ Mi Modal SMD.1420', N'ao-so-mi-modal-smd1420-df802c2803444ce59d473309569ece4b', N'Áo sơ mi dài tay nam với thiết kế thanh lịch, chất liệu cao cấp, mang lại sự thoải mái và phong cách cho phái mạnh. Hoàn hảo cho cả công sở và các sự kiện trang trọng.', N'<li><strong>Chất liệu:</strong>&nbsp;<span style="color: rgb(37, 42, 43); font-family: &quot;Public Sans&quot;; font-size: 14px;">20% modal 78% poly 2% spandex</span></li><li><strong>Kiểu dáng:</strong> Slim fit, tôn dáng</li><li><strong>Màu sắc:</strong> Đa dạng, phù hợp với mọi phong cách</li><li><strong>Tính năng:</strong> Mềm mướt thoáng mát, thấm hút tốt, bền chắc, hạn chế nhăn màu, thân thiện môi trường.</li><li><strong style="color: var(--bs-body-color); font-family: var(--bs-body-font-family); font-size: var(--bs-body-font-size); text-align: var(--bs-body-text-align);">Sự kiện:</strong><span style="color: var(--bs-body-color); font-family: var(--bs-body-font-family); font-size: var(--bs-body-font-size); font-weight: var(--bs-body-font-weight); text-align: var(--bs-body-text-align);"> Thích hợp cho công sở, tiệc tùng, dạo phố</span></li>', N'/Uploads/Products/ao-so-mi-modal-smd1420-1_20250205_150312.jpeg', 50, 1, 0, 0, 0, N'e2eb9cbd-da4c-445d-aee3-3d2bc5c169d9', N'Áo Sơ Mi Dài Tay Nam - Thời Trang Lịch Lãm và Sang Trọng', N'Thiết kế tinh tế và hiện đại giúp bạn tỏa sáng trong mọi hoàn cảnh, từ công sở đến các buổi tiệc. Với khả năng thấm hút mồ hôi tốt và độ bền cao, áo sơ mi modal là lựa chọn lý tưởng cho phong cách thời trang của bạn.', N'áo sơ mi dài tay modal, áo sơ mi modal nam, áo sơ mi thời trang, áo sơ mi thoáng mát, áo sơ mi cao cấp, mua sắm áo sơ mi modal, áo sơ mi nam giá rẻ, thời trang nam', 1, 0, NULL, NULL, CAST(N'2025-02-05T15:03:12.3941843' AS DateTime2), CAST(N'2025-02-05T15:03:12.3941868' AS DateTime2), NULL, N'[]')
GO
INSERT [dbo].[Product] ([Id], [ProductCode], [Title], [Slug], [Description], [Detail], [Image], [Quantity], [IsHome], [IsSale], [IsFeature], [IsHot], [ProductCategoryId], [SeoTitile], [SeoDescription], [SeoKeywords], [IsActive], [IsDeleted], [CategoryId], [CreateBy], [CreateDate], [ModifierDate], [ModifiedBy], [AttributeOptionIds]) VALUES (N'10421d8d-9b76-4591-8545-9b0f7c4165e8', N'SMD.1422', N'Áo Sơ Mi Cotton SMD.1422', N'ao-so-mi-cotton-smd1422-10421d8d9b76459185459b0f7c4165e8', N'Áo sơ mi dài tay nam với thiết kế thanh lịch, chất liệu cao cấp, mang lại sự thoải mái và phong cách cho phái mạnh. Hoàn hảo cho cả công sở và các sự kiện trang trọng.', N'<li style="flex-shrink: 0; width: 1146.8px; max-width: 100%; padding-right: calc(var(--bs-gutter-x) * 0.5); padding-left: calc(var(--bs-gutter-x) * 0.5); margin-top: var(--bs-gutter-y);"><span style="font-weight: 700;">Chất liệu: </span>100% cotton</li><li style="flex-shrink: 0; width: 1146.8px; max-width: 100%; padding-right: calc(var(--bs-gutter-x) * 0.5); padding-left: calc(var(--bs-gutter-x) * 0.5); margin-top: var(--bs-gutter-y);"><span style="font-weight: 700;">Kiểu dáng:</span>&nbsp;Regular fit</li><li style="flex-shrink: 0; width: 1146.8px; max-width: 100%; padding-right: calc(var(--bs-gutter-x) * 0.5); padding-left: calc(var(--bs-gutter-x) * 0.5); margin-top: var(--bs-gutter-y);"><span style="font-weight: 700;">Màu sắc:</span>&nbsp;Be</li><li style="flex-shrink: 0; width: 1146.8px; max-width: 100%; padding-right: calc(var(--bs-gutter-x) * 0.5); padding-left: calc(var(--bs-gutter-x) * 0.5); margin-top: var(--bs-gutter-y);"><span style="font-weight: 700;">Tính năng:</span>&nbsp;Vải cotton cao cấp, mềm mịn, thấm hút tốt, bền chắc.</li><li style="flex-shrink: 0; width: 1146.8px; max-width: 100%; padding-right: calc(var(--bs-gutter-x) * 0.5); padding-left: calc(var(--bs-gutter-x) * 0.5); margin-top: var(--bs-gutter-y);"><span style="font-weight: 700; color: var(--bs-body-color); font-family: var(--bs-body-font-family); font-size: var(--bs-body-font-size); text-align: var(--bs-body-text-align);">Sự kiện:</span><span style="color: var(--bs-body-color); font-family: var(--bs-body-font-family); font-size: var(--bs-body-font-size); font-weight: var(--bs-body-font-weight); text-align: var(--bs-body-text-align);">&nbsp;Thích hợp cho công sở, tiệc tùng, dạo phố</span></li>', N'/Uploads/Products/ao-so-mi-cotton-smd1422-1_20250205_151604.jpeg', 40, 1, 0, 0, 0, N'e2eb9cbd-da4c-445d-aee3-3d2bc5c169d9', N'Áo Sơ Mi Dài Tay Nam - Thời Trang Lịch Lãm và Sang Trọng', N'Thiết kế tinh tế và hiện đại giúp bạn tỏa sáng trong mọi hoàn cảnh, từ công sở đến các buổi tiệc. Với khả năng thấm hút mồ hôi tốt và độ bền cao, áo sơ mi modal là lựa chọn lý tưởng cho phong cách thời trang của bạn.', N'áo sơ mi cotton , áo sơ mi nam, áo sơ mi thời trang, áo sơ mi thoải mái, áo sơ mi cao cấp, mua sắm áo sơ mi, thời trang nam', 1, 0, NULL, NULL, CAST(N'2025-02-05T15:16:04.5308866' AS DateTime2), CAST(N'2025-02-05T15:16:04.5308877' AS DateTime2), NULL, N'[]')
GO
INSERT [dbo].[Product] ([Id], [ProductCode], [Title], [Slug], [Description], [Detail], [Image], [Quantity], [IsHome], [IsSale], [IsFeature], [IsHot], [ProductCategoryId], [SeoTitile], [SeoDescription], [SeoKeywords], [IsActive], [IsDeleted], [CategoryId], [CreateBy], [CreateDate], [ModifierDate], [ModifiedBy], [AttributeOptionIds]) VALUES (N'3ad7c66a-125c-42c8-9698-c18d91ee1d81', N'SMD.1407', N'Áo Sơ Mi Bamboo SMD.1407', N'ao-so-mi-bamboo-smd1407-3ad7c66a125c42c89698c18d91ee1d81', N'Áo sơ mi dài tay bamboo mang lại cảm giác mềm mại và thoáng mát, lý tưởng cho những ngày hè oi ả. Chất liệu thân thiện với môi trường và khả năng thấm hút tốt giúp bạn luôn cảm thấy dễ chịu.', N'<li><strong>Chất liệu:</strong> 50% bamboo, 50% poly</li><li><strong>Kiểu dáng:</strong> Slim fit, tôn dáng</li><li><strong>Màu sắc:</strong> Nâu nhạt kẻ sọc</li><p>


</p><li><strong>Tính năng:</strong>
<ul>
<li>Mềm mướt</li>
<li>Thoáng mát</li>
<li>Thấm hút tốt</li>
<li>Bền chắc</li>
<li>Hạn chế nhăn nhàu</li>
<li>Thân thiện với môi trường</li></ul></li>', N'/Uploads/Products/ao-so-mi-bamboo-smd1407-1_20250205_222755.jpeg', 40, 1, 0, 0, 0, N'e2eb9cbd-da4c-445d-aee3-3d2bc5c169d9', N'Áo Sơ Mi Dài Tay Bamboo - Thoải Mái và Thời Trang', N'Chất liệu 50% bamboo và 50% poly không chỉ mềm mướt và thoáng mát mà còn thấm hút tốt, bền chắc và hạn chế nhăn nhàu. Đây là lựa chọn hoàn hảo cho những ngày hè, giúp bạn tự tin và thoải mái trong mọi hoàn cảnh.', N'áo sơ mi dài tay bamboo, áo sơ mi nam, áo sơ mi thời trang, áo sơ mi thoáng mát, áo sơ mi kẻ sọc, thời trang nam, áo sơ mi cao cấp, mua sắm áo sơ mi', 1, 0, NULL, NULL, CAST(N'2025-02-05T22:27:55.1299151' AS DateTime2), CAST(N'2025-02-05T22:27:55.1299161' AS DateTime2), NULL, N'[]')
GO
INSERT [dbo].[Product] ([Id], [ProductCode], [Title], [Slug], [Description], [Detail], [Image], [Quantity], [IsHome], [IsSale], [IsFeature], [IsHot], [ProductCategoryId], [SeoTitile], [SeoDescription], [SeoKeywords], [IsActive], [IsDeleted], [CategoryId], [CreateBy], [CreateDate], [ModifierDate], [ModifiedBy], [AttributeOptionIds]) VALUES (N'fafa1eec-07cb-4631-b1dc-ffcee3ce8b05', N'SMD.1411', N'Áo Sơ Mi Bamboo SMD.1411', N'ao-so-mi-bamboo-smd1411-fafa1eec07cb4631b1dcffcee3ce8b05', N'Áo sơ mi dài tay bamboo mang lại cảm giác mềm mại và thoáng mát, lý tưởng cho những ngày hè oi ả. Chất liệu thân thiện với môi trường và khả năng thấm hút tốt giúp bạn luôn cảm thấy dễ chịu.', N'<li><strong>Chất liệu:</strong> 50% bamboo, 50% poly</li><li><strong>Kiểu dáng:</strong> Slim fit, tôn dáng</li><li><strong>Màu sắc:</strong> Nâu nhạt kẻ sọc</li><p>


</p><li><strong>Tính năng:</strong>
<ul>
<li>Mềm mướt</li>
<li>Thoáng mát</li>
<li>Thấm hút tốt</li>
<li>Bền chắc</li>
<li>Hạn chế nhăn nhàu</li>
<li>Thân thiện với môi trường</li></ul></li>', N'/Uploads/Products/ao-so-mi-bamboo-smd1411-1_20250205_223040.jpeg', 40, 1, 0, 0, 1, N'e2eb9cbd-da4c-445d-aee3-3d2bc5c169d9', N'Áo Sơ Mi Dài Tay Bamboo - Thoải Mái và Thời Trang', N'Chất liệu 50% bamboo và 50% poly không chỉ mềm mướt và thoáng mát mà còn thấm hút tốt, bền chắc và hạn chế nhăn nhàu. Đây là lựa chọn hoàn hảo cho những ngày hè, giúp bạn tự tin và thoải mái trong mọi hoàn cảnh.', N'áo sơ mi dài tay bamboo, áo sơ mi nam, áo sơ mi thời trang, áo sơ mi thoáng mát, áo sơ mi kẻ sọc, thời trang nam, áo sơ mi cao cấp, mua sắm áo sơ mi', 1, 0, NULL, NULL, CAST(N'2025-02-05T22:30:40.4427277' AS DateTime2), CAST(N'2025-02-05T22:30:40.4427291' AS DateTime2), NULL, N'[]')
GO
INSERT [dbo].[ProductCategory] ([Id], [Title], [Slug], [Description], [SeoTitle], [SeoDescription], [SeoKeywords], [IsDeleted], [IsFeatured], [ParentCategoryId], [CreateBy], [CreateDate], [ModifierDate], [ModifiedBy]) VALUES (N'34b782ca-0607-4dad-0e57-08dd37f6534c', N'Thời Trang Nam', N'thoi-trang-nam', N'Thời trang phái mạnh', N'Thời Trang Nam Đẳng Cấp - Mua Sắm Ngay Hôm Nay', N'Khám phá bộ sưu tập thời trang nam đa dạng với các mẫu áo, quần, và phụ kiện hiện đại. Tôn lên phong cách riêng của bạn với chất liệu cao cấp và thiết kế tinh tế.', N'thời trang nam, quần áo nam, áo thun nam, quần jeans nam, áo khoác nam, phụ kiện nam', 0, 1, NULL, NULL, CAST(N'2025-01-19T02:28:45.5094285' AS DateTime2), CAST(N'2025-01-19T02:41:12.6881630' AS DateTime2), NULL)
GO
INSERT [dbo].[ProductCategory] ([Id], [Title], [Slug], [Description], [SeoTitle], [SeoDescription], [SeoKeywords], [IsDeleted], [IsFeatured], [ParentCategoryId], [CreateBy], [CreateDate], [ModifierDate], [ModifiedBy]) VALUES (N'60b2b60d-e8d2-4801-a0a2-1a988e538a2f', N'Áo Polo', N'ao-polo', N'Khám phá bộ sưu tập áo polo nam, sự kết hợp hoàn hảo giữa phong cách thể thao và sự thanh lịch. Chất liệu cotton cao cấp mang lại cảm giác thoải mái và thoáng mát, lý tưởng cho cả những buổi dạo phố và hoạt động thể thao. Với nhiều màu sắc và kiểu dáng khác nhau, áo polo là lựa chọn tuyệt vời để bạn thể hiện phong cách cá nhân trong mọi hoàn cảnh.', N'Áo Polo Nam - Phong Cách Thể Thao và Thanh Lịch', N'Khám phá bộ sưu tập áo polo nam, sự kết hợp hoàn hảo giữa phong cách thể thao và sự thanh lịch. Chất liệu cotton cao cấp mang lại cảm giác thoải mái và thoáng mát, lý tưởng cho cả những buổi dạo phố và hoạt động thể thao. Với nhiều màu sắc và kiểu dáng khác nhau, áo polo là lựa chọn tuyệt vời để bạn thể hiện phong cách cá nhân trong mọi hoàn cảnh.', N'áo polo nam, áo polo thời trang, áo polo cao cấp, áo polo thể thao, mua sắm áo polo, áo polo nam giá rẻ, thời trang nam', 0, 1, N'34b782ca-0607-4dad-0e57-08dd37f6534c', NULL, CAST(N'2025-02-05T14:41:00.7292121' AS DateTime2), CAST(N'2025-02-12T19:12:46.7260474' AS DateTime2), NULL)
GO
INSERT [dbo].[ProductCategory] ([Id], [Title], [Slug], [Description], [SeoTitle], [SeoDescription], [SeoKeywords], [IsDeleted], [IsFeatured], [ParentCategoryId], [CreateBy], [CreateDate], [ModifierDate], [ModifiedBy]) VALUES (N'e2eb9cbd-da4c-445d-aee3-3d2bc5c169d9', N'Áo sơ mi dài tay', N'ao-so-mi-dai-tay', N'Khám phá bộ sưu tập áo sơ mi dài tay nam với thiết kế hiện đại, chất liệu cao cấp mang lại sự thoải mái và phong cách. Từ công sở đến những buổi tiệc tùng, áo sơ mi dài tay là lựa chọn hoàn hảo giúp bạn luôn nổi bật và tự tin. Với nhiều màu sắc và kiểu dáng đa dạng, bạn dễ dàng tìm thấy phong cách phù hợp cho mình.', N'Áo Sơ Mi Dài Tay Nam - Thời Trang Lịch Lãm và Sang Trọng', N'Khám phá bộ sưu tập áo sơ mi dài tay nam với thiết kế hiện đại, chất liệu cao cấp mang lại sự thoải mái và phong cách. Từ công sở đến những buổi tiệc tùng, áo sơ mi dài tay là lựa chọn hoàn hảo giúp bạn luôn nổi bật và tự tin. Với nhiều màu sắc và kiểu dáng đa dạng, bạn dễ dàng tìm thấy phong cách phù hợp cho mình.', N'áo sơ mi dài tay nam, áo sơ mi công sở, áo sơ mi thời trang, áo sơ mi lịch lãm, áo sơ mi cao cấp, mua sắm áo sơ mi nam, áo sơ mi nam giá rẻ, thời trang nam', 0, 1, N'34b782ca-0607-4dad-0e57-08dd37f6534c', NULL, CAST(N'2025-02-05T14:37:54.9992589' AS DateTime2), CAST(N'2025-02-12T19:12:26.0533667' AS DateTime2), NULL)
GO
INSERT [dbo].[ProductCategory] ([Id], [Title], [Slug], [Description], [SeoTitle], [SeoDescription], [SeoKeywords], [IsDeleted], [IsFeatured], [ParentCategoryId], [CreateBy], [CreateDate], [ModifierDate], [ModifiedBy]) VALUES (N'814e4afc-3bea-4ef1-9eea-5d5eeb163216', N'Thời Trang Nữ', N'thoi-trang-nu', N'Thời trang nữ giới - đẹp quý phái', N'Thời Trang Nữ Đẹp - Mua Sắm Ngay Hôm Nay', N'Thời Trang Nữ Đẹp - Mua Sắm Ngay Hôm Nay', N'thời trang nữ, quần áo nữ, váy đầm, áo thun nữ, quần jeans nữ, phụ kiện nữ', 0, 1, NULL, NULL, CAST(N'2025-02-12T18:49:15.8709810' AS DateTime2), CAST(N'2025-02-12T19:11:40.0460207' AS DateTime2), NULL)
GO
INSERT [dbo].[ProductCategory] ([Id], [Title], [Slug], [Description], [SeoTitle], [SeoDescription], [SeoKeywords], [IsDeleted], [IsFeatured], [ParentCategoryId], [CreateBy], [CreateDate], [ModifierDate], [ModifiedBy]) VALUES (N'779249d6-cde0-4d26-a2aa-8fef28ef13cf', N'Áo sơ mi cộc tay', N'ao-so-mi-coc-tay', N'Khám phá bộ sưu tập áo sơ mi cộc tay nam với thiết kế trẻ trung, phù hợp cho mùa hè và các hoạt động ngoài trời. Chất liệu thoáng mát, dễ chịu giúp bạn luôn cảm thấy thoải mái, trong khi kiểu dáng hiện đại tôn lên vẻ ngoài lịch lãm.', N'Áo Sơ Mi Cộc Tay Nam - Phong Cách Năng Động và Thoải Mái', N'Khám phá bộ sưu tập áo sơ mi cộc tay nam với thiết kế trẻ trung, phù hợp cho mùa hè và các hoạt động ngoài trời. Chất liệu thoáng mát, dễ chịu giúp bạn luôn cảm thấy thoải mái, trong khi kiểu dáng hiện đại tôn lên vẻ ngoài lịch lãm.', N'áo sơ mi cộc tay nam, áo sơ mi mùa hè, áo sơ mi thời trang, áo sơ mi năng động, áo sơ mi nam thoáng mát, mua sắm áo sơ mi cộc tay, áo sơ mi nam giá rẻ, thời trang nam', 0, 1, N'34b782ca-0607-4dad-0e57-08dd37f6534c', NULL, CAST(N'2025-02-05T14:39:30.1950894' AS DateTime2), CAST(N'2025-02-12T19:12:38.0637456' AS DateTime2), NULL)
GO
INSERT [dbo].[ProductImage] ([Id], [ProductId], [Image], [IsDefault]) VALUES (N'1bc72f2e-037f-4b64-89dd-031e65698e37', N'c65520a9-78e9-4448-9624-0c6e0e5f39de', N'/Uploads/Products/ao-so-mi-cotton-smd1421-1_20250205_151948.jpeg', 1)
GO
INSERT [dbo].[ProductImage] ([Id], [ProductId], [Image], [IsDefault]) VALUES (N'a0553fde-188b-49fc-9d68-0955dea67a36', N'0929e105-20f1-4e8e-92d8-25113f9f0c9c', N'/Uploads/Products/ao-so-mi-basic-smd1415-2_20250205_233236.jpeg', 0)
GO
INSERT [dbo].[ProductImage] ([Id], [ProductId], [Image], [IsDefault]) VALUES (N'97254b8e-a519-41d4-8af6-0bde9d3ea3a4', N'bf27bd25-3729-4827-8d16-20cc23e48f4e', N'/Uploads/Products/ao-so-mi-linen-smc480-2_20250205_234614.jpeg', 0)
GO
INSERT [dbo].[ProductImage] ([Id], [ProductId], [Image], [IsDefault]) VALUES (N'ee0c59da-88f6-4b0a-a68e-1172871d18bc', N'3ad7c66a-125c-42c8-9698-c18d91ee1d81', N'/Uploads/Products/ao-so-mi-bamboo-smd1407-3_20250205_222755.jpeg', 0)
GO
INSERT [dbo].[ProductImage] ([Id], [ProductId], [Image], [IsDefault]) VALUES (N'f240935f-bc89-422f-b9cf-195e7dc86683', N'3ad7c66a-125c-42c8-9698-c18d91ee1d81', N'/Uploads/Products/ao-so-mi-bamboo-smd1407-2_20250205_222755.jpeg', 0)
GO
INSERT [dbo].[ProductImage] ([Id], [ProductId], [Image], [IsDefault]) VALUES (N'0c0ff044-c0b4-4776-80fd-45095950047e', N'10421d8d-9b76-4591-8545-9b0f7c4165e8', N'/Uploads/Products/ao-so-mi-cotton-smd1422-2_20250205_151604.jpeg', 0)
GO
INSERT [dbo].[ProductImage] ([Id], [ProductId], [Image], [IsDefault]) VALUES (N'23e0b7ee-1c2a-4882-9d61-452dd3825244', N'fafa1eec-07cb-4631-b1dc-ffcee3ce8b05', N'/Uploads/Products/ao-so-mi-bamboo-smd1411-2_20250205_223040.jpeg', 0)
GO
INSERT [dbo].[ProductImage] ([Id], [ProductId], [Image], [IsDefault]) VALUES (N'3c49a764-5962-415d-81a9-4773f27a85c9', N'df802c28-0344-4ce5-9d47-3309569ece4b', N'/Uploads/Products/ao-so-mi-modal-smd1420-3_20250205_150312.jpeg', 0)
GO
INSERT [dbo].[ProductImage] ([Id], [ProductId], [Image], [IsDefault]) VALUES (N'87e6442b-8c44-4de0-aa23-48c698968c81', N'12507b60-1fe3-441b-b760-1b6e2fda4dc8', N'/Uploads/Products/ao-so-mi-bamboo-smc410-2_20250206_001123.jpeg', 0)
GO
INSERT [dbo].[ProductImage] ([Id], [ProductId], [Image], [IsDefault]) VALUES (N'daa37bd4-62bb-49fa-bccf-58478eb6417a', N'12507b60-1fe3-441b-b760-1b6e2fda4dc8', N'/Uploads/Products/ao-so-mi-bamboo-smc410-1_20250206_001123.jpeg', 1)
GO
INSERT [dbo].[ProductImage] ([Id], [ProductId], [Image], [IsDefault]) VALUES (N'a99ff549-55e7-4a08-9fcf-7de92dfe3db5', N'fafa1eec-07cb-4631-b1dc-ffcee3ce8b05', N'/Uploads/Products/ao-so-mi-bamboo-smd1411-3_20250205_223040.jpeg', 0)
GO
INSERT [dbo].[ProductImage] ([Id], [ProductId], [Image], [IsDefault]) VALUES (N'd54c6957-27eb-4676-b890-82192ea8f625', N'10421d8d-9b76-4591-8545-9b0f7c4165e8', N'/Uploads/Products/ao-so-mi-cotton-smd1422-3_20250205_151604.jpeg', 0)
GO
INSERT [dbo].[ProductImage] ([Id], [ProductId], [Image], [IsDefault]) VALUES (N'650b8fb0-9a56-4276-ba6b-974383fb6a7a', N'3ad7c66a-125c-42c8-9698-c18d91ee1d81', N'/Uploads/Products/ao-so-mi-bamboo-smd1407-1_20250205_222755.jpeg', 1)
GO
INSERT [dbo].[ProductImage] ([Id], [ProductId], [Image], [IsDefault]) VALUES (N'285fe78e-eb25-4463-86f8-a2aa39954a6a', N'bf27bd25-3729-4827-8d16-20cc23e48f4e', N'/Uploads/Products/ao-so-mi-linen-smc480-1_20250205_234614.jpeg', 1)
GO
INSERT [dbo].[ProductImage] ([Id], [ProductId], [Image], [IsDefault]) VALUES (N'6b3690cc-1a4a-49ad-9a74-a42875992dad', N'10421d8d-9b76-4591-8545-9b0f7c4165e8', N'/Uploads/Products/ao-so-mi-cotton-smd1422-1_20250205_151604.jpeg', 1)
GO
INSERT [dbo].[ProductImage] ([Id], [ProductId], [Image], [IsDefault]) VALUES (N'0cceabd9-d1e7-4641-b47c-aa423f17f34b', N'0929e105-20f1-4e8e-92d8-25113f9f0c9c', N'/Uploads/Products/ao-so-mi-basic-smd1415-3_20250205_233236.jpeg', 0)
GO
INSERT [dbo].[ProductImage] ([Id], [ProductId], [Image], [IsDefault]) VALUES (N'006a28dd-b03f-49b6-85a3-ac66bb34472f', N'0929e105-20f1-4e8e-92d8-25113f9f0c9c', N'/Uploads/Products/ao-so-mi-basic-smd1415-1_20250205_233236.jpeg', 1)
GO
INSERT [dbo].[ProductImage] ([Id], [ProductId], [Image], [IsDefault]) VALUES (N'fc7ad2b6-d4c2-4959-b032-b544e0b0f91b', N'df802c28-0344-4ce5-9d47-3309569ece4b', N'/Uploads/Products/ao-so-mi-modal-smd1420-1_20250205_150312.jpeg', 1)
GO
INSERT [dbo].[ProductImage] ([Id], [ProductId], [Image], [IsDefault]) VALUES (N'897809f2-6dab-47c3-a396-c82ff3e6faea', N'c65520a9-78e9-4448-9624-0c6e0e5f39de', N'/Uploads/Products/ao-so-mi-cotton-smd1421-3_20250205_151948.jpeg', 0)
GO
INSERT [dbo].[ProductImage] ([Id], [ProductId], [Image], [IsDefault]) VALUES (N'726807c2-95a2-456a-b86a-de57d411b92a', N'12507b60-1fe3-441b-b760-1b6e2fda4dc8', N'/Uploads/Products/ao-so-mi-bamboo-smc410-3_20250206_001123.jpeg', 0)
GO
INSERT [dbo].[ProductImage] ([Id], [ProductId], [Image], [IsDefault]) VALUES (N'692c9658-6f61-481f-b34e-df8cd44212db', N'c65520a9-78e9-4448-9624-0c6e0e5f39de', N'/Uploads/Products/ao-so-mi-cotton-smd1421-2_20250205_151948.jpeg', 0)
GO
INSERT [dbo].[ProductImage] ([Id], [ProductId], [Image], [IsDefault]) VALUES (N'a1bebb20-f3bc-4860-bf51-ec3d3631dc0b', N'fafa1eec-07cb-4631-b1dc-ffcee3ce8b05', N'/Uploads/Products/ao-so-mi-bamboo-smd1411-1_20250205_223040.jpeg', 1)
GO
INSERT [dbo].[ProductImage] ([Id], [ProductId], [Image], [IsDefault]) VALUES (N'a8acd882-1a5f-42f5-9748-f39d4680f8a4', N'bf27bd25-3729-4827-8d16-20cc23e48f4e', N'/Uploads/Products/ao-so-mi-linen-smc480-3_20250205_234614.jpeg', 0)
GO
INSERT [dbo].[ProductImage] ([Id], [ProductId], [Image], [IsDefault]) VALUES (N'ddc7f591-d841-4cb5-ba9e-f418deb55c42', N'df802c28-0344-4ce5-9d47-3309569ece4b', N'/Uploads/Products/ao-so-mi-modal-smd1420-2_20250205_150312.jpeg', 0)
GO
INSERT [dbo].[ProductVariants] ([Id], [SKU], [ProductId], [Quantity], [IsActive], [CreateBy], [CreateDate], [ModifierDate], [ModifiedBy], [BasePrice], [PriceSale], [Size], [IsDefault], [Color], [ReservedStock]) VALUES (N'485655c3-8922-49c8-a120-057a6cd28913', N'SMD.1411-03', N'fafa1eec-07cb-4631-b1dc-ffcee3ce8b05', 10, 1, NULL, CAST(N'2025-02-05T22:30:07.6154904' AS DateTime2), CAST(N'2025-02-05T22:30:07.6154905' AS DateTime2), NULL, CAST(799000.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'XL', 0, N'Caro xanh rêu', 0)
GO
INSERT [dbo].[ProductVariants] ([Id], [SKU], [ProductId], [Quantity], [IsActive], [CreateBy], [CreateDate], [ModifierDate], [ModifiedBy], [BasePrice], [PriceSale], [Size], [IsDefault], [Color], [ReservedStock]) VALUES (N'0abb6be5-715f-4e35-b611-0d50b5baa824', N'SMD.1415-02', N'0929e105-20f1-4e8e-92d8-25113f9f0c9c', 10, 1, NULL, CAST(N'2025-02-05T23:32:33.0839508' AS DateTime2), CAST(N'2025-02-05T23:32:33.0839508' AS DateTime2), NULL, CAST(599000.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'L', 0, N'Tím than', 0)
GO
INSERT [dbo].[ProductVariants] ([Id], [SKU], [ProductId], [Quantity], [IsActive], [CreateBy], [CreateDate], [ModifierDate], [ModifiedBy], [BasePrice], [PriceSale], [Size], [IsDefault], [Color], [ReservedStock]) VALUES (N'2a21ed89-4880-4c8a-9d77-125e47df817c', N'SMD.1411-04', N'fafa1eec-07cb-4631-b1dc-ffcee3ce8b05', 10, 1, NULL, CAST(N'2025-02-05T22:30:07.6155147' AS DateTime2), CAST(N'2025-02-05T22:30:07.6155148' AS DateTime2), NULL, CAST(799000.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'XXL', 0, N'Caro xanh rêu', 0)
GO
INSERT [dbo].[ProductVariants] ([Id], [SKU], [ProductId], [Quantity], [IsActive], [CreateBy], [CreateDate], [ModifierDate], [ModifiedBy], [BasePrice], [PriceSale], [Size], [IsDefault], [Color], [ReservedStock]) VALUES (N'08fc222c-5ded-4215-a686-1712021f7cac', N'SMD.1422-02', N'10421d8d-9b76-4591-8545-9b0f7c4165e8', 10, 1, NULL, CAST(N'2025-02-05T15:16:04.5189758' AS DateTime2), CAST(N'2025-02-05T15:16:04.5189759' AS DateTime2), NULL, CAST(519000.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'L', 0, N'Be', 0)
GO
INSERT [dbo].[ProductVariants] ([Id], [SKU], [ProductId], [Quantity], [IsActive], [CreateBy], [CreateDate], [ModifierDate], [ModifiedBy], [BasePrice], [PriceSale], [Size], [IsDefault], [Color], [ReservedStock]) VALUES (N'c2d7f7fa-9a15-4d29-8dce-3586b5dda05a', N'SMD.1422-01', N'10421d8d-9b76-4591-8545-9b0f7c4165e8', 10, 1, NULL, CAST(N'2025-02-05T15:16:04.5189066' AS DateTime2), CAST(N'2025-02-05T15:16:04.5189071' AS DateTime2), NULL, CAST(519000.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'M', 1, N'Be', 0)
GO
INSERT [dbo].[ProductVariants] ([Id], [SKU], [ProductId], [Quantity], [IsActive], [CreateBy], [CreateDate], [ModifierDate], [ModifiedBy], [BasePrice], [PriceSale], [Size], [IsDefault], [Color], [ReservedStock]) VALUES (N'e8556d57-3294-468a-9362-387e19bf42fc', N'SMD.1420-03', N'df802c28-0344-4ce5-9d47-3309569ece4b', 10, 1, NULL, CAST(N'2025-02-05T15:03:12.3595951' AS DateTime2), CAST(N'2025-02-05T15:03:12.3595951' AS DateTime2), NULL, CAST(599000.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'XL', 0, N'Trắng', 0)
GO
INSERT [dbo].[ProductVariants] ([Id], [SKU], [ProductId], [Quantity], [IsActive], [CreateBy], [CreateDate], [ModifierDate], [ModifiedBy], [BasePrice], [PriceSale], [Size], [IsDefault], [Color], [ReservedStock]) VALUES (N'0e89f07f-bc3f-4919-85f9-3dd5c7a1aa84', N'SMD.1415-03', N'0929e105-20f1-4e8e-92d8-25113f9f0c9c', 10, 1, NULL, CAST(N'2025-02-05T23:32:33.0839799' AS DateTime2), CAST(N'2025-02-05T23:32:33.0839800' AS DateTime2), NULL, CAST(599000.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'XL', 0, N'Tím than', 0)
GO
INSERT [dbo].[ProductVariants] ([Id], [SKU], [ProductId], [Quantity], [IsActive], [CreateBy], [CreateDate], [ModifierDate], [ModifiedBy], [BasePrice], [PriceSale], [Size], [IsDefault], [Color], [ReservedStock]) VALUES (N'fb38a9f9-3b09-480c-a984-5bde111e6437', N'SMC.410-03', N'12507b60-1fe3-441b-b760-1b6e2fda4dc8', 10, 1, NULL, CAST(N'2025-02-06T00:11:23.4875064' AS DateTime2), CAST(N'2025-02-06T00:11:23.4875066' AS DateTime2), NULL, CAST(659000.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'XL', 0, N'Caro to trắng, vàng, đen', 0)
GO
INSERT [dbo].[ProductVariants] ([Id], [SKU], [ProductId], [Quantity], [IsActive], [CreateBy], [CreateDate], [ModifierDate], [ModifiedBy], [BasePrice], [PriceSale], [Size], [IsDefault], [Color], [ReservedStock]) VALUES (N'843e1b48-8aa6-4d07-bfc3-5d681879699f', N'SMC.480-03', N'bf27bd25-3729-4827-8d16-20cc23e48f4e', 10, 1, NULL, CAST(N'2025-02-05T23:46:14.1722798' AS DateTime2), CAST(N'2025-02-05T23:46:14.1722800' AS DateTime2), NULL, CAST(1099000.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'XL', 0, N'Xanh', 0)
GO
INSERT [dbo].[ProductVariants] ([Id], [SKU], [ProductId], [Quantity], [IsActive], [CreateBy], [CreateDate], [ModifierDate], [ModifiedBy], [BasePrice], [PriceSale], [Size], [IsDefault], [Color], [ReservedStock]) VALUES (N'ea254d80-b061-4370-8137-6db72f91f27e', N'SMD.1420-06', N'df802c28-0344-4ce5-9d47-3309569ece4b', 10, 1, NULL, CAST(N'2025-02-08T00:24:22.5934198' AS DateTime2), CAST(N'2025-02-08T00:24:22.5934205' AS DateTime2), NULL, CAST(599000.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'M', 0, N'Đen', 0)
GO
INSERT [dbo].[ProductVariants] ([Id], [SKU], [ProductId], [Quantity], [IsActive], [CreateBy], [CreateDate], [ModifierDate], [ModifiedBy], [BasePrice], [PriceSale], [Size], [IsDefault], [Color], [ReservedStock]) VALUES (N'dbb08a16-e77f-49b2-9d85-73ea64f96485', N'SMD.1415-04', N'0929e105-20f1-4e8e-92d8-25113f9f0c9c', 10, 1, NULL, CAST(N'2025-02-05T23:32:33.0840087' AS DateTime2), CAST(N'2025-02-05T23:32:33.0840088' AS DateTime2), NULL, CAST(599000.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'XXL', 0, N'Tím than', 0)
GO
INSERT [dbo].[ProductVariants] ([Id], [SKU], [ProductId], [Quantity], [IsActive], [CreateBy], [CreateDate], [ModifierDate], [ModifiedBy], [BasePrice], [PriceSale], [Size], [IsDefault], [Color], [ReservedStock]) VALUES (N'fe5896ba-925e-49b7-9209-7cfbb4814448', N'SMC.480-04', N'bf27bd25-3729-4827-8d16-20cc23e48f4e', 10, 1, NULL, CAST(N'2025-02-05T23:46:14.1723143' AS DateTime2), CAST(N'2025-02-05T23:46:14.1723144' AS DateTime2), NULL, CAST(1099000.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'XXL', 0, N'Xanh', 0)
GO
INSERT [dbo].[ProductVariants] ([Id], [SKU], [ProductId], [Quantity], [IsActive], [CreateBy], [CreateDate], [ModifierDate], [ModifiedBy], [BasePrice], [PriceSale], [Size], [IsDefault], [Color], [ReservedStock]) VALUES (N'c340db51-1cb1-42c6-a8cd-9938c4a1d6b9', N'SMD.1415-01', N'0929e105-20f1-4e8e-92d8-25113f9f0c9c', 10, 1, NULL, CAST(N'2025-02-05T23:32:33.0838808' AS DateTime2), CAST(N'2025-02-05T23:32:33.0838811' AS DateTime2), NULL, CAST(599000.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'M', 1, N'Tím than', 0)
GO
INSERT [dbo].[ProductVariants] ([Id], [SKU], [ProductId], [Quantity], [IsActive], [CreateBy], [CreateDate], [ModifierDate], [ModifiedBy], [BasePrice], [PriceSale], [Size], [IsDefault], [Color], [ReservedStock]) VALUES (N'33cedc32-59a5-49b5-9643-a16203c9f0b4', N'SMD.1421-03', N'c65520a9-78e9-4448-9624-0c6e0e5f39de', 10, 1, NULL, CAST(N'2025-02-05T15:19:48.4046404' AS DateTime2), CAST(N'2025-02-05T15:19:48.4046407' AS DateTime2), NULL, CAST(519000.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'XL', 0, N'Trắng', 0)
GO
INSERT [dbo].[ProductVariants] ([Id], [SKU], [ProductId], [Quantity], [IsActive], [CreateBy], [CreateDate], [ModifierDate], [ModifiedBy], [BasePrice], [PriceSale], [Size], [IsDefault], [Color], [ReservedStock]) VALUES (N'a1d9b804-e3ef-4c3c-be80-a3229ceea4fe', N'SMD.1407-03', N'3ad7c66a-125c-42c8-9698-c18d91ee1d81', 10, 1, NULL, CAST(N'2025-02-05T22:27:15.2822469' AS DateTime2), CAST(N'2025-02-05T22:27:15.2822470' AS DateTime2), NULL, CAST(799000.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'XL', 0, N'Nâu nhạt kẻ sọc', 0)
GO
INSERT [dbo].[ProductVariants] ([Id], [SKU], [ProductId], [Quantity], [IsActive], [CreateBy], [CreateDate], [ModifierDate], [ModifiedBy], [BasePrice], [PriceSale], [Size], [IsDefault], [Color], [ReservedStock]) VALUES (N'dbb6411f-8588-44e2-96e0-a3769b720e9d', N'SMD.1420-04', N'df802c28-0344-4ce5-9d47-3309569ece4b', 10, 1, NULL, CAST(N'2025-02-05T15:03:12.3596180' AS DateTime2), CAST(N'2025-02-05T15:03:12.3596181' AS DateTime2), NULL, CAST(599000.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'XXL', 0, N'Trắng', 0)
GO
INSERT [dbo].[ProductVariants] ([Id], [SKU], [ProductId], [Quantity], [IsActive], [CreateBy], [CreateDate], [ModifierDate], [ModifiedBy], [BasePrice], [PriceSale], [Size], [IsDefault], [Color], [ReservedStock]) VALUES (N'17b8a9ad-9e9e-4211-a704-ae77d5b76ae0', N'SMD.1420-02', N'df802c28-0344-4ce5-9d47-3309569ece4b', 10, 1, NULL, CAST(N'2025-02-05T15:03:12.3595483' AS DateTime2), CAST(N'2025-02-05T15:03:12.3595495' AS DateTime2), NULL, CAST(569000.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'L', 0, N'Trắng', 0)
GO
INSERT [dbo].[ProductVariants] ([Id], [SKU], [ProductId], [Quantity], [IsActive], [CreateBy], [CreateDate], [ModifierDate], [ModifiedBy], [BasePrice], [PriceSale], [Size], [IsDefault], [Color], [ReservedStock]) VALUES (N'ca7eb51c-5124-4af0-aa5f-b9cad5af30c0', N'SMD.1422-03', N'10421d8d-9b76-4591-8545-9b0f7c4165e8', 10, 1, NULL, CAST(N'2025-02-05T15:16:04.5190007' AS DateTime2), CAST(N'2025-02-05T15:16:04.5190007' AS DateTime2), NULL, CAST(519000.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'XL', 0, N'Be', 0)
GO
INSERT [dbo].[ProductVariants] ([Id], [SKU], [ProductId], [Quantity], [IsActive], [CreateBy], [CreateDate], [ModifierDate], [ModifiedBy], [BasePrice], [PriceSale], [Size], [IsDefault], [Color], [ReservedStock]) VALUES (N'e8b733f9-6b8a-42d7-b493-bb834a73e90c', N'SMD.1407-04', N'3ad7c66a-125c-42c8-9698-c18d91ee1d81', 10, 1, NULL, CAST(N'2025-02-05T22:27:15.2822803' AS DateTime2), CAST(N'2025-02-05T22:27:15.2822804' AS DateTime2), NULL, CAST(799000.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'XXL', 0, N'Nâu nhạt kẻ sọc', 0)
GO
INSERT [dbo].[ProductVariants] ([Id], [SKU], [ProductId], [Quantity], [IsActive], [CreateBy], [CreateDate], [ModifierDate], [ModifiedBy], [BasePrice], [PriceSale], [Size], [IsDefault], [Color], [ReservedStock]) VALUES (N'5455f985-a37b-4613-912f-bc4c6db65cb7', N'SMD.1407-01', N'3ad7c66a-125c-42c8-9698-c18d91ee1d81', 10, 1, NULL, CAST(N'2025-02-05T22:27:15.2821451' AS DateTime2), CAST(N'2025-02-05T22:27:15.2821457' AS DateTime2), NULL, CAST(799000.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'M', 1, N'Nâu nhạt kẻ sọc', 0)
GO
INSERT [dbo].[ProductVariants] ([Id], [SKU], [ProductId], [Quantity], [IsActive], [CreateBy], [CreateDate], [ModifierDate], [ModifiedBy], [BasePrice], [PriceSale], [Size], [IsDefault], [Color], [ReservedStock]) VALUES (N'edbad518-43e0-42ef-9a7b-c77c070b7195', N'SMD.1421-01', N'c65520a9-78e9-4448-9624-0c6e0e5f39de', 9, 1, NULL, CAST(N'2025-02-05T15:19:48.4045203' AS DateTime2), CAST(N'2025-02-05T15:19:48.4045207' AS DateTime2), NULL, CAST(519000.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'M', 1, N'Trắng', 0)
GO
INSERT [dbo].[ProductVariants] ([Id], [SKU], [ProductId], [Quantity], [IsActive], [CreateBy], [CreateDate], [ModifierDate], [ModifiedBy], [BasePrice], [PriceSale], [Size], [IsDefault], [Color], [ReservedStock]) VALUES (N'b76936ac-d5c6-4b4c-908b-c8be76e163c4', N'SMC.410-04', N'12507b60-1fe3-441b-b760-1b6e2fda4dc8', 10, 1, NULL, CAST(N'2025-02-06T00:11:23.4875378' AS DateTime2), CAST(N'2025-02-06T00:11:23.4875379' AS DateTime2), NULL, CAST(659000.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'XXL', 0, N'Caro to trắng, vàng, đen', 0)
GO
INSERT [dbo].[ProductVariants] ([Id], [SKU], [ProductId], [Quantity], [IsActive], [CreateBy], [CreateDate], [ModifierDate], [ModifiedBy], [BasePrice], [PriceSale], [Size], [IsDefault], [Color], [ReservedStock]) VALUES (N'1ea157f2-2f69-452e-81b4-d15de1ce090e', N'SMD.1411-02', N'fafa1eec-07cb-4631-b1dc-ffcee3ce8b05', 10, 1, NULL, CAST(N'2025-02-05T22:30:07.6154745' AS DateTime2), CAST(N'2025-02-05T22:30:07.6154745' AS DateTime2), NULL, CAST(799000.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'L', 0, N'Caro xanh rêu', 0)
GO
INSERT [dbo].[ProductVariants] ([Id], [SKU], [ProductId], [Quantity], [IsActive], [CreateBy], [CreateDate], [ModifierDate], [ModifiedBy], [BasePrice], [PriceSale], [Size], [IsDefault], [Color], [ReservedStock]) VALUES (N'd4ba9a3f-0518-4c82-a32b-d78c95f5af19', N'SMC.410-01', N'12507b60-1fe3-441b-b760-1b6e2fda4dc8', 10, 1, NULL, CAST(N'2025-02-06T00:11:23.4873994' AS DateTime2), CAST(N'2025-02-06T00:11:23.4874000' AS DateTime2), NULL, CAST(659000.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'M', 0, N'Caro to trắng, vàng, đen', 0)
GO
INSERT [dbo].[ProductVariants] ([Id], [SKU], [ProductId], [Quantity], [IsActive], [CreateBy], [CreateDate], [ModifierDate], [ModifiedBy], [BasePrice], [PriceSale], [Size], [IsDefault], [Color], [ReservedStock]) VALUES (N'8f737aaa-6b30-4607-965c-e0ea383c89fd', N'SMD.1421-02', N'c65520a9-78e9-4448-9624-0c6e0e5f39de', 10, 1, NULL, CAST(N'2025-02-05T15:19:48.4046013' AS DateTime2), CAST(N'2025-02-05T15:19:48.4046014' AS DateTime2), NULL, CAST(519000.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'L', 0, N'Trắng', 0)
GO
INSERT [dbo].[ProductVariants] ([Id], [SKU], [ProductId], [Quantity], [IsActive], [CreateBy], [CreateDate], [ModifierDate], [ModifiedBy], [BasePrice], [PriceSale], [Size], [IsDefault], [Color], [ReservedStock]) VALUES (N'e58617c9-a9de-4c6a-a410-e1dfd0935ae3', N'SMD.1420-01', N'df802c28-0344-4ce5-9d47-3309569ece4b', 10, 1, NULL, CAST(N'2025-02-05T15:03:12.3569882' AS DateTime2), CAST(N'2025-02-05T15:03:12.3569908' AS DateTime2), NULL, CAST(599000.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'M', 1, N'Trắng', 0)
GO
INSERT [dbo].[ProductVariants] ([Id], [SKU], [ProductId], [Quantity], [IsActive], [CreateBy], [CreateDate], [ModifierDate], [ModifiedBy], [BasePrice], [PriceSale], [Size], [IsDefault], [Color], [ReservedStock]) VALUES (N'0c0c2092-9ef3-43fe-b6e2-e421e3eb3eb3', N'SMC.410-02', N'12507b60-1fe3-441b-b760-1b6e2fda4dc8', 10, 1, NULL, CAST(N'2025-02-06T00:11:23.4874695' AS DateTime2), CAST(N'2025-02-06T00:11:23.4874697' AS DateTime2), NULL, CAST(669000.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'L', 1, N'Caro to trắng, vàng, đen', 0)
GO
INSERT [dbo].[ProductVariants] ([Id], [SKU], [ProductId], [Quantity], [IsActive], [CreateBy], [CreateDate], [ModifierDate], [ModifiedBy], [BasePrice], [PriceSale], [Size], [IsDefault], [Color], [ReservedStock]) VALUES (N'97117a87-e61a-40b8-b3ef-e8db5af636f1', N'SMC.480-02', N'bf27bd25-3729-4827-8d16-20cc23e48f4e', 10, 1, NULL, CAST(N'2025-02-05T23:46:14.1722220' AS DateTime2), CAST(N'2025-02-05T23:46:14.1722232' AS DateTime2), NULL, CAST(1099000.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'L', 0, N'Xanh', 0)
GO
INSERT [dbo].[ProductVariants] ([Id], [SKU], [ProductId], [Quantity], [IsActive], [CreateBy], [CreateDate], [ModifierDate], [ModifiedBy], [BasePrice], [PriceSale], [Size], [IsDefault], [Color], [ReservedStock]) VALUES (N'28b4a833-2497-4c68-8f40-ed20ce8b8243', N'SMC.480-01', N'bf27bd25-3729-4827-8d16-20cc23e48f4e', 10, 1, NULL, CAST(N'2025-02-05T23:46:14.1699291' AS DateTime2), CAST(N'2025-02-05T23:46:14.1699326' AS DateTime2), NULL, CAST(1099000.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'M', 1, N'Xanh', 0)
GO
INSERT [dbo].[ProductVariants] ([Id], [SKU], [ProductId], [Quantity], [IsActive], [CreateBy], [CreateDate], [ModifierDate], [ModifiedBy], [BasePrice], [PriceSale], [Size], [IsDefault], [Color], [ReservedStock]) VALUES (N'a82976ac-b131-41bb-a80f-f0444019acac', N'SMD.1411-01', N'fafa1eec-07cb-4631-b1dc-ffcee3ce8b05', 10, 1, NULL, CAST(N'2025-02-05T22:30:07.6154313' AS DateTime2), CAST(N'2025-02-05T22:30:07.6154313' AS DateTime2), NULL, CAST(799000.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'M', 1, N'Caro xanh rêu', 0)
GO
INSERT [dbo].[ProductVariants] ([Id], [SKU], [ProductId], [Quantity], [IsActive], [CreateBy], [CreateDate], [ModifierDate], [ModifiedBy], [BasePrice], [PriceSale], [Size], [IsDefault], [Color], [ReservedStock]) VALUES (N'ba346695-1a24-4086-8f83-fb04d26f6796', N'SMD.1407-02', N'3ad7c66a-125c-42c8-9698-c18d91ee1d81', 10, 1, NULL, CAST(N'2025-02-05T22:27:15.2822191' AS DateTime2), CAST(N'2025-02-05T22:27:15.2822193' AS DateTime2), NULL, CAST(799000.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'L', 0, N'Nâu nhạt kẻ sọc', 0)
GO
INSERT [dbo].[ProductVariants] ([Id], [SKU], [ProductId], [Quantity], [IsActive], [CreateBy], [CreateDate], [ModifierDate], [ModifiedBy], [BasePrice], [PriceSale], [Size], [IsDefault], [Color], [ReservedStock]) VALUES (N'c5b63bcf-25c1-4353-b143-fba339eb5f81', N'SMD.1422-04', N'10421d8d-9b76-4591-8545-9b0f7c4165e8', 10, 1, NULL, CAST(N'2025-02-05T15:16:04.5190263' AS DateTime2), CAST(N'2025-02-05T15:16:04.5190263' AS DateTime2), NULL, CAST(519000.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'XXL', 0, N'Be', 0)
GO
INSERT [dbo].[ProductVariants] ([Id], [SKU], [ProductId], [Quantity], [IsActive], [CreateBy], [CreateDate], [ModifierDate], [ModifiedBy], [BasePrice], [PriceSale], [Size], [IsDefault], [Color], [ReservedStock]) VALUES (N'fe0dc39c-11ea-494b-9a5b-fca317e7a688', N'SMD.1421-04', N'c65520a9-78e9-4448-9624-0c6e0e5f39de', 10, 1, NULL, CAST(N'2025-02-05T15:19:48.4046982' AS DateTime2), CAST(N'2025-02-05T15:19:48.4046984' AS DateTime2), NULL, CAST(519000.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'XXL', 0, N'Trắng', 0)
GO
SET IDENTITY_INSERT [dbo].[RoleClaims] ON 
GO
INSERT [dbo].[RoleClaims] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (1, N'45c1a0c4-1050-477f-9598-3f85db1d4194', N'Permission', N'ViewRoles')
GO
INSERT [dbo].[RoleClaims] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (2, N'45c1a0c4-1050-477f-9598-3f85db1d4194', N'Permission', N'EditNews')
GO
INSERT [dbo].[RoleClaims] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (3, N'45c1a0c4-1050-477f-9598-3f85db1d4194', N'Permission', N'DeleteNews')
GO
INSERT [dbo].[RoleClaims] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (4, N'45c1a0c4-1050-477f-9598-3f85db1d4194', N'Permission', N'EditProductCategory')
GO
INSERT [dbo].[RoleClaims] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (5, N'45c1a0c4-1050-477f-9598-3f85db1d4194', N'Permission', N'DeletePost')
GO
INSERT [dbo].[RoleClaims] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (6, N'45c1a0c4-1050-477f-9598-3f85db1d4194', N'Permission', N'EditPost')
GO
INSERT [dbo].[RoleClaims] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (7, N'45c1a0c4-1050-477f-9598-3f85db1d4194', N'Permission', N'CreatePost')
GO
INSERT [dbo].[RoleClaims] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (8, N'45c1a0c4-1050-477f-9598-3f85db1d4194', N'Permission', N'ViewUserDetail')
GO
INSERT [dbo].[RoleClaims] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (9, N'45c1a0c4-1050-477f-9598-3f85db1d4194', N'Permission', N'CreateUser')
GO
INSERT [dbo].[RoleClaims] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (10, N'45c1a0c4-1050-477f-9598-3f85db1d4194', N'Permission', N'EditUser')
GO
INSERT [dbo].[RoleClaims] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (11, N'45c1a0c4-1050-477f-9598-3f85db1d4194', N'Permission', N'CreateProduct')
GO
INSERT [dbo].[RoleClaims] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (12, N'45c1a0c4-1050-477f-9598-3f85db1d4194', N'Permission', N'ViewProduct')
GO
INSERT [dbo].[RoleClaims] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (13, N'45c1a0c4-1050-477f-9598-3f85db1d4194', N'Permission', N'DeleteProductCategory')
GO
INSERT [dbo].[RoleClaims] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (14, N'45c1a0c4-1050-477f-9598-3f85db1d4194', N'Permission', N'EditProduct')
GO
INSERT [dbo].[RoleClaims] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (15, N'45c1a0c4-1050-477f-9598-3f85db1d4194', N'Permission', N'ViewProductDetail')
GO
INSERT [dbo].[RoleClaims] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (16, N'45c1a0c4-1050-477f-9598-3f85db1d4194', N'Permission', N'ViewPost')
GO
INSERT [dbo].[RoleClaims] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (17, N'45c1a0c4-1050-477f-9598-3f85db1d4194', N'Permission', N'DeleteUser')
GO
INSERT [dbo].[RoleClaims] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (18, N'45c1a0c4-1050-477f-9598-3f85db1d4194', N'Permission', N'ViewNews')
GO
INSERT [dbo].[RoleClaims] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (19, N'45c1a0c4-1050-477f-9598-3f85db1d4194', N'Permission', N'ViewNewsDetail')
GO
INSERT [dbo].[RoleClaims] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (20, N'45c1a0c4-1050-477f-9598-3f85db1d4194', N'Permission', N'CreateProductCategory')
GO
INSERT [dbo].[RoleClaims] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (21, N'45c1a0c4-1050-477f-9598-3f85db1d4194', N'Permission', N'ViewProductCategoryDetail')
GO
INSERT [dbo].[RoleClaims] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (22, N'45c1a0c4-1050-477f-9598-3f85db1d4194', N'Permission', N'DeleteProduct')
GO
INSERT [dbo].[RoleClaims] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (23, N'45c1a0c4-1050-477f-9598-3f85db1d4194', N'Permission', N'ViewPostDetail')
GO
INSERT [dbo].[RoleClaims] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (24, N'45c1a0c4-1050-477f-9598-3f85db1d4194', N'Permission', N'ManageRoles')
GO
INSERT [dbo].[RoleClaims] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (25, N'45c1a0c4-1050-477f-9598-3f85db1d4194', N'Permission', N'EditRolePermission')
GO
INSERT [dbo].[RoleClaims] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (26, N'45c1a0c4-1050-477f-9598-3f85db1d4194', N'Permission', N'CreateNews')
GO
INSERT [dbo].[RoleClaims] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (27, N'45c1a0c4-1050-477f-9598-3f85db1d4194', N'Permission', N'ViewProductCategory')
GO
INSERT [dbo].[RoleClaims] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (28, N'45c1a0c4-1050-477f-9598-3f85db1d4194', N'Permission', N'AccessAdminPage')
GO
INSERT [dbo].[RoleClaims] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (29, N'45c1a0c4-1050-477f-9598-3f85db1d4194', N'Permission', N'ViewPermissions')
GO
INSERT [dbo].[RoleClaims] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (31, N'06cda887-4f5d-40ff-b27b-a8f2f4bcf91d', N'Permission', N'AccessAdminPage')
GO
INSERT [dbo].[RoleClaims] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (1002, N'45c1a0c4-1050-477f-9598-3f85db1d4194', N'Permission', N'ViewUsers')
GO
INSERT [dbo].[RoleClaims] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (1003, N'45c1a0c4-1050-477f-9598-3f85db1d4194', N'Permission', N'UpdateProfile')
GO
INSERT [dbo].[RoleClaims] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (1004, N'45c1a0c4-1050-477f-9598-3f85db1d4194', N'Permission', N'ViewProfile')
GO
INSERT [dbo].[RoleClaims] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (1005, N'06cda887-4f5d-40ff-b27b-a8f2f4bcf91d', N'Permission', N'UpdateProfile')
GO
INSERT [dbo].[RoleClaims] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (1006, N'06cda887-4f5d-40ff-b27b-a8f2f4bcf91d', N'Permission', N'ViewProfile')
GO
SET IDENTITY_INSERT [dbo].[RoleClaims] OFF
GO
INSERT [dbo].[Roles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'06cda887-4f5d-40ff-b27b-a8f2f4bcf91d', N'Employee', N'EMPLOYEE', N'e6b80123-2502-49cd-83a4-29d764a6e3e6')
GO
INSERT [dbo].[Roles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'45c1a0c4-1050-477f-9598-3f85db1d4194', N'Admin', N'ADMIN', N'690a9368-f3be-4c1c-871d-57162354705a')
GO
INSERT [dbo].[Roles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'ac628b6a-86f0-4cd9-a9db-1b67ecd01042', N'Customer', N'CUSTOMER', NULL)
GO
INSERT [dbo].[SettingConfiguration] ([SettingKey], [SettingValue]) VALUES (N'Address', N'Hoàng Mai, Hà Nội, Việt Nam')
GO
INSERT [dbo].[SettingConfiguration] ([SettingKey], [SettingValue]) VALUES (N'Email', N'valarianguyetvalari.a29.45@gmail.com')
GO
INSERT [dbo].[SettingConfiguration] ([SettingKey], [SettingValue]) VALUES (N'Hotline', N'0123 456 789')
GO
INSERT [dbo].[SettingConfiguration] ([SettingKey], [SettingValue]) VALUES (N'Logo', N'/Uploads/Logo/logo-01.png')
GO
INSERT [dbo].[SettingConfiguration] ([SettingKey], [SettingValue]) VALUES (N'Title', N'HShop')
GO
INSERT [dbo].[Transactions] ([TransactionId], [OrderId], [CreatedAt]) VALUES (N'6387610273074306966311', N'2d1b2137-ae0d-40d8-b702-fd91410d40b3', CAST(N'2025-02-25T17:52:10.7431770' AS DateTime2))
GO
SET IDENTITY_INSERT [dbo].[UserClaims] ON 
GO
INSERT [dbo].[UserClaims] ([Id], [UserId], [ClaimType], [ClaimValue]) VALUES (1, N'1d42a8d4-3d78-43de-88aa-80ab3d66765e', N'UserAccess', N'AccessAdminPage')
GO
INSERT [dbo].[UserClaims] ([Id], [UserId], [ClaimType], [ClaimValue]) VALUES (2, N'7caf8c91-e06b-4260-bcf6-77c5a3f83fef', N'UserAccess', N'AccessAdminPage')
GO
INSERT [dbo].[UserClaims] ([Id], [UserId], [ClaimType], [ClaimValue]) VALUES (3, N'90820a1b-cacb-4186-8b68-8e258b6eac53', N'UserAccess', N'AccessCustomerPage')
GO
INSERT [dbo].[UserClaims] ([Id], [UserId], [ClaimType], [ClaimValue]) VALUES (2004, N'eb6506f9-e0ba-4e5f-a054-608a80bbbdd1', N'UserAccess', N'AccessCustomerPage')
GO
SET IDENTITY_INSERT [dbo].[UserClaims] OFF
GO
INSERT [dbo].[UserLogins] ([LoginProvider], [ProviderKey], [ProviderDisplayName], [UserId]) VALUES (N'Google', N'111339470320141899958', N'Google', N'eb6506f9-e0ba-4e5f-a054-608a80bbbdd1')
GO
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (N'1d42a8d4-3d78-43de-88aa-80ab3d66765e', N'06cda887-4f5d-40ff-b27b-a8f2f4bcf91d')
GO
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (N'7caf8c91-e06b-4260-bcf6-77c5a3f83fef', N'45c1a0c4-1050-477f-9598-3f85db1d4194')
GO
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (N'90820a1b-cacb-4186-8b68-8e258b6eac53', N'ac628b6a-86f0-4cd9-a9db-1b67ecd01042')
GO
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (N'eb6506f9-e0ba-4e5f-a054-608a80bbbdd1', N'ac628b6a-86f0-4cd9-a9db-1b67ecd01042')
GO
INSERT [dbo].[Users] ([Id], [FullName], [Address], [BirthDate], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [LastPasswordResetRequest], [FailedPasswordResetAttempts], [PasswordResetLockoutEnd], [AvatarUrl]) VALUES (N'1d42a8d4-3d78-43de-88aa-80ab3d66765e', N'Nguyễn Tùng Dương', N'Hà Nội', CAST(N'2025-02-16T00:00:00.0000000' AS DateTime2), N'nv01', N'NV01', N'hoadepzaivl@livinitlarge.net', N'HOADEPZAIVL@LIVINITLARGE.NET', 1, N'AQAAAAIAAYagAAAAEEc1/hF0Mc1EocP+3atfN7IaQpMPgm9GixB610PltbFDU45p6RlUJAGxrV2d3K9ujQ==', N'HCGFXSHNP5267R3R3ODU22ZQOD2PUFQ4', N'f033be50-e991-422b-9ad4-633efac10c90', N'0234567889', 0, 0, NULL, 1, 0, CAST(N'2025-02-15T15:39:37.8009988' AS DateTime2), 0, NULL, N'/Uploads/ProfileAdmin/imageavt_20250218_193653.jpg')
GO
INSERT [dbo].[Users] ([Id], [FullName], [Address], [BirthDate], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [LastPasswordResetRequest], [FailedPasswordResetAttempts], [PasswordResetLockoutEnd], [AvatarUrl]) VALUES (N'7caf8c91-e06b-4260-bcf6-77c5a3f83fef', N'Ngô Xuân Hoà', N'Hà Nội', CAST(N'2025-02-15T00:00:00.0000000' AS DateTime2), N'admin', N'ADMIN', N'hoa@gmail.com', N'HOA@GMAIL.COM', 1, N'AQAAAAIAAYagAAAAEEc1/hF0Mc1EocP+3atfN7IaQpMPgm9GixB610PltbFDU45p6RlUJAGxrV2d3K9ujQ==', N'7O4JHIIV65EJ7DQCDY6NOEAI7IELEQPP', N'e26ad745-44bd-4ead-816a-35d1d4f96e4e', N'0323567889', 0, 0, NULL, 1, 0, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[Users] ([Id], [FullName], [Address], [BirthDate], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [LastPasswordResetRequest], [FailedPasswordResetAttempts], [PasswordResetLockoutEnd], [AvatarUrl]) VALUES (N'90820a1b-cacb-4186-8b68-8e258b6eac53', N'Nguyễn Văn Nam', N'Hà Nội', CAST(N'2025-02-21T00:00:00.0000000' AS DateTime2), N'nvnam', N'NVNAM', N'hoamai@yogirt.com', N'HOAMAI@YOGIRT.COM', 1, N'AQAAAAIAAYagAAAAEGYZ5ZQ3P0Bw1OJ0Fh6OEjYFgXqgdi0j2DqRaPatq5tLs8zHVzmDZVJ7leJqzjsvew==', N'5ITROC4ZSOAMAM4TQJNOPDLV2EEH7ZHV', N'ebb561e7-b69e-4d18-80d1-3eb999810bf2', N'0323567889', 0, 0, NULL, 1, 0, CAST(N'2025-02-20T12:31:39.2304715' AS DateTime2), 0, NULL, N'/Uploads/ProfileCustomer/anhgaixinh_20250220_192749.jpg')
GO
INSERT [dbo].[Users] ([Id], [FullName], [Address], [BirthDate], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [LastPasswordResetRequest], [FailedPasswordResetAttempts], [PasswordResetLockoutEnd], [AvatarUrl]) VALUES (N'eb6506f9-e0ba-4e5f-a054-608a80bbbdd1', N'Ngô Xuân Hoà 2', NULL, NULL, N'nxhoa2', N'NXHOA2', N'ngohoa9003@gmail.com', N'NGOHOA9003@GMAIL.COM', 1, NULL, N'PMN74XK45NYU6237QGIVRM67RMKHKO6B', N'395db20e-91df-4760-adce-e88755b7cda0', NULL, 0, 0, NULL, 1, 0, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[Wishlists] ([Id], [ProductId], [UserId], [CreateDate]) VALUES (N'01d4f385-ee8a-441a-b76d-4e25523d2f89', N'bf27bd25-3729-4827-8d16-20cc23e48f4e', N'90820a1b-cacb-4186-8b68-8e258b6eac53', CAST(N'2025-03-01T01:10:40.7706055' AS DateTime2))
GO
INSERT [dbo].[Wishlists] ([Id], [ProductId], [UserId], [CreateDate]) VALUES (N'89abcad9-148a-4fc4-8c4d-c8cf007c2494', N'c65520a9-78e9-4448-9624-0c6e0e5f39de', N'90820a1b-cacb-4186-8b68-8e258b6eac53', CAST(N'2025-03-01T02:16:44.1877049' AS DateTime2))
GO
INSERT [dbo].[Wishlists] ([Id], [ProductId], [UserId], [CreateDate]) VALUES (N'9340b4a8-1efd-4c20-a9d0-f171289cad31', N'3ad7c66a-125c-42c8-9698-c18d91ee1d81', N'90820a1b-cacb-4186-8b68-8e258b6eac53', CAST(N'2025-03-01T02:16:54.9146985' AS DateTime2))
GO
ALTER TABLE [dbo].[Banner] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsShow]
GO
ALTER TABLE [dbo].[Claims] ADD  DEFAULT (N'') FOR [TypeClaim]
GO
ALTER TABLE [dbo].[Contact] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsRead]
GO
ALTER TABLE [dbo].[Order] ADD  DEFAULT (N'') FOR [Address]
GO
ALTER TABLE [dbo].[Order] ADD  DEFAULT (N'') FOR [Status]
GO
ALTER TABLE [dbo].[Order] ADD  DEFAULT (N'') FOR [Email]
GO
ALTER TABLE [dbo].[Order] ADD  DEFAULT (N'') FOR [FullName]
GO
ALTER TABLE [dbo].[Order] ADD  DEFAULT (N'') FOR [Phone]
GO
ALTER TABLE [dbo].[Order] ADD  DEFAULT (N'') FOR [TypePayment]
GO
ALTER TABLE [dbo].[OrderDetail] ADD  DEFAULT ('00000000-0000-0000-0000-000000000000') FOR [Id]
GO
ALTER TABLE [dbo].[OrderDetail] ADD  DEFAULT ('00000000-0000-0000-0000-000000000000') FOR [VariantId]
GO
ALTER TABLE [dbo].[Product] ADD  DEFAULT (N'') FOR [AttributeOptionIds]
GO
ALTER TABLE [dbo].[ProductVariants] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsDefault]
GO
ALTER TABLE [dbo].[ProductVariants] ADD  DEFAULT ((0)) FOR [ReservedStock]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT ((0)) FOR [FailedPasswordResetAttempts]
GO
ALTER TABLE [dbo].[CartItems]  WITH CHECK ADD  CONSTRAINT [FK_CartItems_Carts_CartId] FOREIGN KEY([CartId])
REFERENCES [dbo].[Carts] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CartItems] CHECK CONSTRAINT [FK_CartItems_Carts_CartId]
GO
ALTER TABLE [dbo].[CartItems]  WITH CHECK ADD  CONSTRAINT [FK_CartItems_Product_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([Id])
GO
ALTER TABLE [dbo].[CartItems] CHECK CONSTRAINT [FK_CartItems_Product_ProductId]
GO
ALTER TABLE [dbo].[CartItems]  WITH CHECK ADD  CONSTRAINT [FK_CartItems_ProductVariants_VariantId] FOREIGN KEY([VariantId])
REFERENCES [dbo].[ProductVariants] ([Id])
GO
ALTER TABLE [dbo].[CartItems] CHECK CONSTRAINT [FK_CartItems_ProductVariants_VariantId]
GO
ALTER TABLE [dbo].[Carts]  WITH CHECK ADD  CONSTRAINT [FK_Carts_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Carts] CHECK CONSTRAINT [FK_Carts_Users_UserId]
GO
ALTER TABLE [dbo].[New]  WITH CHECK ADD  CONSTRAINT [FK_New_Category_CategoryId] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Category] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[New] CHECK CONSTRAINT [FK_New_Category_CategoryId]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Users_UserId]
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetail_Order_OrderId] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Order] ([Id])
GO
ALTER TABLE [dbo].[OrderDetail] CHECK CONSTRAINT [FK_OrderDetail_Order_OrderId]
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetail_Product_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrderDetail] CHECK CONSTRAINT [FK_OrderDetail_Product_ProductId]
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetail_ProductVariants_VariantId] FOREIGN KEY([VariantId])
REFERENCES [dbo].[ProductVariants] ([Id])
GO
ALTER TABLE [dbo].[OrderDetail] CHECK CONSTRAINT [FK_OrderDetail_ProductVariants_VariantId]
GO
ALTER TABLE [dbo].[Post]  WITH CHECK ADD  CONSTRAINT [FK_Post_Category_CategoryId] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Category] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Post] CHECK CONSTRAINT [FK_Post_Category_CategoryId]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Category_CategoryId] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Category] ([Id])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Category_CategoryId]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_ProductCategory_ProductCategoryId] FOREIGN KEY([ProductCategoryId])
REFERENCES [dbo].[ProductCategory] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_ProductCategory_ProductCategoryId]
GO
ALTER TABLE [dbo].[ProductCategory]  WITH CHECK ADD  CONSTRAINT [FK_ProductCategory_ProductCategory_ParentCategoryId] FOREIGN KEY([ParentCategoryId])
REFERENCES [dbo].[ProductCategory] ([Id])
GO
ALTER TABLE [dbo].[ProductCategory] CHECK CONSTRAINT [FK_ProductCategory_ProductCategory_ParentCategoryId]
GO
ALTER TABLE [dbo].[ProductImage]  WITH CHECK ADD  CONSTRAINT [FK_ProductImage_Product_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductImage] CHECK CONSTRAINT [FK_ProductImage_Product_ProductId]
GO
ALTER TABLE [dbo].[ProductVariants]  WITH CHECK ADD  CONSTRAINT [FK_ProductVariants_Product_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductVariants] CHECK CONSTRAINT [FK_ProductVariants_Product_ProductId]
GO
ALTER TABLE [dbo].[RoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_RoleClaims_Roles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RoleClaims] CHECK CONSTRAINT [FK_RoleClaims_Roles_RoleId]
GO
ALTER TABLE [dbo].[Transactions]  WITH CHECK ADD  CONSTRAINT [FK_Transactions_Order_OrderId] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Order] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Transactions] CHECK CONSTRAINT [FK_Transactions_Order_OrderId]
GO
ALTER TABLE [dbo].[UserClaims]  WITH CHECK ADD  CONSTRAINT [FK_UserClaims_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserClaims] CHECK CONSTRAINT [FK_UserClaims_Users_UserId]
GO
ALTER TABLE [dbo].[UserLogins]  WITH CHECK ADD  CONSTRAINT [FK_UserLogins_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserLogins] CHECK CONSTRAINT [FK_UserLogins_Users_UserId]
GO
ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_Roles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_Roles_RoleId]
GO
ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_Users_UserId]
GO
ALTER TABLE [dbo].[UserTokens]  WITH CHECK ADD  CONSTRAINT [FK_UserTokens_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserTokens] CHECK CONSTRAINT [FK_UserTokens_Users_UserId]
GO
ALTER TABLE [dbo].[Wishlists]  WITH CHECK ADD  CONSTRAINT [FK_Wishlists_Product_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Wishlists] CHECK CONSTRAINT [FK_Wishlists_Product_ProductId]
GO
ALTER TABLE [dbo].[Wishlists]  WITH CHECK ADD  CONSTRAINT [FK_Wishlists_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Wishlists] CHECK CONSTRAINT [FK_Wishlists_Users_UserId]
GO
