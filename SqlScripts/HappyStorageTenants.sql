USE [HappyStorage]
GO

/****** Object:  Table [dbo].[Tenants]    Script Date: 6/23/2019 12:44:38 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Tenants](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CustomerNumber] [varchar](100) NOT NULL,
	[UnitNumber] [varchar](100) NOT NULL,
	[ReservationDate] [date] NOT NULL,
	[AmountPaid] [money] NULL
) ON [PRIMARY]
GO