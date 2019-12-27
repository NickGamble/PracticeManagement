SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Animal](
	[Id] [uniqueidentifier] ROWGUIDCOL NOT NULL,
	[MicrochipId] [nvarchar](13) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[DateOfBirth] [date] NOT NULL,
	[Breed] [nvarchar](50) NULL,
	[Species] [nvarchar](50) NULL,
	[Colour] [nvarchar](50) NULL
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Animal] ADD  CONSTRAINT [DF_Animal_Id]  DEFAULT (newid()) FOR [Id]
GO