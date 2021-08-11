USE [CatalogoDb]
GO
INSERT [dbo].[Categorias] ([Id], [Nome], [Codigo]) VALUES (N'38a10686-6481-4af2-a05d-a2ae2a4d8815', N'Lanches', 102)
INSERT [dbo].[Categorias] ([Id], [Nome], [Codigo]) VALUES (N'7a3cfd82-bb99-497d-aa3d-f613215ce22d', N'Bebidas', 101)
INSERT [dbo].[Produtos] ([Id], [CategoriaId], [Nome], [Descricao], [Ativo], [Valor], [DataCadastro], [Imagem], [QuantidadeEstoque], [Altura], [Largura], [Profundidade]) VALUES (N'c463ce4d-b4aa-4284-8d9c-41ef71eb4878', N'38a10686-6481-4af2-a05d-a2ae2a4d8815', N'Filé Bacon', N'Pão de Hambúrguer, Queijo Mussarela, Bacon, Alface Americana, Tomate, Filé, Catupiry', 1, CAST(20.00 AS Decimal(18, 2)), CAST(N'2021-08-11T00:00:00.0000000' AS DateTime2), N'file-bacon.png', 10, 1, 1, 1)
INSERT [dbo].[Produtos] ([Id], [CategoriaId], [Nome], [Descricao], [Ativo], [Valor], [DataCadastro], [Imagem], [QuantidadeEstoque], [Altura], [Largura], [Profundidade]) VALUES (N'14764628-97c9-4152-b163-8114ae89f3ba', N'7a3cfd82-bb99-497d-aa3d-f613215ce22d', N'Água mineral', N'Água miniral 250ml', 1, CAST(5.00 AS Decimal(18, 2)), CAST(N'2021-08-11T00:00:00.0000000' AS DateTime2), N'agua-mineral-200ml.png', 20, 1, 1, 1)
