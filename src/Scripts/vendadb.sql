USE [VendaDb]
GO
INSERT [dbo].Comandas ([Id], [Codigo], [ClienteId], [VoucherId], [VoucherUtilizado], [Desconto], [ValorTotal], [DataCadastro], [ComandaStatus]) VALUES (N'44c7ff2b-e6e6-42ad-a669-2da3a1757793', 1, N'9faf55b5-5088-42dd-8c55-5f8698b8295c', NULL, 0, CAST(0.00 AS Decimal(18, 2)), CAST(30.00 AS Decimal(18, 2)), CAST(N'2021-08-11T00:00:00.0000000' AS DateTime2), 0)
INSERT [dbo].Comandas ([Id], [Codigo], [ClienteId], [VoucherId], [VoucherUtilizado], [Desconto], [ValorTotal], [DataCadastro], [ComandaStatus]) VALUES (N'4755fb51-eb13-4684-871e-f15c0f120f7d', 2, N'9faf55b5-5088-42dd-8c55-5f8698b8295c', NULL, 0, CAST(0.00 AS Decimal(18, 2)), CAST(40.00 AS Decimal(18, 2)), CAST(N'2021-08-11T00:00:00.0000000' AS DateTime2), 0)
INSERT [dbo].[ComandaItems] ([Id], [ComandaId], [ProdutoId], [ProdutoNome], [Quantidade], [ValorUnitario], [ItemStatus]) VALUES (N'417e22db-5f46-47ee-98f9-691fa0eb1ba7', N'44c7ff2b-e6e6-42ad-a669-2da3a1757793', N'14764628-97c9-4152-b163-8114ae89f3ba', N'Água mineral', 2, CAST(5.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[ComandaItems] ([Id], [ComandaId], [ProdutoId], [ProdutoNome], [Quantidade], [ValorUnitario], [ItemStatus]) VALUES (N'101ddc3c-fc09-4f4c-926a-9e267223a581', N'44c7ff2b-e6e6-42ad-a669-2da3a1757793', N'c463ce4d-b4aa-4284-8d9c-41ef71eb4878', N'Filé Bacon', 1, CAST(20.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[ComandaItems] ([Id], [ComandaId], [ProdutoId], [ProdutoNome], [Quantidade], [ValorUnitario], [ItemStatus]) VALUES (N'9206a3db-3977-4a8f-a51c-ea6db659f013', N'4755fb51-eb13-4684-871e-f15c0f120f7d', N'14764628-97c9-4152-b163-8114ae89f3ba', N'Água mineral', 8, CAST(5.00 AS Decimal(18, 2)), 1)
