USE [HappyStorage]
GO

/****** Object:  Table [dbo].[Units]    Script Date: 11/29/2017 1:32:57 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].Customers(
	[CustomerNumber] [varchar](100) NOT NULL,
	[FullName] [varchar](100) NOT NULL,
	[Address] [varchar](100) NOT NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[CustomerNumber] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO