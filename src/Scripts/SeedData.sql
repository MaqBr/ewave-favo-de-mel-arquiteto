IF DB_ID(N'CatalogoDb') IS NULL
BEGIN
	CREATE DATABASE [CatalogoDb]
END

GO

IF DB_ID(N'VendaDb') IS NULL
BEGIN
	CREATE DATABASE [VendaDb]
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


INSERT [dbo].[Categorias] ([Id], [Nome], [Codigo]) VALUES (N'38a10686-6481-4af2-a05d-a2ae2a4d8815', N'Lanches', 102)
INSERT [dbo].[Categorias] ([Id], [Nome], [Codigo]) VALUES (N'7a3cfd82-bb99-497d-aa3d-f613215ce22d', N'Bebidas', 101)
INSERT [dbo].[Produtos] ([Id], [CategoriaId], [Nome], [Descricao], [Ativo], [Valor], [DataCadastro], [Imagem], [QuantidadeEstoque], [Altura], [Largura], [Profundidade]) VALUES (N'c463ce4d-b4aa-4284-8d9c-41ef71eb4878', N'38a10686-6481-4af2-a05d-a2ae2a4d8815', N'Filé Bacon', N'Pão de Hambúrguer, Queijo Mussarela, Bacon, Alface Americana, Tomate, Filé, Catupiry', 1, CAST(20.00 AS Decimal(18, 2)), CAST(N'2021-08-11T00:00:00.0000000' AS DateTime2), N'file-bacon.png', 10, 1, 1, 1)
INSERT [dbo].[Produtos] ([Id], [CategoriaId], [Nome], [Descricao], [Ativo], [Valor], [DataCadastro], [Imagem], [QuantidadeEstoque], [Altura], [Largura], [Profundidade]) VALUES (N'14764628-97c9-4152-b163-8114ae89f3ba', N'7a3cfd82-bb99-497d-aa3d-f613215ce22d', N'Água mineral', N'Água miniral 250ml', 1, CAST(5.00 AS Decimal(18, 2)), CAST(N'2021-08-11T00:00:00.0000000' AS DateTime2), N'agua-mineral-200ml.png', 20, 1, 1, 1)


END

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


