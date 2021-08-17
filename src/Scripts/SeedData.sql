/* ############## Catálogo ################ */

IF DB_ID(N'CatalogoDb') IS NULL
BEGIN
	CREATE DATABASE [CatalogoDb]
END

GO

IF (OBJECT_ID(N'Categorias') IS NULL)
BEGIN

	USE [CatalogoDb]

	SET ANSI_NULLS ON
	SET QUOTED_IDENTIFIER ON

	CREATE TABLE [dbo].[Categorias] (
		[Id]     UNIQUEIDENTIFIER NOT NULL,
		[Nome]   VARCHAR (250)    NOT NULL,
		[Codigo] INT              NOT NULL
	);

	ALTER TABLE [dbo].[Categorias]
		ADD CONSTRAINT [PK_Categorias] PRIMARY KEY CLUSTERED ([Id] ASC);

	CREATE TABLE [dbo].[Produtos] (
		[Id]                UNIQUEIDENTIFIER NOT NULL,
		[CategoriaId]       UNIQUEIDENTIFIER NOT NULL,
		[Nome]              VARCHAR (250)    NOT NULL,
		[Descricao]         VARCHAR (500)    NOT NULL,
		[Ativo]             BIT              NOT NULL,
		[Valor]             DECIMAL (18, 2)  NOT NULL,
		[DataCadastro]      DATETIME2 (7)    NOT NULL,
		[Imagem]            VARCHAR (250)    NOT NULL,
		[QuantidadeEstoque] INT              NOT NULL,
		[Altura]            INT              NULL,
		[Largura]           INT              NULL,
		[Profundidade]      INT              NULL
	);

	CREATE NONCLUSTERED INDEX [IX_Produtos_CategoriaId]
		ON [dbo].[Produtos]([CategoriaId] ASC);


	ALTER TABLE [dbo].[Produtos]
		ADD CONSTRAINT [PK_Produtos] PRIMARY KEY CLUSTERED ([Id] ASC);


	ALTER TABLE [dbo].[Produtos]
		ADD CONSTRAINT [FK_Produtos_Categorias_CategoriaId] FOREIGN KEY ([CategoriaId]) REFERENCES [dbo].[Categorias] ([Id]) ON DELETE CASCADE;


	INSERT [dbo].[Categorias] ([Id], [Nome], [Codigo]) VALUES (N'38a10686-6481-4af2-a05d-a2ae2a4d8815', N'Massas', 102)
	INSERT [dbo].[Categorias] ([Id], [Nome], [Codigo]) VALUES (N'7a3cfd82-bb99-497d-aa3d-f613215ce22d', N'Bebidas', 101)
	INSERT INTO [dbo].[Produtos] ([Id], [CategoriaId], [Nome], [Descricao], [Ativo], [Valor], [DataCadastro], [Imagem], [QuantidadeEstoque], [Altura], [Largura], [Profundidade]) VALUES (N'aa1c2bb3-cc7b-4011-b5e7-2521a5c0b9aa', N'38a10686-6481-4af2-a05d-a2ae2a4d8815', N'Talharim (Nero Di Seppia - Tinta de Lula)', N'Talharim (Nero Di Seppia - Tinta de Lula)', 1, CAST(80.00 AS Decimal(18, 2)), N'2021-05-11 00:00:00', N'produto-talharim-tinta-lula.png', 50, 1, 1, 1)
	INSERT INTO [dbo].[Produtos] ([Id], [CategoriaId], [Nome], [Descricao], [Ativo], [Valor], [DataCadastro], [Imagem], [QuantidadeEstoque], [Altura], [Largura], [Profundidade]) VALUES (N'c463ce4d-b4aa-4284-8d9c-41ef71eb4878', N'38a10686-6481-4af2-a05d-a2ae2a4d8815', N'Nhoque de Mandioquinha', N'Nhoque de Mandioquinha recheado com brie combinação perfeita', 1, CAST(20.00 AS Decimal(18, 2)), N'2021-08-11 00:00:00', N'produto-nhoque-mandioquinha.png', 10, 1, 1, 1)
	INSERT INTO [dbo].[Produtos] ([Id], [CategoriaId], [Nome], [Descricao], [Ativo], [Valor], [DataCadastro], [Imagem], [QuantidadeEstoque], [Altura], [Largura], [Profundidade]) VALUES (N'4e86d76c-7e1d-4381-90a5-57016e92667a', N'38a10686-6481-4af2-a05d-a2ae2a4d8815', N'Parpadelle com tomatinho', N'Parpadelle com tomatinho confit, simples e muitooo saboroso.', 1, CAST(50.00 AS Decimal(18, 2)), N'2021-05-11 00:00:00', N'produto-parpadelle-com-tomatinho.png', 20, 1, 1, 1)
	INSERT INTO [dbo].[Produtos] ([Id], [CategoriaId], [Nome], [Descricao], [Ativo], [Valor], [DataCadastro], [Imagem], [QuantidadeEstoque], [Altura], [Largura], [Profundidade]) VALUES (N'14764628-97c9-4152-b163-8114ae89f3ba', N'38a10686-6481-4af2-a05d-a2ae2a4d8815', N'Nhoque de Mandioquinha sem farinha', N'Vamos começar a semana com um delícioso Nhoque de Mandioquinha sem farinha de trigo', 1, CAST(5.00 AS Decimal(18, 2)), N'2021-08-11 00:00:00', N'produto-nhoque-mandioquinha2.png', 20, 1, 1, 1)


