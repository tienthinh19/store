USE Master
GO
create database LoveStore2
GO
USE LoveStore2
GO
create table Account(
	Id [int] identity primary key,
	Email [varchar](50) NOT NULL,
	FullName [nvarchar](50) NOT NULL,
	RoleId [char](2) NOT NULL,
	Password [char](64) NOT NULL
)
GO
create table Employee
(
	Id int identity primary key,
	Position nvarchar(50),
	AccountId int references Account(Id)
)
go
create table Customer
(
	Id int identity primary key,
	AccountId int references Account(Id)
)
go

CREATE TABLE [dbo].[Order](
	[Id] [int] IDENTITY(1,1) NOT NULL primary key,
	[Date] [datetime] NOT NULL,
	[Status] [varchar](30) NULL,
	[CustomerId] [int] NOT NULL references Customer(Id),
	[EmployeeId] [int] NOT NULL references Employee(Id)
)
GO
CREATE TABLE [dbo].Category(
	[Id] [int] IDENTITY(1,1) NOT NULL primary key,
	Name [varchar](50) NOT NULL
)
GO
CREATE TABLE [dbo].[Product](
	[Id] [int] IDENTITY(1,1) NOT NULL primary key,
	[Description] [varchar](50) NOT NULL,
	[Price] [float] NOT NULL,
	[Discount] [float] NOT NULL,
	[CategoryId] [int] NOT NULL references Category(Id)
)
GO
CREATE TABLE [dbo].[OrderDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL primary key,
	[OrderId] [int] NOT NULL references [Order](Id),
	[ProductId] [int] NOT NULL references [Product](Id),
	[Quantity] [int] NOT NULL,
	[Price] [float] NOT NULL,
	[Discount] [float] NOT NULL
)
GO
insert into Account values
('admin@gmail.com','David Smith','AM','6B86B273FF34FCE19D6B804EFF5A3F5747ADA4EAA22F1D49C01E52DDB7875B4B'),
('user@gmail.com', 'John Smith', 'US', 'D4735E3A265E16EEE03F59718B9B5D03019C07D8B6C51F90DA3A666EEC13AB35')
GO
SET IDENTITY_INSERT [dbo].[Category] ON 
INSERT [dbo].[Category] ([id], [name]) VALUES (1, N'Ring')
INSERT [dbo].[Category] ([id], [name]) VALUES (2, N'bracelet')
INSERT [dbo].[Category] ([id], [name]) VALUES (3, N'earring')
INSERT [dbo].[Category] ([id], [name]) VALUES (4, N'necklace')
SET IDENTITY_INSERT [dbo].[Category] OFF
SET IDENTITY_INSERT [dbo].[Product] ON 
INSERT [dbo].[Product] ([id], [description], [price], [discount], [CategoryId]) VALUES (1, N'Silver children ring with stones', 204.99, 0.1, 1)
INSERT [dbo].[Product] ([id], [description], [price], [discount], [CategoryId]) VALUES (2, N'14K White Gold Diamond Ring', 73.94, 0.05, 1)
INSERT [dbo].[Product] ([id], [description], [price], [discount], [CategoryId]) VALUES (3, N'Silver Ring with stones', 74.11, 0.05, 1)
INSERT [dbo].[Product] ([id], [description], [price], [discount], [CategoryId]) VALUES (4, N'14K White Gold Ring with Topaz Stone', 52.73, 0, 1)
INSERT [dbo].[Product] ([id], [description], [price], [discount], [CategoryId]) VALUES (5, N'Silver ring with stones', 171.13, 0.15, 1)
INSERT [dbo].[Product] ([id], [description], [price], [discount], [CategoryId]) VALUES (6, N'Kim Bao Nhu Y 10K White Gold Ring', 140.55, 0.1, 1)
INSERT [dbo].[Product] ([id], [description], [price], [discount], [CategoryId]) VALUES (7, N'10K Gold Ring with Synthetic Stone ', 56.01, 0.1, 1)
INSERT [dbo].[Product] ([id], [description], [price], [discount], [CategoryId]) VALUES (8, N'10K White Gold Ring with ECZ Stone', 48.17, 0.1, 1)
INSERT [dbo].[Product] ([id], [description], [price], [discount], [CategoryId]) VALUES (9, N'14K White Gold Diamond Ring', 62.03, 0, 1)
INSERT [dbo].[Product] ([id], [description], [price], [discount], [CategoryId]) VALUES (10, N'White Gold Diamond Bracelet', 88.19, 0.1, 1)
INSERT [dbo].[Product] ([id], [description], [price], [discount], [CategoryId]) VALUES (11, N'White Gold Diamond Bracelet ', 20.31, 0.05, 1)
INSERT [dbo].[Product] ([id], [description], [price], [discount], [CategoryId]) VALUES (12, N'10K Gold bracelet with synthetic stones', 96.47, 0.15, 1)
INSERT [dbo].[Product] ([id], [description], [price], [discount], [CategoryId]) VALUES (13, N'18K Gold bracelet with ECZ stones', 155.58, 0.05, 1)
INSERT [dbo].[Product] ([id], [description], [price], [discount], [CategoryId]) VALUES (14, N'PNJSilver silver bracelet with cat shape', 204.26, 0.05, 1)
INSERT [dbo].[Product] ([id], [description], [price], [discount], [CategoryId]) VALUES (15, N'Petit CD Bracelet', 148.25, 0.1, 1)
INSERT [dbo].[Product] ([id], [description], [price], [discount], [CategoryId]) VALUES (16, N'Dior Lucky Charms Bracelet 6', 209.9, 0.05, 1)
INSERT [dbo].[Product] ([id], [description], [price], [discount], [CategoryId]) VALUES (17, N'D-Fusion Bracelet', 104.29, 0.1, 1)
INSERT [dbo].[Product] ([id], [description], [price], [discount], [CategoryId]) VALUES (18, N'Dio(r)evolution Bracelet', 146.36, 0, 1)
INSERT [dbo].[Product] ([id], [description], [price], [discount], [CategoryId]) VALUES (19, N'Dior Spring Bracelet', 154.76, 0.15, 1)
INSERT [dbo].[Product] ([id], [description], [price], [discount], [CategoryId]) VALUES (20, N'Petit CD Baroque Bracelet', 135.82, 0.05, 1)
INSERT [dbo].[Product] ([id], [description], [price], [discount], [CategoryId]) VALUES (21, N'Bois de Rose Single Ear Cuff', 158.46, 0.05, 1)
INSERT [dbo].[Product] ([id], [description], [price], [discount], [CategoryId]) VALUES (23, N'Rose Dior Couture Earrings', 165.89, 0.05, 1)
INSERT [dbo].[Product] ([id], [description], [price], [discount], [CategoryId]) VALUES (24, N'Rose des Vents Single Earring', 80.16, 0.05, 1)
INSERT [dbo].[Product] ([id], [description], [price], [discount], [CategoryId]) VALUES (25, N'Rose Dior Bagatelle Single Earring', 19.25, 0.1, 2)
INSERT [dbo].[Product] ([id], [description], [price], [discount], [CategoryId]) VALUES (26, N'Medium Rose Dior Bagatelle Earrings', 171.36, 0.1, 2)
INSERT [dbo].[Product] ([id], [description], [price], [discount], [CategoryId]) VALUES (32, N'18K Gold pendant with CZ stone', 35.08, 0.05, 2)
INSERT [dbo].[Product] ([id], [description], [price], [discount], [CategoryId]) VALUES (33, N'10K White Gold pendant with ECZ stone', 57.92, 0.05, 2)
INSERT [dbo].[Product] ([id], [description], [price], [discount], [CategoryId]) VALUES (34, N'14K White Gold Diamond Pendant', 157.81, 0, 2)
INSERT [dbo].[Product] ([id], [description], [price], [discount], [CategoryId]) VALUES (35, N'14K White Gold pendant with Topaz stone', 151, 0.05, 2)
INSERT [dbo].[Product] ([id], [description], [price], [discount], [CategoryId]) VALUES (36, N'Silver pendant with PNJSilver stone', 126.34, 0.05, 2)
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
raiserror('The PetStore database in now ready for use....',0,1)