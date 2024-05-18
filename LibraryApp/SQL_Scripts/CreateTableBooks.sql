USE [LibraryTest]

/****** Object:  Table [dbo].[Books]    Script Date: 18.05.2024 3:18:41 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[Books](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nchar](100) NULL,
	[Author] [nchar](100) NULL,
	[Description] [nchar](250) NULL,
	[IsAvailable] [bit] NULL,
	[Client_Id] [int] NULL,
 CONSTRAINT [PK_Book] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[Books] ADD  CONSTRAINT [DF_Books_IsAvailable]  DEFAULT ((1)) FOR [IsAvailable]