END

/* ############## Venda ################ */

IF DB_ID(N'VendaDb') IS NULL
BEGIN
	CREATE DATABASE [VendaDb]
END

GO

IF (OBJECT_ID(N'Pedidos') IS NULL)
BEGIN

	USE [VendaDb]

	CREATE TABLE [dbo].[Vouchers] (
		[Id]                  UNIQUEIDENTIFIER NOT NULL,
		[Codigo]              NVARCHAR (MAX)   NULL,
		[Percentual]          DECIMAL (18, 2)  NULL,
		[ValorDesconto]       DECIMAL (18, 2)  NULL,
		[Quantidade]          INT              NOT NULL,
		[TipoDescontoVoucher] INT              NOT NULL,
		[DataCriacao]         DATETIME2 (7)    NOT NULL,
		[DataUtilizacao]      DATETIME2 (7)    NULL,
		[DataValidade]        DATETIME2 (7)    NOT NULL,
		[Ativo]               BIT              NOT NULL,
		[Utilizado]           BIT              NOT NULL
	);

	ALTER TABLE [dbo].[Vouchers]
		ADD CONSTRAINT [PK_Vouchers] PRIMARY KEY CLUSTERED ([Id] ASC);

	CREATE TABLE [dbo].[Pedidos] (
		[Id]               UNIQUEIDENTIFIER NOT NULL,
		[Codigo]           INT              NOT NULL,
		[ClienteId]        UNIQUEIDENTIFIER NOT NULL,
		[VoucherId]        UNIQUEIDENTIFIER NULL,
		[VoucherUtilizado] BIT              NOT NULL,
		[Desconto]         DECIMAL (18, 2)  NOT NULL,
		[ValorTotal]       DECIMAL (18, 2)  NOT NULL,
		[DataCadastro]     DATETIME2 (7)    NOT NULL,
		[PedidoStatus]     INT              NOT NULL
	);

	CREATE NONCLUSTERED INDEX [IX_Pedidos_VoucherId]
		ON [dbo].[Pedidos]([VoucherId] ASC);

	ALTER TABLE [dbo].[Pedidos]
		ADD CONSTRAINT [PK_Pedidos] PRIMARY KEY CLUSTERED ([Id] ASC);

	ALTER TABLE [dbo].[Pedidos]
		ADD CONSTRAINT [FK_Pedidos_Vouchers_VoucherId] FOREIGN KEY ([VoucherId]) REFERENCES [dbo].[Vouchers] ([Id]);


	CREATE TABLE [dbo].[PedidoItems] (
		[Id]            UNIQUEIDENTIFIER NOT NULL,
		[PedidoId]      UNIQUEIDENTIFIER NOT NULL,
		[ProdutoId]     UNIQUEIDENTIFIER NOT NULL,
		[ProdutoNome]   NVARCHAR (MAX)   NULL,
		[Quantidade]    INT              NOT NULL,
		[ValorUnitario] DECIMAL (18, 2)  NOT NULL,
		[ItemStatus]    INT              NOT NULL
	);

	CREATE NONCLUSTERED INDEX [IX_PedidoItems_PedidoId]
		ON [dbo].[PedidoItems]([PedidoId] ASC);

	ALTER TABLE [dbo].[PedidoItems]
		ADD CONSTRAINT [PK_PedidoItems] PRIMARY KEY CLUSTERED ([Id] ASC);

	ALTER TABLE [dbo].[PedidoItems]
		ADD CONSTRAINT [FK_PedidoItems_Pedidos_PedidoId] FOREIGN KEY ([PedidoId]) REFERENCES [dbo].[Pedidos] ([Id]);


	INSERT [dbo].[Pedidos] ([Id], [Codigo], [ClienteId], [VoucherId], [VoucherUtilizado], [Desconto], [ValorTotal], [DataCadastro], [PedidoStatus]) VALUES (N'44c7ff2b-e6e6-42ad-a669-2da3a1757793', 1, N'9faf55b5-5088-42dd-8c55-5f8698b8295c', NULL, 0, CAST(0.00 AS Decimal(18, 2)), CAST(30.00 AS Decimal(18, 2)), CAST(N'2021-08-11T00:00:00.0000000' AS DateTime2), 0)
	INSERT [dbo].[Pedidos] ([Id], [Codigo], [ClienteId], [VoucherId], [VoucherUtilizado], [Desconto], [ValorTotal], [DataCadastro], [PedidoStatus]) VALUES (N'4755fb51-eb13-4684-871e-f15c0f120f7d', 2, N'9faf55b5-5088-42dd-8c55-5f8698b8295c', NULL, 0, CAST(0.00 AS Decimal(18, 2)), CAST(40.00 AS Decimal(18, 2)), CAST(N'2021-08-11T00:00:00.0000000' AS DateTime2), 0)
	INSERT [dbo].[PedidoItems] ([Id], [PedidoId], [ProdutoId], [ProdutoNome], [Quantidade], [ValorUnitario], [ItemStatus]) VALUES (N'417e22db-5f46-47ee-98f9-691fa0eb1ba7', N'44c7ff2b-e6e6-42ad-a669-2da3a1757793', N'14764628-97c9-4152-b163-8114ae89f3ba', N'Água mineral', 2, CAST(5.00 AS Decimal(18, 2)), 1)
	INSERT [dbo].[PedidoItems] ([Id], [PedidoId], [ProdutoId], [ProdutoNome], [Quantidade], [ValorUnitario], [ItemStatus]) VALUES (N'101ddc3c-fc09-4f4c-926a-9e267223a581', N'44c7ff2b-e6e6-42ad-a669-2da3a1757793', N'c463ce4d-b4aa-4284-8d9c-41ef71eb4878', N'Filé Bacon', 1, CAST(20.00 AS Decimal(18, 2)), 1)
	INSERT [dbo].[PedidoItems] ([Id], [PedidoId], [ProdutoId], [ProdutoNome], [Quantidade], [ValorUnitario], [ItemStatus]) VALUES (N'9206a3db-3977-4a8f-a51c-ea6db659f013', N'4755fb51-eb13-4684-871e-f15c0f120f7d', N'14764628-97c9-4152-b163-8114ae89f3ba', N'Água mineral', 8, CAST(5.00 AS Decimal(18, 2)), 1)

END

-- ########### Script Identity ################

IF DB_ID(N'IdentityDb') IS NULL
BEGIN
	CREATE DATABASE [IdentityDb]
END

GO

IF (OBJECT_ID(N'AspNetUsers') IS NULL)
BEGIN

USE [IdentityDb]

	CREATE TABLE [dbo].[__EFMigrationsHistory](
		[MigrationId] [nvarchar](150) NOT NULL,
		[ProductVersion] [nvarchar](32) NOT NULL,
	 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
	(
		[MigrationId] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

	CREATE TABLE [dbo].[AspNetRoleClaims](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[RoleId] [nvarchar](450) NOT NULL,
		[ClaimType] [nvarchar](max) NULL,
		[ClaimValue] [nvarchar](max) NULL,
	 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

	CREATE TABLE [dbo].[AspNetRoles](
		[Id] [nvarchar](450) NOT NULL,
		[Name] [nvarchar](256) NULL,
		[NormalizedName] [nvarchar](256) NULL,
		[ConcurrencyStamp] [nvarchar](max) NULL,
	 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

	CREATE TABLE [dbo].[AspNetUserClaims](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[UserId] [nvarchar](450) NOT NULL,
		[ClaimType] [nvarchar](max) NULL,
		[ClaimValue] [nvarchar](max) NULL,
	 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


	CREATE TABLE [dbo].[AspNetUserLogins](
		[LoginProvider] [nvarchar](128) NOT NULL,
		[ProviderKey] [nvarchar](128) NOT NULL,
		[ProviderDisplayName] [nvarchar](max) NULL,
		[UserId] [nvarchar](450) NOT NULL,
	 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
	(
		[LoginProvider] ASC,
		[ProviderKey] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

	CREATE TABLE [dbo].[AspNetUserRoles](
		[UserId] [nvarchar](450) NOT NULL,
		[RoleId] [nvarchar](450) NOT NULL,
	 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
	(
		[UserId] ASC,
		[RoleId] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

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
	 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

	CREATE TABLE [dbo].[AspNetUserTokens](
		[UserId] [nvarchar](450) NOT NULL,
		[LoginProvider] [nvarchar](128) NOT NULL,
		[Name] [nvarchar](128) NOT NULL,
		[Value] [nvarchar](max) NULL,
	 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
	(
		[UserId] ASC,
		[LoginProvider] ASC,
		[Name] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

	INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210816035949_Initial', N'5.0.9')

	SET IDENTITY_INSERT [dbo].[AspNetUserClaims] ON 
	INSERT [dbo].[AspNetUserClaims] ([Id], [UserId], [ClaimType], [ClaimValue]) VALUES (1, N'd952c834-8802-47ba-bf40-6c197d74dad0', N'Cozinha', N'Adicionar, Atualizar, Remover, Cancelar')
	INSERT [dbo].[AspNetUserClaims] ([Id], [UserId], [ClaimType], [ClaimValue]) VALUES (2, N'53d600db-27df-4de2-b785-689000a87802', N'Comanda', N'Adicionar, Atualizar, Remover, Cancelar')
	SET IDENTITY_INSERT [dbo].[AspNetUserClaims] OFF

	INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'53d600db-27df-4de2-b785-689000a87802', N'garcom@teste.com', N'GARCOM@TESTE.COM', N'garcom@teste.com', N'GARCOM@TESTE.COM', 1, N'AQAAAAEAACcQAAAAENVCDwvhNyXVpe+xLwOrWwW4vDLl66BXkLBDiT3cJttN6yg2SGdY/EZP2qr6Nu5yzA==', N'CP3ZEXWSIJSG5IWUFRTKEIQ46ZS32UUR', N'e86ba905-84b9-4abb-9fe1-5ee252238d2b', NULL, 0, 0, NULL, 1, 0)
	INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'd952c834-8802-47ba-bf40-6c197d74dad0', N'cozinha@teste.com', N'COZINHA@TESTE.COM', N'cozinha@teste.com', N'COZINHA@TESTE.COM', 1, N'AQAAAAEAACcQAAAAEDSfZ8W5PMEb8f37rmfoa9Zco2N0krQ1Djk2cUNFEPfsa1nRv/z/oxHZU6MPQmXhAg==', N'WU3CB5IVVHKLBW2MFPREVYDDC3MZRUBX', N'62dd00c3-9015-4981-90c9-8b83f8c17cc3', NULL, 0, 0, NULL, 1, 0)
	ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
	REFERENCES [dbo].[AspNetRoles] ([Id])
	ON DELETE CASCADE

	ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]

	ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
	REFERENCES [dbo].[AspNetUsers] ([Id])
	ON DELETE CASCADE

	ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]

	ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
	REFERENCES [dbo].[AspNetUsers] ([Id])
	ON DELETE CASCADE

	ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]

	ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
	REFERENCES [dbo].[AspNetRoles] ([Id])
	ON DELETE CASCADE

	ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]

	ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
	REFERENCES [dbo].[AspNetUsers] ([Id])
	ON DELETE CASCADE

	ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]

	ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
	REFERENCES [dbo].[AspNetUsers] ([Id])
	ON DELETE CASCADE

	ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]

END